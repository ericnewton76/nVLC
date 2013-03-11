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

using System.Drawing;
using System.Text;
using Declarations;
using LibVlcWrapper;
using System;
using Implementation.Utils;

namespace Implementation
{
    internal static class Extensions
    {
        public static byte[] ToUtf8(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }

            return Encoding.UTF8.GetBytes(str);
        }

        /// <summary>
        /// String format : [width]x[height]+[left offset]+[top offset]
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static string ToCropFilterString(this Rectangle rect)
        {
            return string.Format("{0}x{1}+{2}+{3}", rect.Width, rect.Height, rect.X, rect.Y);
        }

        public static Rectangle ToRectangle(this string str)
        {
            string[] items = str.Split(new char[] { 'x', '+' }, 4);

            return new Rectangle(int.Parse(items[3]), int.Parse(items[2]), int.Parse(items[1]), int.Parse(items[0]));
        }

        public static MediaStatistics ToMediaStatistics(this libvlc_media_stats_t stats)
        {
            MediaStatistics ms = new MediaStatistics();
            ms.DecodedAudio = stats.i_decoded_audio;
            ms.DecodedVideo = stats.i_decoded_video;
            ms.DemuxBitrate = stats.f_demux_bitrate;
            ms.DemuxCorrupted = stats.i_demux_corrupted;
            ms.DemuxDiscontinuity = stats.i_demux_discontinuity;
            ms.DemuxReadBytes = stats.i_demux_read_bytes;
            ms.DisplayedPictures = stats.i_displayed_pictures;
            ms.InputBitrate = stats.f_input_bitrate;
            ms.LostAbuffers = stats.i_lost_abuffers;
            ms.LostPictures = stats.i_lost_pictures;
            ms.PlayedAbuffers = stats.i_played_abuffers;
            ms.ReadBytes = stats.i_read_bytes;
            ms.SendBitrate = stats.f_send_bitrate;
            ms.SentBytes = stats.i_sent_bytes;
            ms.SentPackets = stats.i_sent_packets;

            return ms;
        }

        public static MediaTrackInfo ToMediaInfo(this libvlc_media_track_info_t tInfo)
        {
            MediaTrackInfo mti = new MediaTrackInfo();
            mti.Channels = tInfo.audio_video.audio.i_channels;
            mti.Codec = tInfo.i_codec;
            mti.Height = tInfo.audio_video.video.i_width;
            mti.Id = tInfo.i_id;
            mti.TrackType = (TrackType)(int)tInfo.i_type;
            mti.Level = tInfo.i_level;
            mti.Profile = tInfo.i_profile;
            mti.Rate = tInfo.audio_video.audio.i_rate;
            mti.Width = tInfo.audio_video.video.i_width;

            return mti;
        }
    }
}
