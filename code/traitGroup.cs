using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterestingTrait.code
{
    internal class traitGroup
    {
        public static void Init()
        {
            ActorTraitGroupAsset interesting2 = new ActorTraitGroupAsset();
            interesting2.id = "interesting2";
            interesting2.name = "trait_group_interesting2";
            interesting2.color = Toolbox.makeColor("#00FFFF", -1f);
            AssetManager.trait_groups.add(interesting2);

            ActorTraitGroupAsset interesting3 = new ActorTraitGroupAsset();
            interesting3.id = "interesting3";
            interesting3.name = "trait_group_interesting3";
            interesting3.color = Toolbox.makeColor("#D02090", -1f);
            AssetManager.trait_groups.add(interesting3);

            ActorTraitGroupAsset interesting4 = new ActorTraitGroupAsset();
            interesting4.id = "interesting4";
            interesting4.name = "trait_group_interesting4";
            interesting4.color = Toolbox.makeColor("#FFC20E", -1f);
            AssetManager.trait_groups.add(interesting4);
        }
    }
}
