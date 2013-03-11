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
using Implementation.Utils;
using System.Runtime.InteropServices;

namespace Implementation
{
    internal unsafe struct PixelData : IDisposable
    {
        public byte* pPixelData;
        public int size;

        public PixelData(int size)
        {
            this.size = size;
            this.pPixelData = (byte*)MemoryHeap.Alloc(size);
        }

        #region IDisposable Members

        public void Dispose()
        {
            MemoryHeap.Free(this.pPixelData);
        }

        #endregion

        public static bool operator ==(PixelData pd1, PixelData pd2)
        {
            return (pd1.size == pd2.size && pd1.pPixelData == pd2.pPixelData);
        }

        public static bool operator !=(PixelData pd1, PixelData pd2)
        {
            return !(pd1 == pd2);
        }

        public override int GetHashCode()
        {
            return size.GetHashCode() ^ pPixelData->GetHashCode();
        }

        public override bool Equals(object obj)
        {
            PixelData pd = (PixelData)obj;
            if (pd == null)
            {
                return false;
            }

            return this == pd;
        }
    }
}
