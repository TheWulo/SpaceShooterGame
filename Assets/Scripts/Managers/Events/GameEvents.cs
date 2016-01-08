using System;
using Assets.Scripts.Scrap;

namespace Assets.Scripts.Managers
{
    public partial class EventManager
    {
        public static EventInvoker<EmptyEventArgs> GameStarting
                = new EventInvoker<EmptyEventArgs>();
        public static EventInvoker<EmptyEventArgs> GameFinishing
                = new EventInvoker<EmptyEventArgs>();
        public static EventInvoker<ScrapMetalCollectedEventArgs> ScrapMetalCollected
                = new EventInvoker<ScrapMetalCollectedEventArgs>();
    }

    public class ScrapMetalCollectedEventArgs : EventArgs
    {
        public readonly ScrapMetal ScrapMetal;

        public ScrapMetalCollectedEventArgs(ScrapMetal scrapMetal)
        {
            ScrapMetal = scrapMetal;
        }
    }
}
