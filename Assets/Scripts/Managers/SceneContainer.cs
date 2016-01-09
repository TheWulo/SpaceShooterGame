using UnityEngine;
using Assets.Scripts.Enemy;

namespace Assets.Scripts.Managers
{
    public class SceneContainer : Singleton<SceneContainer>
    {
        public Transform EnemiesContainer;

        void Start()
        {
            EventManager.EnemySpawned.Listeners += OnEnemySpawned;
            EventManager.GameFinishing.Listeners += OnGameFinishing;
        }

        private void OnGameFinishing(EmptyEventArgs args)
        {
            ClearAllEnemies();
        }

        private void OnEnemySpawned(EnemySpawnedEventArgs args)
        {
            args.SpawnedEnemy.transform.SetParent(EnemiesContainer);
        }

        public void ClearAll()
        {
            ClearAllEnemies();
        }

        public Enemy.Enemy[] GetAllEnemiesOnScene()
        {
            return EnemiesContainer.GetComponentsInChildren<Enemy.Enemy>();
        }

        public void ClearAllEnemies()
        {
            var enemiesArray = EnemiesContainer.GetComponentsInChildren<Transform>();

            for(int i =0 ; i < enemiesArray.Length; ++i)
            {
                if (enemiesArray[i] != EnemiesContainer.transform)
                {
                    Destroy(enemiesArray[i].gameObject);
                }
            }
        }
    }
}
