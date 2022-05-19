using CustomMapUtility;

namespace GlitchedPeter21341
{
    public class EnemyTeamStageManager_Peter_21341 : EnemyTeamStageManager
    {
        public override void OnWaveStart()
        {
            CustomMapHandler.InitCustomMap<GlitchedPeter_21341MapManager>("GlitchedPeter_21341", false, true, 0.5f,
                0.55f);
            CustomMapHandler.EnforceMap();
            Singleton<StageController>.Instance.CheckMapChange();
        }

        public override void OnRoundStart()
        {
            CustomMapHandler.EnforceMap();
        }
    }
}