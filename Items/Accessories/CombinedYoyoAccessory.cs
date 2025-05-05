using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace TysYoyoRedux.Items.Accessories
{
    public class CombinedYoyoAccessory : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<TysYoyoReduxConfigServer>().AddNewAccessories;
        }

        public override void SetStaticDefaults() 
		{
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
		
		public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
           	Item.value = Item.sellPrice(0, 12, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<TysYoyoReduxPlayer>().YoyoBearings = true;
            player.GetModPlayer<TysYoyoReduxPlayer>().YoyoSideEffects = true;
        }
 
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<YoyoSideEffectsAccessory>(), 1)
                .AddIngredient(ModContent.ItemType<YoyoBearingAccessory>(), 1)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}