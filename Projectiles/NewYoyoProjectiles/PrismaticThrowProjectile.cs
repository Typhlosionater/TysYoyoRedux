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
	public class PrismaticThrowProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prismatic Throw");

			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 6f; //Lifetime: 1 per second
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 192f; //Range: 16 per Block
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 11f; //Speed: See Below
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

		public override bool PreDraw(ref Color lightColor)
		{
			//Code from Examplebullet to make bullet not affected by lightning
			Main.instance.LoadProjectile(Projectile.type);
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			// Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = (Projectile.oldPos[k] - Main.screenPosition) + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}

		int LightingCurrentColour;

		bool ColourSelected;

		int OnHitEffectCooldown = 0;

		public override void AI()
		{
			//Startup decide colour
			if (!ColourSelected)
			{
				ColourSelected = true;
				Projectile.netUpdate = true;

				LightingCurrentColour = Main.rand.Next(16);
			}

			//Changes lighting colour at set intervals
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 10)
			{
				Projectile.frameCounter = 0;
				Projectile.netUpdate = true;

				LightingCurrentColour++;
				if(LightingCurrentColour >= 16)
                {
					LightingCurrentColour = 0;
                }
			}

			//Rainbow Lighting
			switch (LightingCurrentColour)
			{
				case 0:
					Lighting.AddLight(Projectile.Center, 0.15f, 0.09f, 0.09f);
					break;
				case 1:
					Lighting.AddLight(Projectile.Center, 0.15f, 0.105f, 0.0705f);//1
					break;
				case 2:
					Lighting.AddLight(Projectile.Center, 0.15f, 0.12f, 0.06f);
					break;
				case 3:
					Lighting.AddLight(Projectile.Center, 0.15f, 0.135f, 0.045f);//2
					break;
				case 4:
					Lighting.AddLight(Projectile.Center, 0.15f, 0.15f, 0.03f);
					break;
				case 5:
					Lighting.AddLight(Projectile.Center, 0.12f, 0.15f, 0.06f);//3
					break;
				case 6:
					Lighting.AddLight(Projectile.Center, 0.09f, 0.15f, 0.09f);
					break;
				case 7:
					Lighting.AddLight(Projectile.Center, 0.06f, 0.15f, 0.12f);//4
					break;
				case 8:
					Lighting.AddLight(Projectile.Center, 0.03f, 0.15f, 0.15f);
					break;
				case 9:
					Lighting.AddLight(Projectile.Center, 0.06f, 0.12f, 0.15f);//5
					break;
				case 10:
					Lighting.AddLight(Projectile.Center, 0.09f, 0.09f, 0.15f);
					break;
				case 11:
					Lighting.AddLight(Projectile.Center, 0.12f, 0.06f, 0.15f);//6
					break;
				case 12:
					Lighting.AddLight(Projectile.Center, 0.15f, 0.03f, 0.15f);
					break;
				case 13:
					Lighting.AddLight(Projectile.Center, 0.15f, 0.045f, 0.135f);//7
					break;
				case 14:
					Lighting.AddLight(Projectile.Center, 0.15f, 0.06f, 0.12f);
					break;
				case 15:
					Lighting.AddLight(Projectile.Center, 0.15f, 0.075f, 0.105f);//8
					break;
			}

			//On hit effect cooldown
			if (OnHitEffectCooldown > 0)
			{
				OnHitEffectCooldown--;
				Projectile.netUpdate = true;
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			//Produces 0-2 Prismatic shard projectiles on impact, edited from crystal bullets
			if (OnHitEffectCooldown == 0)
			{
				int ProjectileAmount = Main.rand.Next(3);
				for (int i = 0; i < ProjectileAmount; i++)
				{
					Vector2 VariableMomentum = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
					float num569 = (0f - VariableMomentum.X) * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
					float num570 = (0f - VariableMomentum.Y) * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(num569, num570), ModContent.ProjectileType<NewYoyoEffects.PrismaticThrowPrismaticShardProjectile>(), Projectile.damage / 4, 0f, Projectile.owner);
				}

				OnHitEffectCooldown = 8;
				Projectile.netUpdate = true;
			}
		}

		public override void OnHitPvp(Player target, int damage, bool crit)
		{
			//Produces 0-2 Prismatic shard projectiles on impact, edited from crystal bullets
			if (OnHitEffectCooldown == 0)
			{
				int ProjectileAmount = Main.rand.Next(3);
				for (int i = 0; i < ProjectileAmount; i++)
				{
					Vector2 VariableMomentum = new Vector2(4, 4).RotatedByRandom(MathHelper.ToRadians(360));
					float num569 = (0f - VariableMomentum.X) * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
					float num570 = (0f - VariableMomentum.Y) * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, new Vector2(num569, num570), ModContent.ProjectileType<NewYoyoEffects.PrismaticThrowPrismaticShardProjectile>(), Projectile.damage / 4, 0f, Projectile.owner);
				}

				OnHitEffectCooldown = 8;
				Projectile.netUpdate = true;
			}
		}
	}
}