using System;
using Assets.Scripts.GUI;
using Assets.Scripts.Attachables;

namespace Assets.Scripts.Managers
{
    public partial class EventManager
    {
        public static EventInvoker<SlotSelectedEventArgs> SlotSelected
                = new EventInvoker<SlotSelectedEventArgs>();
        public static EventInvoker<InventoryItemSelectedEventArgs> InventoryItemSelected
                = new EventInvoker<InventoryItemSelectedEventArgs>();
        public static EventInvoker<ItemAttachedEventArgs> ItemAttached
                = new EventInvoker<ItemAttachedEventArgs>();
        public static EventInvoker<ItemDetachedEventArgs> ItemDetached
                = new EventInvoker<ItemDetachedEventArgs>();
    }

    public class SlotSelectedEventArgs : EventArgs
    {
        public readonly SlotUI SelectedSlot;

        public SlotSelectedEventArgs(SlotUI selectedSlot)
        {
            SelectedSlot = selectedSlot;
        }
    }

    public class InventoryItemSelectedEventArgs : EventArgs
    {
        public readonly InventoryUI InventoryUI;
        public readonly Attachable Item;

        public InventoryItemSelectedEventArgs(InventoryUI inventoryUI, Attachable item)
        {
            InventoryUI = inventoryUI;
            Item = item;
        }
    }

    public class ItemAttachedEventArgs : EventArgs
    {
        public readonly Attachable Item;

        public ItemAttachedEventArgs(Attachable item)
        {
            Item = item;
        }
    }

    public class ItemDetachedEventArgs : EventArgs
    {
        public readonly Attachable Item;

        public ItemDetachedEventArgs(Attachable item)
        {
            Item = item;
        }
    }
}
