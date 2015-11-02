using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Ship
{
    public class ShipBase : MonoBehaviour
    {
        public int Health;
        public int Agility;
        public int Energy;

        public List<WeaponSpot> WeaponSpots;
        public List<SupportSpot> SupportSpots;
        public List<EngineSpot> EngineSPots;

        void Update()
        {
            gameObject.transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime, 0, 0));
            gameObject.transform.Translate(new Vector3(0, Input.GetAxisRaw("Vertical") * Time.deltaTime, 0));
        }
    }
}
