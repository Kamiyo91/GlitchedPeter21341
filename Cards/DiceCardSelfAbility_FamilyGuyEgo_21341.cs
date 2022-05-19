namespace GlitchedPeter21341.Cards
{
    public class DiceCardSelfAbility_FamilyGuyEgo_21341 : DiceCardSelfAbilityBase
    {
        public override bool OnChooseCard(BattleUnitModel owner)
        {
            return owner.emotionDetail.EmotionLevel > 1;
        }
    }
}