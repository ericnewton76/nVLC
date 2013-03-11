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
using Declarations.Media;
using Declarations.Events;

namespace Declarations.Players
{
    /// <summary>
    /// Represents basic media player functionality.
    /// </summary>
    public interface IPlayer : IDisposable, IEqualityComparer<IPlayer>
    {
        /// <summary>
        /// Opens new media item.
        /// </summary>
        /// <param name="media">Media item to play</param>
        void Open(IMedia media);

        /// <summary>
        /// Plays media item.
        /// </summary>
        void Play();

        /// <summary>
        /// Pauses playback of the media.
        /// </summary>
        void Pause();

        /// <summary>
        /// Stops the playback.
        /// </summary>
        void Stop();

        /// <summary>
        /// Gets or sets the media time in milliseconds.
        /// </summary>
        long Time { get; set; }

        /// <summary>
        /// Gets or sets media position.
        /// </summary>
        float Position { get; set; }

        /// <summary>
        /// Gets the media length in milliseconds.
        /// </summary>
        long Length { get; }

        /// <summary>
        /// Gets player events.
        /// </summary>
        IEventBroker Events { get; }

        /// <summary>
        /// Gets value indicating whether the player is playing
        /// </summary>
        bool IsPlaying { get; }
    }
}
