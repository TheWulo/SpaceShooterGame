using UnityEngine;
using System;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Managers
{
    public enum GameState { Playing, Paused, GameOver, Menu, EndStage }

    public class GameManager : Singleton<GameManager>, IInitializable
    {
        public GameState CurrentGameState;
        public float GameTimer;
        public float GameTimeTotal = 120;

        private bool isInitialized;

        

        void Update()
        {
            if (!(CurrentGameState == GameState.Playing)) return;

            GameTimer += Time.deltaTime;
            if (GameTimer >= GameTimeTotal)
            {
                EventManager.StageFinishing.Invoke(new EmptyEventArgs());
                CurrentGameState = GameState.EndStage;
            }
        }

        public void Init()
        {
            EventManager.GameStarting.Listeners += OnGameStarting;
            EventManager.GameFinishing.Listeners += OnGameFinishing;

            isInitialized = true; 
        }

        private void OnGameFinishing(EmptyEventArgs args)
        {
            CurrentGameState = GameState.EndStage;
        }

        private void OnGameStarting(EmptyEventArgs args)
        {
            CurrentGameState = GameState.Playing;
            GameTimer = 0;
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }

    }
}
