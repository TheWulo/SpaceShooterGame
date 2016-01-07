using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemyProjectile : MonoBehaviour
    {
        public float Speed;
        public int Damage;

        public void SetUp(int damage, float speed)
        {
            Speed = speed;
            Damage = damage;
        }

        void Update()
        {
            gameObject.transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
    }
}
