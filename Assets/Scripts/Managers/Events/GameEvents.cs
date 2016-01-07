using System;

namespace Assets.Scripts.Managers
{
    public partial class EventManager
    {
        public static EventInvoker<EmptyEventArgs> GameStarting
                = new EventInvoker<EmptyEventArgs>();
    }
}
