using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Managers
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T instance = null;

        protected Singleton<T> InstanceCheck(Singleton<T> instanceCheck)
        {
            if (instanceCheck != null && instanceCheck != this)
            {
                DestroyImmediate(instanceCheck.gameObject);
            }

            return this;
        }

        protected virtual void Awake()
        {
            instance = InstanceCheck(instance) as T;
        }

        protected void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }

    public abstract class PersistentSingleton<T> : MonoBehaviour where T : PersistentSingleton<T>
    {
        public static T instance = null;

        protected PersistentSingleton<T> InstanceCheck(PersistentSingleton<T> instanceCheck)
        {
            if (instanceCheck != null && instanceCheck != this)
            {
                Destroy(this);
            }

            return instanceCheck;
        }

        protected virtual void Awake()
        {
            instance = InstanceCheck(instance) as T;
        }

        protected void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}
