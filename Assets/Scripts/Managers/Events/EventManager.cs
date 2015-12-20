using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public partial class EventManager
    {
        public class EventInvoker<T> where T : EventArgs
        {
            public delegate void Handler(T args);
            public event Handler Listeners = null;
            public void Invoke(T args)
            {
                if (Listeners == null)
                {
                    return;
                }

                if (args == null)
                {
                    Debug.LogError("Event Arguments cannot be null, ignoring an event");
                    return;
                }

                Listeners.Invoke(args);
            }
        }

        // events are defined in other files (this is a partial class)
	}

    public class EmptyEventArgs : EventArgs
    {
        public EmptyEventArgs()
        {
            // nothing
        }
    }

}