using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.VanillaYoyoEffects
{
	public class EoCMiniServantProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 4;

			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true; 

            Projectile.DamageType = DamageClass.Melee;
			Projectile.timeLeft = 600;

			Projectile.extraUpdates = 3;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			//Ricochets a limited amount of times
			if (Projectile.ai[1] < 5f)
			{
				Projectile.ai[1] += 1f;

				if (Projectile.velocity.X != oldVelocity.X)
				{
					Projectile.velocity.X = -oldVelocity.X;
				}
				if (Projectile.velocity.Y != oldVelocity.Y)
				{
					Projectile.velocity.Y = -oldVelocity.Y;
				}

				return false;
			}

			return true;
		}

		public override void AI()
		{
			//Can't hit enemies on creation
			if (Projectile.timeLeft == 600)
            {
				Projectile.friendly = false;
            }

			//Mini eater projectile AI
			if (Projectile.alpha > 0)
			{
				Projectile.alpha -= 50;
			}
			else
			{
				Projectile.extraUpdates = 0;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) - 1.57f;
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 6)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 4)
			{
				Projectile.frame = 0;
			}
			for (int num276 = 0; num276 < 3; num276++)
			{
				float num277 = Projectile.velocity.X / 3f * (float)num276;
				float num278 = Projectile.velocity.Y / 3f * (float)num276;
				int num279 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 5);
				Main.dust[num279].position.X = Projectile.Center.X - num277;
				Main.dust[num279].position.Y = Projectile.Center.Y - num278;
				Dust dust = Main.dust[num279];
				dust.velocity *= 0f;
				Main.dust[num279].scale = 0.8f;
				Main.dust[num279].noGravity = true;
			}
			float num280 = Projectile.position.X;
			float num281 = Projectile.position.Y;
			float num282 = 100000f;
			bool flag9 = false;
			Projectile.ai[0] += 1f;
			if (Projectile.ai[0] > 30f)
			{
				Projectile.ai[0] = 30f;
				for (int num283 = 0; num283 < 200; num283++)
				{
					if (Main.npc[num283].CanBeChasedBy(this))
					{
						float num284 = Main.npc[num283].position.X + (float)(Main.npc[num283].width / 2);
						float num285 = Main.npc[num283].position.Y + (float)(Main.npc[num283].height / 2);
						float num286 = Math.Abs(Projectile.position.X + (float)(Projectile.width / 2) - num284) + Math.Abs(Projectile.position.Y + (float)(Projectile.height / 2) - num285);
						if (num286 < 800f && num286 < num282 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num283].position, Main.npc[num283].width, Main.npc[num283].height))
						{
							num282 = num286;
							num280 = num284;
							num281 = num285;
							flag9 = true;
						}
					}
				}
			}
			if (!flag9)
			{
				num280 = Projectile.position.X + (float)(Projectile.width / 2) + Projectile.velocity.X * 100f;
				num281 = Projectile.position.Y + (float)(Projectile.height / 2) + Projectile.velocity.Y * 100f;
			}
			else
			{
				Projectile.friendly = true;
			}
			float num287 = 6f;
			float num288 = 0.1f;
			num287 = 9f;
			num288 = 0.2f;
			Vector2 vector25 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
			float num289 = num280 - vector25.X;
			float num290 = num281 - vector25.Y;
			float num291 = (float)Math.Sqrt(num289 * num289 + num290 * num290);
			float num292 = num291;
			num291 = num287 / num291;
			num289 *= num291;
			num290 *= num291;
			if (Projectile.velocity.X < num289)
			{
				Projectile.velocity.X += num288;
				if (Projectile.velocity.X < 0f && num289 > 0f)
				{
					Projectile.velocity.X += num288 * 2f;
				}
			}
			else if (Projectile.velocity.X > num289)
			{
				Projectile.velocity.X -= num288;
				if (Projectile.velocity.X > 0f && num289 < 0f)
				{
					Projectile.velocity.X -= num288 * 2f;
				}
			}
			if (Projectile.velocity.Y < num290)
			{
				Projectile.velocity.Y += num288;
				if (Projectile.velocity.Y < 0f && num290 > 0f)
				{
					Projectile.velocity.Y += num288 * 2f;
				}
			}
			else if (Projectile.velocity.Y > num290)
			{
				Projectile.velocity.Y -= num288;
				if (Projectile.velocity.Y > 0f && num290 < 0f)
				{
					Projectile.velocity.Y -= num288 * 2f;
				}
			}
		}

		public override void OnKill(int timeLeft)
		{
			//Produces blood on death
			for (int d = 0; d < 6; d++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 5);
			}
		}
	}
}