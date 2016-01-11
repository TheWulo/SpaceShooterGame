using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.GUI;
using System;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Managers
{
    public class GUIManager : Singleton<GUIManager>, IInitializable
    {
        [SerializeField]
        private List<GUIWindow> guiWindows;

        private GUIWindow currentWindow;

        private bool isInitialized;

        public void Init()
        {
            EventManager.GameStarting.Listeners += OnGameStarting;
            foreach(var window in guiWindows)
            {
                window.GetComponent<IInitializable>().Init();
            }

            ShowWindow(GUIWindowType.MainMenu);
            isInitialized = true;
        }

        private void OnGameStarting(EmptyEventArgs args)
        {
            ShowWindow(GUIWindowType.Play);
        }

        public bool IsInitialized()
        {
            return isInitialized;
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
