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
using System.ComponentModel;

namespace Declarations.Enums
{
    /// <summary>
    /// Video codecs supported by imem module (IMemoryInputMedia)
    /// </summary>
    public enum VideoCodecs
    {
        /// <summary>
        /// 24 bits per pixel blue, green and red
        /// </summary>
        [Description("RV24")]
        BGR24,

        /// <summary>
        /// 32 bits per pixel blue, green, red and empty (or alpha)
        /// </summary>
        [Description("RV32")]
        BGR32,

        /// <summary>
        /// Motion JPEG stream - each video frame encoded as jpeg image
        /// </summary>
        [Description("MJPG")]
        MJPEG,

        /// <summary>
        /// YUV420 12 bits per pixel Y, Cb and Cr
        /// </summary>
        [Description("I420")]
        I420
    }
}
