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
using System.Drawing;
using Declarations.Filters;
using LibVlcWrapper;

namespace Implementation.Filters
{
   internal class CropFilter : ICropFilter
   {
      IntPtr m_hMediaPlayer;
      bool m_enabled = false;
      
      public CropFilter(IntPtr hMediaPlayer)
      {
         m_hMediaPlayer = hMediaPlayer;
      }

      #region ICropFilter Members

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
               CropGeometry = CropArea.ToCropFilterString();
            }
            else
            {
               CropGeometry = null;
            }
         }
      }

      public Rectangle CropArea { get; set; }

      #endregion

      string CropGeometry
      {
         get
         {
            return LibVlcMethods.libvlc_video_get_crop_geometry(m_hMediaPlayer);
         }
         set
         {
            LibVlcMethods.libvlc_video_set_crop_geometry(m_hMediaPlayer, value.ToUtf8());
         }
      }
   }
}
