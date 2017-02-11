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

using System;

namespace nVLC.Events
{
   /// <summary>
   /// Events raised by IMediaListPlayer object
   /// </summary>
   public interface IMediaListPlayerEvents
   {
      event EventHandler MediaListPlayerPlayed;

      event EventHandler<MediaListPlayerNextItemSet> MediaListPlayerNextItemSet;

      event EventHandler MediaListPlayerStopped;
   }
}
