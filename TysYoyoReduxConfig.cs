using System.ComponentModel;
using Terraria.ModLoader.Config;

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
