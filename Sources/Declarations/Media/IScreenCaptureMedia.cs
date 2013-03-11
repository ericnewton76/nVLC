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

namespace Declarations.Media
{
   /// <summary>
   /// Represents media captured from screen or a part of it.
   /// </summary>
   public interface IScreenCaptureMedia : IMedia
   {
      /// <summary>
      /// Capture area of the screen.
      /// </summary>
      Rectangle CaptureArea { get; set; }

      /// <summary>
      /// Gets or sets the frame rate of the capture.
      /// </summary>
      int FPS { get; set; }

      /// <summary>
      /// Gets or sets value indication whether to include the mouse cursor in the captured frame.
      /// </summary>
      bool FollowMouse { get; set; }

      /// <summary>
      /// Gets or sets full path of the file (PNG) containing cursor icon.
      /// </summary>
      string CursorFile { get; set; }
   }
}
