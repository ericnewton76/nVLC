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
using Declarations.Players;
using Declarations.Media;
using Declarations.VLM;
using Declarations.Discovery;
using Declarations.MediaLibrary;

namespace Declarations
{
    /// <summary>
    /// Defines methods for creating media and player objects
    /// </summary>
    public interface IMediaPlayerFactory : IDisposable
    {
        /// <summary>
        /// Creates new instance of player.
        /// </summary>
        /// <typeparam name="T">Type of the player to create</typeparam>
        /// <returns>Newly created player</returns>
        T CreatePlayer<T>() where T : IPlayer;

        /// <summary>
        /// Creates new instance of media.
        /// </summary>
        /// <typeparam name="T">Type of media to create</typeparam>
        /// <param name="input">The media input string</param>
        /// <param name="options">Optional media options</param>
        /// <returns>Newly created media</returns>
        T CreateMedia<T>(string input, params string[] options) where T : IMedia;

        /// <summary>
        /// Creates new instance of media list.
        /// </summary>
        /// <typeparam name="T">Type of media list</typeparam>
        /// <param name="mediaItems">Collection of media inputs</param>       
        /// <param name="options">Options applied on every media instance of the list</param>
        /// <returns>Newly created media list</returns>
        T CreateMediaList<T>(IEnumerable<string> mediaItems, params string[] options) where T : IMediaList;

        /// <summary>
        /// Creates empty media list instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T CreateMediaList<T>() where T : IMediaList;

        /// <summary>
        /// Creates new instance of media list player
        /// </summary>
        /// <typeparam name="T">Type of media list player</typeparam>
        /// <param name="mediaList">Media list</param>
        /// <returns>Newly created media list player</returns>
        T CreateMediaListPlayer<T>(IMediaList mediaList) where T : IMediaListPlayer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IMediaDiscoverer CreateMediaDiscoverer(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IMediaLibrary CreateMediaLibrary();

        /// <summary>
        /// Gets the libVLC version.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Gets list of available audio filters
        /// </summary>
        IEnumerable<FilterInfo> AudioFilters { get; }

        /// <summary>
        /// Gets list of available video filters
        /// </summary>
        IEnumerable<FilterInfo> VideoFilters { get; }

        /// <summary>
        /// Gets the VLM instance
        /// </summary>
        IVideoLanManager VideoLanManager { get; }

        /// <summary>
        /// Gets list of available audio output modules
        /// </summary>
        IEnumerable<AudioOutputModuleInfo> AudioOutputModules { get; }

        /// <summary>
        /// Gets list of audio output devices for specified output module
        /// </summary>
        /// <param name="audioOutputModule"></param>
        /// <returns></returns>
        IEnumerable<AudioOutputDeviceInfo> GetAudioOutputDevices(AudioOutputModuleInfo audioOutputModule);

        /// <summary>
        /// 
        /// </summary>
        long Clock { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pts"></param>
        /// <returns></returns>
        long Delay(long pts);
    }
}
