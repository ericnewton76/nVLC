using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibVlcWrapper;
using Implementation.Exceptions;
using Implementation.Events;
using Declarations.Events;
using System.Runtime.InteropServices;
using Declarations.VLM;

namespace Implementation
{
    internal class VlmEventManager : EventManager, IVlmEventManager
    {
        public VlmEventManager(IEventProvider eventProvider)
            : base(eventProvider)
        {

        }

        protected override void MediaPlayerEventOccured(ref libvlc_event_t libvlc_event, IntPtr userData)
        {
            switch (libvlc_event.type)
            {

                case libvlc_event_e.libvlc_VlmMediaAdded:
                    if (m_MediaAdded != null)
                    {
                        m_MediaAdded(m_eventProvider, new VlmEvent(Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_instance_name), Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_media_name)));
                    }
                    break;
                case libvlc_event_e.libvlc_VlmMediaRemoved:
                    if (m_MediaRemoved != null)
                    {
                        m_MediaRemoved(m_eventProvider, new VlmEvent(Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_instance_name), Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_media_name)));
                    }
                    break;
                case libvlc_event_e.libvlc_VlmMediaChanged:
                    if (m_MediaChanged != null)
                    {
                        m_MediaChanged(m_eventProvider, new VlmEvent(Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_instance_name), Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_media_name)));
                    }
                    break;
                case libvlc_event_e.libvlc_VlmMediaInstanceStarted:
                    if (m_MediaInstanceStarted != null)
                    {
                        m_MediaInstanceStarted(m_eventProvider, new VlmEvent(Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_instance_name), Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_media_name)));
                    }
                    break;
                case libvlc_event_e.libvlc_VlmMediaInstanceStopped:
                    if (m_MediaInstanceStopped != null)
                    {
                        m_MediaInstanceStopped(m_eventProvider, new VlmEvent(Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_instance_name), Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_media_name)));
                    }
                    break;
                case libvlc_event_e.libvlc_VlmMediaInstanceStatusInit:
                    if (m_MediaInstanceInit != null)
                    {
                        m_MediaInstanceInit(m_eventProvider, new VlmEvent(Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_instance_name), Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_media_name)));
                    }
                    break;
                case libvlc_event_e.libvlc_VlmMediaInstanceStatusOpening:
                    if (m_MediaInstanceOpening != null)
                    {
                        m_MediaInstanceOpening(m_eventProvider, new VlmEvent(Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_instance_name), Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_media_name)));
                    }
                    break;
                case libvlc_event_e.libvlc_VlmMediaInstanceStatusPlaying:
                    if (m_MediaInstancePlaying != null)
                    {
                        m_MediaInstancePlaying(m_eventProvider, new VlmEvent(Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_instance_name), Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_media_name)));
                    }
                    break;
                case libvlc_event_e.libvlc_VlmMediaInstanceStatusPause:
                    if (m_MediaInstancePause != null)
                    {
                        m_MediaInstancePause(m_eventProvider, new VlmEvent(Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_instance_name), Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_media_name)));
                    }
                    break;
                case libvlc_event_e.libvlc_VlmMediaInstanceStatusEnd:
                    if (m_MediaInstanceEnd != null)
                    {
                        m_MediaInstanceEnd(m_eventProvider, new VlmEvent(Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_instance_name), Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_media_name)));
                    }
                    break;
                case libvlc_event_e.libvlc_VlmMediaInstanceStatusError:
                    if (m_MediaInstanceError != null)
                    {
                        m_MediaInstanceError(m_eventProvider, new VlmEvent(Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_instance_name), Marshal.PtrToStringAuto(libvlc_event.MediaDescriptor.vlm_media_event.psz_media_name)));
                    }
                    break;
                default:
                    break;
            }
        }


        private event EventHandler<VlmEvent> m_MediaAdded;

        public event EventHandler<VlmEvent> MediaAdded
        {
            add
            {
                if (m_MediaAdded == null)
                {
                    Attach(libvlc_event_e.libvlc_VlmMediaAdded);
                }
                m_MediaAdded += value;
            }
            remove
            {
                if (m_MediaAdded != null)
                {
                    m_MediaAdded -= value;
                    if (m_MediaAdded == null)
                    {
                        Dettach(libvlc_event_e.libvlc_VlmMediaAdded);
                    }
                }
            }
        }


        private event EventHandler<VlmEvent> m_MediaRemoved;

        public event EventHandler<VlmEvent> MediaRemoved
        {
            add
            {
                if (m_MediaRemoved == null)
                {
                    Attach(libvlc_event_e.libvlc_VlmMediaRemoved);
                }
                m_MediaRemoved += value;
            }
            remove
            {
                if (m_MediaRemoved != null)
                {
                    m_MediaRemoved -= value;
                    if (m_MediaRemoved == null)
                    {
                        Dettach(libvlc_event_e.libvlc_VlmMediaRemoved);
                    }
                }
            }
        }

        private event EventHandler<VlmEvent> m_MediaChanged;

        public event EventHandler<VlmEvent> MediaChanged
        {
            add
            {
                if (m_MediaChanged == null)
                {
                    Attach(libvlc_event_e.libvlc_VlmMediaChanged);
                }
                m_MediaChanged += value;
            }
            remove
            {
                if (m_MediaChanged != null)
                {
                    m_MediaChanged -= value;
                    if (m_MediaChanged == null)
                    {
                        Dettach(libvlc_event_e.libvlc_VlmMediaChanged);
                    }
                }
            }
        }

        private event EventHandler<VlmEvent> m_MediaInstanceStarted;

        public event EventHandler<VlmEvent> MediaInstanceStarted
        {
            add
            {
                if (m_MediaInstanceStarted == null)
                {
                    Attach(libvlc_event_e.libvlc_VlmMediaInstanceStarted);
                }
                m_MediaInstanceStarted += value;
            }
            remove
            {
                if (m_MediaInstanceStarted != null)
                {
                    m_MediaInstanceStarted -= value;
                    if (m_MediaInstanceStarted == null)
                    {
                        Dettach(libvlc_event_e.libvlc_VlmMediaInstanceStarted);
                    }
                }
            }
        }


        private event EventHandler<VlmEvent> m_MediaInstanceStopped;

        public event EventHandler<VlmEvent> MediaInstanceStopped
        {
            add
            {
                if (m_MediaInstanceStopped == null)
                {
                    Attach(libvlc_event_e.libvlc_VlmMediaInstanceStopped);
                }
                m_MediaInstanceStopped += value;
            }
            remove
            {
                if (m_MediaInstanceStopped != null)
                {
                    m_MediaInstanceStopped -= value;
                    if (m_MediaInstanceStopped == null)
                    {
                        Dettach(libvlc_event_e.libvlc_VlmMediaInstanceStopped);
                    }
                }
            }
        }


        private event EventHandler<VlmEvent> m_MediaInstanceInit;

        public event EventHandler<VlmEvent> MediaInstanceInit
        {
            add
            {
                if (m_MediaInstanceInit == null)
                {
                    Attach(libvlc_event_e.libvlc_VlmMediaInstanceStatusInit);
                }
                m_MediaInstanceInit += value;
            }
            remove
            {
                if (m_MediaInstanceInit != null)
                {
                    m_MediaInstanceInit -= value;
                    if (m_MediaInstanceInit == null)
                    {
                        Dettach(libvlc_event_e.libvlc_VlmMediaInstanceStatusInit);
                    }
                }
            }
        }


        private event EventHandler<VlmEvent> m_MediaInstanceOpening;

        public event EventHandler<VlmEvent> MediaInstanceOpening
        {
            add
            {
                if (m_MediaInstanceOpening == null)
                {
                    Attach(libvlc_event_e.libvlc_VlmMediaInstanceStatusOpening);
                }
                m_MediaInstanceOpening += value;
            }
            remove
            {
                if (m_MediaInstanceOpening != null)
                {
                    m_MediaInstanceOpening -= value;
                    if (m_MediaInstanceOpening == null)
                    {
                        Dettach(libvlc_event_e.libvlc_VlmMediaInstanceStatusOpening);
                    }
                }
            }
        }


        private event EventHandler<VlmEvent> m_MediaInstancePlaying;

        public event EventHandler<VlmEvent> MediaInstancePlaying
        {
            add
            {
                if (m_MediaInstancePlaying == null)
                {
                    Attach(libvlc_event_e.libvlc_VlmMediaInstanceStatusPlaying);
                }
                m_MediaInstancePlaying += value;
            }
            remove
            {
                if (m_MediaInstancePlaying != null)
                {
                    m_MediaInstancePlaying -= value;
                    if (m_MediaInstancePlaying == null)
                    {
                        Dettach(libvlc_event_e.libvlc_VlmMediaInstanceStatusPlaying);
                    }
                }
            }
        }


        private event EventHandler<VlmEvent> m_MediaInstancePause;

        public event EventHandler<VlmEvent> MediaInstancePause
        {
            add
            {
                if (m_MediaInstancePause == null)
                {
                    Attach(libvlc_event_e.libvlc_VlmMediaInstanceStatusPause);
                }
                m_MediaInstancePause += value;
            }
            remove
            {
                if (m_MediaInstancePause != null)
                {
                    m_MediaInstancePause -= value;
                    if (m_MediaInstancePause == null)
                    {
                        Dettach(libvlc_event_e.libvlc_VlmMediaInstanceStatusPause);
                    }
                }
            }
        }

        private event EventHandler<VlmEvent> m_MediaInstanceEnd;

        public event EventHandler<VlmEvent> MediaInstanceEnd
        {
            add
            {
                if (m_MediaInstanceEnd == null)
                {
                    Attach(libvlc_event_e.libvlc_VlmMediaInstanceStatusEnd);
                }
                m_MediaInstanceEnd += value;
            }
            remove
            {
                if (m_MediaInstanceEnd != null)
                {
                    m_MediaInstanceEnd -= value;
                    if (m_MediaInstanceEnd == null)
                    {
                        Dettach(libvlc_event_e.libvlc_VlmMediaInstanceStatusEnd);
                    }
                }
            }
        }

        private event EventHandler<VlmEvent> m_MediaInstanceError;

        public event EventHandler<VlmEvent> MediaInstanceError
        {
            add
            {
                if (m_MediaInstanceError == null)
                {
                    Attach(libvlc_event_e.libvlc_VlmMediaInstanceStatusError);
                }
                m_MediaInstanceError += value;
            }
            remove
            {
                if (m_MediaInstanceError != null)
                {
                    m_MediaInstanceError -= value;
                    if (m_MediaInstanceError == null)
                    {
                        Dettach(libvlc_event_e.libvlc_VlmMediaInstanceStatusError);
                    }
                }
            }
        }
    }
}
