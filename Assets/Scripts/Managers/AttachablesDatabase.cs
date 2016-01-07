using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Managers;
using Assets.Scripts.Interfaces;
using System.Linq;

namespace Assets.Scripts.Attachables
{
    public class AttachablesDatabase : Singleton<AttachablesDatabase>, IInitializable
    {
        [SerializeField]
        private List<Attachable> Attachables;

        private bool isInitialized;

        #region IInitializable
        public void Init()
        {
            isInitialized = true;
        }

        public bool IsInitialized()
        {
            return isInitialized;
        }
        #endregion

        public Attachable GetAttachable(string id)
        {
            return Attachables.First(attach => attach.AttachableID == id);
        }

        public Attachable[] GetAllAttachables()
        {
            return Attachables.ToArray();
        }
    }
}
