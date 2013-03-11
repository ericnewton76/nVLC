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
using System.Runtime.InteropServices;
using Declarations.Events;
using Implementation.Media;
using LibVlcWrapper;

namespace Implementation.Events
{
    internal class EventBroker : EventManager, IEventBroker
    {
        public EventBroker(IEventProvider eventProvider)
            : base(eventProvider)
        {

        }

        protected override void MediaPlayerEventOccured(ref libvlc_event_t libvlc_event, IntPtr userData)
        {
            switch (libvlc_event.type)
            {
                case libvlc_event_e.libvlc_MediaPlayerTimeChanged:
                    RaiseTimeChanged(libvlc_event.MediaDescriptor.media_player_time_changed.new_time);
                    break;

                case libvlc_event_e.libvlc_MediaPlayerEndReached:
                    RaiseMediaEnded();
                    break;

                case libvlc_event_e.libvlc_MediaPlayerMediaChanged:
                    if (m_mediaChanged != null)
                    {
                        BasicMedia media = new BasicMedia(libvlc_event.MediaDescriptor.media_player_media_changed.new_media, ReferenceCountAction.AddRef);
                        m_mediaChanged(m_eventProvider, new MediaPlayerMediaChanged(media));
                        //media.Release();
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerNothingSpecial:
                    if (m_nothingSpecial != null)
                    {
                        m_nothingSpecial(m_eventProvider, EventArgs.Empty);
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerOpening:
                    if (m_playerOpening != null)
                    {
                        m_playerOpening(m_eventProvider, EventArgs.Empty);
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerBuffering:
                    if (m_playerBuffering != null)
                    {
                        m_playerBuffering(m_eventProvider, EventArgs.Empty);
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerPlaying:
                    if (m_playerPlaying != null)
                    {
                        m_playerPlaying(m_eventProvider, EventArgs.Empty);
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerPaused:
                    if (m_playerPaused != null)
                    {
                        m_playerPaused(m_eventProvider, EventArgs.Empty);
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerStopped:
                    if (m_playerStopped != null)
                    {
                        m_playerStopped(m_eventProvider, EventArgs.Empty);
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerForward:
                    if (m_playerForward != null)
                    {
                        m_playerForward(m_eventProvider, EventArgs.Empty);
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerBackward:
                    if (m_playerPaused != null)
                    {
                        m_playerPaused(m_eventProvider, EventArgs.Empty);
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerEncounteredError:
                    if (m_playerEncounteredError != null)
                    {
                        m_playerEncounteredError(m_eventProvider, EventArgs.Empty);
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerPositionChanged:
                    if (m_playerPositionChanged != null)
                    {
                        m_playerPositionChanged(m_eventProvider, new MediaPlayerPositionChanged(libvlc_event.MediaDescriptor.media_player_position_changed.new_position));
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerSeekableChanged:
                    if (m_playerSeekableChanged != null)
                    {
                        m_playerSeekableChanged(m_eventProvider, new MediaPlayerSeekableChanged(libvlc_event.MediaDescriptor.media_player_seekable_changed.new_seekable));
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerPausableChanged:
                    if (m_playerPausableChanged != null)
                    {
                        m_playerPausableChanged(m_eventProvider, new MediaPlayerPausableChanged(libvlc_event.MediaDescriptor.media_player_pausable_changed.new_pausable));
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerTitleChanged:
                    if (m_playerTitleChanged != null)
                    {
                        m_playerTitleChanged(m_eventProvider, new MediaPlayerTitleChanged(libvlc_event.MediaDescriptor.media_player_title_changed.new_title));
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerSnapshotTaken:
                    if (m_playerSnapshotTaken != null)
                    {
                        m_playerSnapshotTaken(m_eventProvider, new MediaPlayerSnapshotTaken(Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.media_player_snapshot_taken.psz_filename)));
                    }
                    break;

                case libvlc_event_e.libvlc_MediaPlayerLengthChanged:
                    if (m_playerLengthChanged != null)
                    {
                        m_playerLengthChanged(m_eventProvider, new MediaPlayerLengthChanged(libvlc_event.MediaDescriptor.media_player_length_changed.new_length));
                    }
                    break;
            }
        }

        private void RaiseTimeChanged(long p)
        {
            if (m_timeChanged != null)
            {
                m_timeChanged(m_eventProvider, new MediaPlayerTimeChanged(p));
            }
        }

        internal void RaiseMediaEnded()
        {
            if (m_mediaEnded != null)
            {
                m_mediaEnded.BeginInvoke(m_eventProvider, EventArgs.Empty, null, null);
            }
        }

        private event EventHandler<MediaPlayerTimeChanged> m_timeChanged;
        public event EventHandler<MediaPlayerTimeChanged> TimeChanged
        {
            add
            {
                if (m_timeChanged == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerTimeChanged);
                }
                m_timeChanged += value;
            }
            remove
            {
                if (m_timeChanged != null)
                {
                    m_timeChanged -= value;
                    if (m_timeChanged == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerTimeChanged);
                    }
                }
            }
        }

        private event EventHandler m_mediaEnded;
        public event EventHandler MediaEnded
        {
            add
            {
                if (m_mediaEnded == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerEndReached);
                }
                m_mediaEnded += value;
            }
            remove
            {
                if (m_mediaEnded != null)
                {
                    m_mediaEnded -= value;
                    if (m_mediaEnded == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerEndReached);
                    }
                }
            }
        }

        #region IEventBroker Members

        event EventHandler<MediaPlayerMediaChanged> m_mediaChanged;
        public event EventHandler<MediaPlayerMediaChanged> MediaChanged
        {
            add
            {
                if (m_mediaChanged == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerMediaChanged);
                }
                m_mediaChanged += value;
            }
            remove
            {
                if (m_mediaChanged != null)
                {
                    m_mediaChanged -= value;
                    if (m_mediaChanged == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerMediaChanged);
                    }
                }
            }
        }

        event EventHandler m_nothingSpecial;
        public event EventHandler NothingSpecial
        {
            add
            {
                if (m_nothingSpecial == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerNothingSpecial);
                }
                m_nothingSpecial += value;
            }
            remove
            {
                if (m_nothingSpecial != null)
                {
                    m_nothingSpecial -= value;
                    if (m_nothingSpecial == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerNothingSpecial);
                    }
                }
            }
        }

        event EventHandler m_playerOpening;
        public event EventHandler PlayerOpening
        {
            add
            {
                if (m_playerOpening == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerOpening);
                }
                m_playerOpening += value;
            }
            remove
            {
                if (m_playerOpening != null)
                {
                    m_playerOpening -= value;
                    if (m_playerOpening == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerOpening);
                    }
                }
            }
        }

        event EventHandler m_playerBuffering;
        public event EventHandler PlayerBuffering
        {
            add
            {
                if (m_playerBuffering == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerBuffering);
                }
                m_playerBuffering += value;
            }
            remove
            {
                if (m_playerBuffering != null)
                {
                    m_playerBuffering -= value;
                    if (m_playerBuffering == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerBuffering);
                    }
                }
            }
        }

        event EventHandler m_playerPlaying;
        public event EventHandler PlayerPlaying
        {
            add
            {
                if (m_playerPlaying == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerPlaying);
                }
                m_playerPlaying += value;
            }
            remove
            {
                if (m_playerPlaying != null)
                {
                    m_playerPlaying -= value;
                    if (m_playerPlaying == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerPlaying);
                    }
                }
            }
        }

