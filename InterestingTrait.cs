#define TEST

using HarmonyLib;
using NeoModLoader.api;
using VideoCopilot.code;
using VideoCopilot.code.window;


namespace VideoCopilot
{
#if TEST
    internal class VideoCopilotClass : BasicMod<VideoCopilotClass>,IReloadable
    {
        public static ModDeclare modDeclare;
        public static ModConfig config;
        public static string id = "shiyue.worldbox.mod.VideoCopilot";

        protected override void OnModLoad()
        {            
            Config.isEditor = true;
            stats.Init();
            traitGroup.Init();
            traits.Init();
            SorceryEffect.Init();
            UI.Init();
            modDeclare = GetDeclaration();
            config = GetConfig();
            
            WindowManager.Init();
            new Harmony(id).PatchAll(typeof(patch));
        }

        public void Reload()
        {
            
        }
    }
#else
    internal class VideoCopilotClass : BasicMod<VideoCopilotClass>
    {
        public static ModDeclare modDeclare;
        public static ModConfig config;
        public static string id = "shiyue.worldbox.mod.VideoCopilot";

        protected override void OnModLoad()
        {
            Config.isEditor = true;
            stats.Init();
            traitGroup.Init();
            traits.Init();
            SorceryEffect.Init();
            UI.Init();
            modDeclare = GetDeclaration();
            config = GetConfig();

            WindowManager.Init();
            new Harmony(id).PatchAll(typeof(patch));
        }
    }
#endif
}