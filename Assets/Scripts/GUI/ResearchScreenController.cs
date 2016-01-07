using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Research;
using System.Linq;
using Assets.Scripts.Managers;
using System;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class ResearchScreenController : GUIWindow
    {
        [SerializeField]
        private GameObject WeaponsTab;
        [SerializeField]
        private GameObject EngineTab;
        [SerializeField]
        private GameObject SupportTab;
        [SerializeField]
        private Text ScrapAmount;

        [SerializeField]
        private List<ResearchObjectUI> weaponResearchObjectsUI;
        [SerializeField]
        private List<ResearchObjectUI> engineResearchObjectsUI;
        [SerializeField]
        private List<ResearchObjectUI> supportResearchObjectsUI;

        void Awake()
        {
            EventManager.AttachableResearched.Listeners += OnAttachableResearched;
        }

        private void OnAttachableResearched(AttachableResearchedEventArgs args)
        {
            RefreshWindow();
        }

        public override void Show()
        {
            RefreshWindow();

            OnWeaponTabButton();

            base.Show();
        }

        public void OnWeaponTabButton()
        {
            WeaponsTab.SetActive(true);
            EngineTab.SetActive(false);
            SupportTab.SetActive(false);
        }
        public void OnEngineTabButton()
        {
            WeaponsTab.SetActive(false);
            EngineTab.SetActive(true);
            SupportTab.SetActive(false);
        }

        public void OnSupportTabButton()
        {
            WeaponsTab.SetActive(false);
            EngineTab.SetActive(false);
            SupportTab.SetActive(true);
        }

        void RefreshWindow()
        {
            weaponResearchObjectsUI.ForEach(obj => obj.SetUp());
            weaponResearchObjectsUI.ForEach(obj => obj.CheckIfAvailable());
            engineResearchObjectsUI.ForEach(obj => obj.SetUp());
            engineResearchObjectsUI.ForEach(obj => obj.CheckIfAvailable());
            supportResearchObjectsUI.ForEach(obj => obj.SetUp());
            supportResearchObjectsUI.ForEach(obj => obj.CheckIfAvailable());

            ScrapAmount.text = "Scrap: " + PlayerManager.instance.CollectedScrap;
        }
    }
}
