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
            if (Input.GetKey(KeyCode.UpArrow))
            {
                gameObject.transform.Translate(Vector3.up * MovementSpeed * Time.deltaTime / 100.0f);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                gameObject.transform.Translate(Vector3.up * -MovementSpeed * Time.deltaTime / 100.0f);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.transform.Translate(Vector3.right * -MovementSpeed * Time.deltaTime / 100.0f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.transform.Translate(Vector3.right * MovementSpeed * Time.deltaTime / 100.0f);
            }
        }
    }
}
