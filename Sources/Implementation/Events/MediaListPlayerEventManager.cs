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
    internal class MediaListPlayerEventManager : EventManager, IMediaListPlayerEvents
    {
        public MediaListPlayerEventManager(IEventProvider eventProvider)
            : base(eventProvider)
        {

        }

        protected override void MediaPlayerEventOccured(ref libvlc_event_t libvlc_event, IntPtr userData)
        {
            switch (libvlc_event.type)
            {
                case libvlc_event_e.libvlc_MediaListPlayerPlayed:
                    if (m_mediaListPlayerPlayed != null)
                    {
                        m_mediaListPlayerPlayed(m_eventProvider, EventArgs.Empty);
                    }
                    break;
                case libvlc_event_e.libvlc_MediaListPlayerNextItemSet:
                    if (m_mediaListPlayerNextItemSet != null)
                    {
                        BasicMedia media = new BasicMedia(libvlc_event.MediaDescriptor.media_list_player_next_item_set.item, ReferenceCountAction.AddRef);
                        m_mediaListPlayerNextItemSet(m_eventProvider, new MediaListPlayerNextItemSet(media));
                        //media.Release();
                    }
                    break;
                case libvlc_event_e.libvlc_MediaListPlayerStopped:
                    if (m_mediaListPlayerStopped != null)
                    {
                        m_mediaListPlayerStopped(m_eventProvider, EventArgs.Empty);
                    }
                    break;
            }
        }

        #region IMediaListPlayerEvents Members

        event EventHandler m_mediaListPlayerPlayed;
        public event EventHandler MediaListPlayerPlayed
        {
            add
            {
                if (m_mediaListPlayerPlayed == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaListPlayerPlayed);
                }
                m_mediaListPlayerPlayed += value;
            }
            remove
            {
                if (m_mediaListPlayerPlayed != null)
                {
                    m_mediaListPlayerPlayed -= value;
                    if (m_mediaListPlayerPlayed == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaListPlayerPlayed);
                    }
                }
            }
        }

        event EventHandler<MediaListPlayerNextItemSet> m_mediaListPlayerNextItemSet;
        public event EventHandler<MediaListPlayerNextItemSet> MediaListPlayerNextItemSet
        {
            add
            {
                if (m_mediaListPlayerNextItemSet == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaListPlayerNextItemSet);
                }
                m_mediaListPlayerNextItemSet += value;
            }
            remove
            {
                if (m_mediaListPlayerNextItemSet != null)
                {
                    m_mediaListPlayerNextItemSet -= value;
                    if (m_mediaListPlayerNextItemSet == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaListPlayerNextItemSet);
                    }
                }
            }
        }

        event EventHandler m_mediaListPlayerStopped;
        public event EventHandler MediaListPlayerStopped
        {
            add
            {
                if (m_mediaListPlayerStopped == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaListPlayerStopped);
                }
                m_mediaListPlayerStopped += value;
            }
            remove
            {
                if (m_mediaListPlayerStopped != null)
                {
                    m_mediaListPlayerStopped -= value;
                    if (m_mediaListPlayerStopped == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaListPlayerStopped);
                    }
                }
            }
        }

        #endregion
    }
}
