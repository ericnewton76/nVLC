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
using Declarations.Filters;
using LibVlcWrapper;

namespace Implementation.Filters
{
   internal class MarqueeFilter : IMarqueeFilter
   {
      IntPtr m_hMediaPlayer;

      public MarqueeFilter(IntPtr hMediaPlayer)
      {
         m_hMediaPlayer = hMediaPlayer;
      }

      #region IMarqueeFilter Members

      public bool Enabled
      {
         get
         {
            return GetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Enable) == 1;
         }
         set
         {
            SetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Enable, Convert.ToInt32(value));
         }
      }

      public string Text
      {
         get
         {
            return GetMarqueeString(libvlc_video_marquee_option_t.libvlc_marquee_Text);
         }
         set
         {
            SetMarqueeString(libvlc_video_marquee_option_t.libvlc_marquee_Text,value);
         }
      }

      public VlcColor Color
      {
         get
         {
            return (VlcColor)GetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Color);
         }
         set
         {
            SetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Color, (int)value);
         }
      }

      public Position Position
      {
         get
         {
            return (Position)GetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Position);
         }
         set
         {
            SetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Position, (int)value);
         }
      }

      public int Refresh
      {
         get
         {
            return GetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Refresh);
         }
         set
         {
            SetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Refresh, value);
         }
      }

      public int Size
      {
         get
         {
            return GetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Size);
         }
         set
         {
            SetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Size, value);
         }
      }

      public int Timeout
      {
         get
         {
            return GetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Timeout);
         }
         set
         {
            SetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Timeout, value);
         }
      }

      public int X
      {
         get
         {
            return GetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_X);
         }
         set
         {
            SetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_X, value);
         }
      }

      public int Y
      {
         get
         {
            return GetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Y);
         }
         set
         {
            SetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Y, value);
         }
      }

      public int Opacity
      {
         get
         {
            return GetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Opacity);
         }
         set
         {
            SetMarquee(libvlc_video_marquee_option_t.libvlc_marquee_Opacity, value);
         }
      }

      #endregion

      int GetMarquee(libvlc_video_marquee_option_t option)
      {
         return LibVlcMethods.libvlc_video_get_marquee_int(m_hMediaPlayer, option);
      }

      void SetMarquee(libvlc_video_marquee_option_t option, int argument)
      {
         LibVlcMethods.libvlc_video_set_marquee_int(m_hMediaPlayer, option, argument);
      }

      string GetMarqueeString(libvlc_video_marquee_option_t option)
      {
         return LibVlcMethods.libvlc_video_get_marquee_string(m_hMediaPlayer, option);
      }

      void SetMarqueeString(libvlc_video_marquee_option_t option, string argument)
      {
         LibVlcMethods.libvlc_video_set_marquee_string(m_hMediaPlayer, option, argument.ToUtf8());
      }    
   }
}
