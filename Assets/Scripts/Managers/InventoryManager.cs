using UnityEngine;
using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using Assets.Scripts.Attachables;
using System;

namespace Assets.Scripts.Managers
{
    public class InventoryManager : Singleton<InventoryManager>, IInitializable
    {
        public List<string> PlayersAttachables;

        private bool isInitialized;

        #region IInitializable
        public void Init()
        {
            EventManager.AttachableResearched.Listeners += OnAttachableResearched;

            PlayersAttachables = new List<string>();

            isInitialized = true;

            PlayersAttachables.AddRange(ResearchManager.instance.GetAllUnlockedAttachables());
        }

        private void OnAttachableResearched(AttachableResearchedEventArgs args)
        {
            PlayersAttachables.Add(args.AttachableID);
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }
        #endregion
    }
}
