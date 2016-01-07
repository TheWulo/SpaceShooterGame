using UnityEngine;
using Assets.Scripts.Interfaces;
using System;

namespace Assets.Scripts.Managers
{
    public class LevelManager : Singleton<LevelManager>, IInitializable
    {
        private bool isInitialized;

        [SerializeField]
        private GameObject Level1;

        public GameObject CurrentLevel;

        public void Init()
        {
            EventManager.GameStarting.Listeners += OnGameStarting;
            EventManager.GameFinishing.Listeners += OnGameFinishing;
            EventManager.EnemySpawned.Listeners += OnEnemySpawned;

            isInitialized = true;
        }

        

        public bool IsInitialized()
        {
            return isInitialized;
        }
        
        private void OnGameStarting(EmptyEventArgs args)
        {
            CurrentLevel = Instantiate(Level1);
        }

        private void OnGameFinishing(EmptyEventArgs args)
        {
            Destroy(CurrentLevel.gameObject);
            CurrentLevel = null;
        }

        private void OnEnemySpawned(EnemySpawnedEventArgs args)
        {
            args.SpawnedEnemy.transform.SetParent(CurrentLevel.transform);
        }
    }
}
