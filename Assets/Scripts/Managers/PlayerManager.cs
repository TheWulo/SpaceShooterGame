using UnityEngine;
using Assets.Scripts.Interfaces;
using System;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Managers
{
    public class PlayerManager : Singleton<PlayerManager>, IInitializable
    {
        private bool isInitialized;

        public int CollectedScrap = 0;

        public void Init()
        {
            EventManager.GameStarting.Listeners += OnGameStarting;
            isInitialized = true;
        }

        private void OnGameStarting(EmptyEventArgs args)
        {
            VehiclesManager.instance.PlayerShipCurrent.PrepareShipForLaunch();
        }

        public bool IsInitialized()
        {
            return isInitialized;
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
    }
}
