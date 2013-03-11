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
using Declarations;
using Declarations.Discovery;
using Declarations.Events;
using Declarations.Media;
using Implementation.Events;
using Implementation.Media;
using LibVlcWrapper;

namespace Implementation.Discovery
{
    internal class MediaDiscoverer : DisposableBase, IMediaDiscoverer, INativePointer, IEventProvider
    {
        private IntPtr m_hDiscovery = IntPtr.Zero;
        private IMediaDiscoveryEvents m_events;

        public MediaDiscoverer(IntPtr hMediaLib, string name)
        {
            m_hDiscovery = LibVlcMethods.libvlc_media_discoverer_new_from_name(hMediaLib, name.ToUtf8());
        }

        protected override void Dispose(bool disposing)
        {
            LibVlcMethods.libvlc_media_discoverer_release(m_hDiscovery);
        }

        public bool IsRunning
        {
            get
            {
                return (LibVlcMethods.libvlc_media_discoverer_is_running(m_hDiscovery) == 1);
            }
        }

        public string LocalizedName
        {
            get
            {
                return LibVlcMethods.libvlc_media_discoverer_localized_name(m_hDiscovery);
            }
        }

        public IMediaList MediaList
        {
            get
            {
                return new MediaList(LibVlcMethods.libvlc_media_discoverer_media_list(m_hDiscovery), ReferenceCountAction.None);
            }
        }

        public IntPtr EventManagerHandle
        {
            get 
            {
                return LibVlcMethods.libvlc_media_discoverer_event_manager(m_hDiscovery);
            }
        }

        public IntPtr Pointer
        {
            get 
            {
                return m_hDiscovery;
            }
        }

        public IMediaDiscoveryEvents Events
        {
            get
            {
                if (m_events == null)
                {
                    m_events = new MediaDiscoveryEventManager(this);
                }

                return m_events;
            }
        }
    }
}
