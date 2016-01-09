using UnityEngine;

namespace Assets.Scripts.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public int Damage;
        public float Speed;
        public Vector3 Direction;

        private float lifeTimer = 10;

        protected virtual void Update()
        {
            Move();
            DestroyAfterTime();
        }

        protected virtual void Move()
        {
            gameObject.transform.Translate(Direction * Speed * Time.deltaTime);
        }

        protected virtual void DestroyAfterTime()
        {
            lifeTimer -= Time.deltaTime;
            if (lifeTimer < 0)
            {
                Destroy(gameObject);
            }
        }

        public virtual void SetUp(int damage, float speed, Vector3 direction)
        {
            Speed = speed;
            Damage = damage;
            Direction = direction;
        }
    }
}
