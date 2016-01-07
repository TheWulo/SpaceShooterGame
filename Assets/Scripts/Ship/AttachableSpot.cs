using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Attachables;

namespace Assets.Scripts.Ship
{
    public class AttachableSpot : MonoBehaviour
    {
        public int AttachableID;
        [SerializeField]
        protected Attachable attachedElement;
        public bool HasElement { get { return attachedElement != null; } }

        public void Attach(Attachable element)
        {
            attachedElement = element;
            element.transform.position = gameObject.transform.position;
        }

        public void Detach()
        {
            if (HasElement)
            {
                Destroy(attachedElement.gameObject);
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
