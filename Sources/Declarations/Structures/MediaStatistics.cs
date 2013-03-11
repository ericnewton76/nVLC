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
   /// Data structure containing media statistics' parameters.
   /// </summary>
   [Serializable]
   public struct MediaStatistics
   {
      /* Input */
      public int ReadBytes;
      public float InputBitrate;

      /* Demux */
      public int DemuxReadBytes;
      public float DemuxBitrate;
      public int DemuxCorrupted;
      public int DemuxDiscontinuity;

      /* Decoders */
      public int DecodedVideo;
      public int DecodedAudio;

      /* Video Output */
      public int DisplayedPictures;
      public int LostPictures;

      /* Audio output */
      public int PlayedAbuffers;
      public int LostAbuffers;

      /* Stream output */
      public int SentPackets;
      public int SentBytes;
      public float SendBitrate;
   }
}
