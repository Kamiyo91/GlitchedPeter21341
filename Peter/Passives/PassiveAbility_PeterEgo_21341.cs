using System.Linq;
using KamiyoStaticUtil.Utils;
using LOR_DiceSystem;

namespace GlitchedPeter21341.Peter.Passives
{
    public class PassiveAbility_PeterEgo_21341 : PassiveAbilityBase
    {
        public override void OnStartBattle()
        {
            var aliveList = BattleObjectManager.instance.GetAliveList(UnitUtil.ReturnOtherSideFaction(owner.faction));
            if (!aliveList.Any()) return;
            var target = RandomUtil.SelectOne(aliveList);
            var cards = owner.allyCardDetail.GetAllDeck().Where(x =>
                x.XmlData.Spec.Ranged != CardRange.FarArea && x.XmlData.Spec.Ranged != CardRange.FarAreaEach &&
                x.XmlData.Spec.Ranged != CardRange.Special).ToList();
            if (!cards.Any()) return;
            var cardInfo = RandomUtil.SelectOne(cards);
            var card = new BattlePlayingCardDataInUnitModel
            {
                owner = owner,
                card = cardInfo,
                target = target
            };
            Singleton<StageController>.Instance.AddAllCardListInBattle(card, target);
        }
    }
}