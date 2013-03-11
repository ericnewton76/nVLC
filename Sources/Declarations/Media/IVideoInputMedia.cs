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
   /// Enables raw video frames input into VLC engine (based on invmem access module)
   /// </summary>
   public interface IVideoInputMedia : IMedia
   {
      /// <summary>
      /// Adds frame to the video stream.
      /// </summary>
      /// <param name="frame"></param>
      void AddFrame(Bitmap frame);

      /// <summary>
      /// Sets bitmap format for the video frames.
      /// </summary>
      /// <param name="format"></param>
      void SetFormat(BitmapFormat format);
   }
}
