using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace TysYoyoRedux.Items.Accessories
{
    public class YoyoBearingAccessory : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<TysYoyoReduxConfigServer>().AddNewAccessories;
        }

        public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Ball Bearings");
			Tooltip.SetDefault("Increases yoyo lifetime by 50%");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
		
		public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
           	Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<TysYoyoReduxPlayer>().YoyoBearings = true;
        }
    }
}