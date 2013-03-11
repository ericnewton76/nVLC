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
using Declarations.Events;
using Implementation.Media;
using LibVlcWrapper;

namespace Implementation.Events
{
    class MediaListEventManager : EventManager, IMediaListEvents
    {
        public MediaListEventManager(IEventProvider eventProvider)
            : base(eventProvider)
        {
        }

        protected override void MediaPlayerEventOccured(ref libvlc_event_t libvlc_event, IntPtr userData)
        {
            switch (libvlc_event.type)
            {
                case libvlc_event_e.libvlc_MediaListItemAdded:
                    if (m_itemAdded != null)
                    {
                        BasicMedia media = new BasicMedia(libvlc_event.MediaDescriptor.media_list_item_added.item, ReferenceCountAction.AddRef);
                        m_itemAdded(m_eventProvider, new MediaListItemAdded(media, libvlc_event.MediaDescriptor.media_list_item_added.index));
                        media.Release();
                    }
                    break;

                case libvlc_event_e.libvlc_MediaListWillAddItem:
                    if (m_willAddItem != null)
                    {
                        BasicMedia media2 = new BasicMedia(libvlc_event.MediaDescriptor.media_list_will_add_item.item, ReferenceCountAction.AddRef);
                        m_willAddItem(m_eventProvider, new MediaListWillAddItem(media2, libvlc_event.MediaDescriptor.media_list_will_add_item.index));
                        media2.Release();
                    }
                    break;

                case libvlc_event_e.libvlc_MediaListItemDeleted:
                    if (m_itemDeleted != null)
                    {
                        BasicMedia media3 = new BasicMedia(libvlc_event.MediaDescriptor.media_list_item_deleted.item, ReferenceCountAction.AddRef);
                        m_itemDeleted(m_eventProvider, new MediaListItemDeleted(media3, libvlc_event.MediaDescriptor.media_list_item_deleted.index));
                        media3.Release();
                    }
                    break;

                case libvlc_event_e.libvlc_MediaListWillDeleteItem:
                    if (m_willDeleteItem != null)
                    {
                        BasicMedia media4 = new BasicMedia(libvlc_event.MediaDescriptor.media_list_will_delete_item.item, ReferenceCountAction.AddRef);
                        m_willDeleteItem(m_eventProvider, new MediaListWillDeleteItem(media4, libvlc_event.MediaDescriptor.media_list_will_delete_item.index));
                        media4.Release();
                    }
                    break;
            }
        }

        #region IMediaListEvents Members

        event EventHandler<MediaListItemAdded> m_itemAdded;
        public event EventHandler<MediaListItemAdded> ItemAdded
        {
            add
            {
                if (m_itemAdded == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaListItemAdded);
                }
                m_itemAdded += value;
            }
            remove
            {
                if (m_itemAdded != null)
                {
                    m_itemAdded -= value;
                    if (m_itemAdded == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaListItemAdded);
                    }
                }
            }
        }

        event EventHandler<MediaListWillAddItem> m_willAddItem;
        public event EventHandler<MediaListWillAddItem> WillAddItem
        {
            add
            {
                if (m_willAddItem != null)
                {
                    Attach(libvlc_event_e.libvlc_MediaListWillAddItem);
                }
                m_willAddItem += value;
            }
            remove
            {
                if (m_willAddItem != null)
                {
                    m_willAddItem -= value;
                    if (m_willAddItem == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaListWillAddItem);
                    }
                }
            }
        }

        event EventHandler<MediaListItemDeleted> m_itemDeleted;
        public event EventHandler<MediaListItemDeleted> ItemDeleted
        {
            add
            {
                if (m_itemDeleted == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaListItemDeleted);
                }
                m_itemDeleted += value;
            }
            remove
            {
                if (m_itemDeleted != null)
                {
                    m_itemDeleted -= value;
                    if (m_itemDeleted == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaListItemDeleted);
                    }
                }
            }
        }

        event EventHandler<MediaListWillDeleteItem> m_willDeleteItem;
        public event EventHandler<MediaListWillDeleteItem> WillDeleteItem
        {
            add
            {
                if (m_willDeleteItem == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaListWillDeleteItem);
                }
                m_willDeleteItem += value;
            }
            remove
            {
                if (m_willDeleteItem != null)
                {
                    m_willDeleteItem -= value;
                    if (m_willDeleteItem == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaListWillDeleteItem);
                    }
                }
            }
        }

        #endregion
    }
}
