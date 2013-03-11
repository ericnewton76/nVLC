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
   internal class LogoFilter : ILogoFilter
   {
      IntPtr m_pMediaPlayer;
      private string m_file;
      
      public LogoFilter(IntPtr hMediaPlayer)
      {
         m_pMediaPlayer = hMediaPlayer;
      }

      #region ILogoFilter Members

      public bool Enabled
      {
         get
         {
            return LibVlcMethods.libvlc_video_get_logo_int(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_enable) == 1;
         }
         set
         {
            LibVlcMethods.libvlc_video_set_logo_int(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_enable, Convert.ToInt32(value));
         }
      }

      public string File
      {
         get
         {
            return m_file;
         }
         set
         {
            LibVlcMethods.libvlc_video_set_logo_string(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_file, value.ToUtf8());
            m_file = value;
         }
      }

      public int X
      {
         get
         {
            return LibVlcMethods.libvlc_video_get_logo_int(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_x);
         }
         set
         {
            LibVlcMethods.libvlc_video_set_logo_int(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_x, value);
         }
      }

      public int Y
      {
         get
         {
            return LibVlcMethods.libvlc_video_get_logo_int(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_y);
         }
         set
         {
            LibVlcMethods.libvlc_video_set_logo_int(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_y, value);
         }
      }

      public int Delay
      {
         get
         {
            return LibVlcMethods.libvlc_video_get_logo_int(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_delay);
         }
         set
         {
            LibVlcMethods.libvlc_video_set_logo_int(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_delay, value);
         }
      }

      public int Repeat
      {
         get
         {
            return LibVlcMethods.libvlc_video_get_logo_int(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_repeat);
         }
         set
         {
            LibVlcMethods.libvlc_video_set_logo_int(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_repeat, value);
         }
      }

      public int Opacity
      {
         get
         {
            return LibVlcMethods.libvlc_video_get_logo_int(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_opacity);
         }
         set
         {
            LibVlcMethods.libvlc_video_set_logo_int(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_opacity, value);
         }
      }

      public Position Position
      {
         get
         {
            return (Position)LibVlcMethods.libvlc_video_get_logo_int(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_position);
         }
         set
         {
            LibVlcMethods.libvlc_video_set_logo_int(m_pMediaPlayer, libvlc_video_logo_option_t.libvlc_logo_position, (int)value);
         }
      }

      #endregion
   }
}
