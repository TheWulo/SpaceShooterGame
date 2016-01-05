using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.GUI;

namespace Assets.Scripts.Ship
{
    public class ShipBase : MonoBehaviour
    {
        [Header("Ship Base")]
        public string ShipID;
        public string ShipName;

        public int Health;
        public int Agility;
        public int Energy;

        private int currentHealth;
        private float currentAgility;
        private int currentEnergy;


        [Header("Ship Slots")]
        public List<WeaponSpot> WeaponSpots;
        public List<SupportSpot> SupportSpots;
        public List<EngineSpot> EngineSpots;

        [Header("Ship UI")]
        public ShipUI ShipUIObject;
    }
}
