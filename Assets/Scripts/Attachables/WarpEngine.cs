using UnityEngine;
using Assets.Scripts.Managers;
using System.Collections.Generic;

namespace Assets.Scripts.Attachables
{
    public class WarpEngine : Engine
    {
        [Header("Warp Engine")]
        public float WarpDistance;
        public float Cooldown;
        [SerializeField]
        ParticleSystem WarpParticleSystem;

        private float cooldownTimer;

        private KeyCode lastPressedButton = KeyCode.None;
        private float pressedTimer;

        private List<KeyCode> keysToCheck;

        void Start()
        {
            keysToCheck = new List<KeyCode>();
            keysToCheck.Add(KeyCode.LeftArrow);
            keysToCheck.Add(KeyCode.RightArrow);
            keysToCheck.Add(KeyCode.UpArrow);
            keysToCheck.Add(KeyCode.DownArrow);
        }

        void Update()
        {
            foreach (var keyToCheck in keysToCheck)
            {
                if (Input.GetKeyDown(keyToCheck))
                {
                    if (lastPressedButton == keyToCheck)
                    {
                        Warp(lastPressedButton);
                        lastPressedButton = KeyCode.None;
                    }
                    else
                    {
                        lastPressedButton = keyToCheck;
                        pressedTimer = 0;
                    }
                }
            }

            pressedTimer += Time.deltaTime;
            if (pressedTimer >= 0.5f)
            {
                lastPressedButton = KeyCode.None;
            }

            cooldownTimer += Time.deltaTime;
        }

        void Warp(KeyCode targetKey)
        {
            if (cooldownTimer < Cooldown) return;

            WarpParticleSystem.Play();
            switch (targetKey)
            {
                case KeyCode.LeftArrow:
                    VehiclesManager.instance.PlayerShipCurrent.gameObject.transform.Translate(Vector3.left * WarpDistance);
                    break;
                case KeyCode.RightArrow:
                    VehiclesManager.instance.PlayerShipCurrent.gameObject.transform.Translate(Vector3.right * WarpDistance);
                    break;
                case KeyCode.UpArrow:
                    VehiclesManager.instance.PlayerShipCurrent.gameObject.transform.Translate(Vector3.up * WarpDistance);
                    break;
                case KeyCode.DownArrow:
                    VehiclesManager.instance.PlayerShipCurrent.gameObject.transform.Translate(Vector3.down * WarpDistance);
                    break;
            }
            lastPressedButton = KeyCode.None;
            cooldownTimer = 0;
        }
    }
}
