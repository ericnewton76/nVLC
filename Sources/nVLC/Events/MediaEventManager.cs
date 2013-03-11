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
using Declarations.Events;
using Implementation.Media;
using LibVlcWrapper;

namespace Implementation.Events
{
    class MediaEventManager : EventManager, IMediaEvents
    {
        public MediaEventManager(IEventProvider eventProvider)
            : base(eventProvider)
        {
        }

        protected override void MediaPlayerEventOccured(ref libvlc_event_t libvlc_event, IntPtr userData)
        {
            switch (libvlc_event.type)
            {
                case libvlc_event_e.libvlc_MediaMetaChanged:
                    if (metaDataChanged != null)
                    {
                        metaDataChanged(m_eventProvider, new MediaMetaDataChange((MetaDataType)libvlc_event.MediaDescriptor.media_meta_changed.meta_type));
                    }

                    break;
                case libvlc_event_e.libvlc_MediaSubItemAdded:
                    if (subItemAdded != null)
                    {
                        BasicMedia media = new BasicMedia(libvlc_event.MediaDescriptor.media_subitem_added.new_child, ReferenceCountAction.AddRef);
                        subItemAdded(m_eventProvider, new MediaNewSubItem(media));
                        media.Release();
                    }

                    break;
                case libvlc_event_e.libvlc_MediaDurationChanged:
                    if (durationChanged != null)
                    {
                        durationChanged(m_eventProvider, new MediaDurationChange(libvlc_event.MediaDescriptor.media_duration_changed.new_duration));
                    }

                    break;
                case libvlc_event_e.libvlc_MediaParsedChanged:
                    if (parsedChanged != null)
                    {
                        parsedChanged(m_eventProvider, new MediaParseChange(Convert.ToBoolean(libvlc_event.MediaDescriptor.media_parsed_changed.new_status)));
                    }

                    break;
                case libvlc_event_e.libvlc_MediaFreed:
                    if (mediaFreed != null)
                    {
                        mediaFreed(m_eventProvider, new MediaFree(libvlc_event.MediaDescriptor.media_freed.md));
                    }

                    break;
                case libvlc_event_e.libvlc_MediaStateChanged:
                    if (stateChanged != null)
                    {
                        stateChanged(m_eventProvider, new MediaStateChange((MediaState)libvlc_event.MediaDescriptor.media_state_changed.new_state));
                    }

                    break;

                default:
                    break;
            }
        }

        #region IMediaEvents Members

        event EventHandler<MediaMetaDataChange> metaDataChanged;
        public event EventHandler<MediaMetaDataChange> MetaDataChanged
        {
            add
            {
                if (metaDataChanged == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaMetaChanged);
                }
                metaDataChanged += value;
            }
            remove
            {
                if (metaDataChanged != null)
                {
                    metaDataChanged -= value;
                    if (metaDataChanged == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaMetaChanged);
                    }
                }
            }
        }

        event EventHandler<MediaNewSubItem> subItemAdded;
        public event EventHandler<MediaNewSubItem> SubItemAdded
        {
            add
            {
                if (subItemAdded == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaSubItemAdded);
                }
                subItemAdded += value;
            }
            remove
            {
                if (subItemAdded != null)
                {
                    subItemAdded -= value;
                    if (subItemAdded == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaSubItemAdded);
                    }
                }
            }
        }

        event EventHandler<MediaDurationChange> durationChanged;
        public event EventHandler<MediaDurationChange> DurationChanged
        {
            add
            {
                if (durationChanged == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaDurationChanged);
                }
                durationChanged += value;
            }
            remove
            {
                if (durationChanged != null)
                {
                    durationChanged -= value;
                    if (durationChanged == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaDurationChanged);
                    }
                }
            }
        }

        event EventHandler<MediaParseChange> parsedChanged;
        public event EventHandler<MediaParseChange> ParsedChanged
        {
            add
            {
                if (parsedChanged == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaParsedChanged);
                }
                parsedChanged += value;
            }
            remove
            {
                if (parsedChanged != null)
                {
                    parsedChanged -= value;
                    if (parsedChanged == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaParsedChanged);
                    }
                }
            }
        }

        event EventHandler<MediaFree> mediaFreed;
        public event EventHandler<MediaFree> MediaFreed
        {
            add
            {
                if (mediaFreed == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaFreed);
                }
                mediaFreed += value;
            }
            remove
            {
                if (mediaFreed != null)
                {
                    mediaFreed -= value;
                    if (mediaFreed == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaFreed);
                    }
                }
            }
        }

        event EventHandler<MediaStateChange> stateChanged;
        public event EventHandler<MediaStateChange> StateChanged
        {
            add
            {
                if (stateChanged == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaStateChanged);
                }
                stateChanged += value;
            }
            remove
            {
                if (stateChanged != null)
                {
                    stateChanged -= value;
                    if (stateChanged == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaStateChanged);
                    }
                }
            }
        }

        #endregion
    }
}
