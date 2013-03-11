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
using Declarations;
using LibVlcWrapper;
using Declarations.Players;
using Declarations.Enums;

namespace Implementation.Players
{
    internal class DiskPlayer : VideoPlayer, IDiskPlayer
    {
        public DiskPlayer(IntPtr hMediaLib)
            : base(hMediaLib)
        {

        }

        public int AudioTrack
        {
            get
            {
                return LibVlcMethods.libvlc_audio_get_track(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_audio_set_track(m_hMediaPlayer, value);
            }
        }

        public int AudioTrackCount
        {
            get
            {
                return LibVlcMethods.libvlc_audio_get_track_count(m_hMediaPlayer);
            }
        }

        public int SubTitle
        {
            get
            {
                return LibVlcMethods.libvlc_video_get_spu(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_video_set_spu(m_hMediaPlayer, value);
            }
        }

        public int SubTitleCount
        {
            get
            {
                return LibVlcMethods.libvlc_video_get_spu_count(m_hMediaPlayer);
            }
        }

        public void NextChapter()
        {
            LibVlcMethods.libvlc_media_player_next_chapter(m_hMediaPlayer);
        }

        public void PreviousChapter()
        {
            LibVlcMethods.libvlc_media_player_previous_chapter(m_hMediaPlayer);
        }

        public int Title
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_get_title(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_media_player_set_title(m_hMediaPlayer, value);
            }
        }

        public int TitleCount
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_get_title_count(m_hMediaPlayer);
            }
        }

        public int GetChapterCountForTitle(int title)
        {
            return LibVlcMethods.libvlc_media_player_get_chapter_count_for_title(m_hMediaPlayer, Title);
        }

        public int ChapterCount
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_get_chapter_count(m_hMediaPlayer);
            }
        }

        public int Chapter
        {
            get
            {
                return LibVlcMethods.libvlc_media_player_get_chapter(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_media_player_set_chapter(m_hMediaPlayer, value);
            }
        }

        public int VideoTrackCount
        {
            get
            {
                return LibVlcMethods.libvlc_video_get_track_count(m_hMediaPlayer);
            }
        }

        public int VideoTrack
        {
            get
            {
                return LibVlcMethods.libvlc_video_get_track(m_hMediaPlayer);
            }
            set
            {
                LibVlcMethods.libvlc_video_set_track(m_hMediaPlayer, value);
            }
        }

        public void Navigate(NavigationMode mode)
        {
            LibVlcMethods.libvlc_media_player_navigate(m_hMediaPlayer, (libvlc_navigate_mode_t)mode);
        }
    }
}
