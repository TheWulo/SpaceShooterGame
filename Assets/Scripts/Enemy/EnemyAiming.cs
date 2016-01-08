using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Enemy
{
    public class EnemyAiming : Enemy
    {
        protected override void Shoot()
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= 60f / AttacksPerMinute)
            {
                shootTimer -= 60f / AttacksPerMinute;
                var bullet = Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletPrefab.transform.rotation) as GameObject;
                bullet.transform.SetParent(SceneContainer.instance.transform);
                bullet.GetComponent<EnemyProjectile>().SetUp(AttackDamage, BulletSpeed);

                var rotationToPlayer = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(bullet.transform.position.y - VehiclesManager.instance.PlayerShipCurrent.transform.position.y, bullet.transform.position.x - VehiclesManager.instance.PlayerShipCurrent.transform.position.x) * 180 / Mathf.PI));
                bullet.transform.rotation = rotationToPlayer;
            }
        }
    }
}