        event EventHandler m_playerPaused;
        public event EventHandler PlayerPaused
        {
            add
            {
                if (m_playerPaused == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerPaused);
                }
                m_playerPaused += value;
            }
            remove
            {
                if (m_playerPaused != null)
                {
                    m_playerPaused -= value;
                    if (m_playerPaused == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerPaused);
                    }
                }
            }
        }

        event EventHandler m_playerStopped;
        public event EventHandler PlayerStopped
        {
            add
            {
                if (m_playerStopped == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerStopped);
                }
                m_playerStopped += value;
            }
            remove
            {
                if (m_playerStopped != null)
                {
                    m_playerStopped -= value;
                    if (m_playerStopped == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerStopped);
                    }
                }
            }
        }

        event EventHandler m_playerForward;
        public event EventHandler PlayerForward
        {
            add
            {
                if (m_playerForward == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerForward);
                }
                m_playerForward += value;
            }
            remove
            {
                if (m_playerForward != null)
                {
                    m_playerForward -= value;
                    if (m_playerForward == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerForward);
                    }
                }
            }
        }

        event EventHandler m_playerBackward;
        public event EventHandler PlayerBackward
        {
            add
            {
                if (m_playerBackward == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerBackward);
                }
                m_playerBackward += value;
            }
            remove
            {
                if (m_playerBackward != null)
                {
                    m_playerBackward -= value;
                    if (m_playerBackward == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerBackward);
                    }
                }
            }
        }

        event EventHandler m_playerEncounteredError;
        public event EventHandler PlayerEncounteredError
        {
            add
            {
                if (m_playerEncounteredError == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerEncounteredError);
                }
                m_playerEncounteredError += value;
            }
            remove
            {
                if (m_playerEncounteredError != null)
                {
                    m_playerEncounteredError -= value;
                    if (m_playerEncounteredError == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerEncounteredError);
                    }
                }
            }
        }

        event EventHandler<MediaPlayerPositionChanged> m_playerPositionChanged;
        public event EventHandler<MediaPlayerPositionChanged> PlayerPositionChanged
        {
            add
            {
                if (m_playerPositionChanged == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerPositionChanged);
                }
                m_playerPositionChanged += value;
            }
            remove
            {
                if (m_playerPositionChanged != null)
                {
                    m_playerPositionChanged -= value;
                    if (m_playerPositionChanged == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerPositionChanged);
                    }
                }
            }
        }

        event EventHandler<MediaPlayerSeekableChanged> m_playerSeekableChanged;
        public event EventHandler<MediaPlayerSeekableChanged> PlayerSeekableChanged
        {
            add
            {
                if (m_playerSeekableChanged == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerSeekableChanged);
                }
                m_playerSeekableChanged += value;
            }
            remove
            {
                if (m_playerSeekableChanged != null)
                {
                    m_playerSeekableChanged -= value;
                    if (m_playerSeekableChanged == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerSeekableChanged);
                    }
                }
            }
        }

        event EventHandler<MediaPlayerPausableChanged> m_playerPausableChanged;
        public event EventHandler<MediaPlayerPausableChanged> PlayerPausableChanged
        {
            add
            {
                if (m_playerPausableChanged == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerPausableChanged);
                }
                m_playerPausableChanged += value;
            }
            remove
            {
                if (m_playerPausableChanged != null)
                {
                    m_playerPausableChanged -= value;
                    if (m_playerPausableChanged == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerPausableChanged);
                    }
                }
            }
        }

        event EventHandler<MediaPlayerTitleChanged> m_playerTitleChanged;
        public event EventHandler<MediaPlayerTitleChanged> PlayerTitleChanged
        {
            add
            {
                if (m_playerTitleChanged == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerTitleChanged);
                }
                m_playerTitleChanged += value;
            }
            remove
            {
                if (m_playerTitleChanged != null)
                {
                    m_playerTitleChanged -= value;
                    if (m_playerTitleChanged == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerTitleChanged);
                    }
                }
            }
        }

        event EventHandler<MediaPlayerSnapshotTaken> m_playerSnapshotTaken;
        public event EventHandler<MediaPlayerSnapshotTaken> PlayerSnapshotTaken
        {
            add
            {
                if (m_playerSnapshotTaken == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerSnapshotTaken);
                }
                m_playerSnapshotTaken += value;
            }
            remove
            {
                if (m_playerSnapshotTaken != null)
                {
                    m_playerSnapshotTaken -= value;
                    if (m_playerSnapshotTaken == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerSnapshotTaken);
                    }
                }
            }
        }

        event EventHandler<MediaPlayerLengthChanged> m_playerLengthChanged;
        public event EventHandler<MediaPlayerLengthChanged> PlayerLengthChanged
        {
            add
            {
                if (m_playerLengthChanged == null)
                {
                    Attach(libvlc_event_e.libvlc_MediaPlayerLengthChanged);
                }
                m_playerLengthChanged += value;
            }
            remove
            {
                if (m_playerLengthChanged != null)
                {
                    m_playerLengthChanged -= value;
                    if (m_playerLengthChanged == null)
                    {
                        Dettach(libvlc_event_e.libvlc_MediaPlayerLengthChanged);
                    }
                }
            }
        }

        #endregion
    }
}
