using Terraria.ModLoader;


namespace TysYoyoRedux.Projectiles.VanillaYoyoEffects
{
	public class RallyAfterimageProjectile : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.friendly = true; 

            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
			Projectile.timeLeft = 10;
			Projectile.alpha = 85;

			Projectile.usesIDStaticNPCImmunity = true;
			Projectile.idStaticNPCHitCooldown = 20;

			Projectile.tileCollide = false;
			Projectile.scale = 0.95f;
		}

		public override void AI()
		{
			//fadeout
			Projectile.alpha += 17;
		}
	}
}