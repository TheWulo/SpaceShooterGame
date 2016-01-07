using System;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Ship;
using Assets.Scripts.Attachables;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class VehicleAssemblyManager : Singleton<VehicleAssemblyManager>, IInitializable
    {
        private bool isInitialized;

        public ShipBase ShipBeingBuild;

        private int currentSlotId = -1;

        #region IInitializable
        public void Init()
        {
            EventManager.SlotSelected.Listeners += OnSelectedSlot;
            EventManager.ItemAttached.Listeners += OnItemAttached;
            EventManager.ItemDetached.Listeners += OnItemDetached;
            isInitialized = true;
        }

        private void OnItemDetached(ItemDetachedEventArgs args)
        {
            foreach (var spot in ShipBeingBuild.EngineSpots)
            {
                if (spot.AttachableID == currentSlotId)
                {
                    spot.Detach();
                }
            }
            foreach (var spot in ShipBeingBuild.WeaponSpots)
            {
                if (spot.AttachableID == currentSlotId)
                {
                    spot.Detach();
                }
            }
            foreach (var spot in ShipBeingBuild.SupportSpots)
            {
                if (spot.AttachableID == currentSlotId)
                {
                    spot.Detach();
                }
            }
        }

        private void OnItemAttached(ItemAttachedEventArgs args)
        {
            foreach (var spot in ShipBeingBuild.EngineSpots)
            {
                if (spot.AttachableID == currentSlotId)
                {
                    var attachable = (Instantiate(AttachablesDatabase.instance.GetAttachable(args.Item.AttachableID).gameObject) as GameObject);
                    attachable.transform.SetParent(spot.transform);
                    spot.Attach(attachable.GetComponent<Attachable>());
                }
            }
            foreach (var spot in ShipBeingBuild.WeaponSpots)
            {
                if (spot.AttachableID == currentSlotId)
                {
                    var attachable = (Instantiate(AttachablesDatabase.instance.GetAttachable(args.Item.AttachableID).gameObject) as GameObject);
                    attachable.transform.SetParent(spot.transform);
                    spot.Attach(attachable.GetComponent<Attachable>());
                }
            }
            foreach (var spot in ShipBeingBuild.SupportSpots)
            {
                if (spot.AttachableID == currentSlotId)
                {
                    var attachable = (Instantiate(AttachablesDatabase.instance.GetAttachable(args.Item.AttachableID).gameObject) as GameObject);
                    attachable.transform.SetParent(spot.transform);
                    spot.Attach(attachable.GetComponent<Attachable>());
                }
            }
        }

        private void OnSelectedSlot(SlotSelectedEventArgs args)
        {
            currentSlotId = args.SelectedSlot.SlotID;
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }
        #endregion

        public void Prepare()
        {
            if (ShipBeingBuild != null)
            {
                Destroy(ShipBeingBuild.gameObject);
            }
            ShipBeingBuild = Instantiate(ShipsDatabase.instance.GetShip(VehiclesManager.instance.PlayerCurrentShipID));
            VehiclesManager.instance.PlayerShipCurrent = ShipBeingBuild;
        }
    }
}
