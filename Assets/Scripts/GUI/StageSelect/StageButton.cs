using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Enemy;

namespace Assets.Scripts.GUI.StageSelect
{
    public enum StageState { Finished, Available, Locked }

    public class StageButton : MonoBehaviour
    {
        public string StageID;
        public StageState State;
        [SerializeField]
        private List<StageButton> NextMissions;

        [Header("GUI")]
        [SerializeField]
        private Image StageImage;

        [Header("Roads")]
        [SerializeField]
        private List<Image> RoadsFromMission;

        public void Prepare()
        {
            switch (State)
            {
                case StageState.Available:
                    StageImage.enabled = true;
                    StageImage.color = StagesManager.instance.GetColorForAlienRace(StagesManager.instance.GetAlienRaceForStage(StageID));
                    break;
                case StageState.Finished:
                    StageImage.enabled = true;
                    StageImage.color = StagesManager.instance.FinishedColor;
                    break;
                case StageState.Locked:
                    StageImage.enabled = false;
                    break;
            }
            SetSpriteForRoads();
        }

        void SetSpriteForRoads()
        {
            for (int i = 0; i < RoadsFromMission.Count; ++i)
            {
                if (NextMissions[i].State == StageState.Available && State == StageState.Finished)
                    RoadsFromMission[i].sprite = StagesManager.instance.UnlockedRoadSprite;
                else if (NextMissions[i].State == StageState.Finished && State == StageState.Finished)
                    RoadsFromMission[i].sprite = StagesManager.instance.FinishedRoadSprite;
                else
                    RoadsFromMission[i].sprite = StagesManager.instance.LockedRoadSprite;
            }
        }

        public void OnStageButton()
        {
            if (State == StageState.Available)
                StagesManager.instance.StartStage(StageID);
        }

        public void UnlockNextMissions()
        { 
            foreach (var mission in NextMissions)
            {
                mission.State = StageState.Available;
            }
        }
    }
}
