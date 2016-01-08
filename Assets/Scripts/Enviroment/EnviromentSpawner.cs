using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Enviroment
{
    public class EnviromentSpawner : MonoBehaviour
    {
        [SerializeField]
        protected GameObject ObjectToSpawn;

        public float spawnsPerMinute;
        protected float spawnTimer;

        protected virtual void Spawn()
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= 60f / spawnsPerMinute)
            {
                spawnTimer -= 60f / spawnsPerMinute;
                var spawnedObject = Instantiate(ObjectToSpawn, new Vector3(5, Random.Range(-2f, 2f), 0), ObjectToSpawn.transform.rotation) as GameObject;
                spawnedObject.transform.SetParent(LevelManager.instance.CurrentLevel.transform);
            }
        }

        protected virtual void Update()
        {
            Spawn();
        }
    }
}
