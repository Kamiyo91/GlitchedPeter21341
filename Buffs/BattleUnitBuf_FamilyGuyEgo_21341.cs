using LOR_DiceSystem;

namespace GlitchedPeter21341.Buffs
{
    public class BattleUnitBuf_FamilyGuyEgo_21341 : BattleUnitBuf
    {
        public override bool isAssimilation => true;

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus { power = 1 });
        }

        public override AtkResist GetResistBP(AtkResist origin, BehaviourDetail detail)
        {
            return detail == BehaviourDetail.None ? base.GetResistHP(origin, detail) : AtkResist.Endure;
        }
    }
}