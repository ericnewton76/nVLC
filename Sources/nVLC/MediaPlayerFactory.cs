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
using System.IO;
using System.Runtime.InteropServices;
using Declarations;
using Declarations.Discovery;
using Declarations.Media;
using Declarations.MediaLibrary;
using Declarations.Players;
using Declarations.VLM;
using Implementation.Discovery;
using Implementation.Exceptions;
using Implementation.Loggers;
using Implementation.Media;
using Implementation.MediaLibrary;
using Implementation.Players;
using Implementation.VLM;
using LibVlcWrapper;
using Microsoft.Win32;

namespace Implementation
{
    /// <summary>
    /// Entry point for the nVLC library.
    /// </summary>
    public class MediaPlayerFactory : DisposableBase, IMediaPlayerFactory, IReferenceCount, INativePointer
    {
        IntPtr m_hMediaLib = IntPtr.Zero;
        IVideoLanManager m_vlm = null;
        NLogger m_logger = new NLogger();
        LogSubscriber m_log;
        
        /// <summary>
        /// Initializes media library with default arguments
        /// </summary>
        /// <param name="findLibvlc"></param>
        /// <param name="frameInfo"></param>
        public MediaPlayerFactory(bool findLibvlc = false)
        {
            string[] args = new string[] 
             {
                "-I", 
                "dumy",  
		        "--ignore-config", 
                "--no-osd",
                "--disable-screensaver",
                "--ffmpeg-hw",
		        "--plugin-path=./plugins" 
             };

            Initialize(args, findLibvlc);
        }

        /// <summary>
        /// Initializes media library with user defined arguments
        /// </summary>
        /// <param name="args">Collection of arguments passed to libVLC library</param>
        /// <param name="findLibvlc">True to find libvlc installation path, False to use libvlc in the executable path</param>
        /// <param name="frameInfo"></param>
        public MediaPlayerFactory(string[] args, bool findLibvlc = false)
        {
            Initialize(args, findLibvlc);
        }

        private void Initialize(string[] args, bool findLibvlc)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            if (findLibvlc)
            {
                TrySetVLCPath();
            }

            TrySetupLogging();

            try
            {
                m_hMediaLib = LibVlcMethods.libvlc_new(args.Length, args);
            }
            catch (DllNotFoundException ex)
            {
                throw new LibVlcNotFoundException(ex);
            }

            if (m_hMediaLib == IntPtr.Zero)
            {
                throw new LibVlcInitException();
            }
        }

        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exc = e.ExceptionObject as Exception;
            if(exc != null)
            {
                m_logger.Error("Unhandled exception: " + exc.Message);
            }

