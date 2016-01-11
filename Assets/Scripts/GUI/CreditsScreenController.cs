using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.GUI
{
    public class CreditsScreenController : GUIWindow
    {
        public float ScrollSpeed;
        public float ScrollDelay = 3;
        private float scrollDelayCurrent;

        public GameObject ScrollPanel;

        public override void Show()
        {
            base.Show();
            scrollDelayCurrent = ScrollDelay;
        }

        void Update()
        {
            if (!IsActive) return;

            scrollDelayCurrent -= Time.deltaTime;

            if (scrollDelayCurrent > 0) return;

            ScrollPanel.transform.Translate(Vector3.up * ScrollSpeed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GUIManager.instance.ShowWindow(GUIWindowType.Garage);
                EventManager.GameFinishing.Invoke(new EmptyEventArgs());
            }
        }
    }
}
