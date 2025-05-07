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
	public class SpectrumProjectile : ModProjectile
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
			Projectile.aiStyle = 99;
			Projectile.friendly = true; 
            Projectile.hostile = false;

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;

			Projectile.extraUpdates = 0;
			Projectile.scale = 1f;
		}

		public override void AI()
		{
			//FUCKING FUCK
			if (Projectile.scale == 1f)
			{
				Projectile.scale = 1.05f;
			}

			//Sundance Effect
			Projectile.frameCounter++;

			if (Projectile.frameCounter % 90 == 5)
			{
				int num25 = Projectile.frameCounter / 90;
				float num27 = 5f;
				float num28 = 1f / num27;
				for (float num29 = 0; num29 < 1f; num29 += num28)
				{
					float num30 = (num29 + num28 * 0.5f + (float)num25 * num28 * 0.5f) % 1f;
					float ai = MathHelper.TwoPi * num30;
					Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<NewYoyoEffects.SpectrumSundanceProjectile>(), Projectile.damage / 2, 0f, Projectile.owner, ai, Projectile.whoAmI);
				}
			}

		}

		public override void PostDraw(Color lightColor)
		{
			Texture2D texture = ModContent.Request<Texture2D>("TysYoyoRedux/Projectiles/NewYoyoProjectiles/SpectrumProjectile_Glowmask").Value;
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
	}
}