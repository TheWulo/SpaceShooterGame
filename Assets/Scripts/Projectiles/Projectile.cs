using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public int Damage;
        public float Speed;

        private float lifeTimer = 10;

        void Update()
        {
            gameObject.transform.Translate(Vector3.right * Speed * Time.deltaTime);

            lifeTimer -= Time.deltaTime;
            if (lifeTimer < 0)
            {
                Destroy(gameObject);
            }
        }

        public void SetUp(int damage, float speed)
        {
            Speed = speed;
            Damage = damage;
        }
    }
}
