using UnityEngine;
using Assets.Scripts.Projectiles;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Attachables
{
    public class Weapon : Attachable
    {
        [Header("Weapon")]
        public int Damage;
        public int ShotsPerMinute;
        public float BulletSpeed;
        public bool ToggleFireOn;

        private float ShootTimer;

        [Header("Bullets")]
        public GameObject BulletPrefab;
        public GameObject BulletsSpawnPoint;

        public virtual void Shoot()
        {
            var projectile = (GameObject)Instantiate(BulletPrefab, BulletsSpawnPoint.transform.position, BulletsSpawnPoint.transform.rotation);
            projectile.GetComponent<Projectile>().SetUp(Damage, BulletSpeed);
            projectile.transform.SetParent((SceneContainer.instance.gameObject.transform));
            ShootTimer = 0;
        }

        void Update()
        {
            ShootTimer += Time.deltaTime;
            if (ToggleFireOn)
            {
                if (ShootTimer >= 60f / ShotsPerMinute)
                {
                    Shoot();
                }
            }
        }

        public void ToggleFire()
        {
            ToggleFireOn = !ToggleFireOn;
        }
    }
}
