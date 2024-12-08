using HarmonyLib;
using NeoModLoader.api;
using VideoCopilot.code;
using VideoCopilot.code.window;

namespace VideoCopilot
{
    internal class VideoCopilotClass : BasicMod<VideoCopilotClass>
    {
        public static ModDeclare modDeclare;
        public static string id = "shiyue.worldbox.mod.VideoCopilot";

        protected override void OnModLoad()
        {
            stats.Init();
            traitGroup.Init();
            traits.Init();
            SorceryEffect.Init();
            UI.Init();
            modDeclare = GetDeclaration();

            WindowManager.Init();
            new Harmony(id).PatchAll(typeof(patch));
        }
    }
}