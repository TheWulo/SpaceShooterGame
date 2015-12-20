using System;
using Assets.Scripts.GUI;

namespace Assets.Scripts.Managers
{
    public partial class EventManager
    {
        public static EventInvoker<SlotSelectedEventArgs> SlotSelected
                = new EventInvoker<SlotSelectedEventArgs>();
    }

    public class SlotSelectedEventArgs : EventArgs
    {
        public readonly SlotUI SelectedSlot;

        public SlotSelectedEventArgs(SlotUI selectedSlot)
        {
            SelectedSlot = selectedSlot;
        }
    }
}
