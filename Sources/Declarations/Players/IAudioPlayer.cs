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
using Declarations.Enums;

namespace Declarations.Players
{
    /// <summary>
    /// Player for audio only media
    /// </summary>
    public interface IAudioPlayer : IPlayer
    {
        /// <summary>
        /// Gets or sets the volume (0-100) of the player.
        /// Initial value is 50
        /// </summary>
        int Volume { get; set; }

        /// <summary>
        /// Sets or gets boolean indication whether the sound is on or off.
        /// </summary>
        bool Mute { get; set; }

        /// <summary>
        /// Gets or sets the audio delay in milliseconds.
        /// </summary>
        long Delay { get; set; }

        /// <summary>
        /// Sets or gets the audio channel type
        /// </summary>
        AudioChannelType Channel { get; set; }

        /// <summary>
        /// Sets or gets audio device type
        /// </summary>
        AudioOutputDeviceType DeviceType { get; set; }

        /// <summary>
        /// Toggles mute/unmute
        /// </summary>
        void ToggleMute();

        /// <summary>
        /// Gets custom audio renderer for processing PCM samples 
        /// </summary>
        IAudioRenderer CustomAudioRenderer { get; }

        /// <summary>
        /// Sets audio output module and optionally audio output device
        /// </summary>
        /// <param name="module"></param>
        /// <param name="device"></param>
        void SetAudioOutputModuleAndDevice(AudioOutputModuleInfo module, AudioOutputDeviceInfo device);
    }
}
