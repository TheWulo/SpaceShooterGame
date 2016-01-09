using System;
using Assets.Scripts.Scrap;
using Assets.Scripts.Projectiles;

namespace Assets.Scripts.Managers
{
    public partial class EventManager
    {
        public static EventInvoker<EmptyEventArgs> GameStarting
                = new EventInvoker<EmptyEventArgs>();
        public static EventInvoker<EmptyEventArgs> GameFinishing
                = new EventInvoker<EmptyEventArgs>();
        public static EventInvoker<ScrapMetalCollectedEventArgs> ScrapMetalCollected
                = new EventInvoker<ScrapMetalCollectedEventArgs>();
        public static EventInvoker<ProjectileHitPlayerEventArgs> ProjectileHitPlayer
                = new EventInvoker<ProjectileHitPlayerEventArgs>();
        public static EventInvoker<EmptyEventArgs> PlayerShipDestroyed
                = new EventInvoker<EmptyEventArgs>();
        public static EventInvoker<PlayerTookDamageEventArgs> PlayerTookDamage
                = new EventInvoker<PlayerTookDamageEventArgs>();
        public static EventInvoker<EmptyEventArgs> PlayerEvadedProjectile
                = new EventInvoker<EmptyEventArgs>();

    }

    public class ScrapMetalCollectedEventArgs : EventArgs
    {
        public readonly ScrapMetal ScrapMetal;

        public ScrapMetalCollectedEventArgs(ScrapMetal scrapMetal)
        {
            ScrapMetal = scrapMetal;
        }
    }

    public class ProjectileHitPlayerEventArgs : EventArgs
    {
        public readonly Projectile Projectile;

        public ProjectileHitPlayerEventArgs(Projectile projectile)
        {
            Projectile = projectile;
        }
    }

    public class PlayerTookDamageEventArgs : EventArgs
    {
        public readonly int Damage;

        public PlayerTookDamageEventArgs(int damage)
        {
            Damage = damage;
        }
    }
}
