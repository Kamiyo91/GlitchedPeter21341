using CustomMapUtility;
using UnityEngine;

namespace GlitchedPeter21341
{
    public class GlitchedPeter_21341MapManager : CustomMapManager
    {
        protected internal override string[] CustomBGMs => new[] { "GlitchedPeter21341.ogg" };

        public override void EnableMap(bool b)
        {
            sephirahColor = Color.black;
            base.EnableMap(b);
        }
    }
}