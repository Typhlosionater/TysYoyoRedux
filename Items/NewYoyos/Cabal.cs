using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.ObjectModel;
using Terraria.GameContent;

namespace TysYoyoRedux.Items.NewYoyos
{
	public class Cabal : ModItem
	{
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<TysYoyoReduxConfigServer>().AddNewYoyos;
        }

        public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

			ItemID.Sets.Yoyo[Item.type] = true;
			ItemID.Sets.GamepadExtraRange[Item.type] = 15;
			ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
		}

		public override void SetDefaults()
		{
			Item.damage = 39;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.useTime = 25;
			Item.useAnimation = 25;
			Item.knockBack = 3.3f;

			Item.width = 30;
			Item.height = 26;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.UseSound = SoundID.Item1;
			Item.channel = true;
			Item.noUseGraphic = true;

			Item.value = Item.sellPrice(0, 4, 0, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<Projectiles.NewYoyoProjectiles.CabalProjectile> ();
			Item.shootSpeed = 16f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.WoodYoyo, 1)
				.AddIngredient(ItemID.Ichor, 15)
				.AddIngredient(ItemID.SoulofNight, 10)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}

        public override void PostDrawTooltip(ReadOnlyCollection<DrawableTooltipLine> lines)
        {
            int verticalOffset = 0;

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Name == "Tooltip1")
                {
                    int flickerAmount = (int)((float)(int)Main.mouseTextColor);
                    Color drawColor = Color.Black;
                    for (int l = 0; l < 5; l++)
                    {
                        int xCoord = lines[i].X;
                        int yCoord = lines[i].Y;// + verticalOffset; IDK WHY THIS CHANGED BUT IT DID
                        if (l == 4)
                        {
                            drawColor = new Color(flickerAmount, flickerAmount, flickerAmount, flickerAmount);
                        }
                        switch (l)
                        {
                            case 0:
                                xCoord--;
                                break;
                            case 1:
                                xCoord++;
                                break;
                            case 2:
                                yCoord--;
                                break;
                            case 3:
                                yCoord++;
                                break;
                        }
                        Texture2D oneDropLogo = ((Texture2D)TextureAssets.OneDropLogo);
                        Main.spriteBatch.Draw(oneDropLogo, new Vector2(xCoord, yCoord), null, drawColor, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
                    }
                }
                verticalOffset += (int)FontAssets.MouseText.Value.MeasureString(lines[i].Text).Y;
            }
        }
    }
}