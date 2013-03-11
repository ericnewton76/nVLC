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
using Implementation.Utils;
using LibVlcWrapper;

namespace Implementation
{
    internal sealed unsafe class MemoryRendererEx : DisposableBase, IMemoryRendererEx
    {
        IntPtr m_hMediaPlayer;
        NewFrameDataEventHandler m_callback = null;
        Timer m_timer = new Timer();
        volatile int m_frameRate = 0;
        object m_lock = new object();
        List<Delegate> m_callbacks = new List<Delegate>();
        Func<BitmapFormat, BitmapFormat> m_formatSetupCB = null;
        IntPtr[] m_planes = new IntPtr[3];
        BitmapFormat m_format;

        IntPtr pLockCallback;
        IntPtr pDisplayCallback;
        IntPtr pFormatCallback;
        
        PlanarPixelData m_pixelData = default(PlanarPixelData);

        public MemoryRendererEx(IntPtr hMediaPlayer)
        {
            m_hMediaPlayer = hMediaPlayer;

            LockEventHandler leh = OnpLock;
            DisplayEventHandler deh = OnpDisplay;
            VideoFormatCallback formatCallback = OnFormatCallback;
            
            pFormatCallback = Marshal.GetFunctionPointerForDelegate(formatCallback);
            pLockCallback = Marshal.GetFunctionPointerForDelegate(leh);
            pDisplayCallback = Marshal.GetFunctionPointerForDelegate(deh);

            m_callbacks.Add(leh);
            m_callbacks.Add(deh);
            m_callbacks.Add(formatCallback);
           
            m_timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            m_timer.Interval = 1000;

            LibVlcMethods.libvlc_video_set_format_callbacks(m_hMediaPlayer, pFormatCallback, IntPtr.Zero);        
            LibVlcMethods.libvlc_video_set_callbacks(m_hMediaPlayer, pLockCallback, IntPtr.Zero, pDisplayCallback, IntPtr.Zero);
        }

        private unsafe int OnFormatCallback(void** opaque, char* chroma, int* width, int* height, int* pitches, int* lines)
        {
            IntPtr pChroma = new IntPtr(chroma);
            string chromaStr = Marshal.PtrToStringAnsi(pChroma);

            ChromaType type;
            if (!Enum.TryParse<ChromaType>(chromaStr, out type))
            {
                throw new ArgumentException("Unsupported chroma type " + chromaStr);
            }

            m_format = new BitmapFormat(*width, *height, type);
            if (m_formatSetupCB != null)
            {
                m_format = m_formatSetupCB(m_format);              
            }

            Marshal.Copy(m_format.Chroma.ToUtf8(), 0, pChroma, 4);
            *width = m_format.Width;
            *height = m_format.Height;
    
            for (int i = 0; i < m_format.Planes; i++)
            {
                pitches[i] = m_format.Pitches[i];
                lines[i] = m_format.Lines[i];
            }

            m_pixelData = new PlanarPixelData(m_format.PlaneSizes);

            return m_format.Planes;
        }

        void  timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            m_frameRate = 0;
        }

        unsafe void* OnpLock(void* opaque, void** plane)
        {
            for (int i = 0; i < m_pixelData.Sizes.Length; i++)
            {
                plane[i] = m_pixelData.Data[i];
            }

            return null;
        }

        unsafe void OnpDisplay(void* opaque, void* picture)
        {
            lock (m_lock)
            {
                m_frameRate++;
                for (int i = 0; i < m_pixelData.Sizes.Length; i++)
                {
                    m_planes[i] = new IntPtr(m_pixelData.Data[i]);
                }

                if (m_callback != null)
                {
                    PlanarFrame pf = GetFrame();
                    m_callback(pf);
                }
            }
        }

        internal void StartTimer()
        {
            m_timer.Start();
        }

        private PlanarFrame GetFrame()
        {
            return new PlanarFrame(m_planes, m_format.PlaneSizes);
        }

        #region IMemoryRendererEx Members

        public void SetCallback(NewFrameDataEventHandler callback)
        {
            m_callback = callback;
        }

        public PlanarFrame CurrentFrame
        {
            get
            {
                lock (m_lock)
                {
                    return GetFrame();
                }
            }
        }

        public void SetFormatSetupCallback(Func<BitmapFormat, BitmapFormat> setupCallback)
        {
            m_formatSetupCB = setupCallback;
        }

        public int ActualFrameRate
        {
            get
            {
                return m_frameRate;
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            IntPtr zero = IntPtr.Zero;
            LibVlcMethods.libvlc_video_set_callbacks(m_hMediaPlayer, zero, zero, zero, zero);

            if (m_pixelData != default(PlanarPixelData))
            {
                m_pixelData.Dispose();
            }

            if (disposing)
            {
                m_timer.Dispose();
                m_callback = null;
                m_callbacks.Clear();
            }         
        }
    }
}
