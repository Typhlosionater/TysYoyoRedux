using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using TysYoyoRedux.Items.Accessories;

namespace TysYoyoRedux.NPCs
{
    public class TysYoyoReduxGlobalNPC : GlobalNPC
    {
        //New Sold Items
        public override void ModifyShop(NPCShop shop)
        {
            if (ModContent.GetInstance<TysYoyoReduxConfigServer>().AddNewAccessories)
            {
                if (shop.NpcType == NPCID.Mechanic)
                {
                    shop.InsertAfter(ItemID.MechanicalLens, ModContent.ItemType<YoyoBearingAccessory>());
                }
            }
        }

        // NOTE: When Terraria updates, these need to be updated accordingly
        private static readonly int[] veryRareItemIds =
        [
            ItemID.BambooLeaf,
            ItemID.BedazzledNectar,
            ItemID.BlueEgg,
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
            ItemID.WaterGun,
            ItemID.PulseBow,
            ItemID.YellowCounterweight
        ];

        //Travelling merchant sold items
        public override void SetupTravelShop(int[] shop, ref int nextSlot)
        {
            //Travelling merchant sells spiked side effects post 3 mech boss
            if (ModContent.GetInstance<TysYoyoReduxConfigServer>().AddNewAccessories && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                for (int i = 0; i < shop.Length; i++)
                {
                    if (veryRareItemIds.Contains(shop[i]) && Main.rand.NextBool(veryRareItemIds.Length + 1))
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