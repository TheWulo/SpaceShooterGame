using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.GUI.StageSelect;
using Assets.Scripts.Managers;

namespace Assets.Scripts.GUI
{
    public class StageSelectScreenController : GUIWindow
    {
        [SerializeField]
        private List<StageButton> Stages;

        public override void Show()
        {
            base.Show();

            ChangeStatesOfStages();
            CurrentStage().UnlockNextMissions();
            PrepareStages();
        }

        void ChangeStatesOfStages()
        {
            foreach (var stage in Stages)
            {
                foreach (var unlockedStage in StagesManager.instance.FinishedStages)
                {
                    if (stage.StageID == unlockedStage)
                    {
                        stage.State = StageState.Finished;
                        break;
                    }
                    stage.State = StageState.Locked;
                }
            }
        }

        void PrepareStages()
        {
            foreach (var stage in Stages)
            {
                stage.Prepare();
            }
        }

        StageButton CurrentStage()
        {
            foreach (var stage in Stages)
            {
                if (stage.StageID == StagesManager.instance.CurrentStageID)
                {
                    return stage;
                }
            }
            return null;
        }
    }
}
