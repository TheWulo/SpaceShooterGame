using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Enemy.Projectiles
{
    public class AtheneElite : Enemy
    {
        private float weapon1TimerCurrent;
        private float weapon2TimerCurrent;
        private float weapon3TimerCurrent;

        public Transform Weapon1SpawnSpot;
        public Transform Weapon2SpawnSpot;
        public Transform Weapon3SpawnSpot;

        void Start()
        {
            weapon1TimerCurrent = 0;
            weapon2TimerCurrent = 60f / AttacksPerMinute / 3;
            weapon3TimerCurrent = 60f / AttacksPerMinute / 3 * 2;
        }

        protected override void Shoot()
        {
            weapon1TimerCurrent += Time.deltaTime;
            weapon2TimerCurrent += Time.deltaTime;
            weapon3TimerCurrent += Time.deltaTime;

            if (weapon1TimerCurrent >= 60f / AttacksPerMinute)
            {
                weapon1TimerCurrent -= 60f / AttacksPerMinute;
                var bullet = Instantiate(BulletPrefab, Weapon1SpawnSpot.position, BulletPrefab.transform.rotation) as GameObject;
                bullet.transform.SetParent(SceneContainer.instance.transform);
                bullet.GetComponent<EnemyProjectile>().SetUp(AttackDamage, BulletSpeed);

                var rotationToPlayer = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(bullet.transform.position.y - VehiclesManager.instance.PlayerShipCurrent.transform.position.y, bullet.transform.position.x - VehiclesManager.instance.PlayerShipCurrent.transform.position.x) * 180 / Mathf.PI));
                bullet.transform.rotation = rotationToPlayer;
            }

            if (weapon2TimerCurrent >= 60f / AttacksPerMinute)
            {
                weapon2TimerCurrent -= 60f / AttacksPerMinute;
                var bullet = Instantiate(BulletPrefab, Weapon2SpawnSpot.position, BulletPrefab.transform.rotation) as GameObject;
                bullet.transform.SetParent(SceneContainer.instance.transform);
                bullet.GetComponent<EnemyProjectile>().SetUp(AttackDamage, BulletSpeed);

                var rotationToPlayer = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(bullet.transform.position.y - VehiclesManager.instance.PlayerShipCurrent.transform.position.y, bullet.transform.position.x - VehiclesManager.instance.PlayerShipCurrent.transform.position.x) * 180 / Mathf.PI));
                bullet.transform.rotation = rotationToPlayer;
            }

            if (weapon3TimerCurrent >= 60f / AttacksPerMinute)
            {
                weapon3TimerCurrent -= 60f / AttacksPerMinute;
                var bullet = Instantiate(BulletPrefab, Weapon3SpawnSpot.position, BulletPrefab.transform.rotation) as GameObject;
                bullet.transform.SetParent(SceneContainer.instance.transform);
                bullet.GetComponent<EnemyProjectile>().SetUp(AttackDamage, BulletSpeed);

                var rotationToPlayer = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(bullet.transform.position.y - VehiclesManager.instance.PlayerShipCurrent.transform.position.y, bullet.transform.position.x - VehiclesManager.instance.PlayerShipCurrent.transform.position.x) * 180 / Mathf.PI));
                bullet.transform.rotation = rotationToPlayer;
            }
        }

        protected override void Move()
        {
            if (gameObject.transform.position.x < 2) return;
            gameObject.transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime / 100f, Space.World);
        }
    }
}
