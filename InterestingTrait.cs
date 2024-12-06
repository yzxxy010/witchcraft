using HarmonyLib;

using InterestingTrait.code;
using InterestingTrait.code.window;
using NeoModLoader.api;

namespace InterestingTrait
{
    internal class InterestingTraitClass : BasicMod<InterestingTraitClass>
    {
        public static string id = "shiyue.worldbox.mod.interestingtrait";
        protected override void OnModLoad()
        {
            stats.Init();
            traitGroup.Init();
            traits.Init();
            SorceryEffect.Init();
            UI.Init();
            WindowManager.Init();
            new Harmony(id).PatchAll(typeof(patch));
        }

    }

}
