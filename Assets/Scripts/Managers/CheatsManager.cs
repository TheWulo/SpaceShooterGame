using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class CheatsManager : Singleton<CheatsManager>
    {
        public bool EnableCheats;

        private string cheatString = "";
        private float cheatsTimer;

        void Update()
        {
            if (!EnableCheats) return;

            if (Input.inputString == "")
            {
                cheatsTimer += Time.deltaTime;
            }
            else
            {
                cheatsTimer = 0;
                cheatString += Input.inputString;
                CheckForCheats();
            }

            if (cheatsTimer >= 1 && cheatString != "")
            {
                cheatString = "";
            }
        }

        void CheckForCheats()
        {
            switch (cheatString)
            {
                case "junkyard":
                    PlayerManager.instance.CollectedScrap += 5000;
                    AudioManager.instance.Cheater.Play();
                    break;
                case "fasterthanlight":
                    if (GameManager.instance.CurrentGameState == GameState.Playing)
                    {
                        GameManager.instance.GameTimer = 100000;
                        AudioManager.instance.Cheater.Play();
                    }
                    break;
                case "iamthegod":
                    if (GameManager.instance.CurrentGameState == GameState.Playing)
                    {
                        VehiclesManager.instance.PlayerShipCurrent.GodMode = !VehiclesManager.instance.PlayerShipCurrent.GodMode;
                        AudioManager.instance.Cheater.Play();
                    }
                    break;
                case "rocketscience":
                    ResearchManager.instance.UnlockAllAttachables();
                    VehiclesManager.instance.UnlockAllShips();
                    AudioManager.instance.Cheater.Play();
                    break;
            }
        }
    }
}
