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

namespace Declarations
{
    /// <summary>
    /// Structure for pixel planes and their sizes
    /// </summary>
    public struct PlanarFrame
    {
        /// <summary>
        /// Initializes new instance
        /// </summary>
        /// <param name="planes"></param>
        /// <param name="lenghts"></param>
        public PlanarFrame(IntPtr[] planes, int[] lenghts)
            :this()
        {
            if (planes.Length != lenghts.Length)
            {
                throw new ArgumentException("Number of planes must be equal to lenghts array");
            }

            this.Planes = planes;
            this.Lenghts = lenghts;
        }

        /// <summary>
        /// Gets pointer array to the pixel planes on the native heap 
        /// </summary>
        public IntPtr[] Planes { get; set; }

        /// <summary>
        /// Gets length of each pixel plane
        /// </summary>
        public int[] Lenghts { get; set; }
    }
}
