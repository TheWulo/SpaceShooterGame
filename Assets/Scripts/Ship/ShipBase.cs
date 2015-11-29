using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Ship
{
    public class ShipBase : MonoBehaviour
    {
        public string ShipID;

        public int Health;
        public int Agility;
        public int Energy;

        private int currentHealth;
        private float currentAgility;
        private int currentEnergy;

        public List<WeaponSpot> WeaponSpots;
        public List<SupportSpot> SupportSpots;
        public List<EngineSpot> EngineSPots;

        public GameObject ShipUI;
    }
}
