using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace TysYoyoRedux.Items.NewYoyos
{
	public class EnchantedYoyo : ModItem
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
			Item.damage = 12;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.useTime = 25;
			Item.useAnimation = 25;
			Item.knockBack = 4f;

			Item.width = 30;
			Item.height = 26;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.UseSound = SoundID.Item1;
			Item.channel = true;
			Item.noUseGraphic = true;

			Item.value = Item.sellPrice(0, 0, 10, 0);
			Item.rare = ItemRarityID.Blue;
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<Projectiles.NewYoyoProjectiles.EnchantedYoyoProjectile>();
			Item.shootSpeed = 16f;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.WoodYoyo, 1)
				.AddIngredient(ItemID.FallenStar, 1)
				.Register();
		}
    }
}