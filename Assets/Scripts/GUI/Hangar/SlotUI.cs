using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Attachables;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class SlotUI : MonoBehaviour
    {
        public int SlotID;
        [SerializeField]
        private Image SpotImage;
        [SerializeField]
        private Image AttachableImage;

        public Attachable attachedItem;

        public AttachableType Type;

        public void OnClicked()
        {
            EventManager.SlotSelected.Invoke(new SlotSelectedEventArgs(this));
        }

        public void AttachItem(Attachable itemToBeAttached)
        {
            attachedItem = itemToBeAttached;
            AttachableImage.sprite = itemToBeAttached.PresentationSprite;
            AttachableImage.enabled = true;
            SpotImage.enabled = false;
            EventManager.ItemAttached.Invoke(new ItemAttachedEventArgs(attachedItem));
        }

        public void DetachItem()
        {
            EventManager.ItemDetached.Invoke(new ItemDetachedEventArgs(attachedItem));
            attachedItem = null;
            AttachableImage.enabled = false;
            SpotImage.enabled = true;
        }
    }
}
