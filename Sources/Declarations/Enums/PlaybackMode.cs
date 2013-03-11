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
   /// Available playback modes for the media list player.
   /// </summary>
   public enum PlaybackMode
   {
      /// <summary>
      /// Playes items sequentially.
      /// </summary>
      Default,

      /// <summary>
      /// Loops playlist on end.
      /// </summary>
      Loop,

      /// <summary>
      /// Repeats the current item until another item is forced.
      /// </summary>
      Repeat
   }
}
