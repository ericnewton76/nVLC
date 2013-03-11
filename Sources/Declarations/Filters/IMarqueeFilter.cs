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

namespace Declarations.Filters
{
   /// <summary>
   /// Represents text overlay filter functionality.
   /// </summary>
   public interface IMarqueeFilter
   {
      /// <summary>
      /// Enables or disables text overlay.
      /// </summary>
      bool Enabled { get; set; }

      /// <summary>
      /// Marquee text to display.
      /// (Available format strings:
      /// Time related: %Y = year, %m = month, %d = day, %H = hour,
      /// %M = minute, %S = second, ... 
      /// Meta data related: $a = artist, $b = album, $c = copyright,
      /// $d = description, $e = encoded by, $g = genre,
      /// $l = language, $n = track num, $p = now playing,
      /// $r = rating, $s = subtitles language, $t = title,
      /// $u = url, $A = date,
      /// $B = audio bitrate (in kb/s), $C = chapter,
      /// $D = duration, $F = full name with path, $I = title,
      /// $L = time left,
      /// $N = name, $O = audio language, $P = position (in %), $R = rate,
      /// $S = audio sample rate (in kHz),
      /// $T = time, $U = publisher, $V = volume, $_ = new line) 
      /// </summary>
      string Text { get; set; }

      /// <summary>
      /// Color of the text that will be rendered on the video.
      /// </summary>
      VlcColor Color { get; set; }

      /// <summary>
      /// You can enforce the marquee position on the video.
      /// </summary>
      Position Position { get; set; }

      /// <summary>
      /// Number of milliseconds between string updates. This is mainly useful when using meta data or time format string sequences.
      /// </summary>
      int Refresh { get; set; }

      /// <summary>
      /// Font size, in pixels. Default is -1 (use default font size).
      /// </summary>
      int Size { get; set; }

      /// <summary>
      /// Number of milliseconds the marquee must remain displayed. Default value is 0 (remains forever).
      /// </summary>
      int Timeout { get; set; }

      /// <summary>
      /// X offset, from the left screen edge.
      /// </summary>
      int X { get; set; }

      /// <summary>
      /// Y offset, down from the top.
      /// </summary>
      int Y { get; set; }

      /// <summary>
      /// Opacity (inverse of transparency) of overlayed text. 0 = transparent, 255 = totally opaque. 
      /// </summary>
      int Opacity { get; set; }
   }
}
