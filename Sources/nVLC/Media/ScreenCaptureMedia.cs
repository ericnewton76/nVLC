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
using System.Windows.Forms;
using Declarations.Media;

namespace Implementation.Media
{
   internal class ScreenCaptureMedia : BasicMedia, IScreenCaptureMedia
   {
      private Rectangle m_captureArea;
      private int m_fps;
      private bool m_followMouse = false;
      private string m_cursorFile;

      public ScreenCaptureMedia(IntPtr hMediaLib)
         : base(hMediaLib)
      {
         m_captureArea = Screen.PrimaryScreen.Bounds;
         m_fps = 1;
      }

      #region IScreenCaptureMedia Members

      public Rectangle CaptureArea
      {
         get
         {
            return m_captureArea;
         }
         set
         {
            m_captureArea = value;
            UpdateCaptureArea();
         }
      }

      private void UpdateCaptureArea()
      {
         List<string> options = new List<string>()
         {
            string.Format(":screen-top={0}", m_captureArea.Top),
            string.Format(":screen-left={0}", m_captureArea.Left),
            string.Format(":screen-width={0}", m_captureArea.Width),
            string.Format(":screen-height={0}", m_captureArea.Height)
         };

         AddOptions(options);
      }

      public int FPS
      {
         get
         {
            return m_fps;
         }
         set
         {
            m_fps = value;
            UpdateFrameRate();
         }
      }

      private void UpdateFrameRate()
      {
         List<string> options = new List<string>()
         {
            string.Format(":screen-fps={0}", m_fps)
         };

         AddOptions(options);
      }

      private void UpdateFollowMouse()
      {
         List<string> options = new List<string>()
         {
            string.Format(":screen-follow-mouse={0}", m_followMouse.ToString())
         };

         AddOptions(options);
      }

      public bool FollowMouse
      {
         get
         {
            return m_followMouse;
         }
         set
         {
            m_followMouse = value;
            UpdateFollowMouse();
         }
      }

      public string CursorFile
      {
         get
         {
            return m_cursorFile;
         }
         set
         {
            m_cursorFile = value;
            UpdateCursorImage();
         }
      }

      private void UpdateCursorImage()
      {
         List<string> options = new List<string>()
         {
            string.Format(":screen-mouse-image={0}", m_cursorFile)
         };

         AddOptions(options);      
      }

      #endregion
   }
}
