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
    /// Contains methods for setting custom processing of video frames.
    /// </summary>
    public interface IMemoryRendererEx : IRenderer
    {
        /// <summary>
        /// Sets the callback which invoked when new frame should be displayed
        /// </summary>
        /// <param name="callback">Callback method</param>
        void SetCallback(NewFrameDataEventHandler callback);

        /// <summary>
        /// Gets the latest video frame that was displayed.
        /// </summary>
        PlanarFrame CurrentFrame { get; }

        /// <summary>
        /// Sets the callback invoked before the media playback starts to set the desired frame format.
        /// </summary>
        /// <param name="setupCallback"></param>
        /// <remarks>If not set, original media format will be used</remarks>
        void SetFormatSetupCallback(Func<BitmapFormat, BitmapFormat> setupCallback);
    }
}
