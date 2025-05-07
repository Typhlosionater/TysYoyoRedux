using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.NewYoyoProjectiles
{
	public class RavagerProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 14f; //Lifetime: 1 per second
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 256f; //Range: 16 per Block
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 14.5f; //Speed: See Below
			//Prehard: Wood - 9f, Rally - 11f, Malaise - 12.5f, Artery - 12f, Amazon 13f, Code1 - 13f, Valor - 14f, Cascade - 14f
			//PreMech: Chik - 17f, FormatC - 15f, Helfire - 15f, Amarok - 14f, Gradient - 12f
			//PostMech: Code2 - 17f, Yelets - 16f, Kraken - 16f, EOC - 16.5f, Terrarian 17.5f
			//Dev: Valkyrie - 16f, Red's - 16f

			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
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

		public override bool PreDraw(ref Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * (((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.5f);
				Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}

			return base.PreDraw(ref lightColor);
		}

		int OnHitEffectCooldown = 0;

		public override void AI()
		{
			//On hit effect cooldown
			OnHitEffectCooldown--;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			//Produces 0-2 blood clot projectiles on impact
			if (OnHitEffectCooldown <= 0)
			{
				int ProjectileAmount = Main.rand.Next(3);
				int RandomRot = Main.rand.Next(0, 360);
				for (int d = 0; d < ProjectileAmount; d++)
				{
					RandomRot += 360 / ProjectileAmount;
					if (RandomRot >= 360)
					{
						RandomRot -= 360;
					}
					Vector2 value18 = new Vector2(4, 4).RotatedBy(MathHelper.ToRadians(RandomRot + Main.rand.Next(360 / (ProjectileAmount * 2), 360 / (ProjectileAmount * 2))));
					value18 = value18.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-30, 30)));
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, value18.X, value18.Y, ModContent.ProjectileType<NewYoyoEffects.RavagerBloodClotProjectile>(), Projectile.damage / 4, Projectile.knockBack / 4, Projectile.owner);
				}

				OnHitEffectCooldown = 18;
			}
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			//Produces 0-2 blood clot projectiles on impact
			if (OnHitEffectCooldown <= 0)
			{
				int ProjectileAmount = Main.rand.Next(3);
				int RandomRot = Main.rand.Next(0, 360);
				for (int d = 0; d < ProjectileAmount; d++)
				{
					RandomRot += 360 / ProjectileAmount;
					if (RandomRot >= 360)
					{
						RandomRot -= 360;
					}
					Vector2 value18 = new Vector2(4, 4).RotatedBy(MathHelper.ToRadians(RandomRot + Main.rand.Next(360 / (ProjectileAmount * 2), 360 / (ProjectileAmount * 2))));
					value18 = value18.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-30, 30)));
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, value18.X, value18.Y, ModContent.ProjectileType<NewYoyoEffects.RavagerBloodClotProjectile>(), Projectile.damage / 4, Projectile.knockBack / 4, Projectile.owner);
				}

				OnHitEffectCooldown = 18;
			}
		}
	}
}