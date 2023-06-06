using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Terraria.DataStructures;
using Terraria.Localization;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Terraria.Audio;
using Terraria.GameContent.Creative;
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

        //Add tooltips to vanilla yoyos
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (ModContent.GetInstance<TysYoyoReduxConfigServer>().VanillaYoyoEffects == true)
            {
                //Rally Tooltip
                if (item.type == ItemID.Rally)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Leaves damaging afterimages";
                    }
                }

                //Malaise Tooltip
                if (item.type == ItemID.CorruptYoyo)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Drips with festering bile";
                    }
                }

                //Artery Tooltip
                if (item.type == ItemID.CrimsonYoyo)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Has a chance to steal life from enemies";
                    }
                }

                //Amazon Tooltip
                if (item.type == ItemID.JungleYoyo)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Pointy & poisonous";
                    }
                }

                //Code 1 Tooltip
                if (item.type == ItemID.Code1)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Generates an electrostatic aura";
                    }
                }

                //Valor Tooltip
                if (item.type == ItemID.Valor)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Conjures ghostly wisps";
                    }
                }

                //Cascade Tooltip
                if (item.type == ItemID.Cascade)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Produces fiery explosions on impact";
                    }
                }

                //format:C Tooltip
                if (item.type == ItemID.FormatC)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Critical strikes deal double damage";
                    }
                }

                //Gradient Tooltip
                if (item.type == ItemID.Gradient)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Channels chromatic energy";
                    }
                }

                //Chik Tooltip
                if (item.type == ItemID.Chik)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Rapidly produces crystal shards";
                    }
                }

                //Hel-Fire Tooltip
                if (item.type == ItemID.HelFire)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Fires devastating flame torrents on impact";
                    }
                }

                //Amarok Tooltip
                if (item.type == ItemID.Amarok)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Leaves lingering snowflakes";
                    }
                }

                //Code 2 Tooltip
                if (item.type == ItemID.Code2)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Generates a supercharged electrostatic aura";
                    }
                }

                //Yelets Tooltip
                if (item.type == ItemID.Yelets)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Vicious & venomous";
                    }
                }

                //Valkyrie Yoyo Tooltip
                if (item.type == ItemID.ValkyrieYoyo)
                {
                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == "Tooltip0" && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text = "Fires flurries of feathers at nearby enemies\n'Great for impersonating devs!'";
                    }
                }

                //Red's Yoyo Tooltip
                if (item.type == ItemID.RedsYoyo)
                {
                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == "Tooltip0" && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text = "Inflicts a random debuff on impact\n'Great for impersonating devs!'";
                    }
                }

                //Kraken Tooltip
                if (item.type == ItemID.Kraken)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Lashes out with tentacles on impact";
                    }
                }

                //Eye of Cthulhu Tooltip
                if (item.type == ItemID.TheEyeOfCthulhu)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Produces mini seeking servants of cthulhu";
                    }
                }

                //Terrarian Tooltip
                if (item.type == ItemID.Terrarian)
                {
                    string LineToEdit = (item.material ? "Material" : "Knockback");

                    TooltipLine line = tooltips.FirstOrDefault(x => x.Name == LineToEdit && x.Mod == "Terraria");
                    if (line != null)
                    {
                        line.Text += "\n" + "Radiates with the soul of terraria";
                    }
                }
            }
        }
    }
}