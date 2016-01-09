using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Projectiles;

namespace Assets.Scripts.Enemy
{
    public class FelgorElite : Enemy
    {
        protected override void Move()
        {
            if (gameObject.transform.position.x < 2) return;
            gameObject.transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime / 100f, Space.World);
        }

        protected override void Shoot()
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

                    var rotationToPlayer = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(bullet.transform.position.y - VehiclesManager.instance.PlayerShipCurrent.transform.position.y, bullet.transform.position.x - VehiclesManager.instance.PlayerShipCurrent.transform.position.x) * 180 / Mathf.PI));
                    bullet.transform.rotation = rotationToPlayer;
                }
            }
        }
    }
}