            if (e.IsTerminating)
            {
                m_logger.Error("Due to unhandled exception the application will terminate");
            }
        }

        private void TrySetupLogging()
        {
            try
            {
                m_log = new LogSubscriber(m_logger);
            }
            catch (EntryPointNotFoundException ex)
            {
                MinimalLibVlcVersion minVersion = (MinimalLibVlcVersion)Attribute.GetCustomAttribute(ex.TargetSite, typeof(MinimalLibVlcVersion));
                if (minVersion != null)
                {
                    string msg = string.Format("libVLC logging functinality enabled staring libVLC version {0} while you are using version {1}", minVersion.MinimalVersion, Version);
                    m_logger.Warning(msg);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("Failed to setup logging, reason : {0}", ex.Message);
                m_logger.Error(msg);
            }
        }

        /// <summary>
        /// Creates new instance of player.
        /// </summary>
        /// <typeparam name="T">Type of the player to create</typeparam>
        /// <returns>Newly created player</returns>
        public T CreatePlayer<T>() where T : IPlayer
        {
            return ObjectFactory.Build<T>(m_hMediaLib);
        }

        /// <summary>
        /// Creates new instance of media list player
        /// </summary>
        /// <typeparam name="T">Type of media list player</typeparam>
        /// <param name="mediaList">Media list</param>
        /// <returns>Newly created media list player</returns>
        public T CreateMediaListPlayer<T>(IMediaList mediaList) where T : IMediaListPlayer
        {
            return ObjectFactory.Build<T>(m_hMediaLib, mediaList);
        }

        /// <summary>
        /// Creates new instance of media.
        /// </summary>
        /// <typeparam name="T">Type of media to create</typeparam>
        /// <param name="input">The media input string</param>
        /// <param name="options">Optional media options</param>
        /// <returns>Newly created media</returns>
        public T CreateMedia<T>(string input, params string[] options) where T : IMedia
        {
            T media = ObjectFactory.Build<T>(m_hMediaLib);
            media.Input = input;
            media.AddOptions(options);

            return media;
        }

        /// <summary>
        /// Creates new instance of media list.
        /// </summary>
        /// <typeparam name="T">Type of media list</typeparam>
        /// <param name="mediaItems">Collection of media inputs</param>       
        /// <param name="options"></param>
        /// <returns>Newly created media list</returns>
        public T CreateMediaList<T>(IEnumerable<string> mediaItems, params string[] options) where T : IMediaList
        {
            T mediaList = ObjectFactory.Build<T>(m_hMediaLib);
            foreach (var file in mediaItems)
            {
                mediaList.Add(this.CreateMedia<IMedia>(file, options));
            }

            return mediaList;
        }

        /// <summary>
        /// Creates media list instance with no media items
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateMediaList<T>() where T : IMediaList
        {
            return ObjectFactory.Build<T>(m_hMediaLib);
        }

        /// <summary>
        /// Creates media discovery object
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IMediaDiscoverer CreateMediaDiscoverer(string name)
        {
            return ObjectFactory.Build<IMediaDiscoverer>(m_hMediaLib, name);
        }

        /// <summary>
        /// Creates media library
        /// </summary>
        /// <returns></returns>
        public IMediaLibrary CreateMediaLibrary()
        {
            return ObjectFactory.Build<IMediaLibrary>(m_hMediaLib);
        }

        /// <summary>
        /// Gets the libVLC version.
        /// </summary>
        public string Version
        {
            get
            {
                IntPtr pStr = LibVlcMethods.libvlc_get_version();
                return Marshal.PtrToStringAnsi(pStr);
            }               
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            Release();
        }

        private static class ObjectFactory
        {
            static Dictionary<Type, Type> objectMap = new Dictionary<Type, Type>();
            
            static ObjectFactory()
            {
                objectMap.Add(typeof(IMedia), typeof(BasicMedia));
                objectMap.Add(typeof(IMediaFromFile), typeof(MediaFromFile));
                objectMap.Add(typeof(IVideoInputMedia), typeof(VideoInputMedia));
                objectMap.Add(typeof(IScreenCaptureMedia), typeof(ScreenCaptureMedia));
                objectMap.Add(typeof(IPlayer), typeof(BasicPlayer));
                objectMap.Add(typeof(IAudioPlayer), typeof(AudioPlayer));
                objectMap.Add(typeof(IVideoPlayer), typeof(VideoPlayer));
                objectMap.Add(typeof(IDiskPlayer), typeof(DiskPlayer));
                objectMap.Add(typeof(IMediaList), typeof(MediaList));
                objectMap.Add(typeof(IMediaListPlayer), typeof(MediaListPlayer));
                objectMap.Add(typeof(IVideoLanManager), typeof(VideoLanManager));
                objectMap.Add(typeof(IMediaDiscoverer), typeof(MediaDiscoverer));
                objectMap.Add(typeof(IMediaLibrary), typeof(MediaLibraryImpl));
                objectMap.Add(typeof(IMemoryInputMedia), typeof(MemoryInputMedia));
            }

            public static T Build<T>(params object[] args)
            {
                if (objectMap.ContainsKey(typeof(T)))
                {
                    return (T)Activator.CreateInstance(objectMap[typeof(T)], args);
                }

                throw new ArgumentException("Unregistered type", typeof(T).FullName);
            }
        }

        #region IReferenceCount Members

        public void AddRef()
        {
            LibVlcMethods.libvlc_retain(m_hMediaLib);
        }

        public void Release()
        {
            try
            {
                LibVlcMethods.libvlc_release(m_hMediaLib);
            }
            catch (AccessViolationException)
            { }
        }

        #endregion

        #region INativePointer Members

        public IntPtr Pointer
        {
            get
            {
                return m_hMediaLib;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public long Clock
        {
            get
            {
                return LibVlcMethods.libvlc_clock();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pts"></param>
        /// <returns></returns>
        public long Delay(long pts)
        {
            return LibVlcMethods.libvlc_delay(pts);
        }

        /// <summary>
        /// Gets list of available audio filters
        /// </summary>
        public IEnumerable<FilterInfo> AudioFilters
        {
            get
            {
                IntPtr pList = LibVlcMethods.libvlc_audio_filter_list_get(m_hMediaLib);
                libvlc_module_description_t item = (libvlc_module_description_t)Marshal.PtrToStructure(pList, typeof(libvlc_module_description_t));

                do
                {
                    yield return GetFilterInfo(item);
                    if (item.p_next != IntPtr.Zero)
                    {
                        item = (libvlc_module_description_t)Marshal.PtrToStructure(item.p_next, typeof(libvlc_module_description_t));
                    }
                    else
                    {
                        break;
                    }

                }
                while (true);

                LibVlcMethods.libvlc_module_description_list_release(pList);
            }
        }

        /// <summary>
        /// Gets list of available video filters
        /// </summary>
        public IEnumerable<FilterInfo> VideoFilters
        {
            get
            {
                IntPtr pList = LibVlcMethods.libvlc_video_filter_list_get(m_hMediaLib);
                if (pList == IntPtr.Zero)
                {
                    yield break;
                }

                libvlc_module_description_t item = (libvlc_module_description_t)Marshal.PtrToStructure(pList, typeof(libvlc_module_description_t));

                do
                {
                    yield return GetFilterInfo(item);
                    if (item.p_next != IntPtr.Zero)
                    {
                        item = (libvlc_module_description_t)Marshal.PtrToStructure(item.p_next, typeof(libvlc_module_description_t));
                    }
                    else
                    {
                        break;
                    }
                }
                while (true);

                LibVlcMethods.libvlc_module_description_list_release(pList);
            }
        }

        private FilterInfo GetFilterInfo(libvlc_module_description_t item)
        {
            return new FilterInfo()
            {
                Help = Marshal.PtrToStringAnsi(item.psz_help),
                Longname = Marshal.PtrToStringAnsi(item.psz_longname),
                Name = Marshal.PtrToStringAnsi(item.psz_name),
                Shortname = Marshal.PtrToStringAnsi(item.psz_shortname)
            };
        }

        /// <summary>
        /// Gets the VLM instance
        /// </summary>
        public IVideoLanManager VideoLanManager
        {
            get
            {
                if (m_vlm == null)
                {
                    m_vlm = ObjectFactory.Build<IVideoLanManager>(m_hMediaLib);
                }

                return m_vlm;
            }
        } 

        /// <summary>
        /// Gets list of available audio output modules
        /// </summary>
        public IEnumerable<AudioOutputModuleInfo> AudioOutputModules
        {
            get
            {
                IntPtr pList = LibVlcMethods.libvlc_audio_output_list_get(m_hMediaLib);
                libvlc_audio_output_t pDevice = (libvlc_audio_output_t)Marshal.PtrToStructure(pList, typeof(libvlc_audio_output_t));

                do
                {
                    AudioOutputModuleInfo info = GetDeviceInfo(pDevice);

                    yield return info;
                    if (pDevice.p_next != IntPtr.Zero)
                    {
                        pDevice = (libvlc_audio_output_t)Marshal.PtrToStructure(pDevice.p_next, typeof(libvlc_audio_output_t));
                    }
                    else
                    {
                        break;
                    }
                }
                while (true);

                LibVlcMethods.libvlc_audio_output_list_release(pList);
            }
        }

        /// <summary>
        /// Gets list of available audio output devices
        /// </summary>
        public IEnumerable<AudioOutputDeviceInfo> GetAudioOutputDevices(AudioOutputModuleInfo audioOutputModule)
        {
            int i = LibVlcMethods.libvlc_audio_output_device_count(m_hMediaLib, audioOutputModule.Name.ToUtf8());
            for (int j = 0; j < i; j++)
            {
                AudioOutputDeviceInfo d = new AudioOutputDeviceInfo();
                d.Longname = LibVlcMethods.libvlc_audio_output_device_longname(m_hMediaLib, audioOutputModule.Name.ToUtf8(), j);
                d.Id = LibVlcMethods.libvlc_audio_output_device_id(m_hMediaLib, audioOutputModule.Name.ToUtf8(), j);

                yield return d;
            }
        }

        private AudioOutputModuleInfo GetDeviceInfo(libvlc_audio_output_t pDevice)
        {
            return new AudioOutputModuleInfo()
            {
                Name = Marshal.PtrToStringAnsi(pDevice.psz_name),
                Description = Marshal.PtrToStringAnsi(pDevice.psz_description)
            };
        }

        private void TrySetVLCPath()
        {
            try
            {
                if (Environment.Is64BitProcess)
                {
                    TrySet64BitPath();
                }
                else
                {
                    TrySetVLCPath("vlc media player");
                }
            }
            catch (Exception ex)
            {
                m_logger.Error("Failed to set VLC path: " + ex.Message);
            }
        }

        private void TrySet64BitPath()
        {
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\VideoLAN\VLC"))
            {
                object vlcDir = rk.GetValue("InstallDir");
                if (vlcDir != null)
                {
                    Directory.SetCurrentDirectory(vlcDir.ToString());
                }
            }
        }

        private void TrySetVLCPath(string vlcRegistryKey)
        {
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        object DisplayName = sk.GetValue("DisplayName");
                        if (DisplayName != null)
                        {
                            if (DisplayName.ToString().ToLower().IndexOf(vlcRegistryKey.ToLower()) > -1)
                            {
                                object vlcDir = sk.GetValue("InstallLocation");

                                if (vlcDir != null)
                                {
                                    Directory.SetCurrentDirectory(vlcDir.ToString());
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
