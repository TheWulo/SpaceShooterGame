using UnityEngine;
using Assets.Scripts.Interfaces;
using System;

namespace Assets.Scripts.Managers
{
    public class LevelManager : Singleton<LevelManager>, IInitializable
    {
        private bool isInitialized;

        [SerializeField]
        private GameObject LevelAthene1;
        [SerializeField]
        private GameObject LevelFelgor1;

        public GameObject CurrentLevel;

        public void Init()
        {
            EventManager.GameStarting.Listeners += OnGameStarting;
            EventManager.GameFinishing.Listeners += OnGameFinishing;

            isInitialized = true;
        }

        

        public bool IsInitialized()
        {
            return isInitialized;
        }
        
        private void OnGameStarting(EmptyEventArgs args)
        {
            int level = UnityEngine.Random.Range(0, 2);

            if (level == 0)
            {
                CurrentLevel = Instantiate(LevelAthene1);
            }
            else
            {
                CurrentLevel = Instantiate(LevelFelgor1);
            }
        }

        private void OnGameFinishing(EmptyEventArgs args)
        {
            Destroy(CurrentLevel.gameObject);
            CurrentLevel = null;
        }
    }
}
