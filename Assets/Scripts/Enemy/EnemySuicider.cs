using UnityEngine;
using Assets.Scripts.Managers;
using Assets.Scripts.Ship;

namespace Assets.Scripts.Enemy
{
    public class EnemySuicider : Enemy
    {
        public float VerticalMovementSpeed = 1.0f;

        protected override void Update()
        {
            Move();
        }

        protected override void Move()
        {
            gameObject.transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime / 100f, Space.World);
            
            if (gameObject.transform.position.y < VehiclesManager.instance.PlayerShipCurrent.transform.position.y)
            {
                gameObject.transform.Translate(Vector3.up * MovementSpeed * Time.deltaTime / 1000f * VerticalMovementSpeed, Space.World);
            }
            if (gameObject.transform.position.y > VehiclesManager.instance.PlayerShipCurrent.transform.position.y)
            {
                gameObject.transform.Translate(Vector3.down * MovementSpeed * Time.deltaTime / 1000f * VerticalMovementSpeed, Space.World);
            }
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);

            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<ShipBase>().TakeDamage(AttackDamage);
                Destroy(gameObject);
            }
        }
    }
}
