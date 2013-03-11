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
   /// <summary>
   /// Events raised by IMedia object
   /// </summary>
   public interface IMediaEvents
   {
      event EventHandler<MediaMetaDataChange> MetaDataChanged;

      event EventHandler<MediaNewSubItem> SubItemAdded;

      event EventHandler<MediaDurationChange> DurationChanged;

      event EventHandler<MediaParseChange> ParsedChanged;

      event EventHandler<MediaFree> MediaFreed;

      event EventHandler<MediaStateChange> StateChanged;
   }

   public class MediaMetaDataChange : EventArgs
   {
      public MediaMetaDataChange(MetaDataType type)
      {
         MetaType = type;
      }

      public MetaDataType MetaType { get; private set; }
   }

   public class MediaNewSubItem : EventArgs
   {
      public MediaNewSubItem(IMedia subItem)
      {
         SubItem = subItem;
      }

      public IMedia SubItem { get; private set; }
   }

   public class MediaDurationChange : EventArgs
   {
      public MediaDurationChange(long newDuration)
      {
         NewDuration = newDuration;
      }

      public long NewDuration { get; private set; }
   }

   public class MediaParseChange : EventArgs
   {
      public MediaParseChange(bool parsed)
      {
         Parsed = parsed;
      }

      public bool Parsed { get; private set; }
   }

   public class MediaFree : EventArgs
   {
      public MediaFree(IntPtr hMedia)
      {
         Media = hMedia;
      }

      public IntPtr Media { get; private set; }
   }

   public class MediaStateChange : EventArgs
   {
      public MediaStateChange(MediaState newState)
      {
         NewState = newState;
      }

      public MediaState NewState { get; private set; }
   }
}
