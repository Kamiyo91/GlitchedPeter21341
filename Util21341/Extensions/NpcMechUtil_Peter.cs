using System.Linq;
using GlitchedPeter21341.BLL;
using GlitchedPeter21341.Buffs;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.BaseClass;

namespace GlitchedPeter21341.Util21341.Extensions
{
    public class NpcMechUtil_Peter : NpcMechUtilBase
    {
        private readonly NpcMechUtilBaseModel _model;

        public NpcMechUtil_Peter(NpcMechUtilBaseModel model) : base(model)
        {
            _model = model;
        }

        public void CheckPhase()
        {
            if (_model.Owner.hp > _model.MechHp || _model.Phase > 0) return;
            _model.Phase++;
            _model.Owner.passiveDetail.AddPassive(new LorId(PeterModParameters.PackageId, 2));
            _model.Owner.passiveDetail.AddPassive(new LorId(PeterModParameters.PackageId, 3));
            _model.Owner.passiveDetail.AddPassive(new LorId(PeterModParameters.PackageId, 4));
            _model.Owner.passiveDetail.AddPassive(new LorId(PeterModParameters.PackageId, 5));
            ForcedEgo();
        }

        public override void EgoActive()
        {
            _model.Owner.bufListDetail.AddBuf(new BattleUnitBuf_ShimmeringPeter_21341());
            _model.Owner.UnitData.unitData.SetTempName(ModParameters.EffectTexts
                .FirstOrDefault(x => x.Key.Equals("PeterEgoName_21341")).Value.Name);
            base.EgoActive();
            _model.Owner.view.ChangeHeight(500);
            CameraFilterUtil.EarthQuake(0.08f, 0.02f, 50f, 0.3f);
            MapUtil.PlayScreamEffect(_model.Owner);
            ChangeDeck();
        }

        private void ChangeDeck()
        {
            var handCount = _model.Owner.allyCardDetail.GetHand().Count;
            _model.Owner.allyCardDetail.ExhaustAllCards();
            _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 10));
            _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 10));
            _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 10));
            _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 9));
            _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 9));
            _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 7));
            _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 7));
            _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 8));
            _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 8));
            _model.Owner.allyCardDetail.DrawCards(handCount);
        }

        public void Restart()
        {
            _model.Phase++;
            ForcedEgo();
            SetMassAttack(true);
            SetCounter(_model.MaxCounter);
        }

        public int GetPhase()
        {
            return _model.Phase;
        }
    }
}