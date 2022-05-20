using System.Linq;
using GlitchedPeter21341.BLL;
using KamiyoStaticBLL.MechUtilBaseModels;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.BaseClass;

namespace GlitchedPeter21341.Util21341.Extensions
{
    public class MechUtil_Peter : MechUtilBase
    {
        private readonly MechUtilBaseModel _model;
        private int Sacrifices;
        public MechUtil_Peter(MechUtilBaseModel model) : base(model)
        {
            _model = model;
        }


        public override void EgoActive()
        {
            if (!string.IsNullOrEmpty(_model.SkinName))
                _model.Owner.UnitData.unitData.SetTempName(ModParameters.EffectTexts
                    .FirstOrDefault(x => x.Key.Equals("PeterEgoName_21341")).Value.Name);
            base.EgoActive();
            foreach (var unit in BattleObjectManager.instance.GetAliveList(_model.Owner.faction)
                         .Where(x => x != _model.Owner))
            {
                unit.Die();
                Sacrifices++;
            }

            if (!string.IsNullOrEmpty(_model.SkinName))
                _model.Owner.view.ChangeHeight(500);
            CameraFilterUtil.EarthQuake(0.08f, 0.02f, 50f, 0.3f);
            MapUtil.PlayScreamEffect(_model.Owner);
            _model.Owner.passiveDetail.AddPassive(new LorId(PeterModParameters.PackageId, 2));
            _model.Owner.passiveDetail.AddPassive(new LorId(PeterModParameters.PackageId, 3));
            _model.Owner.passiveDetail.AddPassive(new LorId(PeterModParameters.PackageId, 4));
            _model.Owner.passiveDetail.AddPassive(new LorId(PeterModParameters.PackageId, 5));
            ChangeDeck();
        }

        private void ChangeDeck()
        {
            var changesCount = 0;
            foreach (var card in _model.Owner.allyCardDetail.GetHand().ToList()
                         .Where(card => card.GetID().packageId == PeterModParameters.PackageId))
                switch (card.GetID().id)
                {
                    case 2:
                        changesCount++;
                        _model.Owner.allyCardDetail.ExhaustACardAnywhere(card);
                        _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 10));
                        break;
                    case 3:
                        changesCount++;
                        _model.Owner.allyCardDetail.ExhaustACardAnywhere(card);
                        _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 7));
                        break;
                    case 4:
                        changesCount++;
                        _model.Owner.allyCardDetail.ExhaustACardAnywhere(card);
                        _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 8));
                        break;
                    case 5:
                        changesCount++;
                        _model.Owner.allyCardDetail.ExhaustACardAnywhere(card);
                        _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 9));
                        break;
                }

            foreach (var card in _model.Owner.allyCardDetail.GetAllDeck().ToList()
                         .Where(card => card.GetID().packageId == PeterModParameters.PackageId))
                switch (card.GetID().id)
                {
                    case 2:
                        _model.Owner.allyCardDetail.ExhaustACardAnywhere(card);
                        _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 10));
                        break;
                    case 3:
                        _model.Owner.allyCardDetail.ExhaustACardAnywhere(card);
                        _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 7));
                        break;
                    case 4:
                        _model.Owner.allyCardDetail.ExhaustACardAnywhere(card);
                        _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 8));
                        break;
                    case 5:
                        _model.Owner.allyCardDetail.ExhaustACardAnywhere(card);
                        _model.Owner.allyCardDetail.AddNewCardToDeck(new LorId(PeterModParameters.PackageId, 9));
                        break;
                }

            if (changesCount > 0) _model.Owner.allyCardDetail.DrawCards(changesCount);
        }

        public int GetSacrifices()
        {
            return Sacrifices;
        }
    }
}