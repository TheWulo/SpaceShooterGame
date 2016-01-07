using UnityEngine;
using System.Linq;

namespace Assets.Scripts.Ship
{
    public class WeaponsComponent : MonoBehaviour
    {
        private ShipBase controlledShip;

        void Start()
        {
            controlledShip = GetComponent<ShipBase>();
        }

        public void Prepare()
        {
            
        }

        public void ToggleAllWeapons()
        {
            controlledShip.WeaponSpots.ForEach(spot => spot.GetWeapon().ToggleFire());
        }
    }
}
