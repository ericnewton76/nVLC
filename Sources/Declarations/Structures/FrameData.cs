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
    /// Data structure for single frame of elementary stream
    /// </summary>
    public struct FrameData
    {
        /// <summary>
        /// Pointer to the frame data
        /// </summary>
        public IntPtr Data { get; set; }

        /// <summary>
        /// Data size in bytes
        /// </summary>
        public int DataSize { get; set; }

        /// <summary>
        /// Decoding time stamp in microseconds. -1 means unknown
        /// </summary>
        public long DTS { get; set; }

        /// <summary>
        /// Presentation time stamp in microseconds.
        /// </summary>
        public long PTS { get; set; }
    }
}
