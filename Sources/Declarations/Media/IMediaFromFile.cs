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

namespace Declarations.Media
{
   /// <summary>
   /// Represents audio/visual media file.
   /// </summary>
   public interface IMediaFromFile : IMedia
   {
      /// <summary>
      /// Gets meta data of the media.
      /// </summary>
      /// <param name="dataType"></param>
      /// <returns></returns>
      string GetMetaData(MetaDataType dataType);

      /// <summary>
      /// Sets meta data of the media.
      /// </summary>
      /// <param name="dataType">Meta data type</param>
      /// <param name="argument">New meta data value</param>
      void SetMetaData(MetaDataType dataType, string argument);

      /// <summary>
      /// Saves changes to media meta data.
      /// </summary>
      void SaveMetaData();

      /// <summary>
      /// Gets the duration of media in milliseconds.
      /// </summary>
      long Duration { get; }

      /// <summary>
      /// Gets information describing media elementary streams.
      /// </summary>
      /// <remarks>Returns array of media tracks info in case of success, or null in case of failure</remarks>
      MediaTrackInfo[] TracksInfo { get; }
   }
}
