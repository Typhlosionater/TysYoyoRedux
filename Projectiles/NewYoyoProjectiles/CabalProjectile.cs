using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.NewYoyoProjectiles
{
	public class CabalProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 16f; //Lifetime: 1 per second
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 275f; //Range: 16 per Block
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 17f; //Speed: See Below
			//Prehard: Wood - 9f, Rally - 11f, Malaise - 12.5f, Artery - 12f, Amazon 13f, Code1 - 13f, Valor - 14f, Cascade - 14f
			//PreMech: Chik - 17f, FormatC - 15f, Helfire - 15f, Amarok - 14f, Gradient - 12f
			//PostMech: Code2 - 17f, Yelets - 16f, Kraken - 16f, EOC - 16.5f, Terrarian 17.5f
			//Dev: Valkyrie - 16f, Red's - 16f
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = ProjAIStyleID.Yoyo;
			Projectile.friendly = true; 

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
		}

		int OnHitEffectCooldown = 0;

		public override void AI()
		{
			//On hit effect cooldown
			OnHitEffectCooldown--;

			//Produce Light
			Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 11, 0.2f);
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			//Apply 5-10 seconds of ichor on impact
			target.AddBuff(BuffID.Ichor, 60 * (5 + Main.rand.Next(6)));

			//Calls 1-3 ichor drops from the sky on impact with enemies
			if (OnHitEffectCooldown <= 0)
			{
				int ProjectileAmount = Main.rand.Next(1, 4);
				for (int num41 = 0; num41 < ProjectileAmount; num41++)
				{
					Vector2 TargetLocation = new Vector2(Projectile.Center.X + Main.rand.Next(-16 * 3, 16 * 3), Projectile.Center.Y);
					Vector2 SpawnLocation = new Vector2(Projectile.Center.X + Main.rand.Next(-16 * 12, 16 * 12), Projectile.Center.Y - Main.rand.Next(16 * 30, 16 * 50));
					Vector2 ProjectileVelocity = TargetLocation - SpawnLocation;
					ProjectileVelocity.Normalize();
					ProjectileVelocity *= 2f;
					ProjectileVelocity *= Main.rand.NextFloat(0.6f, 1f);

					Projectile.NewProjectile(Projectile.GetSource_FromThis(), SpawnLocation, ProjectileVelocity, ModContent.ProjectileType<NewYoyoEffects.CabalIchorRainProjectile>(), Projectile.damage / 3, 0.5f, Projectile.owner, 0, Projectile.Center.Y);
				}

				OnHitEffectCooldown = 13;
			}
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			//Apply 5-10 seconds of ichor on impact
			target.AddBuff(BuffID.Ichor, 60 * (5 + Main.rand.Next(6)));

			//Calls 1-3 ichor drops from the sky on impact with enemies
			if (OnHitEffectCooldown == 0)
			{
				int ProjectileAmount = Main.rand.Next(1, 4);
				for (int num41 = 0; num41 < ProjectileAmount; num41++)
				{
					Vector2 TargetLocation = new Vector2(Projectile.Center.X + Main.rand.Next(-16 * 3, 16 * 3), Projectile.Center.Y);
					Vector2 SpawnLocation = new Vector2(Projectile.Center.X + Main.rand.Next(-16 * 12, 16 * 12), Projectile.Center.Y - Main.rand.Next(16 * 30, 16 * 50));
					Vector2 ProjectileVelocity = TargetLocation - SpawnLocation;
					ProjectileVelocity.Normalize();
					ProjectileVelocity *= 2f;
					ProjectileVelocity *= Main.rand.NextFloat(0.6f, 1f);

					Projectile.NewProjectile(Projectile.GetSource_FromThis(), SpawnLocation, ProjectileVelocity, ModContent.ProjectileType<NewYoyoEffects.CabalIchorRainProjectile>(), Projectile.damage / 3, 0.5f, Projectile.owner, 0, Projectile.Center.Y);

				}

				OnHitEffectCooldown = 13;
			}
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
		}
	}
}