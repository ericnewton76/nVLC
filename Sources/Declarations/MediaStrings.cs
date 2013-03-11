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
    /// String values used to identify media types.
    /// </summary>
    public class MediaStrings
    {
        public const string DVD = @"dvd://";
        public const string VCD = @"vcd://";
        public const string CDDA = @"cdda://";
        public const string BLURAY = @"bluray://";

        public const string RTP = @"rtp://";
        public const string RTSP = @"rtsp://";
        public const string HTTP = @"http://";
        public const string UDP = @"udp://";
        public const string MMS = @"mms://";

        public const string DSHOW = @"dshow://";
        public const string SCREEN = @"screen://";

        /// <summary>
        /// Fake access module. Should be used with IVideoInputMedia objects.
        /// </summary>
        public const string FAKE = @"fake://";

        /// <summary>
        /// imem access module. Should be used with IMemoryInputMedia objects
        /// </summary>
        public const string IMEM = @"imem://";
    }
}
