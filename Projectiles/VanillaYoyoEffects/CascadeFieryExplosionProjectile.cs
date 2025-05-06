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
	public class CascadeFieryExplosionProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 80;
			Projectile.height = 80;
			Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
			Projectile.timeLeft = 3;

			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}

		public override void AI()
		{
			//Explosion SFX
			if (Projectile.timeLeft == 3)
			{
				//Play sound
				SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);

				//Grenade explosion effect
				for (int num912 = 0; num912 < 20; num912++)
				{
					int num913 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 1.5f);
					Dust dust = Main.dust[num913];
					dust.velocity *= 1.4f;
				}
				for (int num914 = 0; num914 < 10; num914++)
				{
					int num915 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 2.5f);
					Main.dust[num915].noGravity = true;
					Dust dust = Main.dust[num915];
					dust.velocity *= 5f;
					num915 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
					dust = Main.dust[num915];
					dust.velocity *= 3f;
				}
				for (int i = 0; i < 2; i++)
                {
					int num916 = Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, default(Vector2), Main.rand.Next(61, 64));
					Gore gore = Main.gore[num916];
					gore.velocity *= 0.4f;
					Vector2 GoreRot = new Vector2(1, 1).RotatedByRandom(MathHelper.ToRadians(360));
					Main.gore[num916].velocity.X += GoreRot.X;
					Main.gore[num916].velocity.Y += GoreRot.Y;
				}
			}
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			if (Main.expertMode && target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail)
			{
				modifiers.FinalDamage /= 5f;
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			//Has a 1/3 chance to inflict 1-4 seconds of on fire! on impact
			if(Main.rand.Next(3) == 0)
            {
				target.AddBuff(BuffID.OnFire, 60 * (1 + Main.rand.Next(4)));
			}
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			//Has a 1/3 chance to inflict 1-4 seconds of on fire! on impact
			if (Main.rand.Next(3) == 0)
			{
				target.AddBuff(BuffID.OnFire, 60 * (1 + Main.rand.Next(4)));
			}
		}
	}
}