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
	public class RavagerBloodClotProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blood Clot");

			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.friendly = true; 
            Projectile.hostile = false;

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 1;
			Projectile.timeLeft = 200;

			Projectile.ignoreWater = false;
			Projectile.tileCollide = true;
			Projectile.scale = 0.8f;
		}

		public Vector2 initialSpeed;

		public bool Targeted;

		public override void AI()
		{
			//dust
			int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 5);
			Main.dust[dustIndex].velocity *= 0.2f;

			//Sets up needed variables
			if (Projectile.timeLeft == 200)
			{
				initialSpeed = Projectile.velocity;
				Targeted = false;
				Projectile.friendly = false;
			}

			//Slows down on spawn
			if (Projectile.timeLeft > 170)
			{
				Projectile.velocity -= (initialSpeed / 30);
			}
			if (Projectile.timeLeft == 170)
			{
				Projectile.velocity *= 0;
				Projectile.friendly = true;
			}

			//Auto Aim at nearby enemies
			if (Projectile.timeLeft <= 150)
			{
				if (Targeted == false)
				{
					Vector2 EnemyPos = new Vector2(0, 0);
					float AttackRange = 500f;
					bool ValidTarget = false;
					for (int NpcCheck = 0; NpcCheck < 200; NpcCheck++)
					{
						NPC nPC = Main.npc[NpcCheck];
						if (Main.npc[NpcCheck].CanBeChasedBy(this, ignoreDontTakeDamage: true) && Vector2.Distance(Projectile.Center, nPC.Center) <= AttackRange && Collision.CanHit(Projectile.Center, 1, 1, Main.npc[NpcCheck].Center, 1, 1))
						{
							if (Vector2.Distance(Projectile.Center, nPC.Center) < Vector2.Distance(Projectile.Center, EnemyPos))
							{
								EnemyPos = nPC.Center;
								ValidTarget = true;
							}
						}
					}
					if (ValidTarget)
					{
						Vector2 NewVelocity = EnemyPos - Projectile.Center;
						NewVelocity.Normalize();
						NewVelocity *= 8;
						Projectile.velocity = NewVelocity;
						Targeted = true;
					}
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			for (int d = 0; d < 10; d++)
			{
				int dustIndex = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 5);
			}
		}
	}
}