using Terraria.ID;
using Terraria.ModLoader;

namespace TysYoyoRedux.Projectiles.NewYoyoEffects;

public class AsteroidFireball : ModProjectile
{
    public override string Texture
    {
        get => $"Terraria/Images/Projectile_{ProjectileID.BallofFire}";
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.BallofFire);
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = 90;

        AIType = ProjectileID.BallofFire;
    }
}