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
using Declarations.Media;

namespace Declarations.Events
{
   public class MediaPlayerMediaChanged : EventArgs
   {
      public MediaPlayerMediaChanged(IMedia newMedia)
      {
         NewMedia = newMedia;
      }

      public IMedia NewMedia { get; private set; }
   }

   public class MediaPlayerTimeChanged : EventArgs
   {
      public MediaPlayerTimeChanged(long newTime)
      {
         NewTime = newTime;
      }

      public long NewTime { get; private set; }
   }

   public class MediaPlayerPositionChanged : EventArgs
   {
      public MediaPlayerPositionChanged(float newPosition)
      {
         NewPosition = newPosition;
      }

      public float NewPosition { get; private set; }
   }

   public class MediaPlayerSeekableChanged : EventArgs
   {
      public MediaPlayerSeekableChanged(int newSeekable)
      {
         NewSeekable = newSeekable;
      }

      public int NewSeekable { get; private set; }
   }

   public class MediaPlayerPausableChanged : EventArgs
   {
      public MediaPlayerPausableChanged(int newPausable)
      {
         NewPausable = newPausable;
      }

      public int NewPausable { get; private set; }
   }

   public class MediaPlayerTitleChanged : EventArgs
   {
      public MediaPlayerTitleChanged(int newTitle)
      {
         NewTitle = newTitle;
      }

      public int NewTitle { get; private set; }
   }

   public class MediaPlayerSnapshotTaken : EventArgs
   {
      public MediaPlayerSnapshotTaken(string filename)
      {
         FileName = filename;
      }

      public string FileName { get; private set; }
   }

   public class MediaPlayerLengthChanged : EventArgs
   {
      public MediaPlayerLengthChanged(long newLength)
      {
         NewLength = newLength;
      }

      public long NewLength { get; private set; }
   }
}


