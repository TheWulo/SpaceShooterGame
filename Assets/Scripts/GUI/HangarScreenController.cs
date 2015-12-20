using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Managers;
using Assets.Scripts.GUI;
using Assets.Scripts.Attachables;
using System.Linq;

namespace Assets.Scripts.GUI
{
    public class HangarScreenController : GUIWindow
    {
        [Header("ShipsUI")]
        [SerializeField]
        private GameObject shipUISpawnPlace;
        [SerializeField]
        private Text shipNameText;

        [Header("InventoryUI")]
        [SerializeField]
        private GameObject inventoryContainer;
        [SerializeField]
        private GameObject inventoryUIPrefab;

        [Header("StatisticsUI")]
        [SerializeField]
        private Text healthLabel;
        [SerializeField]
        private Text energyLabel;
        [SerializeField]
        private Text damageLabel;
        [SerializeField]
        private Text speedLabel;

        private ShipUI currentShownUI;

        private SlotUI currentSelectedSlot;

        private void OnSelectedSlot(SlotSelectedEventArgs args)
        {
            if (args.SelectedSlot.attachedItem == null)
            {
                EnableOnlyCorrectInventroy(args.SelectedSlot.Type);
                currentSelectedSlot = args.SelectedSlot;
            }
            else
            {
                args.SelectedSlot.DetachItem();
            }
        }

        void OnInventoryItemSelected(InventoryItemSelectedEventArgs args)
        {
            if (currentSelectedSlot == null) return;

            currentSelectedSlot.AttachItem(args.Item);
            args.InventoryUI.SetInstalled(true);
            currentSelectedSlot = null;
            EnableAllInventory();
        }

        void OnItemDetached(ItemDetachedEventArgs args)
        {
            MoveItemBackToInventory(args.Item);
        }

        public override void Show()
        {
            EventManager.SlotSelected.Listeners += OnSelectedSlot;
            EventManager.InventoryItemSelected.Listeners += OnInventoryItemSelected;
            EventManager.ItemDetached.Listeners += OnItemDetached;

            base.Show();

            ShowCurrentShip();
            PopulateInventoryScreen();
        }

        public override void Hide()
        {
            EventManager.SlotSelected.Listeners -= OnSelectedSlot;
            EventManager.InventoryItemSelected.Listeners -= OnInventoryItemSelected;

            base.Hide();
        }

        void Update()
        {
            //DebugChangeShips();
        }

        private void DebugChangeShips()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                VehiclesManager.instance.PlayerShipCurrent = VehiclesManager.instance.PlayerShips[0];
                ShowCurrentShip();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                VehiclesManager.instance.PlayerShipCurrent = VehiclesManager.instance.PlayerShips[1];
                ShowCurrentShip();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                VehiclesManager.instance.PlayerShipCurrent = VehiclesManager.instance.PlayerShips[2];
                ShowCurrentShip();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                VehiclesManager.instance.PlayerShipCurrent = VehiclesManager.instance.PlayerShips[3];
                ShowCurrentShip();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                VehiclesManager.instance.PlayerShipCurrent = VehiclesManager.instance.PlayerShips[4];
                ShowCurrentShip();
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                VehiclesManager.instance.PlayerShipCurrent = VehiclesManager.instance.PlayerShips[5];
                ShowCurrentShip();
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                VehiclesManager.instance.PlayerShipCurrent = VehiclesManager.instance.PlayerShips[6];
                ShowCurrentShip();
            }
        }

        private void ShowCurrentShip()
        {
            if (currentShownUI != null) Destroy(currentShownUI.gameObject);

            currentShownUI = (Instantiate(VehiclesManager.instance.PlayerShipCurrent.ShipUIObject.gameObject) as GameObject).GetComponent<ShipUI>();

            currentShownUI.gameObject.transform.SetParent(gameObject.transform);
            currentShownUI.gameObject.transform.position = shipUISpawnPlace.transform.position;
            currentShownUI.gameObject.transform.localScale = new Vector3(1, 1, 1);
            currentShownUI.gameObject.transform.SetSiblingIndex(1);

            shipNameText.text = VehiclesManager.instance.PlayerShipCurrent.ShipName;
        }

        private void PopulateInventoryScreen()
        {
            foreach (var invent in InventoryManager.instance.PlayersAttachables)
            {
                InventoryUI uiElement = (Instantiate(inventoryUIPrefab) as GameObject).GetComponent<InventoryUI>();
                uiElement.gameObject.transform.SetParent(inventoryContainer.transform);
                uiElement.gameObject.transform.localScale = new Vector3(1, 1, 1);
                uiElement.SetUp(invent);
            }
        }

        private void EnableOnlyCorrectInventroy(AttachableType type)
        {
            foreach (var inventoryItem in inventoryContainer.GetComponentsInChildren<InventoryUI>())
            {
                if (inventoryItem.Installed) continue;

                inventoryItem.GetComponent<Button>().interactable = inventoryItem.Item.Type == type;
            }
        }

        private void EnableAllInventory()
        {
            foreach (var inventoryItem in inventoryContainer.GetComponentsInChildren<InventoryUI>())
            {
                if (inventoryItem.Installed) continue;

                inventoryItem.GetComponent<Button>().interactable = true;
            }
        }

        private void MoveItemBackToInventory(Attachable item)
        {
            inventoryContainer.GetComponentsInChildren<InventoryUI>().First(inventoryUI => inventoryUI.Item == item).SetInstalled(false);
        }
    }
}
