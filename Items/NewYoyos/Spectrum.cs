using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace TysYoyoRedux.Items.NewYoyos
{
	public class Spectrum : ModItem
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
			Item.damage = 142;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.useTime = 25;
			Item.useAnimation = 25;
			Item.knockBack = 3.8f;
			Item.crit += 6;

			Item.width = 30;
			Item.height = 26;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.UseSound = SoundID.Item1;
			Item.channel = true;
			Item.noUseGraphic = true;

			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Yellow;
			Item.autoReuse = false;
			Item.shoot = ModContent.ProjectileType<Projectiles.NewYoyoProjectiles.SpectrumProjectile > ();
			Item.shootSpeed = 16f;
		}
	}
}