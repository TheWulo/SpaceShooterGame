using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.GUI
{
    public enum GUIWindowType {MainMenu, Save, Play, Garage, Pause, Options, Credits, Research, StageSelect, StageEnd}

    public class GUIWindow : MonoBehaviour, IInitializable
    {
        public GUIWindowType Type;
        private Canvas targetCanvas;
        public bool IsActive { get { return targetCanvas.enabled; }}

        private bool isInitialized;

        #region IInitializable
        public void Init()
        {
            targetCanvas = GetComponent<Canvas>();
            isInitialized = true;
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }
        #endregion

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
