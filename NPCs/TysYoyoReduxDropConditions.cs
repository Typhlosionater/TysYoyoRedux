using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace TysYoyoRedux.NPCs
{
    //Drop condition: drops after skeletron
    public class SkeletronDeadDropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            if (!info.IsInSimulation)
            {
                return NPC.downedBoss3;
            }
            return false;
        }

        public bool CanShowItemDropInUI()
        {
            return true;
        }

        public string GetConditionDescription()
        {
            return "Drops after you have defeated Skeletron";
        }
    }
}