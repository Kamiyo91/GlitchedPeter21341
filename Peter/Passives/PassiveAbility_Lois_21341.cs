using System;
using System.Linq;
using KamiyoStaticUtil.Utils;

namespace GlitchedPeter21341.Peter.Passives
{
    public class PassiveAbility_Lois_21341 : PassiveAbilityBase
    {
        private Random _random;

        public override void Init(BattleUnitModel self)
        {
            base.Init(self);
            _random = new Random();
        }

        public override void BeforeRollDice(BattleDiceBehavior behavior)
        {
            behavior.ApplyDiceStatBonus(new DiceStatBonus { dmg = 5, dmgRate = 10 });
        }

        public override void OnRoundStart()
        {
            if (_random.Next(0, 100) >= 15) return;
            var unitList = BattleObjectManager.instance.GetAliveList(UnitUtil.ReturnOtherSideFaction(owner.faction));
            if (unitList.Any())
                RandomUtil.SelectOne(unitList).bufListDetail
                    .AddKeywordBufThisRoundByEtc(KeywordBuf.NullifyPower, 1, owner);
        }
    }
}