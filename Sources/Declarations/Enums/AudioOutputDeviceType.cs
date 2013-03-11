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

namespace Declarations.Enums
{
    /// <summary>
    /// Audio output device types
    /// </summary>
    public enum AudioOutputDeviceType
    {
        AudioOutputDevice_Error = -1,
        AudioOutputDevice_Mono = 1,
        AudioOutputDevice_Stereo = 2,
        AudioOutputDevice_2F2R = 4,
        AudioOutputDevice_3F2R = 5,
        AudioOutputDevice_5_1 = 6,
        AudioOutputDevice_6_1 = 7,
        AudioOutputDevice_7_1 = 8,
        AudioOutputDevice_SPDIF = 10
    }
}
