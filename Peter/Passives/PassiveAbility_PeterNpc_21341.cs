using System.Collections.Generic;
using System.Linq;
using GlitchedPeter21341.Buffs;
using GlitchedPeter21341.Util21341.Extensions;
using KamiyoStaticBLL.Enums;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using LOR_XML;

namespace GlitchedPeter21341.Peter.Passives
{
    public class PassiveAbility_PeterNpc_21341 : PassiveAbilityBase
    {
        private bool _filterCheck;
        private NpcMechUtil_Peter _util;

        public override void OnBattleEnd()
        {
            owner.UnitData.unitData.bookItem.ClassInfo.CharacterSkin = new List<string> { "PeterPhase1_21341" };
        }

        public override void OnWaveStart()
        {
            _util = new NpcMechUtil_Peter(new NpcMechUtilBaseModel
            {
                Owner = owner,
                Hp = 0,
                MechHp = 502,
                HasEgo = true,
                HasMechOnHp = true,
                RefreshUI = true,
                SkinName = "PeterPhase2_21341",
                EgoType = typeof(BattleUnitBuf_FamilyGuyEgo_21341),
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
            if (owner.hp <= 372) _util.Restart();
        }

        public override int SpeedDiceNumAdder()
        {
            return _util?.GetPhase() > 0 ? 5 : 2;
        }

        public override bool BeforeTakeDamage(BattleUnitModel attacker, int dmg)
        {
            _util.MechHpCheck(dmg);
            return base.BeforeTakeDamage(attacker, dmg);
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


        public override void OnRoundEndTheLast()
        {
            _util.CheckPhase();
        }
    }
}