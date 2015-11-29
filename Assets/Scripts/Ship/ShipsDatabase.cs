using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Interfaces;
using System.Linq;

namespace Assets.Scripts.Ship
{
    class ShipsDatabase : Singleton<ShipsDatabase>, IInitializable
    {
        private List<ShipBase> Ships;

        private bool isInitialized;

        #region IInitializable
        public void Init()
        {
            Ships = new List<ShipBase>();
            foreach (var attach in GetComponentsInChildren<ShipBase>())
            {
                Ships.Add(attach);
            }

            isInitialized = true;
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }
        #endregion

        public ShipBase GetShip(string id)
        {
            return Ships.First(ship => ship.ShipID == id);
        }

        public ShipBase[] GetAllShips()
        {
            return Ships.ToArray();
        }
    }
}
