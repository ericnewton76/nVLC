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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Declarations;
using Declarations.Media;
using Implementation.Media;
using Implementation.Utils;
using System.Drawing;
using System.Drawing.Imaging;

namespace Implementation
{
    internal sealed unsafe class MemoryInputMedia : BasicMedia, IMemoryInputMedia
    {
        IntPtr m_pLock, m_pUnlock;
        List<Delegate> m_callbacks = new List<Delegate>();
        StreamInfo m_streamInfo;
        BlockingCollection<FrameData> m_queue;
        Action<Exception> m_excHandler;
        bool m_initilaized;
        
        public MemoryInputMedia(IntPtr hMediaLib)
            : base(hMediaLib)
        {           
            ImemGet pLock = OnImemGet;
            ImemRelease pUnlock = OnImemRelease;

            m_pLock = Marshal.GetFunctionPointerForDelegate(pLock);
            m_pUnlock = Marshal.GetFunctionPointerForDelegate(pUnlock);

            m_callbacks.Add(pLock);
            m_callbacks.Add(pUnlock);
        }

        public void Initialize(StreamInfo streamInfo, int maxItemsInQueue)
        {
            if (streamInfo == null)
            {
                throw new ArgumentNullException("streamInfo");
            }

            m_streamInfo = streamInfo;
            AddOptions(MediaOptions.ToList());
            m_queue = new BlockingCollection<FrameData>(maxItemsInQueue);
            m_initilaized = true;
        }

        public void AddFrame(FrameData frameData)
        {
            if (!m_initilaized)
            {
                throw new InvalidOperationException("The instance must be initialized first. Call Initialize method before adding frames");
            }

            if (frameData.Data == IntPtr.Zero)
            {
                throw new ArgumentNullException("frameData.Data");
            }

            if (frameData.DataSize == 0)
            {
                throw new ArgumentException("DataSize value must be greater than zero", "frameData.DataSize");
            }

            if (frameData.PTS < 0)
            {
                throw new ArgumentException("Pts value must be greater than zero", "frameData.PTS");
            }

            m_queue.Add(DeepClone(frameData));
        }

        public void AddFrame(byte[] data, long pts, long dts)
        {
            if (!m_initilaized)
            {
                throw new InvalidOperationException("The instance must be initialized first. Call Initialize method before adding frames");
            }

            if (data == null || data.Length == 0)
            {
                throw new ArgumentException("data buffer size must be greater than zero", "data");
            }

            if (pts <= 0)
            {
                throw new ArgumentException("Pts value must be greater than zero", "pts");
            }

            FrameData frame = DeepClone(data);
            frame.PTS = pts;
            frame.DTS = dts;
            m_queue.Add(frame);
        }

        public void AddFrame(Bitmap bitmap, long pts, long dts)
        {
            if (!m_initilaized)
            {
                throw new InvalidOperationException("The instance must be initialized first. Call Initialize method before adding frames");
            }

            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            if (pts < 0)
            {
                throw new ArgumentException("Pts value must be greater than zero", "pts");
            }

            if (bitmap.PixelFormat != PixelFormat.Format24bppRgb &&
                bitmap.PixelFormat != PixelFormat.Format32bppRgb)
            {
                throw new ArgumentException("Supported pixel formats for bitmaps are Format24bppRgb and Format32bppRgb", "bitmap.PixelFormat");
            }

            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);
            FrameData frame = DeepClone(bmpData.Scan0, bmpData.Stride * bmpData.Height);
            bitmap.UnlockBits(bmpData);
            frame.PTS = pts;
            frame.DTS = dts;
            m_queue.Add(frame);
        }

        private FrameData DeepClone(byte[] buffer)
        {
            FrameData clone = new FrameData();
            clone.Data = new IntPtr(MemoryHeap.Alloc(buffer.Length));
            Marshal.Copy(buffer, 0, clone.Data, buffer.Length);
            clone.DataSize = buffer.Length;
            return clone;
        }

        private FrameData DeepClone(FrameData frameData)
        {
            FrameData clone = DeepClone(frameData.Data, frameData.DataSize);
            clone.DTS = frameData.DTS;
            clone.PTS = frameData.PTS;
            return clone;
        }

        private FrameData DeepClone(IntPtr data, int size)
        {
            FrameData clone = new FrameData();
            clone.Data = new IntPtr(MemoryHeap.Alloc(size));
            MemoryHeap.CopyMemory(clone.Data.ToPointer(), data.ToPointer(), size);
            clone.DataSize = size;
            return clone;
        }

        private int OnImemGet(void* data, char* cookie, long* dts, long* pts, int* flags, uint* dataSize, void** ppData)
        {
            try
            {
                FrameData fdata = m_queue.Take();
                *ppData = fdata.Data.ToPointer();
                *dataSize = (uint)fdata.DataSize;
                *pts = fdata.PTS;
                *dts = fdata.DTS;
                *flags = 0;
                return 0;
            }
            catch (Exception ex)
            {
                if (m_excHandler != null)
                {
                    m_excHandler(ex);
                }
                else
                {
                    throw new Exception("imem-get callback failed", ex);
                }
                return 1;
            }           
        }

        private void OnImemRelease(void* data, char* cookie, uint dataSize, void* pData)
        {
            try
            {
                MemoryHeap.Free(pData);
            }
            catch (Exception ex)
            {
                if (m_excHandler != null)
                {
                    m_excHandler(ex);
                }
                else
                {
                    throw new Exception("imem-release callback failed", ex);
                }
            }
        }

        private IEnumerable<string> MediaOptions
        {
            get
            {
                yield return string.Format(":imem-get={0}", m_pLock.ToInt64());
                yield return string.Format(":imem-release={0}", m_pUnlock.ToInt64());
                yield return string.Format(":imem-codec={0}", EnumUtils.GetEnumDescription(m_streamInfo.Codec));
                yield return string.Format(":imem-cat={0}", (int)m_streamInfo.Category);
                yield return string.Format(":imem-id={0}", m_streamInfo.ID);
                yield return string.Format(":imem-group={0}", m_streamInfo.Group);
                yield return string.Format(":imem-fps={0}", m_streamInfo.FPS);
                yield return string.Format(":imem-width={0}", m_streamInfo.Width);
                yield return string.Format(":imem-height={0}", m_streamInfo.Height);
                yield return string.Format(":imem-size={0}", m_streamInfo.Size);
                yield return string.Format(":imem-channels={0}", m_streamInfo.Channels);
                yield return string.Format(":imem-samplerate={0}", m_streamInfo.Samplerate);
                yield return string.Format(":imem-dar={0}", EnumUtils.GetEnumDescription(m_streamInfo.AspectRatio));
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                m_callbacks = null;
                if (m_queue.Count > 0)
                {
                    foreach (var item in m_queue)
                    {
                        MemoryHeap.Free(item.Data.ToPointer());
                    }
                }
                m_queue = null;
            }
        }

        public void SetExceptionHandler(Action<Exception> handler)
        {
            m_excHandler = handler;
        }

        public int PendingFramesCount
        {
            get 
            {
                return m_queue.Count;
            }
        }
    }
}
