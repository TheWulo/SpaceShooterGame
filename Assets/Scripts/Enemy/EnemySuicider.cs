using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Enemy
{
    public class EnemySuicider : Enemy
    {
        protected override void Update()
        {
            Move();
        }

        protected override void Move()
        {
            gameObject.transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime / 100f, Space.World);
            
            if (gameObject.transform.position.y < VehiclesManager.instance.PlayerShipCurrent.transform.position.y)
            {
                gameObject.transform.Translate(Vector3.up * MovementSpeed * Time.deltaTime / 1000f, Space.World);
            }
            if (gameObject.transform.position.y > VehiclesManager.instance.PlayerShipCurrent.transform.position.y)
            {
                gameObject.transform.Translate(Vector3.down * MovementSpeed * Time.deltaTime / 1000f, Space.World);
            }
        }
    }
}
