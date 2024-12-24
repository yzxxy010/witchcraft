using System.Collections.Generic;
using HarmonyLib;
using UnityEngine;
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


    [HarmonyPrefix, HarmonyPatch(typeof(Actor), "getHit")]
    public static void actorGetHit_prefix(
        Actor         __instance,
        ref float     pDamage,
        bool          pFlash,
        AttackType    pAttackType,
        BaseSimObject pAttacker,
        bool          pSkipIfShake,
        bool          pMetallicWeapon)
    {
        __instance.attackedBy = null;
        if (pSkipIfShake && __instance.shake_active)
        {
            return;
        }

        if (__instance.data.health <= 0)
        {
            return;
        }

        if (__instance.hasStatus("invincible"))

        {
            return;
        }

        if (pAttackType == AttackType.Weapon)
        {
            bool flag = pMetallicWeapon && __instance.haveMetallicWeapon();
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


                //新增判定
                newMechanism(__instance, pAttacker);


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
            return;
        }

        if (pAttackType == AttackType.Weapon && !__instance.asset.immune_to_injuries &&
            !__instance.hasStatus("shield"))
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
        if (!__instance.has_attack_target                         && __instance.attackedBy != null &&
            !__instance.shouldIgnoreTarget(__instance.attackedBy) &&
            __instance.canAttackTarget(__instance.attackedBy))
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

        GetHitAction action_get_hit3 = __instance.asset.action_get_hit;
        if (action_get_hit3 == null)
        {
            return;
        }

        action_get_hit3(__instance, pAttacker, __instance.currentTile);
    }

    private static void newMechanism(Actor __instance, BaseSimObject pAttacker)
    {
        if (__instance.a.hasTrait("extraordinary9") && pAttacker.a.hasTrait("extraordinary9"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("fountainhead1") && pAttacker.a.hasTrait("extraordinary9"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("fountainhead2") && pAttacker.a.hasTrait("extraordinary9"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("fountainhead3") && pAttacker.a.hasTrait("extraordinary9"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("fountainhead1") && pAttacker.a.hasTrait("fountainhead1"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("fountainhead1") && pAttacker.a.hasTrait("fountainhead2"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("fountainhead1") && pAttacker.a.hasTrait("fountainhead3"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("fountainhead2") && pAttacker.a.hasTrait("fountainhead2"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("fountainhead3") && pAttacker.a.hasTrait("fountainhead3"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("extraordinary9") && pAttacker.a.hasTrait("fountainhead1"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("extraordinary9") && pAttacker.a.hasTrait("fountainhead2"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("extraordinary9") && pAttacker.a.hasTrait("fountainhead3"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("fountainhead2") && pAttacker.a.hasTrait("fountainhead1"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("fountainhead2") && pAttacker.a.hasTrait("fountainhead3"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("fountainhead3") && pAttacker.a.hasTrait("fountainhead2"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
        }

        if (__instance.a.hasTrait("fountainhead3") && pAttacker.a.hasTrait("fountainhead1"))
        {
            pAttacker.a.ChangeBenYuan(__instance.GetBenYuan() + 1);
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
}