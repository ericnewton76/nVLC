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
   internal class DeinterlaceFilter : IDeinterlaceFilter
   {
      IntPtr m_hMediaPlayer;
      bool m_enabled = false;
      private DeinterlaceMode m_mode;

      public DeinterlaceFilter(IntPtr hMediaPlayer)
      {
         m_hMediaPlayer = hMediaPlayer;
      }

      #region IDeinterlaceFilter Members

      public bool Enabled
      {
          get
          {
              return m_enabled;
          }
          set
          {
              m_enabled = value;
              if (m_enabled)
              {
                  LibVlcMethods.libvlc_video_set_deinterlace(m_hMediaPlayer, Mode.ToString().ToUtf8());
              }
              else
              {
                  LibVlcMethods.libvlc_video_set_deinterlace(m_hMediaPlayer, null);
              }
          }
      }

      public DeinterlaceMode Mode 
      {
          get
          {
              return m_mode;
          }
          set
          {
              m_mode = value;
              LibVlcMethods.libvlc_video_set_deinterlace(m_hMediaPlayer, m_mode.ToString().ToUtf8());
          }
      }

      #endregion
   }
}
