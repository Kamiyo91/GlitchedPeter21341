using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GlitchedPeter21341.BLL;
using KamiyoStaticBLL.Models;
using KamiyoStaticUtil.Utils;
using MonoMod.Utils;

namespace GlitchedPeter21341
{
    public class GlitchedPeter21341ModInit : ModInitializer
    {
        public override void OnInitializeMod()
        {
            InitParameters();
            MapStaticUtil.GetArtWorks(new DirectoryInfo(PeterModParameters.Path + "/ArtWork"));
            UnitUtil.ChangeCardItem(ItemXmlDataList.instance, PeterModParameters.PackageId);
            UnitUtil.ChangePassiveItem(PeterModParameters.PackageId);
            SkinUtil.LoadBookSkinsExtra(PeterModParameters.PackageId);
            LocalizeUtil.AddLocalLocalize(PeterModParameters.Path, PeterModParameters.PackageId);
            SkinUtil.PreLoadBufIcons();
            LocalizeUtil.RemoveError();
            UnitUtil.InitKeywords(Assembly.GetExecutingAssembly());
        }

        private static void InitParameters()
        {
            ModParameters.PackageIds.Add(PeterModParameters.PackageId);
            PeterModParameters.Path =
                Path.GetDirectoryName(
                    Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            ModParameters.Path.Add(PeterModParameters.Path);
            ModParameters.LocalizePackageIdAndPath.Add(PeterModParameters.PackageId, PeterModParameters.Path);
            ModParameters.SpritePreviewChange.AddRange(new Dictionary<string, List<LorId>>
            {
                { "PeterDefault_21341", new List<LorId> { new LorId(PeterModParameters.PackageId, 10000001) } }
            });
            ModParameters.BooksIds.AddRange(new List<LorId>
            {
                new LorId(PeterModParameters.PackageId, 10000001)
            });
            ModParameters.UntransferablePassives.AddRange(new List<LorId>
            {
                new LorId(PeterModParameters.PackageId, 6)
            });
            ModParameters.PersonalCardList.AddRange(new List<LorId>
            {
                new LorId(PeterModParameters.PackageId, 1)
            });
            ModParameters.DynamicNames.AddRange(new Dictionary<LorId, LorId>
            {
                {
                    new LorId(PeterModParameters.PackageId, 10000001),
                    new LorId(PeterModParameters.PackageId, 1)
                }
            });
            ModParameters.DefaultKeyword.Add(PeterModParameters.PackageId, "GlitchedPeterModPage_21341");
            ModParameters.BookList.AddRange(new List<LorId>
            {
                new LorId(PeterModParameters.PackageId, 2)
            });
            ModParameters.SkinNameIds.AddRange(new List<Tuple<string, List<LorId>, string>>
            {
                new Tuple<string, List<LorId>, string>("PeterPhase2_21341",
                    new List<LorId>
                    {
                        new LorId(PeterModParameters.PackageId, 10000001), new LorId(PeterModParameters.PackageId, 1)
                    },
                    "PeterPhase1_21341")
            });
        }
    }
}