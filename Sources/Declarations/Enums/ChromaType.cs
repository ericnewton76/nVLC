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
    /// VLC pixel formats
    /// </summary>
    public enum ChromaType
    {
        /// <summary>
        /// 5 bit for each RGB channel
        /// </summary>
        RV15,

        /// <summary>
        /// 5 bit Red, 6 bit Green and 5 bit Blue
        /// </summary>
        RV16,

        /// <summary>
        /// 8 bit per channel
        /// </summary>
        RV24,

        /// <summary>
        /// 8 bit per RGB channel and 8 bit unused
        /// </summary>
        RV32,

        /// <summary>
        /// 8 bit per each RGBA channel
        /// </summary>
        RGBA,

        /// <summary>
        /// 12 bits per pixel planar format with Y plane followed by V and U planes
        /// </summary>
        YV12,

        /// <summary>
        /// Same as YV12 but V and U are swapped
        /// </summary>
        I420,

        /// <summary>
        /// 12 bits per pixel planar format with Y plane and interleaved UV plane
        /// </summary>
        NV12,

        /// <summary>
        /// 16 bits per pixel packed YUYV array
        /// </summary>
        YUY2,

        /// <summary>
        /// 16 bits per pixel packed UYVY array 
        /// </summary>
        UYVY,

        /// <summary>
        /// Same as I420, mainly used with MJPG codecs
        /// </summary>
        J420
    }
}
