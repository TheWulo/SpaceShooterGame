﻿using System;

namespace Assets.Scripts.Managers
{
    public partial class EventManager
    {
        public static EventInvoker<EmptyEventArgs> GameStarting
                = new EventInvoker<EmptyEventArgs>();
        public static EventInvoker<EmptyEventArgs> GameFinishing
                = new EventInvoker<EmptyEventArgs>();
    }
}
