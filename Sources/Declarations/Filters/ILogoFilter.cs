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

namespace Declarations.Filters
{
   /// <summary>
   /// Logo overlay filter.
   /// </summary>
   public interface ILogoFilter
   {
      /// <summary>
      /// Enables or disables logo filter
      /// </summary>
      bool Enabled { get; set; }

      /// <summary>
      /// Full path of the image files to use.
      /// </summary>
      string File { get; set; }

      /// <summary>
      /// X coordinate of the logo.
      /// </summary>
      int X { get; set; }

      /// <summary>
      /// Y coordinate of the logo.
      /// </summary>
      int Y { get; set; }

      /// <summary>
      /// Individual image display time of 0 - 60000 ms.
      /// </summary>
      int Delay { get; set; }

      /// <summary>
      /// Number of loops for the logo animation. -1 = continuous, 0 = disabled.
      /// </summary>
      int Repeat { get; set; }

      /// <summary>
      /// Logo opacity value (from 0 for full transparency to 255 for full opacity).
      /// </summary>
      int Opacity { get; set; }

      /// <summary>
      /// Logo position.
      /// </summary>
      Position Position { get; set; }
   }
}
