using UnityEngine;
using System.Linq;

namespace Assets.Scripts.Ship
{
    public class MotorComponent : MonoBehaviour
    {
        public float MovementSpeed;
        public float Evasion;

        private ShipBase controlledShip;

        void Start()
        {
            controlledShip = GetComponent<ShipBase>();
        }

        void Update()
        {
            HandleMovement();
        }

        public void Prepare()
        {
            MovementSpeed = 0;
            controlledShip.EngineSpots.ForEach(spot => MovementSpeed += spot.GetEngine().Speed);
            Evasion = 0;
            controlledShip.EngineSpots.ForEach(spot => Evasion += spot.GetEngine().Evasion);
            if (Evasion > 60) Evasion = 60;
        }

        void HandleMovement()
        {
            Vector3 targetPosition = Vector3.zero;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                targetPosition += Vector3.up;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                targetPosition += Vector3.down;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                targetPosition += Vector3.left;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                targetPosition += Vector3.right;
            }
            if (targetPosition != Vector3.zero)
            {
                gameObject.transform.Translate(targetPosition.normalized * MovementSpeed * Time.deltaTime / 100.0f);
            }
        }
    }
}
