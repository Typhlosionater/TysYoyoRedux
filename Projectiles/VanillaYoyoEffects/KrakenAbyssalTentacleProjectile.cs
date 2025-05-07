using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.VanillaYoyoEffects
{
	public class KrakenAbyssalTentacleProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.TrailingMode[Type] = 5;
			ProjectileID.Sets.TrailCacheLength[Type] = 20;
		}

		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 40;
			Projectile.friendly = true; 

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;

			Projectile.tileCollide = false;
			Projectile.extraUpdates = 3;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 0);
		}

		public override void AI()
		{
			//Point forward
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

			//Fade in
			if (Projectile.alpha > 80)
			{
				Projectile.alpha -= 10;
			}

			//Shadowflame tentacle effect
			Vector2 center2 = Projectile.Center;
			Projectile.scale = 1f - Projectile.localAI[0];
			Projectile.width = (int)(20f * Projectile.scale);
			Projectile.height = Projectile.width;
			Projectile.position.X = center2.X - (float)(Projectile.width / 2);
			Projectile.position.Y = center2.Y - (float)(Projectile.height / 2);
			if ((double)Projectile.localAI[0] < 0.1)
			{
				Projectile.localAI[0] += 0.01f;
			}
			else
			{
				Projectile.localAI[0] += 0.025f;
			}
			if (Projectile.localAI[0] >= 0.95f)
			{
				Projectile.Kill();
			}
			Projectile.velocity.X += Projectile.ai[0] * 1.5f;
			Projectile.velocity.Y += Projectile.ai[1] * 1.5f;
			if (Projectile.velocity.Length() > 16f)
			{
				Projectile.velocity.Normalize();
				Projectile.velocity *= 16f;
			}
			Projectile.ai[0] *= 1.05f;
			Projectile.ai[1] *= 1.05f;
			if (Projectile.scale < 1f)
			{
				for (int num1096 = 0; (float)num1096 < Projectile.scale * 3f; num1096++)
				{
					int num1098 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 160, Projectile.velocity.X, Projectile.velocity.Y, 100, Color.White, 1.2f);
					Main.dust[num1098].position = (Main.dust[num1098].position + Projectile.Center) / 2f;
					Main.dust[num1098].noGravity = true;
					Dust dust53 = Main.dust[num1098];
					Dust dust189 = dust53;
					dust189.velocity *= 0.1f;
					dust53 = Main.dust[num1098];
					dust189 = dust53;
					dust189.velocity -= Projectile.velocity * (1.3f - Projectile.scale);
					Main.dust[num1098].fadeIn = 100 + Projectile.owner;
					dust53 = Main.dust[num1098];
					dust189 = dust53;
					dust189.scale += Projectile.scale * 0.75f;
				}
			}
		}

		private static VertexStrip vertexStrip = new();

		public override bool PreDraw(ref Color lightColor)
		{
			Color StripColors(float progressOnStrip)
			{
				return Color.Turquoise;
			}

			float StripWidth(float progressOnStrip) => float.Max(2f, progressOnStrip * 10f);

			MiscShaderData miscShaderData = GameShaders.Misc["LightDisc"];
			miscShaderData.UseSaturation(-2.8f);
			miscShaderData.UseOpacity(2f);
			miscShaderData.Apply();
			var positions = Projectile.oldPos.Select(x => x + (Projectile.Size / 2f));
			vertexStrip.PrepareStripWithProceduralPadding(Projectile.oldPos, Projectile.oldRot, StripColors, StripWidth, -Main.screenPosition + Projectile.Size / 2f);
			vertexStrip.DrawTrail();

			Main.pixelShader.CurrentTechnique.Passes[0].Apply();

			return false;
		}
	}
}