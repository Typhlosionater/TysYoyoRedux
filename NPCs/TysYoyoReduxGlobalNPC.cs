using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using TysYoyoRedux.Items;
using TysYoyoRedux.NPCs;
using System.Linq;
using System.Collections.Generic;

namespace TysYoyoRedux.NPCs
{
	public class TysYoyoReduxGlobalNPC : GlobalNPC
	{
		//New Sold Items
		/* TODO: Fix
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
			//All new accessories that are sold
			if (ModContent.GetInstance<TysYoyoReduxConfigServer>().AddNewAccessories)
			{
				//The mechanic sells ball bearings
				if (type == NPCID.Mechanic)
				{
					//Create List
					List<Item> inventory = shop.item.ToList();

					//Find Slot to insert into
					Item NewItemSlot = inventory.FirstOrDefault(i => i.type == ItemID.MechanicalLens); //Change target
					int index = 21; //Change Default
					if (NewItemSlot != null)
						index = inventory.IndexOf(NewItemSlot) + 1;

					//Insert item into slot
					inventory.Insert(index, new(ModContent.ItemType<Items.Accessories.YoyoBearingAccessory>())); //Changes Item
					inventory[index].isAShopItem = true;
					nextSlot++;

					//Bruh Moment
					shop.item = inventory.ToArray();
				}
			}
        }
        */

		//Travelling merchant sold items
		public override void SetupTravelShop(int[] shop, ref int nextSlot)
		{
			//Very Rare Items List
			int[] veryRareItemIds = new int[]
			{
			ItemID.BedazzledNectar,
			ItemID.ExoticEasternChewToy,
			ItemID.BirdieRattle,
			ItemID.AntiPortalBlock,
			ItemID.CompanionCube,
			ItemID.SittingDucksFishingRod,
			ItemID.HunterCloak,
			ItemID.WinterCape,
			ItemID.RedCape,
			ItemID.MysteriousCape,
			ItemID.CrimsonCloak,
			ItemID.DiamondRing,
			ItemID.CelestialMagnet,
			ItemID.WaterGun,
			ItemID.PulseBow,
			ItemID.YellowCounterweight
			};

			//Travelling merchant sells spiked side effects post 3 mech boss
			if (ModContent.GetInstance<TysYoyoReduxConfigServer>().AddNewAccessories && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				for (int i = 0; i < shop.Length; i++)
				{
					if (veryRareItemIds.Contains(shop[i]) && Main.rand.NextBool(17))
					{
						shop[i] = ModContent.ItemType<Items.Accessories.YoyoSideEffectsAccessory>();
						return;
					}
				}
			}
		}

		//New Loot Drops
		public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
		{
			//All new yoyos that are dropped
			if (ModContent.GetInstance<TysYoyoReduxConfigServer>().AddNewYoyos)
			{
				//Dripplers & Blood zombies drop ravager post skeletron at 1% drop chance, doubled to 2% in expert
				if (npc.type == NPCID.Drippler || npc.type == NPCID.BloodZombie)
				{
					IItemDropRule KilledSkelly = new LeadingConditionRule(new SkeletronDeadDropCondition());
					KilledSkelly.OnSuccess(ItemDropRule.NormalvsExpert(ModContent.ItemType<Items.NewYoyos.Ravager>(), 100, 50));
					npcLoot.Add(KilledSkelly);
				}

				//Extraterrestrial Taser drops according to the same rule as the charged blaster cannon
				if (npc.type == NPCID.MartianWalker || npc.type == NPCID.MartianOfficer || npc.type == NPCID.GigaZapper || npc.type == NPCID.GrayGrunt || npc.type == NPCID.RayGunner || npc.type == NPCID.BrainScrambler || npc.type == NPCID.ScutlixRider || npc.type == NPCID.MartianEngineer)
				{
					npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.NewYoyos.ExtraterrestrialTaser>(), 800));
				}

				//On not on expert+ empress of light drops Spectrum
				if (npc.type == NPCID.HallowBoss)
				{
					IItemDropRule NotExpert = new LeadingConditionRule(new Conditions.NotExpert());
					NotExpert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Items.NewYoyos.Spectrum>(), 50));
					npcLoot.Add(NotExpert);
				}
			}
		}
	}
}