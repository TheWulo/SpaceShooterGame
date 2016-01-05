using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Ship;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Managers
{
    public class VehiclesManager : Singleton<VehiclesManager>, IInitializable
    {
        public List<ShipBase> PlayerShips;
        public ShipBase PlayerShipCurrent;

        private bool isInitialized;

        #region IInitializable
        public void Init()
        {
            PlayerShips = new List<ShipBase>();

            DebugUnlockAllShips();

            PlayerShipCurrent = PlayerShips[1];

            isInitialized = true;
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }
        #endregion

        private void DebugUnlockAllShips()
        {
            foreach (var ship in ShipsDatabase.instance.GetAllShips())
            {
                PlayerShips.Add(ship);
            }
        }
    }
}
