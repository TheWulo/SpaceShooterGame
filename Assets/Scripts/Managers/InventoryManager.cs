using UnityEngine;
using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using Assets.Scripts.Attachables;

namespace Assets.Scripts.Managers
{
    public class InventoryManager : Singleton<InventoryManager>, IInitializable
    {
        public List<Attachable> PlayersAttachables;

        private bool isInitialized;

        #region IInitializable
        public void Init()
        {
            PlayersAttachables = new List<Attachable>();

            DebugAddInventory();

            isInitialized = true;
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }
        #endregion

        private void DebugAddInventory()
        {
            foreach(var attach in AttachablesDatabase.instance.GetAllAttachables())
            {
                PlayersAttachables.Add(attach);
                PlayersAttachables.Add(attach);
            }
        }
    }
}
