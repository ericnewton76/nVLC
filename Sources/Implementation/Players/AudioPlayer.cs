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
using Declarations;
using Declarations.Players;
using LibVlcWrapper;
using Declarations.Enums;
using Implementation.Exceptions;

namespace Implementation.Players
{
    internal class AudioPlayer : BasicPlayer, IAudioPlayer
    {
        private AudioRenderer m_render = null;

        public AudioPlayer(IntPtr hMediaLib)
            : base(hMediaLib)
        {

        }

        #region IAudioPlayer Members

        public int Volume
        {
            get
            {
                return LibVlcMethods.libvlc_audio_get_volume(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_audio_set_volume(m_hMediaPlayer, value);
            }
        }

        public bool Mute
        {
            get
            {
                return LibVlcMethods.libvlc_audio_get_mute(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_audio_set_mute(m_hMediaPlayer, value);
            }
        }

        public long Delay
        {
            get
            {
                return LibVlcMethods.libvlc_audio_get_delay(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_audio_set_delay(m_hMediaPlayer, value);
            }
        }

        public AudioChannelType Channel
        {
            get
            {
                return (AudioChannelType)LibVlcMethods.libvlc_audio_get_channel(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_audio_set_channel(m_hMediaPlayer, (libvlc_audio_output_channel_t)value);
            }
        }

        public void ToggleMute()
        {
            LibVlcMethods.libvlc_audio_toggle_mute(m_hMediaPlayer);
        }

        public IAudioRenderer CustomAudioRenderer
        {
            get 
            {
                if (m_render == null)
                {
                    m_render = new AudioRenderer(m_hMediaPlayer);
                }
                return m_render; 
            }
        }

        public AudioOutputDeviceType DeviceType
        {
            get
            {
                return (AudioOutputDeviceType)LibVlcMethods.libvlc_audio_output_get_device_type(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_audio_output_set_device_type(m_hMediaPlayer, (libvlc_audio_output_device_types_t)value);
            }
        }

        public void SetAudioOutputModuleAndDevice(AudioOutputModuleInfo module, AudioOutputDeviceInfo device)
        {
            if (module == null)
            {
                throw new ArgumentNullException("module");
            }

            if (device != null)
            {
                LibVlcMethods.libvlc_audio_output_device_set(m_hMediaPlayer, module.Name.ToUtf8(), device.Id.ToUtf8());
            }

            int res = LibVlcMethods.libvlc_audio_output_set(m_hMediaPlayer, module.Name.ToUtf8());
            if (res < 0)
            {
                throw new LibVlcException();
            }
        }

        #endregion

        public override void Play()
        {
            base.Play();
            if (m_render != null)
            {
                m_render.StartTimer();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (m_render != null)
            {
                m_render.Dispose();
                m_render = null;
            }

            base.Dispose(disposing);
        }
    }
}
