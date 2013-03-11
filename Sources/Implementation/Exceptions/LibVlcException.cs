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
using LibVlcWrapper;

namespace Implementation.Exceptions
{
    /// <summary>
    /// Throws an exception with the latest error message.
    /// </summary>
    [Serializable]
    public class LibVlcException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the LibVlcException class with the last error that occurred.
        /// </summary>
        public LibVlcException()
            : base(LibVlcMethods.libvlc_errmsg())
        {

        }

        public LibVlcException(string message)
            : base(message)
        {
        }
    }
}
