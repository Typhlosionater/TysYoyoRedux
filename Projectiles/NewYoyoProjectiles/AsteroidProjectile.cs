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
	public class AsteroidProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = 8f; //Lifetime: 1 per second
			ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 216f; //Range: 16 per Block
			ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 13.5f; //Speed: See Below
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
			//Fires Fireballs at regular intervals
			Projectile.frameCounter++;
			if(Projectile.frameCounter >= 40)
            {
				Projectile.frameCounter = 0;
				Projectile.netUpdate = true;

				Vector2 ProjectileVelocity = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
				int FiredProjectile = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, ProjectileVelocity, ProjectileID.BallofFire, Projectile.damage / 2, Projectile.knockBack / 2, Projectile.owner);
				Main.projectile[FiredProjectile].timeLeft = 90;
				Main.projectile[FiredProjectile].DamageType = DamageClass.Melee;
			}
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			//Apply on fire on impact
			if (Main.rand.Next(2) == 0)
            {
				target.AddBuff(BuffID.OnFire, 60 * 3);
			}
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
			//Apply on fire on impact
			if (Main.rand.Next(2) == 0)
			{
				target.AddBuff(BuffID.OnFire, 60 * 3);
			}
        }
	}
}