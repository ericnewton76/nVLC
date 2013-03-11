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
using System.Linq;
using System.Text;
using Implementation.Utils;

namespace Implementation
{
    internal unsafe struct PlanarPixelData : IDisposable
    {
        public int[] Sizes;
        public byte** Data;

        public PlanarPixelData(int[] lineSizes)
        {
            Sizes = lineSizes;

            Data = (byte**)MemoryHeap.Alloc(sizeof(byte*) * Sizes.Length);

            for (int i = 0; i < Sizes.Length; i++)
            {
                Data[i] = (byte*)MemoryHeap.Alloc(sizeof(byte) * Sizes[i]);
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < Sizes.Length; i++)
            {
                MemoryHeap.Free(Data[i]);
            }

            MemoryHeap.Free(Data);
        }

        public static bool operator ==(PlanarPixelData pd1, PlanarPixelData pd2)
        {
            return (pd1.Data == pd2.Data && pd1.Sizes == pd2.Sizes);
        }

        public static bool operator !=(PlanarPixelData pd1, PlanarPixelData pd2)
        {
            return !(pd1 == pd2);
        }

        public override int GetHashCode()
        {
            return Sizes.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            PlanarPixelData pd = (PlanarPixelData)obj;
            if (pd == null)
            {
                return false;
            }

            return this == pd;
        }
    }
}
