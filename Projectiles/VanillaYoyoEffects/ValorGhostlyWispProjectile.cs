using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.VanillaYoyoEffects
{
	public class ValorGhostlyWispProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ghostly Wisp");
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true; 
            Projectile.hostile = false;

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
			Projectile.alpha = 255;

			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.scale = 0.6f;
			Projectile.extraUpdates = 4;
		}

		int CurrentDistance = 0;

		bool HasAngle = false;

		float CurrentAngle;

		public override void AI()
		{
			//Set parent
			Projectile parent = Main.projectile[(int)Projectile.ai[1]];
			if (!parent.active)
			{
				Projectile.Kill();
			}
			else
			{
				Projectile.timeLeft = 2;
			}

			//Wisps orbit the main yoyo
			if (CurrentDistance < 75)
            {
				CurrentDistance++;
            }
			if (!HasAngle)
			{
				CurrentAngle = (120 * Projectile.ai[0]);
				HasAngle = true;
			}
			CurrentAngle++;
			if (CurrentAngle >= 360)
            {
				CurrentAngle -= 360;
            }
			Projectile.Center = parent.Center + (new Vector2(CurrentDistance, 0).RotatedBy(MathHelper.ToRadians(CurrentAngle)));

			//Point forward
			Vector2 Lastpos = parent.Center + (new Vector2(CurrentDistance, 0).RotatedBy(MathHelper.ToRadians(CurrentAngle - 2)));
			Vector2 CoughCoughVelocityCoughCough = Projectile.Center - Lastpos;
			Projectile.rotation = CoughCoughVelocityCoughCough.ToRotation() + MathHelper.PiOver2;

			//Dust
			for (int d = 0; d < 6; d++)
			{
				CoughCoughVelocityCoughCough.Normalize();
				int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SpectreStaff, CoughCoughVelocityCoughCough.X, CoughCoughVelocityCoughCough.Y);
				Main.dust[dustIndex].noGravity = true;
			}

			//fade in
			if (Projectile.alpha > 100)
			{
				Projectile.alpha -= 5;
			}

			//Lighting
			Lighting.AddLight(Projectile.Center, 0.3f, 0.3f, 0.3f);
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 0);
		}
	}
}