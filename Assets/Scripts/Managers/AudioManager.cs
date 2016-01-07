using UnityEngine;
using Assets.Scripts.Interfaces;
using System;

namespace Assets.Scripts.Managers
{
    public class AudioManager : Singleton<AudioManager>, IInitializable
    {
        private bool isInitialized;

        public AudioSource MountSound;

        public void Init()
        {
            EventManager.ItemAttached.Listeners += OnItemAttached;
            EventManager.ItemDetached.Listeners += OnItemDetached;
            isInitialized = true;
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
