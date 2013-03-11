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

namespace Declarations.Players
{
   /// <summary>
   /// Player used for IMediaList playback.
   /// </summary>
   public interface IMediaListPlayer : IPlayer
   {
      /// <summary>
      /// Plays next item in the media list.
      /// </summary>
      void PlayNext();

      /// <summary>
      /// Plays previos item in the media list.
      /// </summary>
      void PlayPrevios();

      /// <summary>
      /// Sets or gets media list playback mode.
      /// </summary>
      PlaybackMode PlaybackMode { get; set; }

      /// <summary>
      /// Plays media item at specified index.
      /// </summary>
      /// <param name="index">Index of media</param>
      void PlayItemAt(int index);

      /// <summary>
      /// Gets media list player state.
      /// </summary>
      MediaState PlayerState { get; }

      /// <summary>
      /// Gets the inner player of media list player.
      /// </summary>
      IVideoPlayer InnerPlayer { get; }

      /// <summary>
      /// Gets events raised by media list player instnce.
      /// </summary>
      IMediaListPlayerEvents MediaListPlayerEvents { get; }
   }
}
