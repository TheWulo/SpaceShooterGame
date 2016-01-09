using UnityEngine;
using Assets.Scripts.Managers;
using System.Collections.Generic;

namespace Assets.Scripts.Scrap
{
    public class ScrapMetal : MonoBehaviour
    {
        public List<Sprite> ScrapLevelsSprites;
        public List<int> ScrapLevelAmount;

        public int ScrapAmount;
        public float MovementSpeed;

        void Update()
        {
            transform.Translate(Vector3.left * MovementSpeed * Time.deltaTime / 100.0f);
        }

        public void SetUp(int scrapAmount)
        {
            ScrapAmount = scrapAmount;
            for (int i = 0; i<ScrapLevelAmount.Count ;i++)
            {
                if (ScrapAmount > ScrapLevelAmount[i])
                {
                    continue;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = ScrapLevelsSprites[i];
                    break;
                }
            }
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.tag == "Player")
            {
                EventManager.ScrapMetalCollected.Invoke(new ScrapMetalCollectedEventArgs(this));
                Destroy(gameObject);
            }
        }
    }
}
