using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Projectiles;

namespace Assets.Scripts.Enemy
{
    public enum AlienRace { Felgor, Athene, Cleethak, MetaCasyn, Reaplosos}

    public class Enemy : MonoBehaviour
    {
        [Header("Enemy Base")]
        public string EnemyID;
        public AlienRace EnemyRace;

        [Header("Enemy Stats: Defense")]
        public int EnemyHealthMax;
        public int EnemyHealth;
        public int EnemyArmor;

        [Header("Enemy Stats: Movement")]
        public int MovementSpeed;

        [Header("Enemy Stats: Offense")]
        public int AttackDamage;
        public float BulletSpeed;
        public int AttacksPerMinute;
        public Transform BulletSpawnPoint;
        public GameObject BulletPrefab;
        private float shootTimer;

        [Header("Enemy Stats: Others")]
        public int ScrapDropMin;
        public int ScrapDropMax;

        protected virtual void Update()
        {
            Move();
            Shoot();
        }

        protected virtual void Move()
        {
            gameObject.transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime / 100f, Space.World);
        }

        protected virtual void Shoot()
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= 60f / AttacksPerMinute)
            {
                shootTimer -= 60f / AttacksPerMinute;
                var bullet = Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletPrefab.transform.rotation) as GameObject;
                bullet.transform.SetParent(SceneContainer.instance.transform);
                bullet.GetComponent<EnemyProjectile>().SetUp(AttackDamage, BulletSpeed);
            }
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "PlayerProjectile")
            {
                TakeDamage(other.gameObject.GetComponent<Projectile>().Damage);
                Destroy(other.gameObject);
            }
        }

        protected void TakeDamage(int damage)
        {
            int finalDamage = damage - EnemyArmor;
            EnemyHealth -= finalDamage;
            if (EnemyHealth <= 0)
            {
                PlayerManager.instance.CollectedScrap += Random.Range(ScrapDropMin, ScrapDropMax);
                Die();
            }
        }

        protected void Die()
        {
            Destroy(gameObject);
        }
    }
}
