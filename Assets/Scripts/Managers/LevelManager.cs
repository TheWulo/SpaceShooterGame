using UnityEngine;
using Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Managers
{
    public class LevelManager : Singleton<LevelManager>, IInitializable
    {
        private bool isInitialized;

        [SerializeField]
        private List<GameObject> LevelsAthene;
        [SerializeField]
        private List<GameObject> LevelsFelgor;

        public GameObject CurrentLevel;

        public void Init()
        {
            EventManager.GameStarting.Listeners += OnGameStarting;
            EventManager.GameFinishing.Listeners += OnGameFinishing;
            EventManager.StageFinishing.Listeners += OnStageFinishing;

            isInitialized = true;
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }

        private void OnStageFinishing(EmptyEventArgs args)
        {
            if (CurrentLevel != null)
                Destroy(CurrentLevel.gameObject);
            CurrentLevel = null;
        }
        
        private void OnGameStarting(EmptyEventArgs args)
        {
            switch (StagesManager.instance.GetAlienRaceForStage(StagesManager.instance.CurrentStageID))
            {
                case Enemy.AlienRace.Athene:
                    CurrentLevel = Instantiate(LevelsAthene[0]) as GameObject;
                    break;
                case Enemy.AlienRace.Felgor:
                    CurrentLevel = Instantiate(LevelsFelgor[0]) as GameObject;
                    break;
            }
        }

        private void OnGameFinishing(EmptyEventArgs args)
        {
            if (CurrentLevel != null)
                Destroy(CurrentLevel.gameObject);
            CurrentLevel = null;
        }
    }
}
