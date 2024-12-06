
namespace InterestingTrait.code
{
    internal class stats
    {
        public static void Init()
        {
            BaseStatAsset yuanneng = new BaseStatAsset();
            yuanneng.id = "yuanneng";
            yuanneng.normalize = true;
            yuanneng.normalize_min = -999999;
            yuanneng.normalize_max = 999999;
            yuanneng.mod = true;
            yuanneng.used_only_for_civs = false;
            AssetManager.base_stats_library.add(yuanneng);
            
            BaseStatAsset xiaohao = new BaseStatAsset();
            xiaohao.id = "xiaohao";
            xiaohao.normalize = true;
            xiaohao.normalize_min = -999999;
            xiaohao.normalize_max = 999999;
            xiaohao.mod = true;
            xiaohao.used_only_for_civs = false;
            AssetManager.base_stats_library.add(xiaohao);

            BaseStatAsset benyuan = new BaseStatAsset();
            benyuan.id = "benyuan";
            benyuan.normalize = true;
            benyuan.normalize_min = -999999;
            benyuan.normalize_max = 999999;
            benyuan.mod = true;
            benyuan.used_only_for_civs = false;
            AssetManager.base_stats_library.add(benyuan);
        }
    }
}
