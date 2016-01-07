using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        protected GameObject EnemyToSpawn;

        public float spawnsPerMinute;
        protected float spawnTimer;

        protected virtual void Spawn()
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= 60f / spawnsPerMinute)
            {
                spawnTimer -= 60f / spawnsPerMinute;
                var enemy = Instantiate(EnemyToSpawn, new Vector3(5, Random.Range(-2f, 2f), 0), EnemyToSpawn.transform.rotation) as GameObject;
                EventManager.EnemySpawned.Invoke(new EnemySpawnedEventArgs(enemy.GetComponent<Enemy>()));
            }
        }

        protected virtual void Update()
        {
            Spawn();
        }
    }
}
