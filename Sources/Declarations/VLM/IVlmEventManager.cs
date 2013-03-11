using System;

namespace Declarations.VLM
{
    /// <summary>
    /// Gets events raised by the VLM object
    /// </summary>
    public interface IVlmEventManager
    {
        event EventHandler<VlmEvent> MediaAdded;
        event EventHandler<VlmEvent> MediaChanged;
        event EventHandler<VlmEvent> MediaInstanceEnd;
        event EventHandler<VlmEvent> MediaInstanceError;
        event EventHandler<VlmEvent> MediaInstanceInit;
        event EventHandler<VlmEvent> MediaInstanceOpening;
        event EventHandler<VlmEvent> MediaInstancePause;
        event EventHandler<VlmEvent> MediaInstancePlaying;
        event EventHandler<VlmEvent> MediaInstanceStarted;
        event EventHandler<VlmEvent> MediaInstanceStopped;
        event EventHandler<VlmEvent> MediaRemoved;
    }
}
