using System.Collections.Generic;
using HarmonyLib;
using NeoModLoader.api.attributes;
using NeoModLoader.General;
using UnityEngine;
using ReflectionUtility;
using UnityEngine.UI;
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


    public static string[] traits =
    {
        "extraordinary9",
        "fountainhead1",
        "fountainhead2",
        "fountainhead3"
    };

    [HarmonyPostfix, HarmonyPatch(typeof(Actor), nameof(Actor.newKillAction))]
    private static void newMechanism(Actor __instance, Actor pDeadUnit)
    {
        foreach (string trait in traits)
        {
            if (pDeadUnit.hasTrait(trait) && __instance.hasTrait(trait))
            {
                __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
            }
            else
            {
                // 检查交叉特质
                foreach (string otherTrait in traits)
                {
                    if (trait != otherTrait && pDeadUnit.hasTrait(trait) && __instance.hasTrait(otherTrait))
                    {
                        __instance.ChangeBenYuan(pDeadUnit.GetBenYuan() + 1);
                    }
                }
            }
        }
    }

    public static bool displayAreaInitialization = false;

    [Hotfixable]
    [HarmonyPostfix, HarmonyPatch(typeof(WindowCreatureInfo), "OnEnable")]
    public static void OnEnable_Postfix(WindowCreatureInfo __instance)
    {
        if (!displayAreaInitialization)
        {
            displayAreaInitialization = true;
            var obj = new GameObject("YuanNnegShow", typeof(Text), typeof(ContentSizeFitter));
            obj.transform.SetParent(__instance.transform.Find("Background"));
            obj.transform.localScale = Vector3.one;
            RectTransform rect = obj.GetComponent<RectTransform>();

            rect.pivot = new Vector2(0f, 1f);
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.localPosition = new Vector3(0, 155, 0);
            rect.sizeDelta = new Vector2(800, 200);
        }

        Transform ActorShowYuanNneg = __instance.transform.Find("Background/YuanNnegShow");
        __instance.transform.Find("Background/Clickable obj").gameObject.SetActive(false);
        Text ActorShowYuanNnegText = ActorShowYuanNneg.GetComponent<Text>();
        ActorShowYuanNnegText.text = LM.Get("yuanneng") + ":\t" + __instance.actor.GetYuanNeng();
        ActorShowYuanNnegText.font = LocalizedTextManager.currentFont;
        ActorShowYuanNnegText.alignment = TextAnchor.UpperLeft;
        ActorShowYuanNnegText.raycastTarget = false;


        __instance.showStat("xiaohao", __instance.actor.stats["xiaohao"]);
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
        window.UiPanelInfo.UpdateText();
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
        UpdateYuannengBasedOnGrade(__instance, "Grade3", 72.0f);
        UpdateYuannengBasedOnGrade(__instance, "Grade6", 288.0f);
        UpdateYuannengBasedOnGrade(__instance, "flair9", 0f);
    }

    private static void UpdateYuannengBasedOnGrade(Actor actor, string traitName, float maxYuanneng)
    {
        if (actor.hasTrait(traitName))
        {
            actor.SetYuanNeng(Mathf.Min(maxYuanneng, actor.GetYuanNeng()));
        }
    }


    //禁止雷电劈出永生
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

    //祝福之地增加魔法
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

    private static Dictionary<string, int> actorAnimationValues = new();

    //修改单位移动动画
    [HarmonyPrefix, HarmonyPatch(typeof(ActorBase), "checkAnimationContainer")]
    public static void checkAnimationContainer(ActorBase __instance)
    {
        Actor actor = __instance.a;

        if (actor == null || actor.data == null || actor.asset == null || actor.batch == null || !actor.asset.unit ||
            !actor.isAlive())
            return;

        string pid = __instance.asset.id;
        string texturePath = actor.asset.texture_path;
        string animationContainerPath = "actors/" + texturePath;
        bool setAnimationContainer = false;
        Dictionary<string, string> unitToCavalryTexture = new()
        {
            { "unit_orc", "actors/wolf_cavalry" },
            { "unit_human", "actors/human_cavalry" },
            { "unit_elf", "actors/elf_cavalry" },
            { "unit_dwarf", "actors/Dwarf_cavalry" },
        };
        if (unitToCavalryTexture.ContainsKey(actor.asset.id))
        {
            animationContainerPath = unitToCavalryTexture[actor.asset.id];
        }

        // 定义字典保存特质和路径,省略一堆沟槽的if-else
        //切记!!!!!!!!!!!!注意先顺序,最前面的优先级比较高,如果同时有多个特性,只判定前面的第一个
        Dictionary<string, string> traitToAnimationFrame = new Dictionary<string, string>
        {
            { "Grade91", "actors/Grade91_Wizard" },
            { "Ring34", "actors/Ring34_Wizard" },
            { "sorcery35", "actors/sorcery35_Wizard" },
            { "sorcery34", "actors/sorcery34_Wizard" },
            { "sorcery33", "actors/sorcery33_Wizard" },
            { "sorcery32", "actors/sorcery32_Wizard" },
            { "sorcery31", "actors/sorcery31_Wizard" },
            { "Ring24", "actors/Ring24_Wizard" }
        };

        foreach (var trait in traitToAnimationFrame.Keys)
        {
            if (actor.hasStatus(trait) || actor.hasTrait(trait))
            {
                animationContainerPath = traitToAnimationFrame[trait];
                setAnimationContainer = true;
                string pPath = "actors/base_head";
                actor.checkHeadID();
                actor.setHeadSprite(ActorAnimationLoader.getHead(pPath, 0));
                actor.has_rendered_sprite_head = true;
                actor.dirty_sprite_head = false;
                actor.dirty_sprite_item = false;
                actor.checkRenderedItem();
                actor.cached_sprite_item = Resources.Load<Sprite>("actors/base_head/head_0.png");;
                actor.has_rendered_sprite_item = true;
                break;
            }
        }

        if (actor.hasTrait("Grade4") || actor.hasTrait("Grade5") || actor.hasTrait("Grade6"))
        {
            if (!actorAnimationValues.ContainsKey(actor.data.id))
            {
                actorAnimationValues.Add(actor.data.id,
                                         Toolbox.randomInt(0, 22));
            }

            animationContainerPath = $"actors/Grade4_Wizard/Grade4.{actorAnimationValues[actor.data.id]}_Wizard";
            setAnimationContainer = true;
            string pPath = "actors/base_head";
            actor.checkHeadID();
            actor.setHeadSprite(ActorAnimationLoader.getHead(pPath, 0));
            actor.has_rendered_sprite_head = true;
            actor.dirty_sprite_head = false;
            actor.dirty_sprite_item = false;
            actor.checkRenderedItem();
            actor.cached_sprite_item = Resources.Load<Sprite>("actors/base_head/head_0.png");;
            actor.has_rendered_sprite_item = true;
        }

        if (actor.hasTrait("Grade1") || actor.hasTrait("Grade2") || actor.hasTrait("Grade3"))
        {
            if (!actorAnimationValues.ContainsKey(actor.data.id))
            {
                actorAnimationValues.Add(actor.data.id,
                                         Toolbox.randomInt(0, 14));
            }

            animationContainerPath = $"actors/Grade1_Wizard/Grade1.{actorAnimationValues[actor.data.id]}_Wizard";
            setAnimationContainer = true;
            string pPath = "actors/base_head";
            actor.checkHeadID();
            actor.setHeadSprite(ActorAnimationLoader.getHead(pPath, 0));
            actor.has_rendered_sprite_head = true;
            actor.dirty_sprite_head = false;
            actor.dirty_sprite_item = false;
            actor.checkRenderedItem();
            actor.cached_sprite_item = Resources.Load<Sprite>("actors/base_head/head_0.png");
            actor.has_rendered_sprite_item = true;
        }

        if (setAnimationContainer)
        {
            actor.animationContainer = ActorAnimationLoader.loadAnimationUnit(animationContainerPath, actor.asset);
        }
    }
}