using System.Collections.Generic;
using System.Linq;
using GlitchedPeter21341.BLL;
using GlitchedPeter21341.Buffs;
using GlitchedPeter21341.Util21341.Extensions;
using KamiyoStaticBLL.Enums;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using LOR_XML;

namespace GlitchedPeter21341.Peter.Passives
{
    public class PassiveAbility_PeterPlayer_21341 : PassiveAbilityBase
    {
        private bool _filterCheck;
        private MechUtil_Peter _util;

        public override void OnBattleEnd()
        {
            owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { "PeterPhase1_21341" };
        }

        public override void OnWaveStart()
        {
            _util = new MechUtil_Peter(new MechUtilBaseModel
            {
                Owner = owner,
                HasEgo = true,
                RefreshUI = true,
                SkinName = "PeterPhase2_21341",
                EgoType = typeof(BattleUnitBuf_FamilyGuyEgo_21341),
                EgoCardId = new LorId(PeterModParameters.PackageId, 1),
                HasEgoAbDialog = true,
                EgoAbColorColor = AbColorType.Negative,
                EgoAbDialogList = new List<AbnormalityCardDialog>
                {
                    new AbnormalityCardDialog
                    {
                        id = "Peter",
                        dialog = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("PeterEgoActive1_21341"))
                            .Value.Desc
                    }
                }
            });
            if (UnitUtil.CheckSkinProjection(owner))
                _util.DoNotChangeSkinOnEgo();
            else
                owner.view.ChangeHeight(250);
        }

        public override void OnStartBattle()
        {
            UnitUtil.RemoveImmortalBuff(owner);
        }

        public override void OnRoundStart()
        {
            if (!_util.EgoCheck()) return;
            _util.EgoActive();
            var newPassiveDesc = ModParameters.EffectTexts.FirstOrDefault(x => x.Key.Equals("PeterEgoPassive_21341"));
            name = newPassiveDesc.Value.Name;
            desc = newPassiveDesc.Value.Desc;
            owner.RecoverHP(10 * _util.GetSacrifices());
            owner.breakDetail.RecoverBreak(10 * _util.GetSacrifices());
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            _util.OnUseExpireCard(curCard.card.GetID());
        }

        public override void OnRoundStartAfter()
        {
            if (!owner.bufListDetail.HasBuf<BattleUnitBuf_FamilyGuyEgo_21341>()) return;
            _filterCheck = true;
            MapStaticUtil.ActiveCreatureBattleCamFilterComponent();
        }

        public override void OnRoundEndTheLast_ignoreDead()
        {
            if (!owner.IsDead() || !_filterCheck) return;
            _filterCheck = false;
            MapStaticUtil.ActiveCreatureBattleCamFilterComponent(false);
        }

        public override int GetMaxHpBonus()
        {
            return 10 * _util?.GetSacrifices() ?? 0;
        }

        public override int GetMaxBpBonus()
        {
            return 10 * _util?.GetSacrifices() ?? 0;
        }
    }
}