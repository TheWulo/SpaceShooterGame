using UnityEngine;

namespace Assets.Scripts.Attachables
{
    public class ShieldComponent : MonoBehaviour
    {
        [SerializeField]
        private ShieldGenerator shieldGenerator;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "EnemyProjectile")
            {
                shieldGenerator.TakeDamage();
                Destroy(other.gameObject);
            }
        }
    }
}
