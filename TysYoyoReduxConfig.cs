using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;

namespace TysYoyoRedux
{
    [Label("Mod Config")]
    class TysYoyoReduxConfigServer : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        //Vanilla Yoyo Effects
        [Label("Give effects to vanilla yoyos")]
        [Tooltip("Gives a special effect to each vanilla yoyo. (Enabled by default)")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool VanillaYoyoEffects  { get; set; }

        //Vanilla Yoyo Recipes
        [Label("Give recipes to developer yoyos")]
        [Tooltip("Gives recipes to Red's Throw and Valkyrie Yoyo. (Enabled by default)")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool VanillaYoyoRecipes { get; set; }

        //Add Modded Yoyos
        [Label("Add new modded yoyos")]
        [Tooltip("Adds new modded yoyos to various points in progression. (Enabled by default)")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool AddNewYoyos { get; set; }

        //Add Modded Yoyo Accessories
        [Label("Add new modded yoyo accessories")]
        [Tooltip("Adds new modded yoyo subclass accessories. (Enabled by default)")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool AddNewAccessories { get; set; }

        //Changes to counterweights
        [Label("Changes to counterweights")]
        [Tooltip("Speeds up counterweights along with giving them local immunity. In addtion counterweights will switch out after 4-6 seconds. (Enabled by default)")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool BuffsToCounterweights { get; set; }
    }
}
