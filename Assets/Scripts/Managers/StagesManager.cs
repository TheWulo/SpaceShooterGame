using UnityEngine;
using Assets.Scripts.Enemy;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using System;
using System.Linq;

namespace Assets.Scripts.Managers
{
    [Serializable]
    public class StageColorForEnemy
    {
        public AlienRace Race;
        public Color Color;
    }

    [Serializable]
    public class RaceForStage
    {
        public string StageID;
        public AlienRace Race;
    }

    public class StagesManager : Singleton<StagesManager>, IInitializable
    {
        public List<RaceForStage> RacesForStages;
        public List<string> FinishedStages;
        public string CurrentStageID;

        private bool isInitialized;

        [Header("Stage Screen Info")]
        public List<StageColorForEnemy> StageColorsForEnemy;
        public Color FinishedColor;

        [Header("Roads")]
        public Sprite LockedRoadSprite;
        public Sprite UnlockedRoadSprite;
        public Sprite FinishedRoadSprite;

        public void Init()
        {
            EventManager.StageFinishing.Listeners += OnStageFinishing;
            EventManager.GameFinishing.Listeners += OnGameFinishing;

            ResetStages();

            isInitialized = true;
        }

        private void OnGameFinishing(EmptyEventArgs args)
        {
            ResetStages();
        }

        private void OnStageFinishing(EmptyEventArgs args)
        {
            FinishedStages.Add(CurrentStageID);
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }

        public void RandomiseAlienRacesForStages()
        {
            foreach (var stage in RacesForStages)
            {
                int random = UnityEngine.Random.Range(0, 2);
                
                if (random == 0)
                {
                    stage.Race = AlienRace.Felgor;
                }
                else
                {
                    stage.Race = AlienRace.Athene;
                }
            }
        }

        public AlienRace GetAlienRaceForStage(string stageId)
        {
            return RacesForStages.First(stage => stage.StageID == stageId).Race;
        }

        public Color GetColorForAlienRace(AlienRace race)
        {
            return StageColorsForEnemy.First(alien => alien.Race == race).Color;
        }

        public void StartStage(string stageID)
        {
            CurrentStageID = stageID;
            EventManager.StageStarted.Invoke(new StageStartedEventArgs(stageID));
            EventManager.GameStarting.Invoke(new EmptyEventArgs());
        }

        void ResetStages()
        {
            RandomiseAlienRacesForStages();

            FinishedStages.Clear();

            if (FinishedStages.Count == 0)
            {
                FinishedStages.Add("0_1");
                CurrentStageID = "0_1";
            }
        }
    }
}
