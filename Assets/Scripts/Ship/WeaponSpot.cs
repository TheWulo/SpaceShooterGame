using Assets.Scripts.Attachables;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class WeaponSpot : AttachableSpot
    {
        public Weapon GetWeapon()
        {
            if (HasElement)
            {
                return (Weapon)attachedElement;
            }
            Debug.LogWarning("Tried to access empty AttachableSpot");
            return null;
        }
    }
}
