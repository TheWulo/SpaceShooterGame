using UnityEngine;

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
                Instantiate(EnemyToSpawn, new Vector3(10, Random.Range(-5f, 5f), 0), EnemyToSpawn.transform.rotation);
            }
        }

        protected virtual void Update()
        {
            Spawn();
        }
    }
}
