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
	public class HelFireFlameTorrentProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.friendly = true; 
            Projectile.hostile = false;

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
			Projectile.timeLeft = 105;

			Projectile.extraUpdates = 3;
		}

		public override void AI()
		{
            //Kill in water
            if (Projectile.wet)
            {
				Projectile.Kill();
            }

			//Flamethrower style flame Code
			if (Projectile.ai[0] > 12f)
			{
				if (Main.rand.Next(3) == 0)
				{
					int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 127, Projectile.velocity.X * 1.2f, Projectile.velocity.Y * 1.2f, 130, default(Color), 3.25f);
					Main.dust[dust].noGravity = true;
					Random random = new Random();
					double randomDustSpread = random.NextDouble() * (2.25 - 1.75) + 1.75;
					Main.dust[dust].velocity *= (float)randomDustSpread;
				}
			}
			else
			{
				Projectile.ai[0] += 1f;
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			//inflict 3-8 seconds of on fire! on impact
			target.AddBuff(BuffID.OnFire3, 60 * (3 + Main.rand.Next(6)));
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			//inflict 3-8 seconds of on fire! on impact
			target.AddBuff(BuffID.OnFire3, 60 * (3 + Main.rand.Next(6)));
		}
	}
}