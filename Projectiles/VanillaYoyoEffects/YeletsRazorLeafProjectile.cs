using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.VanillaYoyoEffects
{
	public class YeletsRazorLeafProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.friendly = true;

			Projectile.DamageType = DamageClass.Melee;
			Projectile.timeLeft = 30;
		}

        public override void AI()
        {
			//Leafblower Leaf Ai
			if (Projectile.alpha > 0)
            {
				Projectile.alpha -= 30;
			}
			if (Projectile.alpha < 0)
			{
				Projectile.alpha = 0;
			}
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 3)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
			}
			if (Projectile.frame >= 5)
			{
				Projectile.frame = 0;
			}
			Projectile.velocity.X += Projectile.ai[0];
			Projectile.velocity.Y += Projectile.ai[1];
			Projectile.localAI[1] += 1f;
			if (Projectile.localAI[1] == 50f)
			{
				Projectile.localAI[1] = 51f;
				Projectile.ai[0] = (float)Main.rand.Next(-100, 101) * 6E-05f;
				Projectile.ai[1] = (float)Main.rand.Next(-100, 101) * 6E-05f;
			}
			if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) > 16f)
			{
				Projectile.velocity.X *= 0.95f;
				Projectile.velocity.Y *= 0.95f;
			}
			if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < 12f)
			{
				Projectile.velocity.X *= 1.05f;
				Projectile.velocity.Y *= 1.05f;
			}
			Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 3.14f;
		}

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(BuffID.Venom, 60 * Main.rand.Next(5, 8));
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
			target.AddBuff(BuffID.Venom, 60 * Main.rand.Next(5, 8));
        }

		public override void OnKill(int timeLeft)
		{
			for (int num349 = 0; num349 < 5; num349++)
			{
				int deathdust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 40);
				Main.dust[deathdust].velocity += Projectile.velocity;
				Main.dust[deathdust].scale = 0.8f;
			}
		}
	}
}