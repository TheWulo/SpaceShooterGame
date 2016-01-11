using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Attachables
{
    [Serializable]
    public class ShieldForShipInfo
    {
        public string ShipID;
        public Vector3 ShieldTargetPosition;
        public Vector3 ShieldTargetScale;
    }

    [Serializable]
    public class ShieldHpColorAndAlpha
    {
        public int HeathLevel;
        public Color ColorForLevel;
    }

    public class ShieldGenerator : Support
    {
        [Header("Shield Info")]
        public List<ShieldForShipInfo> ShieldForShipsInfo;
        public List<ShieldHpColorAndAlpha> ShieldHealthLevels;
        public GameObject ShieldComponent;

        public int ShieldHealth;
        public int ShieldHealthCurrent;

        protected override void Update()
        {
            cooldownCurrent += Time.deltaTime;

            Activate();
        }

        public override void Prepare(string shipID)
        {
            base.Prepare(shipID);
            var shieldInfo = ShieldForShipsInfo.First(info => info.ShipID == shipID);
            ShieldComponent.transform.localScale = shieldInfo.ShieldTargetScale;
            ShieldComponent.transform.localPosition = shieldInfo.ShieldTargetPosition;
            ShieldHealthCurrent = ShieldHealth;
            cooldownCurrent = Cooldown;
            ShieldComponent.GetComponent<SpriteRenderer>().color = ShieldHealthLevels.FirstOrDefault(level => level.HeathLevel == ShieldHealthCurrent).ColorForLevel;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            if (ShieldHealthCurrent < ShieldHealth)
            {
                ShieldHealthCurrent++;
                ShieldComponent.SetActive(true);
                ShieldComponent.GetComponent<SpriteRenderer>().color = ShieldHealthLevels.FirstOrDefault(level => level.HeathLevel == ShieldHealthCurrent).ColorForLevel;
                EventManager.ShieldActivated.Invoke(new EmptyEventArgs());
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            ShieldComponent.SetActive(false);
            cooldownCurrent = 0;
            EventManager.ShieldDeactivated.Invoke(new EmptyEventArgs());
        }

        public void TakeDamage()
        {
            ShieldHealthCurrent--;
            cooldownCurrent = 0;
            if (ShieldHealthCurrent <= 0)
            {
                Deactivate();
            }
            else
            {
                ShieldComponent.GetComponent<SpriteRenderer>().color = ShieldHealthLevels.FirstOrDefault(level => level.HeathLevel == ShieldHealthCurrent).ColorForLevel;
                EventManager.ShieldHit.Invoke(new EmptyEventArgs());
            }
        }

        public void Regenerate()
        {

        }
    }
}
