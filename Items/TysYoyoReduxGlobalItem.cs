using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace TysYoyoRedux.Items
{
    public class TysYoyoReduxGlobalItem : GlobalItem
    {
        //New Loot Bag Items
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            //Spectrum drops from empress of light's treasure bag
            if (ModContent.GetInstance<TysYoyoReduxConfigServer>().AddNewYoyos && item.type == ItemID.FairyQueenBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.NewYoyos.Spectrum>(), 20));
            }
        }

        //Add Recipies to Vanilla Yoyos
        public override void AddRecipes()
        {
            if (ModContent.GetInstance<TysYoyoReduxConfigServer>().VanillaYoyoRecipes == true)
            {
                //Valkyrie Yoyo Recipie
                Recipe.Create(ItemID.ValkyrieYoyo)
                    .AddIngredient(ItemID.WoodYoyo, 1)
                    .AddIngredient(ItemID.Feather, 25)
                    .AddIngredient(ItemID.SoulofMight, 5)
                    .AddIngredient(ItemID.SoulofSight, 5)
                    .AddIngredient(ItemID.SoulofFright, 5)
                    .AddTile(TileID.MythrilAnvil)
                    .Register();

                //Red's Throw Recipie
                Recipe.Create(ItemID.RedsYoyo)
                    .AddIngredient(ItemID.WoodYoyo, 1)
                    .AddIngredient(ItemID.Obsidian, 25)
                    .AddIngredient(ItemID.SoulofMight, 5)
                    .AddIngredient(ItemID.SoulofSight, 5)
                    .AddIngredient(ItemID.SoulofFright, 5)
                    .AddTile(TileID.MythrilAnvil)
                    .Register();
            }
        }

        private static readonly HashSet<int> modifiedVanillaYoyos =
        [
            ItemID.Rally,
            ItemID.CorruptYoyo,
            ItemID.CrimsonYoyo,
            ItemID.JungleYoyo,
            ItemID.Code1,
            ItemID.Valor,
            ItemID.Cascade,
            ItemID.FormatC,
            ItemID.Gradient,
            ItemID.Chik,
            ItemID.HelFire,
            ItemID.Amarok,
            ItemID.Code2,
            ItemID.Yelets,
            ItemID.ValkyrieYoyo,
            ItemID.RedsYoyo,
            ItemID.Kraken,
            ItemID.TheEyeOfCthulhu,
            ItemID.Terrarian,
        ];

        private void AddTooltip(List<TooltipLine> tooltips, Item item)
        {
            int index = tooltips.FindIndex(x => x.Name == "Tooltip0");
            if (index == -1)
            {
                index = tooltips.FindIndex(x => item.material ? x.Name == "Material" : x.Name == "Knockback");
                index++; // We want to insert after material or knockback, but before tooltip if it exists, so only increment here
                if (index == -1)
                {
                    // Should never happen, but bail out to avoid errors
                    return;
                }
            }

            TooltipLine line = new TooltipLine(Mod, "VanillaItemTooltip",
                Mod.GetLocalization($"VanillaItemTooltips.{ItemID.Search.GetName(item.type)}").Value);
            tooltips.Insert(index, line);
        }

        //Add tooltips to vanilla yoyos
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<TysYoyoReduxConfigServer>().VanillaYoyoEffects == true && modifiedVanillaYoyos.Contains(item.type))
            {
                AddTooltip(tooltips, item);
            }

        }
    }
}