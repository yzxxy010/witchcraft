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
        "meditation1",
        "meditation2",
        "meditation3"
    };

    [HarmonyPostfix, HarmonyPatch(typeof(Actor), nameof(Actor.newKillAction))]
    private static void newMechanism(Actor __instance, Actor pDeadUnit)
    {
        foreach (string trait in traits)
        {
            if (pDeadUnit.hasTrait(trait) && __instance.hasTrait(trait))
            {
                float deadUnitMeditation = pDeadUnit.GetMeditation();
                float additionalWuLi = deadUnitMeditation * UnityEngine.Random.Range(0.1f, 0.3f);
                __instance.ChangeMeditation(additionalWuLi);
            }
            else
            {
                // 检查交叉特质
                foreach (string otherTrait in traits)
                {
                    if (trait != otherTrait && pDeadUnit.hasTrait(trait) && __instance.hasTrait(otherTrait))
                    {
                        float deadUnitMeditation = pDeadUnit.GetMeditation();
                        float additionalWuLi = deadUnitMeditation * UnityEngine.Random.Range(0.1f, 0.3f);
                        __instance.ChangeMeditation(additionalWuLi);
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
            var yuanNnegObj = new GameObject("YuanNnegShow", typeof(Text), typeof(ContentSizeFitter));
            yuanNnegObj.transform.SetParent(__instance.transform.Find("Background"));
            yuanNnegObj.transform.localScale = Vector3.one;
            RectTransform yuanNnegRect = yuanNnegObj.GetComponent<RectTransform>();
            yuanNnegRect.pivot = new Vector2(0f, 1f);
            yuanNnegRect.anchorMin = new Vector2(0.5f, 1f);
            yuanNnegRect.anchorMax = new Vector2(0.5f, 1f);
            yuanNnegRect.localPosition = new Vector3(0, 155, 0);
            yuanNnegRect.sizeDelta = new Vector2(800, 200);
            yuanNnegObj.SetActive(false);

            var meditationObj = new GameObject("MeditationShow", typeof(Text));
            meditationObj.transform.SetParent(__instance.transform.Find("Background"));
            meditationObj.transform.localScale = Vector3.one;
            RectTransform meditationRect = meditationObj.GetComponent<RectTransform>();
            meditationRect.pivot = new Vector2(0f, 0f); // 假设放在底部
            meditationRect.anchorMin = new Vector2(0.5f, 0f);
            meditationRect.anchorMax = new Vector2(0.5f, 0f);
            meditationRect.localPosition = new Vector3(120, -200, 0); // 假设的位置，可能需要调整
            meditationRect.sizeDelta = new Vector2(800, 50); // 假设的大小，可能需要根据字体大小调整
            meditationObj.SetActive(false);
        }

        Transform ActorShowYuanNneg = __instance.transform.Find("Background/YuanNnegShow");
        Text ActorShowYuanNnegText = ActorShowYuanNneg.GetComponent<Text>();
        ActorShowYuanNnegText.text = string.Empty;// 重置文本内容，确保不会显示旧数据
        if (__instance.actor.GetYuanNeng() > 0f)
        {
            ActorShowYuanNneg.gameObject.SetActive(true); // 当值大于0时激活对象
            ActorShowYuanNnegText.color = new Color(1f, 1f, 0f);
            ActorShowYuanNnegText.text = LM.Get("yuanneng") + ":\t" + __instance.actor.GetYuanNeng();
            ActorShowYuanNnegText.font = LocalizedTextManager.currentFont;
            ActorShowYuanNnegText.alignment = TextAnchor.UpperLeft;
            ActorShowYuanNnegText.raycastTarget = false;
        }
        else
        {
            ActorShowYuanNneg.gameObject.SetActive(false);
        }

        Transform ActormeditationTextTransform = __instance.transform.Find("Background/MeditationShow");
        Text ActormeditationTextComponent = ActormeditationTextTransform.GetComponent<Text>();
        ActormeditationTextComponent.text = string.Empty; // 重置文本内容，确保不会显示旧数据
        if (__instance.actor.GetMeditation() > 0f)
        {
            ActormeditationTextTransform.gameObject.SetActive(true);
            ActormeditationTextComponent.color = new Color(128f / 255f, 0f / 255f, 128f / 255f);
            float ActormeditationValue = __instance.actor.GetMeditation(); // 假设Actor有GetMeditation方法
            ActormeditationTextComponent.text = LM.Get("meditation") + ":\t" + __instance.actor.GetMeditation();
            ActormeditationTextComponent.font = LocalizedTextManager.currentFont;
            ActormeditationTextComponent.alignment = TextAnchor.UpperLeft;
            ActormeditationTextComponent.raycastTarget = false;
        }
        else
        {
            ActormeditationTextTransform.gameObject.SetActive(false);
        }
        __instance.transform.Find("Background/Clickable obj").gameObject.SetActive(false);
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

        float age = (float)actor.getAge();

        // 检查 flair81 和 flair9 特质，如果存在则不执行后续操作
        if (actor.hasTrait("flair81") || actor.hasTrait("flair9"))
        {
            return;
        }

        // 境界源能上限
        var grades = new Dictionary<string, float>
        {
            { "Grade02", 18.0f },
            { "Grade3", 77.0f },
            { "Grade6", 288.0f },
            { "Grade7", 300.0f },
            { "Grade8", 600.0f },
            { "Grade9", 1200.0f },
            { "Grade91", 3000.0f }
        };
        foreach (var grade in grades)
        {
            UpdateYuannengBasedOnGrade(actor, grade.Key, grade.Value);
        }

        // 境界无根之源上限（不受Grade7和Grade8限制的逻辑）
        float maxMeditation = 0f;
        if (actor.hasTrait("flair8"))
        {
            maxMeditation = 1000.0f;
        }
        else if (actor.hasTrait("flair91"))
        {
            maxMeditation = 2000.0f;
        }
        else
        {
            var meditationLimits = new Dictionary<string, float>
            {
                { "Grade7", 160.0f },
                { "Grade8", 360.0f }
            };
            foreach (var grade in meditationLimits)
            {
                if (actor.hasTrait(grade.Key))
                {
                    maxMeditation = grade.Value;
                    break;
                }
            }
        }
        actor.SetMeditation(Mathf.Min(maxMeditation, actor.GetMeditation()));

        // 到年龄后执行 flair81 的逻辑
        var traitThresholds = new Dictionary<string, float>
        {
            { "flair6", 100f },
            { "flair7", 70f },
            { "Grade0", 68f },
            { "Grade01", 68f },
            { "Grade02", 68f },
            { "Grade1", 88f },
            { "Grade2", 93f },
            { "Grade3", 98f },
            { "Grade4", 158f },
            { "Grade5", 173f },
            { "Grade6", 188f },
            { "Grade7", 288f },
            { "Grade8", 388f },
            { "Grade9", 500f }
        };

        const float flair81Probability = 0.2f;
        bool hasGradeTrait = false;

// 第一层遍历：检查是否存在任意等级特质
        foreach (var grade in traitThresholds.Keys)
        {
            if (actor.hasTrait(grade))
            {
                hasGradeTrait = true;
                break;
            }
        }

        if (hasGradeTrait)
        {
            // 第二层遍历：使用 KeyValuePair 替代解构语法
            foreach (var kvp in traitThresholds)
            {
                string grade = kvp.Key;
                float ageThreshold = kvp.Value;

                if (actor.hasTrait(grade) &&
                    age > ageThreshold &&
                    Toolbox.randomChance(flair81Probability))
                {
                    actor.addTrait("flair81", false);
                    break; // 添加后立即退出循环
                }
            }
        }

        // 检查 xiaohao 值
        if (Globals.Tsotw >= actor.stats["xiaohao"])
        {
            Globals.Tsotw += actor.stats["xiaohao"];
            actor.ChangeYuanNeng(-actor.stats["xiaohao"]);
        }
        if (actor.hasTrait("flair8"))
        {
            actor.ChangeMeditation(1f);
        }
        if (actor.hasTrait("flair91"))
        {
            actor.ChangeMeditation(2f);
        }
        // 检查 meditation1 到 meditation3 特质
        if (actor.hasTrait("meditation1") || actor.hasTrait("meditation2") || actor.hasTrait("meditation3"))
        {
            float randomMeditationChange = UnityEngine.Random.Range(1f, 5f); // 随机值为1到5
            actor.ChangeMeditation(randomMeditationChange);
        }

        // 检查 flair1 到 flair7 特质，并设置随机的 ["xiaohao"] 值
        if (SetRandomXiaohaoBasedOnFlair(actor))
        {
            if (Globals.Tsotw >= actor.stats["xiaohao"])
            {
                Globals.Tsotw += actor.stats["xiaohao"];
                actor.ChangeYuanNeng(-actor.stats["xiaohao"]);
            }
        }
        else
        {
            return; // 如果没有 flair1 到 flair7 特质，则不执行后续与 ["xiaohao"] 相关的操作
        }
    }
    private static bool SetRandomXiaohaoBasedOnFlair(Actor actor)
    {
        // 定义flair特质对应的["xiaohao"]随机范围
        var flairXiaohaoRanges = new Dictionary<string, (float min, float max)>
        {
            { "flair1", (-0.2f, -0.4f) },
            { "flair2", (-0.5f, -0.7f) },
            { "flair3", (-0.8f, -1.0f) },
            { "flair4", (-1.1f, -1.3f) },
            { "flair5", (-1.4f, -1.6f) },
            { "flair6", (-1.8f, -3.5f) },
            { "flair7", (-3.8f, -5.5f) }
        };

        // 遍历flair特质，检查演员是否拥有，并设置随机的["xiaohao"]值
        foreach (var kvp in flairXiaohaoRanges)
        {
            string flair = kvp.Key;
            float min = kvp.Value.min;
            float max = kvp.Value.max;

            if (actor.hasTrait(flair))
            {
                float randomXiaohao = UnityEngine.Random.Range(min, max);
                actor.stats["xiaohao"] = randomXiaohao;
                return true; // 设置成功，返回true
            }
        }

        // 如果没有找到匹配的flair特质，则返回false
        return false;
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
            { "Ring93", "actors/Ring93_Wizard" },
            { "Ring92", "actors/Ring92_Wizard" },
            { "Grade91", "actors/Grade91_Wizard" },
            { "Ring34", "actors/Ring34_Wizard" },
            { "sorcery35", "actors/sorcery35_Wizard" },
            { "sorcery34", "actors/sorcery34_Wizard" },
            { "sorcery33", "actors/sorcery33_Wizard" },
            { "sorcery32", "actors/sorcery32_Wizard" },
            { "sorcery31", "actors/sorcery31_Wizard" },
            { "Ring24", "actors/Ring24_Wizard" }
        };

        bool hasTrait = false;
        foreach (var trait in traitToAnimationFrame.Keys)
        {
            if (actor.hasStatus(trait) || actor.hasTrait(trait))
            {
                hasTrait = true;
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

        if (!hasTrait && (actor.hasTrait("Grade4") || actor.hasTrait("Grade5") || actor.hasTrait("Grade6")))
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

        if (!hasTrait && (actor.hasTrait("Grade1") || actor.hasTrait("Grade2") || actor.hasTrait("Grade3")))
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
    [HarmonyPrefix, HarmonyPatch(typeof(ActionLibrary), "showWhisperTip")]
    public static bool Prefix(string pText)
    {


        // 自定义逻辑：显示停留时间为x秒的提示信息
        string text = LocalizedTextManager.getText(pText, null);
            if (Config.whisperA != null)
            {
                text = text.Replace("$kingdom_A$", Config.whisperA.name);
            }
            if (Config.whisperB != null)
            {
                text = text.Replace("$kingdom_B$", Config.whisperB.name);
            }
            WorldTip.showNow(text, false, "top", 6f);


        // 如果不需要跳过原方法，则返回true
        return false;
    }
}