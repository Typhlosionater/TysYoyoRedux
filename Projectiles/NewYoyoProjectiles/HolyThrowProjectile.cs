using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.NewYoyoProjectiles
{
	public class HolyThrowProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailCacheLength[Type] = 15;
			ProjectileID.Sets.TrailingMode[Type] = 5;

			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 20f; //Lifetime: 1 per second
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 320f; //Range: 16 per Block
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

			Projectile.ArmorPenetration = 10;
		}

		public override void AI()
		{
			//Gungir Dust
			if (Main.rand.Next(6) == 0)
			{
				int num20 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 57, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 200, default(Color), 0.75f);
				Main.dust[num20].velocity += Projectile.velocity * 0.3f;
				Main.dust[num20].velocity *= 0.2f;
			}
			if (Main.rand.Next(8) == 0)
			{
				int num21 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 43, 0f, 0f, 254, default(Color), 0.2f);
				Main.dust[num21].velocity += Projectile.velocity * 0.5f;
				Main.dust[num21].velocity *= 0.5f;
			}
		}

		private static VertexStrip vertexStrip = new();

		public override bool PreDraw(ref Color lightColor)
		{
			Color StripColors(float progressOnStrip)
			{
				float num = 1f - progressOnStrip;
				Color result = new Color(48, 63, 150) * (num * num * num * num) * 0.5f;
				result.A = 0;
				return result;
			}

			float StripWidth(float progressOnStrip) => 6f;

			MiscShaderData miscShaderData = GameShaders.Misc["LightDisc"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(2f);
			miscShaderData.Apply();
			vertexStrip.PrepareStripWithProceduralPadding(Projectile.oldPos, Projectile.oldRot, StripColors, StripWidth, -Main.screenPosition + Projectile.Size / 2f);
			vertexStrip.DrawTrail();

			Main.pixelShader.CurrentTechnique.Passes[0].Apply();

			return true;
		}
	}
}