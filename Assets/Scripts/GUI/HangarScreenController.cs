using System.Collections.Generic;
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
                EnableOnlyCorrectInventroy(args.SelectedSlot.Type);
                currentSelectedSlot = args.SelectedSlot;
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
            //currentSelectedSlot = null;
            //EnableAllInventory();
        }

        public void OnAssemblyFinished()
        {
            if (!VehiclesManager.instance.PlayerShipCurrent.IsReadyToFly())
            {
                Debug.Log("Ship not ready!");
                return;
            }
            EventManager.GameStarting.Invoke(new EmptyEventArgs());
            GUIManager.instance.ShowWindow(GUIWindowType.Play);
        }

        public override void Show()
        {
            EventManager.SlotSelected.Listeners += OnSelectedSlot;
            EventManager.InventoryItemSelected.Listeners += OnInventoryItemSelected;

            base.Show();

            ShowCurrentShip();
            PopulateInventoryScreen();

            VehicleAssemblyManager.instance.Prepare();
        }

        public override void Hide()
        {
            EventManager.SlotSelected.Listeners -= OnSelectedSlot;
            EventManager.InventoryItemSelected.Listeners -= OnInventoryItemSelected;

            base.Hide();
        }

        private void ShowCurrentShip()
        {
            if (currentShownUI != null) Destroy(currentShownUI.gameObject);
            
            currentShownUI = (Instantiate(ShipsDatabase.instance.GetShip(VehiclesManager.instance.PlayerCurrentShipID).ShipUIObject.gameObject) as GameObject).GetComponent<ShipUI>();

            currentShownUI.gameObject.transform.SetParent(gameObject.transform);
            currentShownUI.gameObject.transform.position = shipUISpawnPlace.transform.position;
            currentShownUI.gameObject.transform.localScale = new Vector3(1, 1, 1);
            currentShownUI.gameObject.transform.SetSiblingIndex(2);

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

        public void ToggleInventoryShowing()
        {
            inventoryShowing = !inventoryShowing;

            GetComponent<Animator>().Play((inventoryShowing ? "InventorySlideIn" : "InventorySlideOut"));
        }

        #endregion

        public void OnNextShipButton()
        {
            VehiclesManager.instance.ChangeShipToNext();
            ShowCurrentShip();
            PopulateInventoryScreen();

            VehicleAssemblyManager.instance.Prepare();
        }

        public void OnPrevShipButton()
        {
            VehiclesManager.instance.ChangeShipToPrevious();
            ShowCurrentShip();
            PopulateInventoryScreen();

            VehicleAssemblyManager.instance.Prepare();
        }

        public void DeselectSlot()
        {
            currentSelectedSlot = null;
            EnableAllInventory();
        }
    }
}
