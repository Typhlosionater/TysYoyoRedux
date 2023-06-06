using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace TysYoyoRedux.Items.Accessories
{
    [AutoloadEquip(EquipType.Waist)]
    public class MasterYoyoKit : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<TysYoyoReduxConfigServer>().AddNewAccessories;
        }

        public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Master Yoyo Kit");
			Tooltip.SetDefault("Increases your yoyo skills beyond comprehension\n'The last of your kind, a true yoyo master...'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
		
		public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 15, 0, 0);
            Item.rare = ItemRarityID.Yellow;       
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //Vanilla effects
            player.counterWeight = 556 + Main.rand.Next(6);
            player.yoyoGlove = true;
            player.yoyoString = true;

            //Modded Effects
            player.GetModPlayer<TysYoyoReduxPlayer>().YoyoBearings = true;
            player.GetModPlayer<TysYoyoReduxPlayer>().YoyoSideEffects = true;
        }
 
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.YoyoBag, 1)
                .AddIngredient(ModContent.ItemType<CombinedYoyoAccessory>(), 1)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}