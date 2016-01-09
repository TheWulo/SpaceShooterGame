﻿using UnityEngine;
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

        public int Health;
        public int Agility;
        public int Energy;

        private int currentHealth;
        private float currentAgility;
        private int currentEnergy;

        [Header("Ship Components")]
        public WeaponsComponent WeaponComponent;
        public MotorComponent MotorComponent;

        [Header("Ship Slots")]
        public List<WeaponSpot> WeaponSpots;
        public List<SupportSpot> SupportSpots;
        public List<EngineSpot> EngineSpots;

        [Header("Ship UI")]
        public ShipUI ShipUIObject;

        public void PrepareShipForLaunch()
        {
            WeaponComponent.Prepare();
            MotorComponent.Prepare();

            gameObject.transform.position = Vector3.zero;
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
            Health -= amount;
            EventManager.PlayerTookDamage.Invoke(new PlayerTookDamageEventArgs(amount));

            if (Health <= 0)
            {
                EventManager.PlayerShipDestroyed.Invoke(new EmptyEventArgs());
                EventManager.GameFinishing.Invoke(new EmptyEventArgs());
                Destroy(gameObject);
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
