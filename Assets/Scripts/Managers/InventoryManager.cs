using UnityEngine;
using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using Assets.Scripts.Attachables;

namespace Assets.Scripts.Managers
{
    public class InventoryManager : Singleton<InventoryManager>, IInitializable
    {
        public List<string> PlayersAttachables;

        private bool isInitialized;

        #region IInitializable
        public void Init()
        {
            PlayersAttachables = new List<string>();

            isInitialized = true;

            PlayersAttachables.AddRange(ResearchManager.instance.GetAllUnlockedAttachables());
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }
        #endregion
    }
}
