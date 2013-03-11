using System;
using Declarations;
using Declarations.Media;
using Declarations.MediaLibrary;
using Implementation.Media;
using LibVlcWrapper;

namespace Implementation.MediaLibrary
{
    internal class MediaLibraryImpl : DisposableBase, IReferenceCount, INativePointer, IMediaLibrary
    {
        private IntPtr m_hMediaLib = IntPtr.Zero;

        public MediaLibraryImpl(IntPtr mediaLib)
        {
            m_hMediaLib = LibVlcMethods.libvlc_media_library_new(mediaLib);
        }

        protected override void Dispose(bool disposing)
        {
            Release();
        }

        public void Load()
        {
            int result = LibVlcMethods.libvlc_media_library_load(m_hMediaLib);
        }

        public IMediaList MediaList
        {
            get
            {
                return new MediaList(LibVlcMethods.libvlc_media_library_media_list(m_hMediaLib));
            }
        }

        public void AddRef()
        {
            LibVlcMethods.libvlc_media_library_retain(m_hMediaLib);
        }

        public void Release()
        {
            LibVlcMethods.libvlc_media_library_release(m_hMediaLib);
        }

        public IntPtr Pointer
        {
            get 
            {
                return m_hMediaLib;
            }
        }
    }
}
