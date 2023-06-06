using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.NewYoyoEffects
{
	public class CabalIchorRainProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ichor Rain");
		}

		public override void SetDefaults()
		{
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.friendly = true;
			Projectile.hostile = false;

			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = 1;
			Projectile.alpha = 255;

			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 2;
		}

		public override void AI()
		{
			//Doesn't collide with blocks until 10 blocks above the yoyo collision
			if (Projectile.timeLeft == 600)
            {
				Projectile.tileCollide = false;
            }
			if(Projectile.Center.Y >= (Projectile.ai[1] - (16 * 10)))
            {
				Projectile.tileCollide = true;
            }

			//Speeds up
			Projectile.velocity *= 1.02f;

			//Shrinks and kills if too small (From golden shower)
			Projectile.scale -= 0.002f;
			if (Projectile.scale <= 0f)
			{
				Projectile.Kill();
			}

			//Produces Dust (From golden shower)
			for (int num110 = 0; num110 < 3; num110++)
			{
				float num111 = Projectile.velocity.X / 3f * (float)num110;
				float num112 = Projectile.velocity.Y / 3f * (float)num110;
				int num114 = Dust.NewDust(new Vector2(Projectile.position.X + (float)14, Projectile.position.Y + (float)14), Projectile.width - 14 * 2, Projectile.height - 14 * 2, DustID.Ichor, 0f, 0f, 100);
				Main.dust[num114].noGravity = true;
				Dust dust = Main.dust[num114];
				dust.velocity *= 0.1f;
				dust = Main.dust[num114];
				dust.velocity += Projectile.velocity * 0.5f;
				Main.dust[num114].position.X -= num111;
				Main.dust[num114].position.Y -= num112;
				Main.dust[num114].velocity *= 0.6f;
			}
		}

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
			width = 10;
			height = 10;
			return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
		}
    }
}