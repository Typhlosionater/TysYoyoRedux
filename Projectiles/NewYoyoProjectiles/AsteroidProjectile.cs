using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TysYoyoRedux.Projectiles.NewYoyoEffects;


namespace TysYoyoRedux.Projectiles.NewYoyoProjectiles
{
	public class AsteroidProjectile : ModProjectile
	{
		private ref float Timer => ref Projectile.ai[2];

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
			Projectile.aiStyle = ProjAIStyleID.Yoyo;
			Projectile.friendly = true; 

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
		}

        public override void AI()
        {
			//Fires Fireballs at regular intervals
			Timer++;
			if (Timer >= 40 && Projectile.owner == Main.myPlayer)
			{
				Timer = 0;
				Projectile.netUpdate = true;

				Vector2 ProjectileVelocity = new Vector2(2, 2).RotatedByRandom(MathHelper.ToRadians(360));
				Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, ProjectileVelocity, ModContent.ProjectileType<AsteroidFireball>(), Projectile.damage / 2, Projectile.knockBack / 2, Projectile.owner);
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