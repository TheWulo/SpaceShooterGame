using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.GUI.Inventory;

namespace Assets.Scripts.GUI
{
    public class HangarScreenController : GUIWindow
    {
        //SHIPS
        [SerializeField]
        private List<GameObject> ShipsUIPrefabs;
        [SerializeField]
        private GameObject shipUISpawnPlace;

        //INVENTORY
        [SerializeField]
        private GameObject inventoryContainer;
        [SerializeField]
        private GameObject inventoryUIPrefab;

        private GameObject currentShownUI;

        public override void Show()
        {
            base.Show();

            ShowCurrentShip();
            PopulateInventoryScreen();
        }

        void Update()
        {
            DebugChangeShips();
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

            currentShownUI = (Instantiate(VehiclesManager.instance.PlayerShipCurrent.ShipUI) as GameObject);

            currentShownUI.gameObject.transform.SetParent(gameObject.transform);
            currentShownUI.gameObject.transform.position = shipUISpawnPlace.transform.position;
            currentShownUI.gameObject.transform.localScale = new Vector3(1, 1, 1);
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
    }
}
