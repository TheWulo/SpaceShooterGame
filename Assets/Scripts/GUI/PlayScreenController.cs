using UnityEngine;
using Assets.Scripts.Managers;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public class PlayScreenController : GUIWindow
    {
        [SerializeField]
        private Text HealthLabel;
        [SerializeField]
        private Text ScrapLabel;

        void Start()
        {
            EventManager.ScrapMetalCollected.Listeners += OnScrapMetalCollected;
            EventManager.PlayerTookDamage.Listeners += OnPlayerTookDamage;
            EventManager.GameStarting.Listeners += OnGameStarting;
            EventManager.GameFinishing.Listeners += OnGameFinishing;
        }

        private void OnGameFinishing(EmptyEventArgs args)
        {
            GUIManager.instance.ShowWindow(GUIWindowType.Garage);
        }

        private void OnGameStarting(EmptyEventArgs args)
        {
            ScrapLabel.text = "Scrap: " + PlayerManager.instance.CollectedScrap;
            HealthLabel.text = "Health: " + VehiclesManager.instance.PlayerShipCurrent.CurrentHealth;
        }

        private void OnPlayerTookDamage(PlayerTookDamageEventArgs args)
        {
            HealthLabel.text = "Health: " + VehiclesManager.instance.PlayerShipCurrent.CurrentHealth;
        }

        private void OnScrapMetalCollected(ScrapMetalCollectedEventArgs args)
        {
            ScrapLabel.text = "Scrap: " + PlayerManager.instance.CollectedScrap;
        }

        void Update()
        {
            if (!IsActive) return;

            //QQ
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EventManager.GameFinishing.Invoke(new EmptyEventArgs());
            }
        }
    }
}
