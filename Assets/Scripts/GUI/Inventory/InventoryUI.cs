using UnityEngine;
using Assets.Scripts.Attachables;
using Assets.Scripts.Managers;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Inventory
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

        private AttachableType Type;

        public void SetUp(Attachable attach)
        {
            Type = attach.Type;
            AttachableImage.sprite = attach.PresentationSprite;
            AttachableText.text = attach.AttachableName;

            switch (Type)
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


    }
}
