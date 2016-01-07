﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Managers;
using Assets.Scripts.GUI;
using Assets.Scripts.Attachables;
using System.Linq;
using Assets.Scripts.Ship;

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
        private bool inventoryShowing;

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

        private List<InventoryUI> InventoryList;

        void Awake()
        {
            InventoryList = new List<InventoryUI>();
        }

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
            if (!inventoryShowing)
            {
                GetComponent<Animator>().Play("InventorySlideIn");
                inventoryShowing = true;
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

        public void OnAssemblyFinished()
        {
            EventManager.GameStarting.Invoke(new EmptyEventArgs());
            GUIManager.instance.ShowWindow(GUIWindowType.Play);
        }

        public override void Show()
        {
            EventManager.SlotSelected.Listeners += OnSelectedSlot;
            EventManager.InventoryItemSelected.Listeners += OnInventoryItemSelected;
            EventManager.ItemDetached.Listeners += OnItemDetached;

            base.Show();

            ShowCurrentShip();
            PopulateInventoryScreen();

            VehicleAssemblyManager.instance.Prepare();
        }

        public override void Hide()
        {
            EventManager.SlotSelected.Listeners -= OnSelectedSlot;
            EventManager.InventoryItemSelected.Listeners -= OnInventoryItemSelected;
            EventManager.ItemDetached.Listeners -= OnItemDetached;

            base.Hide();
        }

        void Update()
        {
            if (!IsActive) return;

            DebugChangeShips();
        }

        private void DebugChangeShips()
        {
            bool changed = false;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                VehiclesManager.instance.PlayerCurrentShipID = "StarDart";
                changed = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                VehiclesManager.instance.PlayerCurrentShipID = "StarBug";
                changed = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                VehiclesManager.instance.PlayerCurrentShipID = "WuL0Wing";
                changed = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                VehiclesManager.instance.PlayerCurrentShipID = "XAT801";
                changed = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                VehiclesManager.instance.PlayerCurrentShipID = "828GEagle";
                changed = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                VehiclesManager.instance.PlayerCurrentShipID = "Harbinger";
                changed = true;
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                VehiclesManager.instance.PlayerCurrentShipID = "TheCorkscrew";
                changed = true;
            }

            if (changed)
            {
                RefreshInventory();
                ShowCurrentShip();
                VehicleAssemblyManager.instance.Prepare();
            }

        }

        private void ShowCurrentShip()
        {
            if (currentShownUI != null) Destroy(currentShownUI.gameObject);
            
            currentShownUI = (Instantiate(ShipsDatabase.instance.GetShip(VehiclesManager.instance.PlayerCurrentShipID).ShipUIObject.gameObject) as GameObject).GetComponent<ShipUI>();

            currentShownUI.gameObject.transform.SetParent(gameObject.transform);
            currentShownUI.gameObject.transform.position = shipUISpawnPlace.transform.position;
            currentShownUI.gameObject.transform.localScale = new Vector3(1, 1, 1);
            currentShownUI.gameObject.transform.SetSiblingIndex(1);

            shipNameText.text = ShipsDatabase.instance.GetShip(VehiclesManager.instance.PlayerCurrentShipID).ShipName;
        }

        #region Inventory

        private void PopulateInventoryScreen()
        {
            if (InventoryList.Count != 0)
            {
                var inventoryItems = InventoryList.ToArray();
                for (int i = 0; i < inventoryItems.Length; ++i)
                {
                    Destroy(inventoryItems[i].gameObject);
                }
                InventoryList.Clear();
            }
            foreach (var invent in InventoryManager.instance.PlayersAttachables)
            {
                InventoryUI uiElement = (Instantiate(inventoryUIPrefab) as GameObject).GetComponent<InventoryUI>();
                uiElement.gameObject.transform.SetParent(inventoryContainer.transform);
                uiElement.gameObject.transform.localScale = new Vector3(1, 1, 1);
                uiElement.SetUp(AttachablesDatabase.instance.GetAttachable(invent));
                InventoryList.Add(uiElement);
            }
        }

        private void EnableOnlyCorrectInventroy(AttachableType type)
        {
            foreach (var inventoryItem in InventoryList)
            {
                inventoryItem.gameObject.SetActive(inventoryItem.Item.Type == type);
            }
        }

        private void EnableAllInventory()
        {
            foreach (var inventoryItem in InventoryList)
            {
                inventoryItem.gameObject.SetActive(true);
            }
        }

        private void RefreshInventory()
        {
            foreach (var inventoryItem in InventoryList)
            {
                if (!inventoryItem.Installed) continue;

                inventoryItem.SetInstalled(false);
            }
        }

        private void MoveItemBackToInventory(Attachable item)
        {
            InventoryList.First(inventoryUI => inventoryUI.Item == item && inventoryUI.Installed == true).SetInstalled(false);
        }

        public void ToggleInventoryShowing()
        {
            inventoryShowing = !inventoryShowing;

            GetComponent<Animator>().Play((inventoryShowing ? "InventorySlideIn" : "InventorySlideOut"));
        }

        #endregion
    }
}
