using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Assets.Scripts.GUI;
using Assets.Scripts.Projectiles;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Ship
{
    public class ShipBase : MonoBehaviour
    {
        [Header("Ship Base")]
        public string ShipID;
        public string ShipName;
        public int ScrapCost;
        public Sprite ShipDefaultSprite;
        public Sprite ShipDamageStage1Sprite;
        public Sprite ShipDamageStage2Sprite;
        public Sprite ShipDamageStage3Sprite;

        public int Health;
        public int Agility;
        public int Energy;

        public int CurrentHealth;
        private float currentAgility;
        private int currentEnergy;

        public bool GodMode = false;

        [Header("Ship Components")]
        public WeaponsComponent WeaponComponent;
        public MotorComponent MotorComponent;
        public SpriteRenderer ShipSpriteRenderer;

        [Header("Ship Slots")]
        public List<WeaponSpot> WeaponSpots;
        public List<SupportSpot> SupportSpots;
        public List<EngineSpot> EngineSpots;

        [Header("Ship UI")]
        public ShipUI ShipUIObject;

        void Start()
        {
            CurrentHealth = Health;
        }

        public void PrepareShipForLaunch()
        {
            WeaponComponent.Prepare();
            MotorComponent.Prepare();

            foreach (var supportSpot in SupportSpots)
            {
                supportSpot.GetSupport().Prepare(ShipID);
            }

            gameObject.transform.position = new Vector3(-2, 0, 0);
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if(other.transform.tag == "EnemyProjectile")
            {
                if (Random.Range(0, 100) < MotorComponent.Evasion)
                {
                    Debug.Log("Evaded!");
                    EventManager.PlayerEvadedProjectile.Invoke(new EmptyEventArgs());
                }
                else
                {
                    TakeDamage(other.gameObject.GetComponent<Projectile>().Damage);
                    EventManager.ProjectileHitPlayer.Invoke(new ProjectileHitPlayerEventArgs(other.GetComponent<Projectile>()));
                    Destroy(other.gameObject);
                }
            }
        }

        public virtual void TakeDamage(int amount)
        {
            if (GodMode) return;

            CurrentHealth -= amount;
            EventManager.PlayerTookDamage.Invoke(new PlayerTookDamageEventArgs(amount));

            if (CurrentHealth <= 0)
            {
                EventManager.PlayerShipDestroyed.Invoke(new EmptyEventArgs());
                EventManager.GameFinishing.Invoke(new EmptyEventArgs());
                Destroy(gameObject);
                return;
            }

            ChangeSpriteToDamageStage();
        }

        void ChangeSpriteToDamageStage()
        {
            if (ShipDamageStage1Sprite == null || ShipDamageStage2Sprite == null || ShipDamageStage3Sprite == null) return;

            if (CurrentHealth <= Health * 0.25f)
            {
                ShipSpriteRenderer.sprite = ShipDamageStage3Sprite;
            }
            else if (CurrentHealth <= Health * 0.5f)
            {
                ShipSpriteRenderer.sprite = ShipDamageStage2Sprite;
            }
            else if (CurrentHealth <= Health * 0.75f)
            {
                ShipSpriteRenderer.sprite = ShipDamageStage1Sprite;
            }
        }

        public virtual bool IsReadyToFly()
        {
            foreach (var spot in WeaponSpots)
            {
                if (!spot.HasElement) return false;
            }
            foreach (var spot in SupportSpots)
            {
                if (!spot.HasElement) return false;
            }
            foreach (var spot in EngineSpots)
            {
                if (!spot.HasElement) return false;
            }
            return true;
        }
    }
}
