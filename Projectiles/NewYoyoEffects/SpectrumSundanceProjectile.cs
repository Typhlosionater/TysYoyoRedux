using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace TysYoyoRedux.Projectiles.NewYoyoEffects
{
    public class SpectrumSundanceProjectile : ModProjectile
    {
        private ref float AI_FrameCounter => ref Projectile.localAI[0];
        private ref float AI_RotationOffset => ref Projectile.ai[0];
        private ref float AI_YoyoWhoAmI => ref Projectile.ai[1];

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.timeLeft = 150;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
        }

        private float maxTimeLeft = 150f;
        private float initialScale = 0.2f;

        public override void AI()
        {
            // Increment frame counter
            AI_FrameCounter++;
            if (AI_FrameCounter > maxTimeLeft)
            {
                Projectile.Kill();
                return;
            }

            // Makes projectile fade in
            Projectile.alpha -= 15;
            if (Projectile.alpha < 0)
                Projectile.alpha = 0;

            float rotationOffset = MathHelper.Pi / 0.75f; //1.5f
            Projectile.scale = Utils.GetLerpValue(0f, 20f, AI_FrameCounter, true) * Utils.GetLerpValue(maxTimeLeft, maxTimeLeft - 60, AI_FrameCounter, true) * initialScale;
            float lerpValue = Utils.GetLerpValue(0f, maxTimeLeft, AI_FrameCounter, true);
            Projectile.rotation = AI_RotationOffset + lerpValue * rotationOffset;

            Projectile yoyoOwner = Main.projectile[(int)AI_YoyoWhoAmI];
            if (yoyoOwner.active)
            {
                // Move to our yoyo's center
                Projectile.Center = yoyoOwner.Center;
                Projectile.velocity = Vector2.Zero;
                // I think this is for lighting?
                Vector2 rotationVector = Projectile.rotation.ToRotationVector2();
                Vector3 v3_ = Main.hslToRgb((AI_FrameCounter / (MathHelper.TwoPi) + AI_FrameCounter / maxTimeLeft) % 1f, 1f, 0.85f).ToVector3() * Projectile.scale; // No clue what this is but its in vanilla code
                float scaleFactor = 800f * Projectile.scale;
                DelegateMethods.v3_1 = v3_;
                for (float i = 0; i < 1f; i += 0.08333336f) // Funky for loop
                {
                    Point point = (Projectile.Center + rotationVector * scaleFactor * i).ToTileCoordinates();
                    DelegateMethods.CastLightOpen(point.X, point.Y);
                }
            }
            else
                Projectile.Kill();
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Rectangle rectangle = texture.Frame(1, 2);
            Rectangle rectangle2 = texture.Frame(1, 2, 0, 1);
            Vector2 origin = rectangle.Size() * new Vector2(0.03f, 0.5f);
            float rotation = AI_RotationOffset / (MathHelper.TwoPi + AI_FrameCounter / maxTimeLeft);
            float scale = Utils.GetLerpValue(0f, 30f, AI_FrameCounter, true) * Utils.GetLerpValue(maxTimeLeft, maxTimeLeft - 30f, AI_FrameCounter, true);
            Color drawColor1 = Main.hslToRgb(rotation % 1f, 1f, 1f) * scale;
            float lerpValue = Utils.GetLerpValue(0f, 30f, AI_FrameCounter, true);
            Vector2 vector = new Vector2(1f, MathHelper.Lerp(0.25f, 0.7f, lerpValue)) * scale;
            Color drawColor2 = Main.hslToRgb((rotation + 0.3f) % 1f, 1f, MathHelper.Lerp(0.3f, 0.66f, lerpValue)) * scale;
            drawColor2 = Color.Lerp(drawColor2, Color.White, 0.1f);
            drawColor2.A /= 2;

            Main.spriteBatch.Draw(texture, drawPosition, rectangle2, drawColor2, Projectile.rotation, origin, vector * 1.2f * initialScale, SpriteEffects.None, 0f);

            Color drawColor3 = Main.hslToRgb((maxTimeLeft + 0.15f) % 1f, 1f, MathHelper.Lerp(0.3f, 0.5f, lerpValue)) * scale;
            drawColor3 = Color.Lerp(drawColor3, Color.White, 0.1f);
            drawColor3.A /= 2;
            Main.spriteBatch.Draw(texture, drawPosition, rectangle2, drawColor3, Projectile.rotation, origin, vector * 1.1f * initialScale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture, drawPosition, rectangle, drawColor1 * 0.5f, Projectile.rotation, origin, vector * initialScale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(texture, drawPosition, rectangle2, drawColor1 * lerpValue, Projectile.rotation, origin, vector * initialScale, SpriteEffects.None, 0f);

            return false;
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCs.Add(index);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float collisionPoint = 0f;
            Vector2 objectPosition = targetHitbox.TopLeft();
            Vector2 objectDimensions = targetHitbox.Size();
            Vector2 rotVector = Projectile.rotation.ToRotationVector2();
            float scale = initialScale * 0.7f;

            if (Collision.CheckAABBvLineCollision(objectPosition, objectDimensions, Projectile.Center, Projectile.Center + rotVector * scale * 510f, scale * 100f, ref collisionPoint))
                return true;
            if (Collision.CheckAABBvLineCollision(objectPosition, objectDimensions, Projectile.Center, Projectile.Center + rotVector * scale * 660, scale * 60f, ref collisionPoint))
                return true;
            if (Collision.CheckAABBvLineCollision(objectPosition, objectDimensions, Projectile.Center, Projectile.Center + rotVector * scale * 800, scale * 10f, ref collisionPoint))
                return true;

            return false;
        }
    }
}