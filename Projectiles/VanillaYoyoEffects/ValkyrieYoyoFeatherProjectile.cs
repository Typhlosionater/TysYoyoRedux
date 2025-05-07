using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.VanillaYoyoEffects
{
	public class ValkyrieYoyoFeatherProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.friendly = true; 

            Projectile.DamageType = DamageClass.Melee;
			Projectile.timeLeft = 300;
			Projectile.alpha = 250;
			Projectile.extraUpdates = 1;

			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.scale = 1.1f;
		}

		public override void AI()
		{
			//Point forward
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

			//fade in
			if (Projectile.timeLeft >= 290)
			{
				Projectile.alpha -= 25;
			}
		}

		public override void OnKill(int timeLeft)
		{
			for (int num689 = 0; num689 < 6; num689++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 42, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f);
			}
		}
	}
}