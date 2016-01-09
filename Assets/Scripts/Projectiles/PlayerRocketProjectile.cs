using UnityEngine;
using Assets.Scripts.Enemy;

namespace Assets.Scripts.Projectiles
{
    public class PlayerRocketProjectile : PlayerProjectile
    {
        private Enemy.Enemy targetEnemy;

        public void SetUpTarget(Enemy.Enemy targetEnemy)
        {
            this.targetEnemy = targetEnemy;
        }

        protected override void Update()
        {
            if (targetEnemy != null)
            {
                var rotationToEnemy = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(targetEnemy.transform.position.y - gameObject.transform.position.y,targetEnemy.transform.position.x - gameObject.transform.position.x) * 180 / Mathf.PI));
                gameObject.transform.rotation = rotationToEnemy;
                //gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(gameObject.transform.rotation.eulerAngles.z , rotationToEnemy.eulerAngles.z, 0.1f));
            }
            base.Update();
        }
    }
}
