using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Declarations.VLM
{
    /// <summary>
    /// Represents VLM events 
    /// </summary>
    [Serializable]
    public class VlmEvent : EventArgs
    {
        /// <summary>
        /// Gets the VLM media name
        /// </summary>
        public string MediaName { get; private set; }

        /// <summary>
        /// Gets the VLM media instance name
        /// </summary>
        public string InstanceName { get; private set; }

        /// <summary>
        /// Initializes new intance of VlmEvent
        /// </summary>
        /// <param name="instanceName"></param>
        /// <param name="mediaName"></param>
        public VlmEvent(string instanceName, string mediaName)
        {
            InstanceName = instanceName;
            MediaName = mediaName;
        }
    }
}
