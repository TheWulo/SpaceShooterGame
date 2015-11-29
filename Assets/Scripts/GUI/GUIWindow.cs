using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GUI
{
    public enum GUIWindowType {MainMenu, Save, Play, Garage, Pause, Options, Credits}

    public class GUIWindow : MonoBehaviour
    {
        public GUIWindowType Type;
        private Canvas targetCanvas;
        public bool IsActive { get { return targetCanvas.enabled; }}

        void Start()
        {
            targetCanvas = GetComponent<Canvas>();
        }

        public virtual void Show()
        {
            targetCanvas.enabled = true;
        }

        public virtual void Hide()
        {
            targetCanvas.enabled = false;
        }
    }
}
