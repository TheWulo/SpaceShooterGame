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
            PlayersAttachables.Add(AttachablesDatabase.instance.GetAttachable("LiquidEngine1"));
            PlayersAttachables.Add(AttachablesDatabase.instance.GetAttachable("LiquidEngine2"));
            PlayersAttachables.Add(AttachablesDatabase.instance.GetAttachable("WarpEngine1"));
            isInitialized = true;
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }
        #endregion
    }
}
