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
using Declarations.Events;

namespace Declarations.Events
{
   /// <summary>
   /// Events raised by the IPlayer object
   /// </summary>
   public interface IEventBroker
   {
      /// <summary>
      /// Event raised when media reaches end.
      /// </summary>
      event EventHandler MediaEnded;
      
      /// <summary>
      /// Event raised when media time changed.
      /// </summary>
      event EventHandler<MediaPlayerTimeChanged> TimeChanged;

      event EventHandler<MediaPlayerMediaChanged> MediaChanged;

      event EventHandler NothingSpecial;

      event EventHandler PlayerOpening;

      event EventHandler PlayerBuffering;

      event EventHandler PlayerPlaying;

      event EventHandler PlayerPaused;

      event EventHandler PlayerStopped;

      event EventHandler PlayerForward;

      event EventHandler PlayerBackward;

      event EventHandler PlayerEncounteredError;

      event EventHandler<MediaPlayerPositionChanged> PlayerPositionChanged;

      event EventHandler<MediaPlayerSeekableChanged> PlayerSeekableChanged;

      event EventHandler<MediaPlayerPausableChanged> PlayerPausableChanged;

      event EventHandler<MediaPlayerTitleChanged> PlayerTitleChanged;

      event EventHandler<MediaPlayerSnapshotTaken> PlayerSnapshotTaken;

      event EventHandler<MediaPlayerLengthChanged> PlayerLengthChanged;
   }
}
