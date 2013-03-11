//    nVLC
//    
//    Author:  Roman Ginzburg
//
//    nVLC is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    nVLC is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//     
// ========================================================================

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Timers;
using Declarations;
using Declarations.Enums;
using LibVlcWrapper;

namespace Implementation
{
    internal unsafe sealed class AudioRenderer : DisposableBase, IAudioRenderer
    {
        private IntPtr m_hMediaPlayer;
        private AudioCallbacks m_callbacks = new AudioCallbacks();
        private Func<SoundFormat, SoundFormat> m_formatSetupCB;
        private SoundFormat m_format;
        private List<Delegate> m_callbacksDelegates = new List<Delegate>();
        private Action<Exception> m_excHandler;
        private IntPtr m_hSetup;
        private IntPtr m_hVolume;
        private IntPtr m_hSound;
        private IntPtr m_hPause;
        private IntPtr m_hResume;
        private IntPtr m_hFlush;
        private IntPtr m_hDrain;
        private Timer m_timer = new Timer();
        volatile int m_frameRate = 0;
        int m_latestFps;

        public AudioRenderer(IntPtr hMediaPlayer)
        {
            m_hMediaPlayer = hMediaPlayer;

            PlayCallbackEventHandler pceh = PlayCallback;
            VolumeCallbackEventHandler vceh = VolumeCallback;
            SetupCallbackEventHandler sceh = SetupCallback;
            AudioCallbackEventHandler pause = PauseCallback;
            AudioCallbackEventHandler resume = ResumeCallback;
            AudioCallbackEventHandler flush = FlushCallback;
            AudioDrainCallbackEventHandler drain = DrainCallback;

            m_hSound = Marshal.GetFunctionPointerForDelegate(pceh);
            m_hVolume = Marshal.GetFunctionPointerForDelegate(vceh);
            m_hSetup = Marshal.GetFunctionPointerForDelegate(sceh);
            m_hPause = Marshal.GetFunctionPointerForDelegate(pause);
            m_hResume = Marshal.GetFunctionPointerForDelegate(resume);
            m_hFlush = Marshal.GetFunctionPointerForDelegate(flush);
            m_hDrain = Marshal.GetFunctionPointerForDelegate(drain);

            m_callbacksDelegates.Add(pceh);
            m_callbacksDelegates.Add(vceh);
            m_callbacksDelegates.Add(sceh);
            m_callbacksDelegates.Add(pause);
            m_callbacksDelegates.Add(resume);
            m_callbacksDelegates.Add(flush);
            m_callbacksDelegates.Add(drain);

            m_timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            m_timer.Interval = 1000;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            m_latestFps = m_frameRate;
            m_frameRate = 0;
        }

        public void SetCallbacks(VolumeChangedEventHandler volume, NewSoundEventHandler sound)
        {
            m_callbacks.VolumeCallback = volume;
            m_callbacks.SoundCallback = sound;
            LibVlcMethods.libvlc_audio_set_callbacks(m_hMediaPlayer, m_hSound, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
            LibVlcMethods.libvlc_audio_set_volume_callback(m_hMediaPlayer, m_hVolume);
        }

        public void SetCallbacks(AudioCallbacks callbacks)
        {
            if (callbacks.SoundCallback == null)
            {
                throw new ArgumentNullException("Sound playback callback must be set");
            }

            m_callbacks = callbacks;
            LibVlcMethods.libvlc_audio_set_callbacks(m_hMediaPlayer, m_hSound, m_hPause, m_hResume, m_hFlush, m_hDrain, IntPtr.Zero);
            LibVlcMethods.libvlc_audio_set_volume_callback(m_hMediaPlayer, m_hVolume);
        }

        public void SetFormat(SoundFormat format)
        {
            m_format = format;
            LibVlcMethods.libvlc_audio_set_format(m_hMediaPlayer, m_format.Format.ToUtf8(), m_format.Rate, m_format.Channels);
        }

        public void SetFormatCallback(Func<SoundFormat, SoundFormat> formatSetup)
        {
            m_formatSetupCB = formatSetup;
            LibVlcMethods.libvlc_audio_set_format_callbacks(m_hMediaPlayer, m_hSetup, IntPtr.Zero);
        }

        internal void StartTimer()
        {
            m_timer.Start();
        }

        private void PlayCallback(void* data, void* samples, uint count, long pts)
        {
            Sound s = new Sound();
            s.SamplesData = new IntPtr(samples);
            s.SamplesSize = (uint)(count * m_format.BlockSize);
            s.Pts = pts;

            if (m_callbacks.SoundCallback != null)
            {
                m_callbacks.SoundCallback(s);
            }
        }

        private void PauseCallback(void* data, long pts)
        {
            if (m_callbacks.PauseCallback != null)
            {
                m_callbacks.PauseCallback(pts);
            }
        }

        private void ResumeCallback(void* data, long pts)
        {
            if (m_callbacks.ResumeCallback != null)
            {
                m_callbacks.ResumeCallback(pts);
            }
        }

        private void FlushCallback(void* data, long pts)
        {
            if (m_callbacks.FlushCallback != null)
            {
                m_callbacks.FlushCallback(pts);
            }
        }

        private void DrainCallback(void* data)
        {
            if (m_callbacks.DrainCallback != null)
            {
                m_callbacks.DrainCallback();
            }
        }

        private void VolumeCallback(void* data, float volume, bool mute)
        {
            if (m_callbacks.VolumeCallback != null)
            {
                m_callbacks.VolumeCallback(volume, mute);
            }
        }

        private int SetupCallback(void** data, char* format, int* rate, int* channels)
        {
            IntPtr pFormat = new IntPtr(format);
            string formatStr = Marshal.PtrToStringAnsi(pFormat);

            SoundType sType;
            if (!Enum.TryParse<SoundType>(formatStr, out sType))
            {
                ArgumentException exc = new ArgumentException("Unsupported sound type " + formatStr);
                if (m_excHandler != null)
                {
                    m_excHandler(exc);
                    return 1;
                }
                else
                {
                    throw exc;
                }
            }

            m_format = new SoundFormat(sType, *rate, *channels);
            if (m_formatSetupCB != null)
            {
                m_format = m_formatSetupCB(m_format);
            }
            
            Marshal.Copy(m_format.Format.ToUtf8(), 0, pFormat, 4);
            *rate = m_format.Rate;
            *channels = m_format.Channels;

            return m_format.UseCustomAudioRendering == true ? 0 : 1;
        }

        protected override void Dispose(bool disposing)
        {
            LibVlcMethods.libvlc_audio_set_format_callbacks(m_hMediaPlayer, IntPtr.Zero, IntPtr.Zero);
            LibVlcMethods.libvlc_audio_set_callbacks(m_hMediaPlayer, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            if (disposing)
            {
                m_formatSetupCB = null;
                m_excHandler = null;
                m_callbacks = null;
                m_callbacksDelegates.Clear();
            }          
        }

        public void SetExceptionHandler(Action<Exception> handler)
        {
            m_excHandler = handler;
        }

        public int ActualFrameRate
        {
            get
            {
                return m_latestFps;
            }
        }
    }
}
