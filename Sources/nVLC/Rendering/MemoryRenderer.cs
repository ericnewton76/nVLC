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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Timers;
using Declarations;
using Implementation.Utils;
using LibVlcWrapper;

namespace Implementation
{
    internal sealed unsafe class MemoryRenderer : DisposableBase, IMemoryRenderer
    {
        IntPtr m_hMediaPlayer;
        NewFrameEventHandler m_callback = null;
        BitmapFormat m_format;
        Timer m_timer = new Timer();
        volatile int m_frameRate = 0;
        int m_latestFps;
        object m_lock = new object();
        List<Delegate> m_callbacks = new List<Delegate>();

        IntPtr pLockCallback;
        IntPtr pUnlockCallback;
        IntPtr pDisplayCallback;
        Action<Exception> m_excHandler = null;
        GCHandle m_pixelDataPtr = default(GCHandle);
        PixelData m_pixelData;
        void* m_pBuffer = null;

        public MemoryRenderer(IntPtr hMediaPlayer)
        {
            m_hMediaPlayer = hMediaPlayer;

            LockEventHandler leh = OnpLock;
            UnlockEventHandler ueh = OnpUnlock;
            DisplayEventHandler deh = OnpDisplay;

            pLockCallback = Marshal.GetFunctionPointerForDelegate(leh);
            pUnlockCallback = Marshal.GetFunctionPointerForDelegate(ueh);
            pDisplayCallback = Marshal.GetFunctionPointerForDelegate(deh);

            m_callbacks.Add(leh);
            m_callbacks.Add(deh);
            m_callbacks.Add(ueh);

            m_timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            m_timer.Interval = 1000;
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            m_latestFps = m_frameRate;
            m_frameRate = 0;
        }

        unsafe void* OnpLock(void* opaque, void** plane)
        {
            PixelData* px = (PixelData*)opaque;
            *plane = px->pPixelData;
            return null;
        }

        unsafe void OnpUnlock(void* opaque, void* picture, void** plane)
        {

        }

        unsafe void OnpDisplay(void* opaque, void* picture)
        {
            lock (m_lock)
            {
                try
                {
                    PixelData* px = (PixelData*)opaque;
                    MemoryHeap.CopyMemory(m_pBuffer, px->pPixelData, px->size);

                    m_frameRate++;
                    if (m_callback != null)
                    {
                        using (Bitmap frame = GetBitmap())
                        {
                            m_callback(frame);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (m_excHandler != null)
                    {
                        m_excHandler(ex);
                    }
                    else
                    {
                        throw ex;
                    }
                }
            }
        }

        private Bitmap GetBitmap()
        {
            return new Bitmap(m_format.Width, m_format.Height, m_format.Pitch, m_format.PixelFormat, new IntPtr(m_pBuffer));
        }

        #region IMemoryRenderer Members

        public void SetCallback(NewFrameEventHandler callback)
        {
            m_callback = callback;
        }

        public void SetFormat(BitmapFormat format)
        {
            m_format = format;

            LibVlcMethods.libvlc_video_set_format(m_hMediaPlayer, m_format.Chroma.ToUtf8(), m_format.Width, m_format.Height, m_format.Pitch);
            m_pBuffer = MemoryHeap.Alloc(m_format.ImageSize);

            m_pixelData = new PixelData(m_format.ImageSize);
            m_pixelDataPtr = GCHandle.Alloc(m_pixelData, GCHandleType.Pinned);
            LibVlcMethods.libvlc_video_set_callbacks(m_hMediaPlayer, pLockCallback, pUnlockCallback, pDisplayCallback, m_pixelDataPtr.AddrOfPinnedObject());
        }

        internal void StartTimer()
        {
            m_timer.Start();
        }

        public int ActualFrameRate
        {
            get
            {
                return m_latestFps;
            }
        }

        public Bitmap CurrentFrame
        {
            get
            {
                lock (m_lock)
                {
                    return GetBitmap();
                }
            }
        }

        public void SetExceptionHandler(Action<Exception> handler)
        {
            m_excHandler = handler;
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            IntPtr zero = IntPtr.Zero;
            LibVlcMethods.libvlc_video_set_callbacks(m_hMediaPlayer, zero, zero, zero, zero);

            m_pixelDataPtr.Free();
            m_pixelData.Dispose();

            MemoryHeap.Free(m_pBuffer);

            if (disposing)
            {
                m_timer.Dispose();
                m_callback = null;
                m_callbacks.Clear();
            }
        }
    }
}
