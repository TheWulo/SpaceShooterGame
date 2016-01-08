using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Scrap
{
    public class ScrapMetal : MonoBehaviour
    {
        public int scrapAmount;
        public float movementSpeed;

        protected void OnTriggerEnter2D(Collider2D other)
        {
            EventManager.ScrapMetalCollected.Invoke(new ScrapMetalCollectedEventArgs(this));
            Destroy(gameObject);
        }
    }
}
