using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class LoadingManager : Singleton<LoadingManager>
    {
        [SerializeField]
        private List<GameObject> ManagerToInitialize;
        
        void Start()
        {
            foreach(var initializableObject in ManagerToInitialize)
            {
                try
                {
                    (initializableObject.GetComponent<IInitializable>()).Init();
                }
                catch
                {
                    Debug.LogError("LoadingManager tried to load an object that is not Initializable! Make sure that all objects in LoadingManager are Initializable.");
                }
            }
        }
    }
}
