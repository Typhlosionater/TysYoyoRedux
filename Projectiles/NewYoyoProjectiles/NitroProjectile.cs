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
	public class NitroProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nitro");

			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 16f; //Lifetime: 1 per second
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 275f; //Range: 16 per Block
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

		int EmberNumber = 1;

		public override void AI()
		{
			//Produce ember trail when moving
			if(Projectile.velocity.Length() > 0.1)
            {
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0, 0, ModContent.ProjectileType<NewYoyoEffects.NitroCursedEmberProjectile>(), Projectile.damage / 2, 0, Projectile.owner, EmberNumber, Projectile.whoAmI);
			}

			//Count up ember number
			EmberNumber++;
			if (EmberNumber > 240)
            {
				EmberNumber = 1;
				Projectile.netUpdate = true;
			}

			//Produce Light
			Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 8, 0.2f);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			//Apply 5-10 seconds of cursed inferno on impact
			target.AddBuff(BuffID.CursedInferno, 60 * (5 + Main.rand.Next(6)));
		}

		public override void OnHitPvp(Player target, int damage, bool crit)
		{
			//Apply 5-10 seconds of cursed inferno on impact
			target.AddBuff(BuffID.CursedInferno, 60 * (5 + Main.rand.Next(6)));
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color(255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha, 255 - Projectile.alpha);
		}
	}
}