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

namespace Implementation.Exceptions
{
    /// <summary>
    /// Exception thrown when one of the vlc libraries are missing.
    /// </summary>
    [Serializable]
    public class LibVlcNotFoundException : DllNotFoundException
    {
        const string msg = "Failed to load VLC modules. Make sure libvlc.dll, libvlccore.dll and plugins directory located in the executable path.";

        public LibVlcNotFoundException()
            : base(msg)
        {

        }

        public LibVlcNotFoundException(Exception ex)
            : base(msg, ex)
        {
        }
    }
}
