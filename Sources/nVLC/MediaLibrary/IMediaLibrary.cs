using System;
using nVLC.Media;

namespace nVLC.MediaLibrary
{
    public interface IMediaLibrary
    {
        void Load();
        IMediaList MediaList { get; }
    }
}
