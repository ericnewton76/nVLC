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
using System.Text;
using Declarations;
using Declarations.Events;
using Declarations.Media;
using Implementation.Events;
using LibVlcWrapper;

namespace Implementation.Media
{
    internal class BasicMedia : DisposableBase, IMedia, INativePointer, IReferenceCount, IEventProvider
    {
        protected readonly IntPtr m_hMediaLib;
        protected IntPtr m_hMedia;
        private string m_path;
        IntPtr m_hEventManager = IntPtr.Zero;
        IMediaEvents m_events;

        public BasicMedia(IntPtr hMediaLib)
        {
            m_hMediaLib = hMediaLib;
        }

        public BasicMedia(IntPtr hMedia, ReferenceCountAction refCountAction)
        {
            m_hMedia = hMedia;
            m_path = LibVlcMethods.libvlc_media_get_mrl(m_hMedia);
            switch (refCountAction)
            {
                case ReferenceCountAction.AddRef:
                    AddRef();
                    break;

                case ReferenceCountAction.Release:
                    Release();
                    break;
            }
        }

        protected override void Dispose(bool disposing)
        {
            Release();
        }

        #region IMedia Members

        public string Input
        {
            get
            {
                return m_path;
            }
            set
            {
                m_path = value;
                byte[] bytes = Encoding.UTF8.GetBytes(m_path);
                m_hMedia = LibVlcMethods.libvlc_media_new_path(m_hMediaLib, bytes);
            }
        }

        public MediaState State
        {
            get
            {
                return (MediaState)LibVlcMethods.libvlc_media_get_state(m_hMedia);
            }
        }

        public void AddOptions(IEnumerable<string> options)
        {
            foreach (var item in options)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    LibVlcMethods.libvlc_media_add_option(m_hMedia, item.ToUtf8());
                }
            }
        }

        public void AddOptionFlag(string option, int flag)
        {
            LibVlcMethods.libvlc_media_add_option_flag(m_hMedia, option.ToUtf8(), flag);
        }

        public IMedia Duplicate()
        {
            IntPtr clone = LibVlcMethods.libvlc_media_duplicate(m_hMedia);
            return new BasicMedia(clone, ReferenceCountAction.None);
        }

        public void Parse(bool aSync)
        {
            if (aSync)
            {
                LibVlcMethods.libvlc_media_parse_async(m_hMedia);
            }
            else
            {
                LibVlcMethods.libvlc_media_parse(m_hMedia);
            }
        }

        public bool IsParsed
        {
            get
            {
                return LibVlcMethods.libvlc_media_is_parsed(m_hMedia);
            }
        }

        public IntPtr Tag
        {
            get
            {
                return LibVlcMethods.libvlc_media_get_user_data(m_hMedia);
            }
            set
            {
                LibVlcMethods.libvlc_media_set_user_data(m_hMedia, value);
            }
        }

        public IMediaEvents Events
        {
            get
            {
                if (m_events == null)
                {
                    m_events = new MediaEventManager(this);
                }

                return m_events;
            }
        }

        public MediaStatistics Statistics
        {
            get
            {
                libvlc_media_stats_t t;

                int num = LibVlcMethods.libvlc_media_get_stats(m_hMedia, out t);

                return t.ToMediaStatistics();
            }
        }

        public IMediaList SubItems
        {
            get
            {
                IntPtr hMediaList = LibVlcMethods.libvlc_media_subitems(m_hMedia);
                if (hMediaList == IntPtr.Zero)
                {
                    return null;
                }

                return new MediaList(hMediaList, ReferenceCountAction.None);
            }
        }

        #endregion

        #region INativePointer Members

        public IntPtr Pointer
        {
            get
            {
                return m_hMedia;
            }
        }

        #endregion

        #region IReferenceCount Members

        public void AddRef()
        {
            LibVlcMethods.libvlc_media_retain(m_hMedia);
        }

        public void Release()
        {
            try
            {
                LibVlcMethods.libvlc_media_release(m_hMedia);
            }
            catch (Exception)
            { }
        }

        #endregion

        #region IEventProvider Members

        public IntPtr EventManagerHandle
        {
            get
            {
                if (m_hEventManager == IntPtr.Zero)
                {
                    m_hEventManager = LibVlcMethods.libvlc_media_event_manager(m_hMedia);
                }

                return m_hEventManager;
            }
        }

        #endregion

        #region IEqualityComparer<IMedia> Members

        public bool Equals(IMedia x, IMedia y)
        {
            INativePointer x1 = (INativePointer)x;
            INativePointer y1 = (INativePointer)y;

            return x1.Pointer == y1.Pointer;
        }

        public int GetHashCode(IMedia obj)
        {
            return ((INativePointer)obj).Pointer.GetHashCode();
        }

        #endregion

        public override bool Equals(object obj)
        {
            return this.Equals((IMedia)obj, this);
        }

        public override int GetHashCode()
        {
            return m_hMedia.GetHashCode();
        }
    }
}
