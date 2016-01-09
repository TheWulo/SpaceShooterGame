namespace Assets.Scripts.Projectiles
{
    public enum PlayerProjectileType { Bullet, Laser, EMP, Rocket }

    public class PlayerProjectile : Projectile
    {
        public PlayerProjectileType ProjectileType;
    }
}
