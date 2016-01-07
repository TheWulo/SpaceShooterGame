using System;

namespace Assets.Scripts.Managers
{
    public partial class EventManager
    {
        public static EventInvoker<AttachableResearchedEventArgs> AttachableResearched
                = new EventInvoker<AttachableResearchedEventArgs>();
    }

    public class AttachableResearchedEventArgs : EventArgs
    {
        public readonly string AttachableID;

        public AttachableResearchedEventArgs(string attachableID)
        {
            AttachableID = attachableID;
        }
    }
}
