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
	public class PoltergeistProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f; //Lifetime: 1 per second
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 352f; //Range: 16 per Block
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 16.5f; //Speed: See Below
			//Prehard: Wood - 9f, Rally - 11f, Malaise - 12.5f, Artery - 12f, Amazon 13f, Code1 - 13f, Valor - 14f, Cascade - 14f
			//PreMech: Chik - 17f, FormatC - 15f, Helfire - 15f, Amarok - 14f, Gradient - 12f
			//PostMech: Code2 - 17f, Yelets - 16f, Kraken - 16f, EOC - 16.5f, Terrarian 17.5f
			//Dev: Valkyrie - 16f, Red's - 16f

			ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
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

			//Poltergeist Effect
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			//Red Afterimage
			Texture2D texture = ModContent.Request<Texture2D>("TysYoyoRedux/Projectiles/NewYoyoProjectiles/PoltergeistProjectile_Afterimage").Value;

			Vector2 drawOrigin = new Vector2(TextureAssets.Projectile[Projectile.type].Value.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * (((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length) * 0.3f);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, 1f, SpriteEffects.None, 0);
			}

			return base.PreDraw(ref lightColor);
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
		}

        public override void AI()
        {
			//FUCKING FUCK
			if (Projectile.scale == 1f)
			{
				Projectile.scale = 1.1f;
			}

			//Produce Light
			Lighting.AddLight(Projectile.Center, 0.65f, 0.35f, 0f);
		}
    }
}