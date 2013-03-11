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

namespace Declarations
{
    /// <summary>
    /// Enables custom processing of audio samples
    /// </summary>
    public interface IAudioRenderer : IRenderer
    {
        /// <summary>
        /// Sets callback methods for volume change and audio samples playback
        /// </summary>
        /// <param name="volume">Callback method invoked when volume changed or muted</param>
        /// <param name="sound">Callback method invoked when new audio samples should be played</param>
        void SetCallbacks(VolumeChangedEventHandler volume, NewSoundEventHandler sound);

        /// <summary>
        /// Sets callback for audio playback
        /// </summary>
        /// <param name="callbacks"></param>
        void SetCallbacks(AudioCallbacks callbacks);

        /// <summary>
        /// Sets audio format
        /// </summary>
        /// <param name="format"></param>
        /// <remarks>Mutually exclusive with SetFormatCallback</remarks>
        void SetFormat(SoundFormat format);

        /// <summary>
        /// Sets audio format callback, to get/set format before playback starts
        /// </summary>
        /// <param name="formatSetup"></param>
        /// <remarks>Mutually exclusive with SetFormat</remarks>
        void SetFormatCallback(Func<SoundFormat, SoundFormat> formatSetup);
    }
}
