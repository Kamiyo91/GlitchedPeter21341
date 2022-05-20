using System.Collections.Generic;
using CustomMapUtility;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using Sound;
using UnityEngine;

namespace GlitchedPeter21341.Util21341
{
#pragma warning disable
    public static class MapUtil
    {
        public static void ChangeMap(MapModel model, Faction faction = Faction.Player)
        {
            if (MapStaticUtil.CheckStageMap(model.StageIds) || SingletonBehavior<BattleSceneRoot>
                    .Instance.currentMapObject.isEgo ||
                Singleton<StageController>.Instance.GetStageModel().ClassInfo.stageType == StageType.Creature) return;
            CustomMapHandler.InitCustomMap(model.Stage, model.Component, model.IsPlayer, model.InitBgm, model.Bgx,
                model.Bgy, model.Fx, model.Fy);
            if (model.IsPlayer && !model.OneTurnEgo)
            {
                CustomMapHandler.ChangeToCustomEgoMapByAssimilation(model.Stage, faction);
                return;
            }

            CustomMapHandler.ChangeToCustomEgoMap(model.Stage, faction);
            MapStaticUtil.MapChangedValue(true);
        }

        public static void ReturnFromEgoMap(string mapName, List<LorId> ids)
        {
            if (MapStaticUtil.CheckStageMap(ids) ||
                Singleton<StageController>.Instance.GetStageModel().ClassInfo.stageType ==
                StageType.Creature) return;
            CustomMapHandler.RemoveCustomEgoMapByAssimilation(mapName);
            MapStaticUtil.RemoveValueInAddedMap(mapName);
        }

        public static void PlayScreamEffect(BattleUnitModel owner)
        {
            var gameObject = Util.LoadPrefab("Battle/CreatureEffect/New_IllusionCardFX/6_G/FX_IllusionCard_6_G_Shout");
            if (gameObject != null)
                if (owner?.view != null)
                {
                    gameObject.transform.parent = owner.view.camRotationFollower;
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.transform.localScale = Vector3.one;
                    gameObject.transform.localRotation = Quaternion.identity;
                }

            SingletonBehavior<SoundEffectManager>.Instance.PlayClip("Creature/Danggo_Lv2_Shout");
        }
    }
}