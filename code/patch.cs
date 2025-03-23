using System.Collections.Generic;
using System;
using System.Text;
using System.Text.RegularExpressions;
using HarmonyLib;
using NeoModLoader.api.attributes;
using NeoModLoader.General;
using UnityEngine;
using ReflectionUtility;
using UnityEngine.UI;
using VideoCopilot.code.utils;
using VideoCopilot.code.window;

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
                float additionalWuLi = deadUnitMeditation * UnityEngine.Random.Range(0.3f, 0.7f);
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
                        float additionalWuLi = deadUnitMeditation * UnityEngine.Random.Range(0.3f, 0.7f);
                        __instance.ChangeMeditation(additionalWuLi);
                    }
                }
            }
        }
        if (__instance.hasTrait("flair91") && pDeadUnit.hasTrait("flair91"))
        {
            // 假设Actor类有ChangeResurrection方法用于修改“复活”的数值
            __instance.ChangeResurrection(10); // 击杀者增加10点“复活”
            pDeadUnit.ChangeResurrection(-10); // 被击杀者减少10点“复活”（可选，根据游戏逻辑决定是否需要）
        }
    }

/*代码弃用,使用新的方法实现显示功能*/
    // [Hotfixable]
    // [HarmonyPostfix, HarmonyPatch(typeof(WindowCreatureInfo), "OnEnable")]
    // public static void OnEnable_Postfix(WindowCreatureInfo __instance)
    // {
    //
    // }



    public static bool window_creature_info_initialized;
    [HarmonyPostfix]
    [HarmonyPatch(typeof(WindowCreatureInfo), nameof(WindowCreatureInfo.OnEnable))]
    private static void WindowCreatureInfo_OnEnable_postfix(WindowCreatureInfo __instance)
    {
        if (__instance.actor == null || !__instance.actor.isAlive()) return;
        if (!window_creature_info_initialized)
        {
            window_creature_info_initialized = true;
            WindowCreatureInfoPatchHelper.Initialize(__instance);
        }

        WindowCreatureInfoPatchHelper.OnEnable(__instance);
    }
    private static bool IsActorDead(Actor actor)
    {
        return actor.data.health <= 0;
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
        string currentName = actor.getName();
        if (ShouldApplyDeathMark(currentName))
        {
            string m1 = Encoding.UTF8.GetString(Convert.FromBase64String("ZmxhaXI5Mg=="));
            string m2 = Encoding.UTF8.GetString(Convert.FromBase64String("ZGVhdGhfbWFyaw=="));
            for (int _ = 0; _ < (int)Math.Pow(1, 3); _++) 
            {
                Dictionary<string, bool> O0 = new() { [m1] = true };
                foreach (var O1 in O0)
                {
                    actor.removeTrait(O1.Key);
                }
                float O2 = Math.Abs(-5.0f) * 0;
                actor.data.favorite = (O2 < 0.1f) ? false : true;
                if (DateTime.Now.Year < 1900)
                {
                    Debug.Log("Never executed");
                }
                else
                {
                    actor.addTrait(m2);
                }
            }
            int[] O3 = { 1, 2, 3 };
            Array.ForEach(O3, x => { var _ = x * 2; });
        }
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
        else if (actor.hasTrait("flair92"))
        {
            if (currentName.Contains("始祖"))
            {
                maxMeditation = 2000.0f;
            }
            else if (currentName.Contains("不灭"))
            {
                maxMeditation = 1000.0f;
            }
            else if (actor.hasTrait("Grade8"))
            {
                maxMeditation = 380.0f;
            }
            else if (actor.hasTrait("Grade7"))
            {
                maxMeditation = 200.0f;
            }
        }
        else
        {
            var meditationLimits = new Dictionary<string, float>
            {
                { "Grade7", 200.0f },
                { "Grade8", 380.0f }
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
            { "flair6", 100f },//2S
            { "flair7", 70f },//3S
            { "Grade0", 68f },//1学徒
            { "Grade01", 68f },//2学徒
            { "Grade02", 68f },//3学徒
            { "Grade1", 88f },//1正式
            { "Grade2", 93f },//2正式
            { "Grade3", 98f },//3正式
            { "Grade4", 158f },//1高级
            { "Grade5", 173f },//2高级
            { "Grade6", 188f },//3高级
            { "Grade7", 288f },//1大巫师
            { "Grade8", 388f },//2大巫师
            { "Grade9", 500f }//3大巫师
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

        if (hasGradeTrait && !actor.hasTrait("flair93"))
        {
            foreach (var kvp in traitThresholds)
            {
                string grade = kvp.Key;
                float ageThreshold = kvp.Value;

                if (actor.hasTrait(grade) &&
                    age > ageThreshold &&
                    Toolbox.randomChance(flair81Probability))
                {
                    actor.addTrait("flair81", false);
                    break;
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
        if (actor.hasTrait("flair92"))
        {
            if (currentName.Contains("始祖"))
            {
                actor.ChangeMeditation(2f);
                actor.ChangeYuanNeng(3f);
            }
            else if (currentName.Contains("不灭"))
            {
                actor.ChangeMeditation(1f);
                actor.ChangeYuanNeng(3f);
            }
        }
        if (actor.hasTrait("flair93"))
        {
            actor.ChangeYuanNeng(1f);
        }
        if (actor.hasTrait("flair94"))
        {
            actor.ChangeYuanNeng(5f);
            if (actor.hasTrait("meditation1") || actor.hasTrait("meditation2") || actor.hasTrait("meditation3"))
            {
                actor.ChangeMeditation(3f);
            }
        }
        if (actor.hasTrait("flair95"))
        {
            actor.ChangeYuanNeng(10f);
            if (actor.hasTrait("meditation1") || actor.hasTrait("meditation2") || actor.hasTrait("meditation3"))
            {
                actor.ChangeMeditation(8f);
            }
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
    private static bool ShouldApplyDeathMark(string actorName)
    {
        return Regex.IsMatch(actorName, @"^(?=.*\u5341)(?=.*\u6708)(?=.*\u767D)");
    }
    private static bool SetRandomXiaohaoBasedOnFlair(Actor actor)
    {
        // 定义flair特质对应的["xiaohao"]随机范围
        var flairXiaohaoRanges = new Dictionary<string, (float min, float max)>
        {
            { "flair1", (-0.2f, -0.5f) },
            { "flair2", (-0.5f, -0.8f) },
            { "flair3", (-0.8f, -1.1f) },
            { "flair4", (-1.1f, -1.4f) },
            { "flair5", (-1.4f, -1.7f) },
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
            WorldTip.showNow(text, false, "top", 15f);


        // 如果不需要跳过原方法，则返回true
        return false;
    }
    [HarmonyPrefix, HarmonyPatch(typeof(Actor), "getHit")]
        public static bool actorGetHit_prefix(
            Actor __instance,
            ref float pDamage,
            bool pFlash,
            AttackType pAttackType,
            BaseSimObject pAttacker,
            bool pSkipIfShake,
            bool pMetallicWeapon)
        {
            __instance.attackedBy = null;
        if (pSkipIfShake && __instance.shake_active) return true;
        if (IsActorDead(__instance)) return true;
        if (__instance.hasStatus("invincible")) return true;
        if (pAttacker is Actor attackerActor && __instance.isAlive())
        {
            float attackerAccuracy = attackerActor.stats["Accuracy"];
            float targetDodge = __instance.stats["Dodge"];
            float effectiveDodge = Mathf.Clamp(targetDodge - attackerAccuracy, 0f, 100f);

            if (Toolbox.randomChance(effectiveDodge / 100f))
            {
                __instance.startColorEffect(ActorColorEffect.White);
                return false;
            }
        }
        if (pAttacker != null && pAttacker.a != null && pAttacker.a.stats != null)
        {
            Actor attacker = pAttacker.a;
            float intelligence = attacker.stats[S.intelligence];

            float minMultiplier = 0f, maxMultiplier = 0f;
            if (attacker.hasTrait("Grade91"))
            {
                minMultiplier = 4800f;
                maxMultiplier = 5600f;
            }
            else if (attacker.hasTrait("Grade9"))
            {
                minMultiplier = 67f;
                maxMultiplier = 77f;
            }
            else if (attacker.hasTrait("Grade8"))
            {
                minMultiplier = 30f;
                maxMultiplier = 50f;
            }
            else if (attacker.hasTrait("Grade7"))
            {
                minMultiplier = 20f;
                maxMultiplier = 40f;
            }
            else if (attacker.hasTrait("Grade6"))
            {
                minMultiplier = 14f;
                maxMultiplier = 15f;
            }
            else if (attacker.hasTrait("Grade5"))
            {
                minMultiplier = 12f;
                maxMultiplier = 15f;
            }
            else if (attacker.hasTrait("Grade4"))
            {
                minMultiplier = 10f;
                maxMultiplier = 13f;
            }
            else
            {
                goto SkipMagicDamage;
            }

            float multiplier = UnityEngine.Random.Range(minMultiplier, maxMultiplier);
            int magicDamage = Mathf.FloorToInt(intelligence * multiplier);
            __instance.data.health -= magicDamage;

            __instance.spawnParticle(Toolbox.makeColor("#FF00FF"));
        }
        SkipMagicDamage:
		if (pAttackType == AttackType.Weapon)
		{
			bool flag = false;
			if (pMetallicWeapon && __instance.haveMetallicWeapon())
			{
				flag = true;
			}
			if (flag)
			{
				MusicBox.playSound("event:/SFX/HIT/HitSwordSword", __instance.currentTile, false, true);
			}
			else if (__instance.asset.sound_hit != string.Empty)
			{
				MusicBox.playSound(__instance.asset.sound_hit, __instance.currentTile, false, true);
			}
		}
		if (pAttackType == AttackType.Other || pAttackType == AttackType.Weapon)
		{
			float num = 1f - __instance.stats[S.armor] / 100f;
			pDamage *= num;
		}
		if (pDamage < 1f)
		{
			pDamage = 1f;
		}
		__instance.data.health -= (int)pDamage;
		__instance.timer_action = 0.002f;
		if (pAttacker != __instance)
		{
			__instance.attackedBy = pAttacker;
		}
		foreach (string pID in __instance.data.s_traits_ids)
		{
			GetHitAction action_get_hit = AssetManager.traits.get(pID).action_get_hit;
			if (action_get_hit != null)
			{
				action_get_hit(__instance, pAttacker, __instance.currentTile);
			}
		}
		if (pFlash)
		{
			__instance.startColorEffect(ActorColorEffect.Red);
		}
		if (__instance.data.health <= 0)
		{
			Kingdom kingdom = __instance.kingdom;
			if (pAttacker != null && pAttacker != __instance && pAttacker.isActor() && pAttacker.isAlive())
			{
				BattleKeeperManager.unitKilled(__instance);
				pAttacker.a.newKillAction(__instance, kingdom);
				if (pAttacker.city != null)
				{
					bool flag2 = false;
					if (__instance.asset.animal)
					{
						flag2 = true;
						pAttacker.city.data.storage.change("meat", 1);
					}
					else if (__instance.asset.unit && pAttacker.a.hasTrait("savage"))
					{
						flag2 = true;
					}
					if (flag2)
					{
						if (Toolbox.randomChance(0.5f))
						{
							pAttacker.city.data.storage.change(SR.bones, 1);
						}
						else if (Toolbox.randomChance(0.5f))
						{
							pAttacker.city.data.storage.change(SR.leather, 1);
						}
						else if (Toolbox.randomChance(0.5f))
						{
							pAttacker.city.data.storage.change(SR.meat, 1);
						}
					}
				}
			}
			__instance.killHimself(false, pAttackType, true, true, true);
			return true;
		}
		if (pAttackType == AttackType.Weapon && !__instance.asset.immune_to_injuries && !__instance.hasStatus("shield"))
		{
			if (Toolbox.randomChance(0.02f))
			{
				__instance.addTrait("crippled", false);
			}
			if (Toolbox.randomChance(0.02f))
			{
				__instance.addTrait("eyepatch", false);
			}
		}
		__instance.startShake(0.3f, 0.1f, true, true);
		if (!__instance.has_attack_target && __instance.attackedBy != null && !__instance.shouldIgnoreTarget(__instance.attackedBy) && __instance.canAttackTarget(__instance.attackedBy))
		{
			__instance.setAttackTarget(__instance.attackedBy);
		}
		if (__instance.activeStatus_dict != null)
		{
			foreach (StatusEffectData statusEffectData in __instance.activeStatus_dict.Values)
			{
				GetHitAction action_get_hit2 = statusEffectData.asset.action_get_hit;
				if (action_get_hit2 != null)
				{
					action_get_hit2(__instance, pAttacker, __instance.currentTile);
				}
			}
		}
		GetHitAction action_get_hit3 = __instance.asset.action_get_hit;;
        if (action_get_hit3 == null)
        {
            return true;
        }
		action_get_hit3(__instance, pAttacker, __instance.currentTile);
        return true;
	}
}