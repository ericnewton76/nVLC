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
using System.Runtime.InteropServices;
using Declarations;
using Declarations.Events;
using LibVlcWrapper;

namespace Implementation.Events
{
   internal abstract class EventManager
   {
      protected IEventProvider m_eventProvider;
      List<VlcEventHandlerDelegate> m_callbacks = new List<VlcEventHandlerDelegate>();
      IntPtr hCallback1;

      protected EventManager(IEventProvider eventProvider)
      {
         m_eventProvider = eventProvider;

         VlcEventHandlerDelegate callback1 = MediaPlayerEventOccured;

         hCallback1 = Marshal.GetFunctionPointerForDelegate(callback1);
         m_callbacks.Add(callback1);

         GC.KeepAlive(callback1);
      }

      protected void Attach(libvlc_event_e eType)
      {
         if (LibVlcMethods.libvlc_event_attach(m_eventProvider.EventManagerHandle, eType, hCallback1, IntPtr.Zero) != 0)
         {
            throw new OutOfMemoryException("Failed to subscribe to event notification");
         }
      }

      protected void Dettach(libvlc_event_e eType)
      {
         LibVlcMethods.libvlc_event_detach(m_eventProvider.EventManagerHandle, eType, hCallback1, IntPtr.Zero);
      }

      protected abstract void MediaPlayerEventOccured(ref libvlc_event_t libvlc_event, IntPtr userData);
   }
}
