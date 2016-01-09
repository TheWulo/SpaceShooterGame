using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Projectiles;
using System.Collections.Generic;
using Assets.Scripts.Scrap;

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
        public List<Transform> BulletSpawnPoint;
        public GameObject BulletPrefab;
        protected float shootTimer;

        [Header("Enemy Stats: Others")]
        public int ScrapDroppedInstances = 1;
        public int ScrapDropMin;
        public int ScrapDropMax;
        public GameObject ScrapPrefab;

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
            if (gameObject.transform.position.x <= -3) return;

            shootTimer += Time.deltaTime;
            if (shootTimer >= 60f / AttacksPerMinute)
            {
                shootTimer -= 60f / AttacksPerMinute;
                foreach (var spawnPoint in BulletSpawnPoint)
                {
                    var bullet = Instantiate(BulletPrefab, spawnPoint.position, BulletPrefab.transform.rotation) as GameObject;
                    bullet.transform.SetParent(SceneContainer.instance.transform);
                    bullet.GetComponent<Projectile>().SetUp(AttackDamage, BulletSpeed, Vector3.left);
                }
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "PlayerProjectile")
            {
                TakeDamage(other.gameObject.GetComponent<Projectile>().Damage, other.GetComponent<PlayerProjectile>().ProjectileType);
                Destroy(other.gameObject);
            }
        }

        protected virtual void TakeDamage(int damage, PlayerProjectileType type)
        {
            int finalDamage = damage;

            if (type != PlayerProjectileType.Laser) finalDamage -= EnemyArmor; //Laser goes through Armor

            if (finalDamage < 0) finalDamage = 0;
  
            EnemyHealth -= finalDamage;
            if (EnemyHealth <= 0)
            {
                PlayerManager.instance.CollectedScrap += Random.Range(ScrapDropMin, ScrapDropMax);
                Die();
            }
        }

        protected virtual void Die()
        {
            DropScrap();
            Destroy(gameObject);
        }

        protected virtual void DropScrap()
        {
            for (int i = 0; i < ScrapDroppedInstances; i++)
            {
                int scrapAmount = Random.Range(ScrapDropMin, ScrapDropMax);
                if (scrapAmount == 0) return;

                var randomAddPosition = Random.insideUnitSphere * 0.2f;
                randomAddPosition.z = 0;

                var scrap = Instantiate(ScrapPrefab, gameObject.transform.position + randomAddPosition, gameObject.transform.rotation * Quaternion.Euler(0, 0, 90 + Random.Range(-15, 15))) as GameObject;
                scrap.transform.SetParent(LevelManager.instance.CurrentLevel.transform);
                scrap.GetComponent<ScrapMetal>().SetUp(scrapAmount);
            }
        }
    }
}
