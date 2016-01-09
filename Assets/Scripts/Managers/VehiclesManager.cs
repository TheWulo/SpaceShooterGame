using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Ship;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Managers
{
    public class VehiclesManager : Singleton<VehiclesManager>, IInitializable
    {
        private List<string> PlayerShips;
        public string PlayerCurrentShipID;
        public ShipBase PlayerShipCurrent;

        private bool isInitialized;

        #region IInitializable
        public void Init()
        {
            EventManager.GameFinishing.Listeners += OnGameFinishing;

            PlayerShips = new List<string>();

            LoadShipsFromPlayerPrefs();

            PlayerCurrentShipID = "StarDart";

            isInitialized = true;
        }

        private void OnGameFinishing(EmptyEventArgs args)
        {
            //Destroy(PlayerShipCurrent.gameObject);
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }
        #endregion

        public bool TryUnlockNewShip(string shipID)
        {
            foreach (var unlockedShip in PlayerShips)
            {
                if (shipID == unlockedShip)
                {
                    Debug.Log("Ship Already Unloced!");
                    return false;
                }
            }

            if (ShipsDatabase.instance.GetShip(shipID).ScrapCost <= PlayerManager.instance.CollectedScrap)
            {
                PlayerManager.instance.CollectedScrap -= ShipsDatabase.instance.GetShip(shipID).ScrapCost;
                PlayerShips.Add(shipID);
                EventManager.ShipResearched.Invoke(new ShipResearchedEventArgs(shipID));
                SaveShipsToPlayerPrefs();
            }
            return true;
        }

        void SaveShipsToPlayerPrefs()
        {
            foreach (var unlockedShip in PlayerShips)
            {
                PlayerPrefs.SetInt(unlockedShip + "_unlocked", 1);
            }
        }

        void LoadShipsFromPlayerPrefs()
        {
            foreach (var ship in ShipsDatabase.instance.GetAllShips())
            {
                if (PlayerPrefs.HasKey(ship.ShipID + "_unlocked"))
                {
                    PlayerShips.Add(ship.ShipID);
                }
            }
            if (PlayerShips.Count == 0) //No unlocked Ships
            {
                PlayerShips.Add("StarDart");
                SaveShipsToPlayerPrefs();
            }
        }

        public void ChangeShipToNext()
        {
            for (int i = 0; i < PlayerShips.Count; i++)
            {
                if (PlayerShips[i] == PlayerCurrentShipID && i != PlayerShips.Count - 1 )
                {
                    PlayerCurrentShipID = PlayerShips[i + 1];
                    return;
                }
            }
        }

        public void ChangeShipToPrevious()
        {
            for (int i = 0; i < PlayerShips.Count; i++)
            {
                if (PlayerShips[i] == PlayerCurrentShipID && i != 0)
                {
                    PlayerCurrentShipID = PlayerShips[i - 1];
                    return;
                }
            }
        }

        public bool IsShipUnlocked(string shipID)
        {
            foreach (var ship in PlayerShips)
            {
                if (ship == shipID) return true;
            }
            return false;
        }
    }
}
