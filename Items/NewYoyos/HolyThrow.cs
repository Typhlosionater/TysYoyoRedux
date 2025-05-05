using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace TysYoyoRedux.Items.NewYoyos
{
	public class HolyThrow : ModItem
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
			Item.damage = 50;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.useTime = 25;
			Item.useAnimation = 25;
			Item.knockBack = 5f;

			Item.width = 30;
			Item.height = 26;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.UseSound = SoundID.Item1;
			Item.channel = true;
			Item.noUseGraphic = true;

			Item.value = Item.sellPrice(0, 4, 60, 0);
			Item.rare = ItemRarityID.Pink;
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<Projectiles.NewYoyoProjectiles.HolyThrowProjectile> ();
			Item.shootSpeed = 16f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HallowedBar, 12)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}