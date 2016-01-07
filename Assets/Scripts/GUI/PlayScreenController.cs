using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.GUI
{
    public class PlayScreenController : GUIWindow
    {
        void Update()
        {
            if (!IsActive) return;

            //QQ
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GUIManager.instance.ShowWindow(GUIWindowType.Garage);
            }
        }
    }
}
