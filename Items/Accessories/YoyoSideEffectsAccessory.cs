using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace TysYoyoRedux.Items.Accessories
{
    public class YoyoSideEffectsAccessory : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<TysYoyoReduxConfigServer>().AddNewAccessories;
        }

        public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Spiked Side Effects");
			Tooltip.SetDefault("Consecutive yoyo hits builds up combo\nCombo increases damage dealt by yoyos by up to 20%");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
		
		public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 18;
           	Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<TysYoyoReduxPlayer>().YoyoSideEffects = true;
        }
    }
}