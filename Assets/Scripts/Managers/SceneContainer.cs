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
            EventManager.StageFinishing.Listeners += OnStageFinishing;
        }

        private void OnStageFinishing(EmptyEventArgs args)
        {
            ClearAll();
        }

        private void OnGameFinishing(EmptyEventArgs args)
        {
            ClearAll();
        }

        private void OnEnemySpawned(EnemySpawnedEventArgs args)
        {
            args.SpawnedEnemy.transform.SetParent(EnemiesContainer);
        }

        public void ClearAll()
        {
            ClearAllEnemies();
            var childrenToAbort = gameObject.GetComponentsInChildren<Transform>();
            for (int i = 0; i < childrenToAbort.Length; ++i)
            {
                if (childrenToAbort[i] != this.transform && childrenToAbort[i] != EnemiesContainer.transform)
                {
                    Destroy(childrenToAbort[i].gameObject);
                }
            }
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
