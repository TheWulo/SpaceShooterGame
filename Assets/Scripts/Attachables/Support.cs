using UnityEngine;

namespace Assets.Scripts.Attachables
{
    public class Support : Attachable
    {
        public bool IsActive;
        public bool AutoActivate;
        public float Cooldown;
        protected float cooldownCurrent;

        protected virtual void Activate()
        {
            if (cooldownCurrent >= Cooldown)
            {
                OnActivated();
                cooldownCurrent = 0;
                IsActive = true;
            }
        }

        protected virtual void Deactivate()
        {
            OnDeactivated();
            IsActive = false;
        }

        protected virtual void Update()
        {
            cooldownCurrent += Time.deltaTime;
            if (AutoActivate && !IsActive)
            {
                Activate();
            }
        }

        protected virtual void OnActivated()
        {

        }

        protected virtual void OnDeactivated()
        {

        }

        public virtual void Prepare(string ShipID)
        { 
        
        }
    }
}
