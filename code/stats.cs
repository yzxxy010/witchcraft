
namespace VideoCopilot.code
{
    internal class stats
    {
        public static void Init()
        {
            BaseStatAsset yuanneng = new BaseStatAsset();
            yuanneng.id = "yuanneng";
            yuanneng.normalize = true;
            yuanneng.normalize_min = -99999;
            yuanneng.normalize_max = 1000;
            yuanneng.mod = true;
            yuanneng.used_only_for_civs = false;
            AssetManager.base_stats_library.add(yuanneng);
            
            BaseStatAsset xiaohao = new BaseStatAsset();
            xiaohao.id = "xiaohao";
            xiaohao.normalize = true;
            xiaohao.normalize_min = -99999;
            xiaohao.normalize_max = 99999;
            xiaohao.mod = true;
            xiaohao.used_only_for_civs = false;
            AssetManager.base_stats_library.add(xiaohao);

            BaseStatAsset meditation = new BaseStatAsset();
            meditation.id = "meditation";
            meditation.normalize = true;
            meditation.normalize_min = -99999;
            meditation.normalize_max = 99999;
            meditation.mod = true;
            meditation.used_only_for_civs = false;
            AssetManager.base_stats_library.add(meditation);
            // 复活次数
            BaseStatAsset resurrection = new BaseStatAsset();
            resurrection.id = "resurrection";
            resurrection.normalize = true;
            resurrection.normalize_min = -99999;
            resurrection.normalize_max = 99999;
            resurrection.mod = true;
            resurrection.used_only_for_civs = false;
            AssetManager.base_stats_library.add(resurrection);

            BaseStatAsset Rresurrection = new BaseStatAsset();
            Rresurrection.id = "Rresurrection";
            Rresurrection.normalize = true;
            Rresurrection.normalize_min = 1;
            Rresurrection.normalize_max = 99999;
            Rresurrection.mod = true;
            Rresurrection.used_only_for_civs = false;
            AssetManager.base_stats_library.add(Rresurrection);

            BaseStatAsset Accuracy = new BaseStatAsset();
            Accuracy.id = "Accuracy";
            Accuracy.normalize = true;
            Accuracy.normalize_min = 0;
            Accuracy.normalize_max = 99999;
            Accuracy.mod = true;
            Accuracy.used_only_for_civs = false;
            AssetManager.base_stats_library.add(Accuracy);

            BaseStatAsset Dodge = new BaseStatAsset();
            Dodge.id = "Dodge";
            Dodge.normalize = true;
            Dodge.normalize_min = 0;
            Dodge.normalize_max = 99999;
            Dodge.mod = true;
            Dodge.used_only_for_civs = false;
            AssetManager.base_stats_library.add(Dodge);
        }
    }
}
