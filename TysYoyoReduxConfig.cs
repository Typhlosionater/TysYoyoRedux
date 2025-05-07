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
        [DefaultValue(true)]
        [ReloadRequired]
        public bool VanillaYoyoEffects  { get; set; }

        //Vanilla Yoyo Recipes
        [DefaultValue(true)]
        [ReloadRequired]
        public bool VanillaYoyoRecipes { get; set; }

        //Add Modded Yoyos
        [DefaultValue(true)]
        [ReloadRequired]
        public bool AddNewYoyos { get; set; }

        //Add Modded Yoyo Accessories
        [DefaultValue(true)]
        [ReloadRequired]
        public bool AddNewAccessories { get; set; }

        //Changes to counterweights
        [DefaultValue(true)]
        [ReloadRequired]
        public bool BuffsToCounterweights { get; set; }
    }
}
