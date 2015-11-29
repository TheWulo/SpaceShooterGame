using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GUI;
using System;

namespace Assets.Scripts.Managers
{
    public class GUIManager : Singleton<GUIManager>
    {
        [SerializeField]
        private List<GUIWindow> guiWindows;

        private GUIWindow currentWindow;

        void Start()
        {
            ShowWindow(GUIWindowType.Garage);
        }

        public void ShowWindow(GUIWindowType type)
        {
            foreach (var window in guiWindows)
            {
                if (window.Type == type)
                {
                    CloseWindow();
                    currentWindow = window;
                    window.Show();
                }
            }
        }

        public void ShowWindow(string type)
        {
            GUIWindowType targerType = (GUIWindowType)Enum.Parse(typeof(GUIWindowType), type);
            ShowWindow(targerType);
        }

        public void CloseWindow()
        {
            if (currentWindow == null) return;

            currentWindow.Hide();
            currentWindow = null;
        }
    }
}
