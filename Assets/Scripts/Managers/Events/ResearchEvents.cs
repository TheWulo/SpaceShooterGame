using System;

namespace Assets.Scripts.Managers
{
    public partial class EventManager
    {
        public static EventInvoker<AttachableResearchedEventArgs> AttachableResearched
                = new EventInvoker<AttachableResearchedEventArgs>();
        public static EventInvoker<ShipResearchedEventArgs> ShipResearched
                = new EventInvoker<ShipResearchedEventArgs>();
    }

    public class AttachableResearchedEventArgs : EventArgs
    {
        public readonly string AttachableID;

        public AttachableResearchedEventArgs(string attachableID)
        {
            AttachableID = attachableID;
        }
    }

    public class ShipResearchedEventArgs : EventArgs
    {
        public readonly string ShipID;

        public ShipResearchedEventArgs(string shipID)
        {
            ShipID = shipID;
        }
    }
}
