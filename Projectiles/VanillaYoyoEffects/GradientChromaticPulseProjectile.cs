using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.VanillaYoyoEffects
{
	public class GradientChromaticPulseProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 8;
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true; 

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;

			Projectile.tileCollide = false;
			Projectile.scale = 0.8f;
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
			if (CurrentDistance < 125)
            {
				CurrentDistance++;
            }
			if (!HasAngle)
			{
				CurrentAngle = (72 * Projectile.ai[0]);
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
				CoughCoughVelocityCoughCough *= 2f;
				CoughCoughVelocityCoughCough.RotatedByRandom(10f);
				int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Flare, CoughCoughVelocityCoughCough.X, CoughCoughVelocityCoughCough.Y, 100, default(Color), 0.75f);
				Main.dust[dustIndex].noGravity = true;
			}

			/*fade in
			if (Projectile.alpha > 60)
			{
				Projectile.alpha -= 5;
			}*/

			//Animate
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 100)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 8)
			{
				Projectile.frame = 0;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 0);
		}
	}
}