using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.VanillaYoyoEffects
{
	public class AmarokSnowflakeProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 34;
			Projectile.height = 34;
			Projectile.friendly = true;

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
			Projectile.timeLeft = 80;
			Projectile.alpha = 255;

			Projectile.tileCollide = false;
		}

		public override void AI()
		{
			//Temp Fade in and grow
			if (Projectile.ai[0] == 0)
			{
				Projectile.scale = 0.3f;
				Projectile.rotation += Main.rand.NextFloat(0f, 4f);
			}

			if (Projectile.ai[0] < 40)
			{
				Projectile.scale += 0.02f;
				Projectile.alpha -= 6;
			}

			Projectile.rotation += 0.1f * (80f - Projectile.ai[0]) / 80f;

			Projectile.ai[0]++;

			//Produce Light
			Lighting.AddLight(Projectile.Center, 0.25f, 0.30f, 0.30f);

			//fade out
			if (Projectile.timeLeft < 20)
			{
				Projectile.alpha += 13;
				Projectile.scale -= 0.03f;
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			//Has a 2 in 3 chance to inflict 2-5 seconds of frostburn on impact
			if (Main.rand.Next(3) <= 1)
			{
				target.AddBuff(BuffID.Frostburn2, 60 * (2 + Main.rand.Next(4)));
			}
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			//Has a 2 in 3 chance to inflict 2-5 seconds of frostburn on impact
			if (Main.rand.Next(3) <= 1)
			{
				target.AddBuff(BuffID.Frostburn2, 60 * (2 + Main.rand.Next(4)));
			}
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
        }
    }
}