using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TysYoyoRedux.Projectiles.NewYoyoEffects;


namespace TysYoyoRedux.Projectiles.NewYoyoProjectiles
{
	public class ExtraterrestrialTaserProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f; //Lifetime: 1 per second
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 384f; //Range: 16 per Block
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
			//FUCKING FUCK
			if(Projectile.scale == 1f)
            {
				Projectile.scale = 1.15f;
            }

			//Produce Light
			Lighting.AddLight(Projectile.Center, 0.1f, 0.2f, 0.225f);

			//On hit effect cooldown
			OnHitEffectCooldown--;
		}

		public override void PostDraw(Color lightColor)
        {
			Texture2D texture = ModContent.Request<Texture2D>("TysYoyoRedux/Projectiles/NewYoyoProjectiles/ExtraterrestrialTaserProjectile_Glowmask").Value;
			Main.spriteBatch.Draw
			(
				texture,
				new Vector2
				(
					Projectile.position.X - Main.screenPosition.X + Projectile.width * 0.5f,
					Projectile.position.Y - Main.screenPosition.Y + Projectile.height - texture.Height * 0.5f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),
				Color.White,
				Projectile.rotation,
				texture.Size() * 0.5f,
				Projectile.scale,
				SpriteEffects.None,
				0f
			);
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			//Electrosphere launcher electrosphere spawn
			if (OnHitEffectCooldown <= 0)
			{
				SoundEngine.PlaySound(SoundID.Item93 with { Volume = 0.3f }, Projectile.position);
				Rectangle value4 = new Rectangle((int)Projectile.Center.X - 40, (int)Projectile.Center.Y - 40, 80, 80);
				for (int num216 = 0; num216 < 1000; num216++)
				{
					if (num216 != Projectile.whoAmI && Main.projectile[num216].active && Main.projectile[num216].owner == Projectile.owner && Main.projectile[num216].type == 443 && Main.projectile[num216].getRect().Intersects(value4))
					{
						Main.projectile[num216].ai[1] = 1f;
						Main.projectile[num216].velocity = (Projectile.Center - Main.projectile[num216].Center) / 5f;
						Main.projectile[num216].netUpdate = true;
					}
				}
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<ExtraterrestrialTaserElectrosphere>(), hit.Damage / 3, 0.2f, Projectile.owner);

				OnHitEffectCooldown = 8;
			}
		}

		public override void OnHitPlayer(Player target, Player.HurtInfo info)
		{
			//Electrosphere launcher electrosphere spawn
			if (OnHitEffectCooldown <= 0)
			{
				SoundEngine.PlaySound(SoundID.Item93 with { Volume = 0.3f }, Projectile.position);
				Rectangle value4 = new Rectangle((int)Projectile.Center.X - 40, (int)Projectile.Center.Y - 40, 80, 80);
				for (int num216 = 0; num216 < 1000; num216++)
				{
					if (num216 != Projectile.whoAmI && Main.projectile[num216].active && Main.projectile[num216].owner == Projectile.owner && Main.projectile[num216].type == 443 && Main.projectile[num216].getRect().Intersects(value4))
					{
						Main.projectile[num216].ai[1] = 1f;
						Main.projectile[num216].velocity = (Projectile.Center - Main.projectile[num216].Center) / 5f;
						Main.projectile[num216].netUpdate = true;
					}
				}
				int projfire = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<ExtraterrestrialTaserElectrosphere>(), info.Damage / 3, 0.2f, Projectile.owner);
				Main.projectile[projfire].DamageType = DamageClass.Melee;
				Main.projectile[projfire].timeLeft /= 2;

				OnHitEffectCooldown = 8;
			}
		}
	}
}