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
using System.Drawing;
using Declarations;
using Declarations.Filters;
using Declarations.Players;
using Implementation.Filters;
using Implementation.Utils;
using LibVlcWrapper;

namespace Implementation.Players
{
    internal class VideoPlayer : AudioPlayer, IVideoPlayer
    {
        MemoryRenderer m_memRender = null;
        MemoryRendererEx m_memRenderEx = null;
        IAdjustFilter m_adjust = null;
        ILogoFilter m_logo = null;
        IMarqueeFilter m_marquee = null;
        ICropFilter m_crop = null;
        IDeinterlaceFilter m_deinterlace = null;

        bool m_keyInputEnabled = true;
        bool m_mouseInputEnabled = true;
        Dictionary<string, Enum> m_aspectMapper;

        public VideoPlayer(IntPtr hMediaLib)
            : base(hMediaLib)
        {
            m_aspectMapper = EnumUtils.GetEnumMapping(typeof(AspectRatioMode));
        }

        public override void Play()
        {
            base.Play();
            if (m_memRender != null)
            {
                m_memRender.StartTimer();
            }

            if (m_memRenderEx != null)
            {
                m_memRenderEx.StartTimer();
            }
        }

        #region IVideoPlayer Members

        public IntPtr WindowHandle
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_get_hwnd(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_media_player_set_hwnd(m_hMediaPlayer, value);
            }
        }

        public void TakeSnapShot(uint stream, string path)
        {
            LibVlcMethods.libvlc_video_take_snapshot(m_hMediaPlayer, stream, path.ToUtf8(), 0, 0);
        }

        public float PlaybackRate
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_get_rate(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_media_player_set_rate(m_hMediaPlayer, value);
            }
        }

        public float FPS
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_get_fps(m_hMediaPlayer);
            }
        }

        public void NextFrame()
        {
            LibVlcMethods.libvlc_media_player_next_frame(m_hMediaPlayer);
        }

        public Size GetVideoSize(uint stream)
        {
            uint width = 0, height = 0;
            LibVlcMethods.libvlc_video_get_size(m_hMediaPlayer, stream, out width, out height);
            return new Size((int)width, (int)height);
        }

        public Point GetCursorPosition(uint stream)
        {
            int px = 0, py = 0;
            LibVlcMethods.libvlc_video_get_cursor(m_hMediaPlayer, stream, out px, out py);
            return new Point(px, py);
        }

        public bool KeyInputEnabled
        {
            get
            {
                return m_keyInputEnabled;
            }
            set
            {
                LibVlcMethods.libvlc_video_set_key_input(m_hMediaPlayer, value);
                m_keyInputEnabled = value;
            }
        }

        public bool MouseInputEnabled
        {
            get
            {
                return m_mouseInputEnabled;
            }
            set
            {
                LibVlcMethods.libvlc_video_set_mouse_input(m_hMediaPlayer, value);
                m_mouseInputEnabled = value;
            }
        }

        public float VideoScale
        {
            get
            {
                return LibVlcMethods.libvlc_video_get_scale(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_video_set_scale(m_hMediaPlayer, value);
            }
        }

        public AspectRatioMode AspectRatio
        {
            get
            {
                string str = LibVlcMethods.libvlc_video_get_aspect_ratio(m_hMediaPlayer);
                return (AspectRatioMode)m_aspectMapper[str];
            }
            set
            {
                string val = EnumUtils.GetEnumDescription(value);
                LibVlcMethods.libvlc_video_set_aspect_ratio(m_hMediaPlayer, val.ToUtf8());
            }
        }

        public void SetSubtitleFile(string path)
        {
            LibVlcMethods.libvlc_video_set_subtitle_file(m_hMediaPlayer, path.ToUtf8());
        }

        public int Teletext
        {
            get
            {
                return LibVlcMethods.libvlc_video_get_teletext(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_video_set_teletext(m_hMediaPlayer, value);
            }
        }

        public void ToggleTeletext()
        {
            LibVlcMethods.libvlc_toggle_teletext(m_hMediaPlayer);
        }

        public bool PlayerWillPlay
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_will_play(m_hMediaPlayer);
            }
        }

        public int VoutCount
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_has_vout(m_hMediaPlayer);
            }
        }

        public bool IsSeekable
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_is_seekable(m_hMediaPlayer);
            }
        }

        public bool CanPause
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_can_pause(m_hMediaPlayer);
            }
        }

        public ICropFilter CropGeometry
        {
            get
            {
                if (m_crop == null)
                {
                    m_crop = new CropFilter(m_hMediaPlayer);
                }

                return m_crop;
            }
        }

        public IAdjustFilter Adjust
        {
            get
            {
                if (m_adjust == null)
                {
                    m_adjust = new AdjustFilter(m_hMediaPlayer);
                }

                return m_adjust;
            }
        }

        public IMemoryRenderer CustomRenderer
        {
            get
            {
                if (m_memRenderEx != null)
                {
                    throw new InvalidOperationException("CustomRenderer is mutually exclusive with CustomRendererEx");
                }

                if (m_memRender == null)
                {
                    m_memRender = new MemoryRenderer(m_hMediaPlayer);
                }
                return m_memRender;
            }
        }

        public IMemoryRendererEx CustomRendererEx
        {
            get
            {
                if (m_memRender != null)
                {
                    throw new InvalidOperationException("CustomRendererEx is mutually exclusive with CustomRenderer");
                }

                if (m_memRenderEx == null)
                {
                    m_memRenderEx = new MemoryRendererEx(m_hMediaPlayer);
                }
                return m_memRenderEx;
            }
        }

        public ILogoFilter Logo
        {
            get
            {
                if (m_logo == null)
                {
                    m_logo = new LogoFilter(m_hMediaPlayer);
                }
                return m_logo;
            }
        }

        public IMarqueeFilter Marquee
        {
            get
            {
                if (m_marquee == null)
                {
                    m_marquee = new MarqueeFilter(m_hMediaPlayer);
                }
                return m_marquee;
            }
        }

        public IDeinterlaceFilter Deinterlace
        {
            get
            {
                if (m_deinterlace == null)
                {
                    m_deinterlace = new DeinterlaceFilter(m_hMediaPlayer);
                }
                return m_deinterlace;
            }
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (m_memRender != null)
            {
                m_memRender.Dispose();
            }

            if (m_memRenderEx != null)
            {
                m_memRenderEx.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
