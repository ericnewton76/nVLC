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
using System.Drawing;

namespace Declarations
{
    /// <summary>
    /// Enables custom processing of video frames.
    /// </summary>
    public interface IMemoryRenderer : IRenderer
    {
        /// <summary>
        /// Sets the callback which invoked when new frame should be displayed
        /// </summary>
        /// <param name="callback">Callback method</param>
        /// <remarks>The frame will be auto-disposed after callback invokation.</remarks>
        void SetCallback(NewFrameEventHandler callback);

        /// <summary>
        /// Gets the latest video frame that was displayed.
        /// </summary>
        Bitmap CurrentFrame { get; }

        /// <summary>
        /// Sets the bitmap format for the callback.
        /// </summary>
        /// <param name="format">Bitmap format of the video frame</param>
        void SetFormat(BitmapFormat format);
    }
}
