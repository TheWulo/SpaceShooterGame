using UnityEngine;
using Assets.Scripts.Interfaces;
using System;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Managers
{
    public class PlayerManager : Singleton<PlayerManager>, IInitializable
    {
        private bool isInitialized;

        public int CollectedScrap;

        public void Init()
        {
            EventManager.GameStarting.Listeners += OnGameStarting;
            EventManager.ScrapMetalCollected.Listeners += OnScrapMetalCollected;
            EventManager.GameFinishing.Listeners += OnGameFinishing;

            if (PlayerPrefs.HasKey("CollectedScrap"))
            {

                CollectedScrap = PlayerPrefs.GetInt("CollectedScrap");
            }
            else
            {
                CollectedScrap = 0;
            }

            isInitialized = true;
        }

        private void OnGameFinishing(EmptyEventArgs args)
        {
            PlayerPrefs.SetInt("CollectedScrap", CollectedScrap);
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }

        private void OnScrapMetalCollected(ScrapMetalCollectedEventArgs args)
        {
            CollectedScrap += args.ScrapMetal.ScrapAmount;
        }

        private void OnGameStarting(EmptyEventArgs args)
        {
            VehiclesManager.instance.PlayerShipCurrent.PrepareShipForLaunch();
        }

        void Update()
        {
            HandleInput();
        }

        #region Input
        void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                VehiclesManager.instance.PlayerShipCurrent.WeaponComponent.ToggleAllWeapons();
            }
        }
        #endregion

        public void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
