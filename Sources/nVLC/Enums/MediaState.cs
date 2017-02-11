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

namespace nVLC
{
   /// <summary>
   /// Possible state of the media objects
   /// </summary>
   public enum MediaState
   {
      NothingSpecial = 0,
      Opening,
      Buffering,
      Playing,
      Paused,
      Stopped,
      Ended,
      Error
   }
}
