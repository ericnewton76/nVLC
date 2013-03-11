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
   /// Manages video adjustment parameters.
   /// </summary>
   public interface IAdjustFilter
   {
      /// <summary>
      /// Enables or disables video adjust filter.
      /// </summary>          
      bool Enabled { get; set; }

      /// <summary>
      /// Image contrast in the 0-2 range.
      /// </summary>          
      float Contrast { get; set; }

      /// <summary>
      /// Image brightness in the 0-2 range.
      /// </summary>       
      float Brightness { get; set; }

      /// <summary>
      /// Image hue in the 0-360 range
      /// </summary>    
      int Hue { get; set; }

      /// <summary>
      /// Image saturation in the 0-3 range.
      /// </summary>   
      float Saturation { get; set; }

      /// <summary>
      /// Image gamma in the 0-10 range.
      /// </summary>    
      float Gamma { get; set; }
   }
}
