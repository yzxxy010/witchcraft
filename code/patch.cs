using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
using ReflectionUtility;
using VideoCopilot.code.utils;

namespace VideoCopilot.code;

internal class patch
{
    public static bool isLoadSave;

    [HarmonyPrefix, HarmonyPatch(typeof(SavedMap), "create")]
    public static bool create_prefix()
    {
        List<Actor> actors = World.world.units.getSimpleList();
        foreach (Actor actor in actors)
        {
            actor.data.set("wushu.global", Globals.Tsotw);
        }

        return true;
    }

    [HarmonyPostfix, HarmonyPatch(typeof(ActorManager), "loadObject")]
    public static void AddCustomData(ref Actor __result, ActorData pData)
    {
        if (__result != null)
        {
            __result.data.get("wushu.global", out Globals.Tsotw);
        }
    }

    [HarmonyPostfix, HarmonyPatch(typeof(Actor), nameof(Actor.newKillAction))]
    private static void newMechanism(Actor __instance, Actor pDeadUnit)
    {
        if (pDeadUnit.hasTrait("extraordinary9") && __instance.hasTrait("extraordinary9"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("fountainhead1") && __instance.hasTrait("extraordinary9"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("fountainhead2") && __instance.hasTrait("extraordinary9"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("fountainhead3") && __instance.hasTrait("extraordinary9"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("fountainhead1") && __instance.hasTrait("fountainhead1"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("fountainhead1") && __instance.hasTrait("fountainhead2"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("fountainhead1") && __instance.hasTrait("fountainhead3"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("fountainhead2") && __instance.hasTrait("fountainhead2"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("fountainhead3") && __instance.hasTrait("fountainhead3"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("extraordinary9") && __instance.hasTrait("fountainhead1"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("extraordinary9") && __instance.hasTrait("fountainhead2"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("extraordinary9") && __instance.hasTrait("fountainhead3"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("fountainhead2") && __instance.hasTrait("fountainhead1"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("fountainhead2") && __instance.hasTrait("fountainhead3"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("fountainhead3") && __instance.hasTrait("fountainhead2"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }

        if (pDeadUnit.hasTrait("fountainhead3") && __instance.hasTrait("fountainhead1"))
        {
            __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
        }
    }

    [HarmonyPostfix, HarmonyPatch(typeof(WindowCreatureInfo), "OnEnable")]
    public static void OnEnable_Postfix(WindowCreatureInfo __instance)
    {
        if (Config.selectedUnit == null)
        {
            return;
        }

        __instance.showStat("benyuan",  __instance.actor.GetBenYuan());
        __instance.showStat("TSOTW",    Globals.Tsotw.ToString());
        __instance.showStat("xiaohao",  __instance.actor.stats["xiaohao"]);
        __instance.showStat("yuanneng", __instance.actor.GetYuanNeng());
    } 

    [HarmonyPostfix, HarmonyPatch(typeof(MapBox), "updateObjectAge")]
    public static void updateWorldTime_Postfix(MapBox __instance)
    {
        if (Globals.WorldName != __instance.mapStats.name && !isLoadSave)
        {
            Globals.Tsotw = Globals.baseTsotw;
            Globals.WorldName = __instance.mapStats.name;
            return;
        }
        else if (isLoadSave)
        {
            Globals.WorldName = __instance.mapStats.name;
            isLoadSave = false;
            return;
        }

        Globals.Tsotw += Globals.TsotwAdd;
    }

    [HarmonyPostfix, HarmonyPatch(typeof(Actor), "updateAge")]
    public static void ActorUpdateAge_Postfix(Actor __instance)
    {
        Actor actor = __instance;
        if (actor == null)
        {
            return;
        }

        if (!Globals.Actors.ContainsKey(actor.data.id))
        {
            Globals.Actors.Add(actor.data.id, actor);
        }

        if (Globals.Tsotw >= actor.stats["xiaohao"])
        {
            Globals.Tsotw += actor.stats["xiaohao"];
            actor.ChangeYuanNeng(-actor.stats["xiaohao"]);
        }

        UpdateYuannengBasedOnGrade(__instance, "Grade02", 18.0f);
        UpdateYuannengBasedOnGrade(__instance, "Grade3",  72.0f);
        UpdateYuannengBasedOnGrade(__instance, "Grade6",  288.0f);
        UpdateYuannengBasedOnGrade(__instance, "flair9",  0f);
    }

    private static void UpdateYuannengBasedOnGrade(Actor actor, string traitName, float maxYuanneng)
    {
        if (actor.hasTrait(traitName))
        {
            actor.SetYuanNeng(Mathf.Min(maxYuanneng, actor.GetYuanNeng()));
        }
    }

    [HarmonyPrefix, HarmonyPatch(typeof(MapAction), "checkLightningAction")]
    public static bool checkLightningAction(Vector2Int pPos, int pRad)
    {
        bool flag = false;
        List<Actor> simpleList = World.world.units.getSimpleList();
        for (int i = 0; i < simpleList.Count; i++)
        {
            Actor actor = simpleList[i];
            if (Toolbox.DistVec2(actor.currentTile.pos, pPos) <= (float)pRad)
            {
                if (actor.asset.id == SA.godFinger)
                {
                    actor.GetComponent<GodFinger>().lightAction();
                }
                else if (actor.asset.id == SA.tornado)
                {
                    if (actor.GetComponent<Tornado>().split(true))
                    {
                        return false;
                    }
                }
            }
        }

        return false;
    }

    [HarmonyPrefix, HarmonyPatch(typeof(ActionLibrary), "giveEnchanted")]
    public static bool giveEnchanted(WorldTile pTile, ActorBase pActor)
    {
        pActor.removeTrait("cursed");
        pActor.addStatusEffect("enchanted", -1f);
        // 检查 pActor 是否具有指定的特性之一
        if (pActor.hasTrait("flair1") ||
            pActor.hasTrait("flair2") ||
            pActor.hasTrait("flair3") ||
            pActor.hasTrait("flair4") ||
            pActor.hasTrait("flair5") ||
            pActor.hasTrait("flair6") ||
            pActor.hasTrait("flair7"))
            pActor.addStatusEffect("miracle_power");

        return false;
    }
    [HarmonyPrefix, HarmonyPatch(typeof(MapAction), "checkLightningAction")]
    public static void checkAnimationContainer(ActorBase __instance)
    {
        Actor actor = __instance.a;

        string pid = __instance.asset.id;
        string texturePath = actor.asset.texture_path;
        string animationContainerPath = "actors/" + texturePath;
        bool setAnimationContainer = false;

        Dictionary<string, string> unitToCavalryTexture = new()
        {
            { "unit_human", "actors/Fire_Wizard" },
        };


            if (actor.hasTrait("sorcery32"))
            {
                setAnimationContainer = true;
                animationContainerPath = "actors/other_cavalry";
                string pPath = "actors/heads_nothing";
                actor.checkHeadID();
                actor.setHeadSprite(ActorAnimationLoader.getHead(pPath, 0));
                actor.has_rendered_sprite_head = true;
                actor.dirty_sprite_head = false;

                if (unitToCavalryTexture.ContainsKey(actor.asset.id))
                {
                    animationContainerPath = unitToCavalryTexture[actor.asset.id];
                }
            }
            if (setAnimationContainer)
            {
                actor.animationContainer = ActorAnimationLoader.loadAnimationUnit(animationContainerPath, actor.asset);
            }
    }
}