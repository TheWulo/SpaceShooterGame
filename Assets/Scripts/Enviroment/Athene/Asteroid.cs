using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Enviroment.Athene
{
    public class Asteroid : MonoBehaviour
    {
        public List<GameObject> AsteroidsToDrop;
        public float secondAsteroidSpawnDistance;

        public float movementSpeed;

        protected void Update()
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime / 100.0f, Space.Self);
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "PlayerProjectile")
            {
                foreach (var asteroid in AsteroidsToDrop)
                {
                    var randomAddPosition = Random.insideUnitSphere * secondAsteroidSpawnDistance;
                    randomAddPosition.z = 0;

                    Instantiate(asteroid, gameObject.transform.position + randomAddPosition, gameObject.transform.rotation * Quaternion.Euler(0,0,Random.Range(-15,15)));
                }
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
