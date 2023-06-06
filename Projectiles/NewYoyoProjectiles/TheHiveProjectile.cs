using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.NewYoyoProjectiles
{
	public class TheHiveProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Hive");

			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 10f; //Lifetime: 1 per second
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 216f; //Range: 16 per Block
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 13.5f; //Speed: See Below
			//Prehard: Wood - 9f, Rally - 11f, Malaise - 12.5f, Artery - 12f, Amazon 13f, Code1 - 13f, Valor - 14f, Cascade - 14f
			//PreMech: Chik - 17f, FormatC - 15f, Helfire - 15f, Amarok - 14f, Gradient - 12f
			//PostMech: Code2 - 17f, Yelets - 16f, Kraken - 16f, EOC - 16.5f, Terrarian 17.5f
			//Dev: Valkyrie - 16f, Red's - 16f
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = 99;
			Projectile.friendly = true; 
            Projectile.hostile = false;

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;

			Projectile.extraUpdates = 0;
			Projectile.scale = 1f;
		}

		int OnHitEffectCooldown = 0;

        public override void AI()
        {
			//On hit effect cooldown
			if (OnHitEffectCooldown > 0)
			{
				OnHitEffectCooldown--;
				Projectile.netUpdate = true;
			}
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			//Bees knees arrow style bee production effect
			if (OnHitEffectCooldown == 0)
			{
				for (int num591 = 0; num591 < 3; num591++)
				{
					if (num591 == 1 || Main.rand.Next(2) == 0)
					{
						Vector2 position8 = Projectile.position;
						Vector2 oldVelocity2 = Projectile.oldVelocity;
						oldVelocity2.Normalize();
						oldVelocity2 *= 8f;
						float num592 = (float)Main.rand.Next(-35, 36) * 0.01f;
						float num593 = (float)Main.rand.Next(-35, 36) * 0.01f;
						position8 -= oldVelocity2 * num591;
						num592 += Projectile.oldVelocity.X / 6f;
						num593 += Projectile.oldVelocity.Y / 6f;
						int num594 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position8.X, position8.Y, num592, num593, Main.player[Projectile.owner].beeType(), Main.player[Projectile.owner].beeDamage(damage / 3), Main.player[Projectile.owner].beeKB(0f), Main.myPlayer);
						Main.projectile[num594].DamageType = DamageClass.Melee;
					}
				}

				OnHitEffectCooldown = 13;
				Projectile.netUpdate = true;
			}
		}

		public override void OnHitPvp(Player target, int damage, bool crit)
		{
			//Bees knees arrow style bee production effect
			if (OnHitEffectCooldown == 0)
			{
				for (int num591 = 0; num591 < 3; num591++)
				{
					if (num591 == 1 || Main.rand.Next(2) == 0)
					{
						Vector2 position8 = Projectile.position;
						Vector2 oldVelocity2 = Projectile.oldVelocity;
						oldVelocity2.Normalize();
						oldVelocity2 *= 8f;
						float num592 = (float)Main.rand.Next(-35, 36) * 0.01f;
						float num593 = (float)Main.rand.Next(-35, 36) * 0.01f;
						position8 -= oldVelocity2 * num591;
						num592 += Projectile.oldVelocity.X / 6f;
						num593 += Projectile.oldVelocity.Y / 6f;
						int num594 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), position8.X, position8.Y, num592, num593, Main.player[Projectile.owner].beeType(), Main.player[Projectile.owner].beeDamage(damage / 3), Main.player[Projectile.owner].beeKB(0f), Main.myPlayer);
						Main.projectile[num594].DamageType = DamageClass.Melee;
					}
				}

				OnHitEffectCooldown = 13;
				Projectile.netUpdate = true;
			}
		}
	}
}