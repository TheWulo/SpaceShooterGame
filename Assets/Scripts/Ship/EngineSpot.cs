using Assets.Scripts.Attachables;
using UnityEngine;

namespace Assets.Scripts.Ship
{
    public class EngineSpot : AttachableSpot
    {
        public Engine GetEngine()
        {
            if (HasElement)
            {
                return (Engine)attachedElement;
            }
            Debug.LogWarning("Tried to access empty AttachableSpot");
            return null;
        }
    }
}
