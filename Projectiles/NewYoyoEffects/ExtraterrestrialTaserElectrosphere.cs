using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TysYoyoRedux.Projectiles.NewYoyoEffects;

public class ExtraterrestrialTaserElectrosphere : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 4;
    }

    public override string Texture
    {
        get => $"Terraria/Images/Projectile_{ProjectileID.Electrosphere}";
    }

    public override void SetDefaults()
    {
        Projectile.CloneDefaults(ProjectileID.Electrosphere);
        Projectile.DamageType = DamageClass.Melee;
        Projectile.timeLeft = 150;

        AIType = ProjectileID.Electrosphere;
    }
}