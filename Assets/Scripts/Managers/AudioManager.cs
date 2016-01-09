using UnityEngine;
using Assets.Scripts.Interfaces;
using System;

namespace Assets.Scripts.Managers
{
    public class AudioManager : Singleton<AudioManager>, IInitializable
    {
        private bool isInitialized;

        [Header("GUI")]
        public AudioSource MountSound;

        [Header("Weapons")]
        public AudioSource BulletCannonShoot;
        public AudioSource LaserCannonShoot;
        public AudioSource EMPCannonShoot;
        public AudioSource RocketCannonShoot;
        public AudioSource RocketCannonHit;

        [Header("Music")]
        public AudioSource MainMenu;
        public AudioSource LevelAthene;

        public void Init()
        {
            EventManager.ItemAttached.Listeners += OnItemAttached;
            EventManager.ItemDetached.Listeners += OnItemDetached;
            EventManager.GameFinishing.Listeners += OnGameFinishing;
            EventManager.GameStarting.Listeners += OnGameStarting;
            EventManager.WeaponFired.Listeners += OnWeaponFired;
            MainMenu.Play();
            isInitialized = true;
        }

        private void OnWeaponFired(WeaponFiredEventArgs args)
        {
            switch (args.Weapon.WeaponType)
            {
                case Attachables.WeaponTypes.Bullet:
                    BulletCannonShoot.Play();
                    break;
                case Attachables.WeaponTypes.Laser:
                    LaserCannonShoot.Play();
                    break;
                case Attachables.WeaponTypes.EMP:
                    EMPCannonShoot.Play();
                    break;
                case Attachables.WeaponTypes.Rocket:
                    RocketCannonShoot.Play();
                    break;
            }
        }

        private void OnGameStarting(EmptyEventArgs args)
        {
            MainMenu.Stop();
            LevelAthene.Play();
        }

        private void OnGameFinishing(EmptyEventArgs args)
        {
            LevelAthene.Stop();
            MainMenu.Play();
        }

        #region Events

        private void OnItemDetached(ItemDetachedEventArgs args)
        {
            MountSound.Play();
        }

        private void OnItemAttached(ItemAttachedEventArgs args)
        {
            MountSound.Play();
        }

        #endregion

        public bool IsInitialized()
        {
            return isInitialized;
        }
    }
}
