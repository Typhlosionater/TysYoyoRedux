using Terraria.ModLoader;

namespace TysYoyoRedux
{
	public class TysYoyoRedux : Mod
	{
		public override void PostSetupContent()
		{
			if (ModLoader.TryGetMod("BossChecklist", out Mod bossChecklist))
			{
				if (ModContent.GetInstance<TysYoyoReduxConfigServer>().AddNewYoyos)
				{
					bossChecklist.Call("AddToBossLoot", "Terraria", "Blood Moon", ModContent.ItemType<Items.NewYoyos.Ravager>());
					bossChecklist.Call("AddToBossLoot", "Terraria", "HallowBoss", ModContent.ItemType<Items.NewYoyos.Spectrum>());
					bossChecklist.Call("AddToBossLoot", "Terraria", "Martian Madness", ModContent.ItemType<Items.NewYoyos.ExtraterrestrialTaser>());
				}
			}
		}
	}
}