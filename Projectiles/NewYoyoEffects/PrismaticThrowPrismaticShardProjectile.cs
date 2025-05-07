using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.NewYoyoEffects
{
	public class PrismaticThrowPrismaticShardProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 8;
		}

		public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 6;
			Projectile.friendly = true; 

            Projectile.DamageType = DamageClass.Melee;
			Projectile.timeLeft = 600;

			Projectile.alpha = 50;
			Projectile.scale = 1.2f;
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

		public override void AI()
        {
			//On spawn choose random color and become intangible
			if (Projectile.timeLeft == 600)
			{
				Projectile.frame = Main.rand.Next(8);
				Projectile.friendly = false;
			}

			//Become tangible after 1/6 a second
			if (Projectile.timeLeft == 590)
            {
				Projectile.friendly = true;
            }

			//Crystal bullet shard Ai 
			Projectile.rotation += Projectile.velocity.X * 0.2f;
			Projectile.ai[1] += 1f;
			Projectile.velocity *= 0.985f;
			if (Projectile.ai[1] > 130f)
			{
				Projectile.scale -= 0.05f;
				if (Projectile.scale <= 0.2)
				{
					Projectile.scale = 0.2f;
					Projectile.Kill();
				}
			}
			Projectile.velocity *= 0.96f;
			if (Projectile.ai[1] > 15f)
			{
				Projectile.scale -= 0.05f;
				if (Projectile.scale <= 0.2)
				{
					Projectile.scale = 0.2f;
					Projectile.Kill();
				}
			}

			//Lighting
			switch (Projectile.frame)
			{
				case 0:
					Lighting.AddLight(Projectile.Center, 0.25f * Projectile.scale, 0.15f * Projectile.scale, 0.15f * Projectile.scale);
					break;
				case 1:
					Lighting.AddLight(Projectile.Center, 0.25f * Projectile.scale, 0.2f * Projectile.scale, 0.1f * Projectile.scale);
					break;
				case 2:
					Lighting.AddLight(Projectile.Center, 0.25f * Projectile.scale, 0.25f * Projectile.scale, 0.05f * Projectile.scale);
					break;
				case 3:
					Lighting.AddLight(Projectile.Center, 0.15f * Projectile.scale, 0.25f * Projectile.scale, 0.15f * Projectile.scale);
					break;
				case 4:
					Lighting.AddLight(Projectile.Center, 0.05f * Projectile.scale, 0.25f * Projectile.scale, 0.25f * Projectile.scale);
					break;
				case 5:
					Lighting.AddLight(Projectile.Center, 0.15f * Projectile.scale, 0.15f * Projectile.scale, 0.25f * Projectile.scale);
					break;
				case 6:
					Lighting.AddLight(Projectile.Center, 0.25f * Projectile.scale, 0.05f * Projectile.scale, 0.25f * Projectile.scale);
					break;
				case 7:
					Lighting.AddLight(Projectile.Center, 0.25f * Projectile.scale, 0.1f * Projectile.scale, 0.2f * Projectile.scale);
					break;
			}
		}
	}
}