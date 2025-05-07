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
	public class AmarokSnowflakeProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.width = 34;
			Projectile.height = 34;
			Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
			Projectile.timeLeft = 80;
			Projectile.alpha = 255;

			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.scale = 1f;
		}

		public override void AI()
		{
			//Choose Texture on Creation
			if (Projectile.timeLeft == 80)
			{
				Projectile.frame = Main.rand.Next(4);
			}

			//Temp Fade in and grow
			if (Projectile.ai[0] == 0)
			{
				Projectile.scale = 0.05f;
				Projectile.rotation += Main.rand.NextFloat(0f, 4f);
			}
			if (Projectile.ai[0] < 19)
			{
				Projectile.scale += 0.05f;
				Projectile.alpha -= 15;
				Projectile.rotation += 0.1f;

				Projectile.ai[0]++;
			}

			//Produce Light
			Lighting.AddLight(Projectile.Center, 0.25f, 0.30f, 0.30f);

			//fade out
			if (Projectile.timeLeft < 20)
			{
				Projectile.alpha += 13;
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
	}
}