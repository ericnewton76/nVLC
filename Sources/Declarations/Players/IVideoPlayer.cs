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
using System.Drawing;
using Declarations.Filters;

namespace Declarations.Players
{
    /// <summary>
    /// Player for audio visual media.
    /// </summary>
    public interface IVideoPlayer : IAudioPlayer
    {
        /// <summary>
        /// Sets or gets a window handle for video rendering.
        /// </summary>
        IntPtr WindowHandle { get; set; }

        /// <summary>
        /// Saves current video frame to a file in PNG format.
        /// </summary>
        /// <param name="stream">Number of video stream starting from 0</param>
        /// <param name="path">Full path for the snapshot file.</param>
        void TakeSnapShot(uint stream, string path);

        /// <summary>
        /// Gets or sets media playback rate.
        /// </summary>
        float PlaybackRate { get; set; }

        /// <summary>
        /// Gets number of frames  per second.
        /// </summary>
        float FPS { get; }

        /// <summary>
        /// Shows next frame
        /// </summary>
        void NextFrame();

        /// <summary>
        /// Gets video size in pixels
        /// </summary>
        /// <param name="stream">Number of video stream starting from 0</param>
        /// <returns>Size of the video frame</returns>
        Size GetVideoSize(uint stream);

        /// <summary>
        /// Gets cursor position in video pixel coordinates.
        /// </summary>
        /// <param name="stream">Number of video stream starting from 0</param>
        /// <returns>Cursor position</returns>
        Point GetCursorPosition(uint stream);

        /// <summary>
        /// Gets or sets a value indicating whether video widget handles key input.
        /// </summary>
        bool KeyInputEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether video widget handles mouse input.
        /// </summary>
        bool MouseInputEnabled { get; set; }

        /// <summary>
        /// Gets or sets video scale.
        /// </summary>
        float VideoScale { get; set; }

        /// <summary>
        /// Gets or sets video aspect ratio.
        /// </summary>
        AspectRatioMode AspectRatio { get; set; }

        /// <summary>
        /// Sets video subtitle file.
        /// </summary>
        /// <param name="path">Full path to the video subtitles.</param>
        void SetSubtitleFile(string path);

        /// <summary>
        /// Gets or sets teletext page.
        /// </summary>
        int Teletext { get; set; }

        /// <summary>
        /// Toggles teletext transparent status on video output.
        /// </summary>
        void ToggleTeletext();

        /// <summary>
        /// Gets value indicating whether a media player can play the media.
        /// </summary>
        bool PlayerWillPlay { get; }

        /// <summary>
        /// Gets number of video output modules.
        /// </summary>
        int VoutCount { get; }

        /// <summary>
        /// Gets value indicating whether a media player is seekable.
        /// </summary>
        bool IsSeekable { get; }

        /// <summary>
        /// Gets value indicating whether a media player can pause.
        /// </summary>
        bool CanPause { get; }

        /// <summary>
        /// Sets or gets video adjustments
        /// </summary>
        IAdjustFilter Adjust { get; }

        /// <summary>
        /// Gets or sets video crop filter.
        /// </summary>
        ICropFilter CropGeometry { get; }

        /// <summary>
        /// Gets custom renderer object allowing to process each frame as System.Drawing.Bitmap object.
        /// </summary>
        IMemoryRenderer CustomRenderer { get; }

        /// <summary>
        /// Gets custom renderer object allowing to process each frame as YUV or RGB raw pixel format. 
        /// It also implements pre-playback callback to get and optionally set video width, height and chroma type.
        /// </summary>
        IMemoryRendererEx CustomRendererEx { get; }

        /// <summary>
        /// Gets logo overlay filter.
        /// </summary>
        ILogoFilter Logo { get; }

        /// <summary>
        /// Gets text overlay filter.
        /// </summary>
        IMarqueeFilter Marquee { get; }

        /// <summary>
        /// Gets video deinterlace filter.
        /// </summary>
        IDeinterlaceFilter Deinterlace { get; }
    }
}
