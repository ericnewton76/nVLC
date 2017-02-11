﻿//    nVLC
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

using System.Drawing;

namespace nVLC.Filters
{
   /// <summary>
   /// Represents video crop filter
   /// </summary>
   public interface ICropFilter
   {
      /// <summary>
      /// Enables or disables croping filter
      /// </summary>
      bool Enabled { get; set; }

      /// <summary>
      /// Gets or sets croping area.
      /// </summary>
      Rectangle CropArea { get; set; }
   }
}
