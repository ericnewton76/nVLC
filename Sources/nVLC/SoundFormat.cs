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

namespace Declarations
{
    /// <summary>
    /// Specifies the parameters of the sound.
    /// </summary>
    [Serializable]
    public class SoundFormat
    {
        /// <summary>
        /// Initializes new instance of SoundFormat class
        /// </summary>
        /// <param name="soundType"></param>
        /// <param name="rate"></param>
        /// <param name="channels"></param>
        public SoundFormat(SoundType soundType, int rate, int channels)
        {
            SoundType = soundType;
            Format = soundType.ToString();
            Rate = rate;
            Channels = channels;
            Init();
            BlockSize = BitsPerSample / 8 * Channels;
            UseCustomAudioRendering = true;
        }

        private void Init()
        {
            switch (SoundType)
            {
                case SoundType.S16N:
                    BitsPerSample = 16;
                    break;
            }
        }

        /// <summary>
        /// Audio format
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// Sampling rate in Hz
        /// </summary>
        public int Rate { get; private set; }

        /// <summary>
        /// Number of channels used by audio sample
        /// </summary>
        public int Channels { get; private set; }

        /// <summary>
        /// Size of single audio sample in bytes
        /// </summary>
        public int BitsPerSample { get; private set; }

        /// <summary>
        /// Specifies sound sample format
        /// </summary>
        public SoundType SoundType { get; private set; }

        /// <summary>
        /// Size of audio block (BitsPerSample / 8 * Channels)
        /// </summary>
        public int BlockSize { get; private set; }

        /// <summary>
        /// Indicated whether to use custom audio renderer (True), or to use default audio output (False)
        /// </summary>
        public bool UseCustomAudioRendering { get; set; }
    }
}
