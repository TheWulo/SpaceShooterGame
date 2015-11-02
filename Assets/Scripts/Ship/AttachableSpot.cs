using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Attachables;

namespace Assets.Scripts.Ship
{
    public class AttachableSpot : MonoBehaviour
    {
        protected Attachable attachedElement;
        public bool HasElement { get { return attachedElement != null; } }

        public void Attach(Attachable element)
        {
            attachedElement = element;
        }

        public void Detach()
        {
            if (HasElement)
            {
                Destroy(attachedElement);
                attachedElement = null;
            }
        }

        public Attachable GetElement()
        {
            if (HasElement)
            {
                return attachedElement;
            }
            Debug.LogWarning("Tried to access empty AttachableSpot");
            return null;
        }
    }
}
