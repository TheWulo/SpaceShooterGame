using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.GUI
{
    public class StageEndScreenController : GUIWindow
    {
        public void OnContinueButton()
        {
            if (StagesManager.instance.CurrentStageID == "7_1")
            {
                GUIManager.instance.ShowWindow(GUIWindowType.Credits);
                EventManager.ShowingCredits.Invoke(new EmptyEventArgs());
            }
            else
            {
                GUIManager.instance.ShowWindow(GUIWindowType.StageSelect);
            }
        }
    }
}
