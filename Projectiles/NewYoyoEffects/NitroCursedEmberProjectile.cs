using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.NewYoyoEffects
{
	public class NitroCursedEmberProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.friendly = true; 

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
			Projectile.timeLeft = 60;

            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;

            Projectile.tileCollide = false;
		}

        public override void AI()
        {
            //Check for last ember
            bool LastEmberExists = false;
            Projectile LastEmberProjectile = this.Projectile;
            for (int num216 = 0; num216 < 1000; num216++)
            {
                if (Main.projectile[num216].active && Main.projectile[num216].owner == Projectile.owner && Main.projectile[num216].type == ModContent.ProjectileType<NitroCursedEmberProjectile>() && Main.projectile[num216].ai[1] == Projectile.ai[1])
                {
                    if((Main.projectile[num216].ai[0] == (Projectile.ai[0] - 1)) || ((Projectile.ai[0] == 1) && (Main.projectile[num216].ai[0] == 240)))
                    {
                        LastEmberExists = true;
                        LastEmberProjectile = Main.projectile[num216];
                    }
                }
            }

            //DustCounting
            Projectile.frameCounter++;

            //Flame dust for between Projectiles
            if (LastEmberExists)
            {
                //Work out dust location
                Vector2 ToLastEmber = Projectile.position - LastEmberProjectile.position;
                ToLastEmber *= Main.rand.NextFloat();

                //Produce Dust at incremenetal rates
                if (Projectile.timeLeft > 20)
                {
                    int num306 = Dust.NewDust(Projectile.position + ToLastEmber, Projectile.width, Projectile.height, DustID.CursedTorch, 0f, 0f, 100);
                    Main.dust[num306].position.X -= 2f;
                    Main.dust[num306].position.Y += 2f;
                    Main.dust[num306].rotation += Main.rand.NextFloat();
                    Dust dust63 = Main.dust[num306];
                    Dust dust189 = dust63;
                    dust189.scale += Main.rand.Next(50) * 0.01f;
                    Main.dust[num306].noGravity = true;
                    Main.dust[num306].velocity.Y -= 2f;
                    if (Main.rand.Next(2) == 0)
                    {
                        int num307 = Dust.NewDust(Projectile.position + ToLastEmber, Projectile.width, Projectile.height, DustID.CursedTorch, 0f, 0f, 100);
                        Main.dust[num307].position.X -= 2f;
                        Main.dust[num307].position.Y += 2f;
                        Main.dust[num307].rotation += Main.rand.NextFloat();
                        dust63 = Main.dust[num307];
                        dust189 = dust63;
                        dust189.scale += 0.3f + Main.rand.Next(50) * 0.01f;
                        Main.dust[num307].noGravity = true;
                        dust63 = Main.dust[num307];
                        dust189 = dust63;
                        dust189.velocity *= 0.1f;
                    }
                }
                if ((Projectile.timeLeft <= 20 && Projectile.timeLeft > 10) && Projectile.frameCounter % 2 == 0)
                {
                    int num306 = Dust.NewDust(Projectile.position + ToLastEmber, Projectile.width, Projectile.height, DustID.CursedTorch, 0f, 0f, 100);
                    Main.dust[num306].position.X -= 2f;
                    Main.dust[num306].position.Y += 2f;
                    Main.dust[num306].rotation += Main.rand.NextFloat();
                    Dust dust63 = Main.dust[num306];
                    Dust dust189 = dust63;
                    dust189.scale += Main.rand.Next(50) * 0.01f;
                    Main.dust[num306].noGravity = true;
                    Main.dust[num306].velocity.Y -= 2f;
                    if (Main.rand.Next(2) == 0)
                    {
                        int num307 = Dust.NewDust(Projectile.position + ToLastEmber, Projectile.width, Projectile.height, DustID.CursedTorch, 0f, 0f, 100);
                        Main.dust[num307].position.X -= 2f;
                        Main.dust[num307].position.Y += 2f;
                        Main.dust[num307].rotation += Main.rand.NextFloat();
                        dust63 = Main.dust[num307];
                        dust189 = dust63;
                        dust189.scale += 0.3f + Main.rand.Next(50) * 0.01f;
                        Main.dust[num307].noGravity = true;
                        dust63 = Main.dust[num307];
                        dust189 = dust63;
                        dust189.velocity *= 0.1f;
                    }
                }
                if (Projectile.timeLeft <= 10 && Projectile.frameCounter % 3 == 0)
                {
                    int num306 = Dust.NewDust(Projectile.position + ToLastEmber, Projectile.width, Projectile.height, DustID.CursedTorch, 0f, 0f, 100);
                    Main.dust[num306].position.X -= 2f;
                    Main.dust[num306].position.Y += 2f;
                    Main.dust[num306].rotation += Main.rand.NextFloat();
                    Dust dust63 = Main.dust[num306];
                    Dust dust189 = dust63;
                    dust189.scale += Main.rand.Next(50) * 0.01f;
                    Main.dust[num306].noGravity = true;
                    Main.dust[num306].velocity.Y -= 2f;
                    if (Main.rand.Next(2) == 0)
                    {
                        int num307 = Dust.NewDust(Projectile.position + ToLastEmber, Projectile.width, Projectile.height, DustID.CursedTorch, 0f, 0f, 100);
                        Main.dust[num307].position.X -= 2f;
                        Main.dust[num307].position.Y += 2f;
                        Main.dust[num307].rotation += Main.rand.NextFloat();
                        dust63 = Main.dust[num307];
                        dust189 = dust63;
                        dust189.scale += 0.3f + Main.rand.Next(50) * 0.01f;
                        Main.dust[num307].noGravity = true;
                        dust63 = Main.dust[num307];
                        dust189 = dust63;
                        dust189.velocity *= 0.1f;
                    }
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			//Apply 3-6 seconds of cursed inferno on impact
			target.AddBuff(BuffID.CursedInferno, 60 * (3 + Main.rand.Next(4)));
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
			//Apply 3-6 seconds of cursed inferno on impact
			target.AddBuff(BuffID.CursedInferno, 60 * (3 + Main.rand.Next(4)));
        }
	}
}