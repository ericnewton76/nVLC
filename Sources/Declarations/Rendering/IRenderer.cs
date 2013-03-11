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
    /// Base type for custom rendering types
    /// </summary>
    public interface IRenderer : IDisposable
    {
        /// <summary>
        /// Sets exception handler for exceptions thrown by background threads.
        /// </summary>
        /// <param name="handler"></param>
        void SetExceptionHandler(Action<Exception> handler);

        /// <summary>
        /// Gets the actual frame rate of the rendering.
        /// </summary>
        int ActualFrameRate { get; }
    }
}
