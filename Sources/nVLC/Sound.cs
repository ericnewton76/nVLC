﻿//    nVLC
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
    /// Structure incapsulation for audio samples
    /// </summary>
    [Serializable]
    public struct Sound
    {
        /// <summary>
        /// Pointer to the first audio sample
        /// </summary>
        public IntPtr SamplesData { get; set; }

        /// <summary>
        /// Size in bytes of SamplesData buffer
        /// </summary>
        public uint SamplesSize { get; set; }

        /// <summary>
        /// Playback time stamp in microseconds
        /// </summary>
        public long Pts { get; set; }
    }
}
