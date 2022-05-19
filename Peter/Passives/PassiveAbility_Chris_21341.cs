using GlitchedPeter21341.BLL;
using KamiyoStaticUtil.Utils;

namespace GlitchedPeter21341.Peter.Passives
{
    public class PassiveAbility_Chris_21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            UnitUtil.ReadyCounterCard(owner, 6, PeterModParameters.PackageId);
        }

        public override void OnRoundStart()
        {
            owner.bufListDetail.AddKeywordBufThisRoundByEtc(KeywordBuf.Protection, 3, owner);
        }
    }
}