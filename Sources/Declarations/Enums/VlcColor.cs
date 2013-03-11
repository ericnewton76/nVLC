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
   /// Represents colors in HTML format
   /// </summary>
   public enum VlcColor : uint
   {
      Default = 0xf0000000,
      Black = 0x00000000,
      Gray = 0x00808080,
      Silver = 0x00C0C0C0,
      White = 0x00FFFFFF,
      Maroon = 0x00800000,
      Red = 0x00FF0000,
      Fuchsia = 0x00FF00FF,
      Yellow = 0x00FFFF00,
      Olive = 0x00808000,
      Green = 0x00008000,
      Teal = 0x00008080,
      Lime = 0x0000FF00,
      Purple = 0x00800080,
      Navy = 0x00000080,
      Blue = 0x000000FF,
      Aqua = 0x0000FFFF
   }
}
