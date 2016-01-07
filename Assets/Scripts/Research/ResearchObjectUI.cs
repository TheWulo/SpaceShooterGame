using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Attachables;
using System.Collections.Generic;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Research
{
    public enum ResearchObjectState { Unlocked, Available, Locked }

    public class ResearchObjectUI : MonoBehaviour
    {
        [Header("Research Object")]
        public string AttachableID;
        public ResearchObjectState State;
        [SerializeField]
        private List<ResearchObjectUI> requiredResearchedObjects;

        [Header("Research Object UI Elements")]
        [SerializeField]
        private Image UnlockableImage;
        [SerializeField]
        private Text UnlockableName;
        [SerializeField]
        private Text UnlockablePrice;

        public void SetUp()
        {
            var attach = AttachablesDatabase.instance.GetAttachable(AttachableID);
            UnlockableImage.sprite = attach.PresentationSprite;
            UnlockableName.text = attach.AttachableName;
            if (ResearchManager.instance.IsAttachableUnlocked(AttachableID))
            {
                UnlockablePrice.text = "UNLOCKED!";
                State = ResearchObjectState.Unlocked;
                GetComponent<Button>().interactable = true;
                GetComponent<Image>().color = new Color(1, 1, 0, 1);
            }
            else
            {
                UnlockablePrice.text = "Cost: " + attach.ScrapCost;
                State = ResearchObjectState.Locked;
                GetComponent<Button>().interactable = false;
            }

        }

        public void CheckIfAvailable()
        {
            if (State != ResearchObjectState.Locked) return;

            bool result = true;
            foreach (var researchObj in requiredResearchedObjects)
            {
                if (researchObj.State != ResearchObjectState.Unlocked)
                {
                    result = false;
                }
            }
            if (result)
            {
                State = ResearchObjectState.Available;
                GetComponent<Button>().interactable = true;
            }
        }

        public void TryUnlock()
        {
            if (State == ResearchObjectState.Available)
                ResearchManager.instance.TryUnlock(AttachableID);
        }
    }
}
