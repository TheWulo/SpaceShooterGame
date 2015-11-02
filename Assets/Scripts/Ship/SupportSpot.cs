using Assets.Scripts.Attachables;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class SupportSpot : AttachableSpot
    {
        public Support GetSupport()
        {
            if (HasElement)
            {
                return (Support)attachedElement;
            }
            Debug.LogWarning("Tried to access empty AttachableSpot");
            return null;
        }
    }
}
