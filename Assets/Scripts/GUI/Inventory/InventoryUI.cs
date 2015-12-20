using UnityEngine;
using Assets.Scripts.Attachables;
using Assets.Scripts.Managers;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField]
        private Sprite WeaponTypeSprite;
        [SerializeField]
        private Sprite EngineTypeSprite;
        [SerializeField]
        private Sprite SupportTypeSprite;

        [SerializeField]
        private Image BackgroundSprite;
        [SerializeField]
        private Image AttachableImage;
        [SerializeField]
        private Text AttachableText;
        [SerializeField]
        private Text InstalledText;

        public Attachable Item;

        public bool Installed;

        public void SetUp(Attachable attach)
        {
            Item = attach;
            AttachableImage.sprite = attach.PresentationSprite;
            AttachableText.text = attach.AttachableName;

            switch (attach.Type)
            {
                case AttachableType.Weapon:
                    BackgroundSprite.sprite = WeaponTypeSprite;
                    break;
                case AttachableType.Engine:
                    BackgroundSprite.sprite = EngineTypeSprite;
                    break;
                case AttachableType.Support:
                    BackgroundSprite.sprite = SupportTypeSprite;
                    break;
            }
        }

        public void OnClick()
        {
            EventManager.InventoryItemSelected.Invoke(new InventoryItemSelectedEventArgs(this, Item));
        }

        public void SetInstalled(bool isInstalled)
        {
            Installed = isInstalled;
            InstalledText.enabled = Installed;
            GetComponent<Button>().interactable = !Installed;
        }
    }
}
