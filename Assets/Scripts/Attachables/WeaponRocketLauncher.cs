using UnityEngine;
using Assets.Scripts.Enemy;
using Assets.Scripts.Managers;
using Assets.Scripts.Projectiles;

namespace Assets.Scripts.Attachables
{
    public class WeaponRocketLauncher : Weapon
    {
        public override void Shoot()
        {
            var projectile = (GameObject)Instantiate(BulletPrefab, BulletsSpawnPoint.transform.position, BulletsSpawnPoint.transform.rotation);
            projectile.GetComponent<Projectile>().SetUp(Damage, BulletSpeed, Vector3.right);
            projectile.transform.SetParent((SceneContainer.instance.gameObject.transform));
            ShootTimer = 0;

            projectile.GetComponent<PlayerRocketProjectile>().SetUpTarget(FindClosetEnemy());

            EventManager.WeaponFired.Invoke(new WeaponFiredEventArgs(this));
        }

        private Enemy.Enemy FindClosetEnemy()
        {
            if (SceneContainer.instance.GetAllEnemiesOnScene().Length == 0)
            {
                return null;
            }
            Enemy.Enemy closestEnemy = SceneContainer.instance.GetAllEnemiesOnScene()[0];

            foreach (var enemy in SceneContainer.instance.GetAllEnemiesOnScene())
            {
                if ((gameObject.transform.position - enemy.transform.position).magnitude < (gameObject.transform.position - closestEnemy.transform.position).magnitude)
                {
                    closestEnemy = enemy;
                }
            }
            return closestEnemy;
        }
    }
}
