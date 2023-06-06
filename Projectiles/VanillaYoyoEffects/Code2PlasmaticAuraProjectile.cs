using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.VanillaYoyoEffects
{
	public class Code2PlasmaticAuraProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Plasmatic Aura");

			Main.projFrames[Projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			Projectile.width = 160;
			Projectile.height = 160;
			Projectile.friendly = true; 
            Projectile.hostile = false;

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
			Projectile.alpha = 255;

			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 10;

			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.scale = 1f;
		}

		public override void AI()
		{
			//Set parent
			Projectile parent = Main.projectile[(int)Projectile.ai[1]];

			//Movement
			Projectile.Center = parent.Center;
			if (!parent.active)
			{
				Projectile.Kill();
			}
			else
			{
				Projectile.timeLeft = 2;
			}

			//Animation
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 6)
            {
				Projectile.frameCounter = 0;
				Projectile.netUpdate = true;

				Projectile.frame++;
				if (Projectile.frame >= 4)
                {
					Projectile.frame = 0;
                }
			}

			//Rotates
			Projectile.rotation += 0.01f;

			//Fade in and grow
			if (Projectile.ai[0] == 0)
			{
				Projectile.scale = 0.05f;
			}
			if (Projectile.ai[0] < 19)
			{
				Projectile.scale += 0.05f;
				Projectile.alpha -= 10;

				Projectile.ai[0]++;
			}

			//Produce Light
			Lighting.AddLight(Projectile.Center, 0.2f, 0.4f, 0.45f);
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 200 - Projectile.alpha);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			//Code from Examplebullet to make bullet not affected by coloured lightning
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
	}
}