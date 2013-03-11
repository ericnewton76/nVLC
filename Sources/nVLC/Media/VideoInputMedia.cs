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
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using Declarations;
using Declarations.Media;
using Implementation.Utils;
using LibVlcWrapper;

namespace Implementation.Media
{
    [MaxLibVlcVersion("1.1.x")]
    internal sealed unsafe class VideoInputMedia : BasicMedia, IVideoInputMedia
    {
        BitmapFormat m_format;
        PixelData m_data = default(PixelData);
        object m_lock = new object();
        IntPtr m_pLock, m_pUnlock;
        GCHandle m_pData;
        List<Delegate> m_callbacks = new List<Delegate>();

        public VideoInputMedia(IntPtr hMediaLib)
            : base(hMediaLib)
        {
            CallbackEventHandler pLock = LockCallback;
            CallbackEventHandler pUnlock = UnlockCallback;

            m_pLock = Marshal.GetFunctionPointerForDelegate(pLock);
            m_pUnlock = Marshal.GetFunctionPointerForDelegate(pUnlock);

            m_callbacks.Add(pLock);
            m_callbacks.Add(pUnlock);
        }

        #region IVideoInputMedia Members

        public void AddFrame(Bitmap frame)
        {
            Monitor.Enter(m_lock);

            try
            {
                Rectangle rect = new Rectangle(0, 0, frame.Width, frame.Height);
                BitmapData bmpData = frame.LockBits(rect, ImageLockMode.ReadOnly, frame.PixelFormat);

                void* pData = bmpData.Scan0.ToPointer();
                MemoryHeap.CopyMemory(m_data.pPixelData, pData, m_data.size);

                frame.UnlockBits(bmpData);
            }
            finally
            {
                Monitor.Exit(m_lock);
            }
        }

        public void SetFormat(BitmapFormat format)
        {
            if (m_data == default(PixelData))
            {
                m_format = format;
                m_data = new PixelData(m_format.ImageSize);
                m_pData = GCHandle.Alloc(m_data, GCHandleType.Pinned);
                InitMedia();
            }
            else
            {
                throw new InvalidOperationException("Bitmap format already set");
            }
        }

        #endregion

        private void InitMedia()
        {
            List<string> options = new List<string>()
            {
               ":codec=invmem",
               string.Format(":invmem-width={0}", m_format.Width),
               string.Format(":invmem-height={0}", m_format.Height),
               string.Format(":invmem-lock={0}", m_pLock.ToInt64()),
               string.Format(":invmem-unlock={0}", m_pUnlock.ToInt64()),
               string.Format(":invmem-chroma={0}", m_format.Chroma),
               string.Format(":invmem-data={0}", m_pData.AddrOfPinnedObject().ToInt64())
            };

            AddOptions(options);
        }

        void* LockCallback(void* data)
        {
            Monitor.Enter(m_lock);
            PixelData* pd = (PixelData*)data;
            return pd->pPixelData;
        }

        void* UnlockCallback(void* data)
        {
            Monitor.Exit(m_lock);
            PixelData* pd = (PixelData*)data;
            return pd->pPixelData;
        }

        protected override void Dispose(bool disposing)
        {
            m_data.Dispose();
            m_pData.Free();

            if (disposing)
            {
                m_callbacks = null;
            }

            base.Dispose(disposing);
        }
    }
}
