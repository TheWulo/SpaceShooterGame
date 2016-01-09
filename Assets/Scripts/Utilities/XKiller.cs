using UnityEngine;

namespace Assets.Scripts.Utilities
{
    public class XKiller : MonoBehaviour
    {
        public float LeftX = -5;
        public float RightX = 5;

        void Update()
        {
            CheckKillX();
        }

        private void CheckKillX()
        {
            if (gameObject.transform.position.x <= LeftX)
            {
                Destroy(gameObject);
            }
            if (gameObject.transform.position.x >= RightX)
            {
                Destroy(gameObject);
            }
        }
    }
}
