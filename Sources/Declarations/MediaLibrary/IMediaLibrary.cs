using System;
using Declarations.Media;

namespace Declarations.MediaLibrary
{
    public interface IMediaLibrary
    {
        void Load();
        IMediaList MediaList { get; }
    }
}
