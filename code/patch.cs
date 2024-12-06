using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;


namespace VideoCopilot.code
{
    internal class patch
    {
        [HarmonyPrefix, HarmonyPatch(typeof(Actor), "getHit")]
        public static void actorGetHit_prefix(
            Actor __instance,
            ref float pDamage,
            bool pFlash,
            AttackType pAttackType,
            BaseSimObject pAttacker,
            bool pSkipIfShake,
            bool pMetallicWeapon)
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
            if (!__instance.has_attack_target && __instance.attackedBy != null &&
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
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("fountainhead1") && pAttacker.a.hasTrait("extraordinary9"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("fountainhead2") && pAttacker.a.hasTrait("extraordinary9"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("fountainhead3") && pAttacker.a.hasTrait("extraordinary9"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("fountainhead1") && pAttacker.a.hasTrait("fountainhead1"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("fountainhead1") && pAttacker.a.hasTrait("fountainhead2"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("fountainhead1") && pAttacker.a.hasTrait("fountainhead3"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("fountainhead2") && pAttacker.a.hasTrait("fountainhead2"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("fountainhead3") && pAttacker.a.hasTrait("fountainhead3"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("extraordinary9") && pAttacker.a.hasTrait("fountainhead1"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("extraordinary9") && pAttacker.a.hasTrait("fountainhead2"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("extraordinary9") && pAttacker.a.hasTrait("fountainhead3"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("fountainhead2") && pAttacker.a.hasTrait("fountainhead1"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("fountainhead2") && pAttacker.a.hasTrait("fountainhead3"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("fountainhead3") && pAttacker.a.hasTrait("fountainhead2"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }

            if (__instance.a.hasTrait("fountainhead3") && pAttacker.a.hasTrait("fountainhead1"))
            {
                pAttacker.a.stats["benyuan"] += __instance.stats["benyuan"] + 1;
            }
        }

        [HarmonyPostfix, HarmonyPatch(typeof(WindowCreatureInfo), "OnEnable")]
        public static void OnEnable_Postfix(WindowCreatureInfo __instance)
        {
            if (Config.selectedUnit == null)
            {
                return;
            }

            __instance.showStat("benyuan", __instance.actor.stats["benyuan"]);
            __instance.showStat("TSOTW", Globals.Tsotw.ToString());
            __instance.showStat("xiaohao", __instance.actor.stats["xiaohao"]);
            __instance.showStat("yuanneng", __instance.actor.stats["yuanneng"]);
        }

        static float currentBenyuan = 0;
        static float currentYuanneng = 0;


        [HarmonyPrefix, HarmonyPatch(typeof(ActorBase), "updateStats")]
        public static bool updateStats(ActorBase __instance)
        {
            if (!__instance.statsDirty)
            {
                return false;
            }

            currentBenyuan = __instance.stats["benyuan"];
            currentYuanneng = __instance.stats["yuanneng"];

            __instance.statsDirty = false;
            __instance.batch.c_stats_dirty.Remove(__instance.a);
            if (!__instance.isAlive())
            {
                return false;
            }

            __instance.checkColorSets();
            if (string.IsNullOrEmpty(__instance.data.mood))
            {
                __instance.data.mood = "normal";
            }

            MoodAsset moodAsset = AssetManager.moods.get(__instance.data.mood);
            __instance.stats.clear();
            __instance.stats.mergeStats(__instance.asset.base_stats);
            __instance.stats.mergeStats(moodAsset.base_stats);
            __instance.stats["benyuan"] = currentBenyuan;
            __instance.stats["yuanneng"] = currentYuanneng;
            BaseStats stats = __instance.stats;
            string pKey = S.diplomacy;
            stats[pKey] += (float)__instance.data.diplomacy;
            stats = __instance.stats;
            pKey = S.stewardship;
            stats[pKey] += (float)__instance.data.stewardship;
            stats = __instance.stats;
            pKey = S.intelligence;
            stats[pKey] += (float)__instance.data.intelligence;
            stats = __instance.stats;
            pKey = S.warfare;
            stats[pKey] += (float)__instance.data.warfare;
            if (__instance.hasAnyStatusEffect())
            {
                foreach (StatusEffectData statusEffectData in __instance.activeStatus_dict.Values)
                {
                    __instance.stats.mergeStats(statusEffectData.asset.base_stats);
                }
            }

            if (!__instance.hasWeapon())
            {
                ItemAsset itemAsset = AssetManager.items.get(__instance.asset.defaultAttack);
                if (itemAsset != null)
                {
                    __instance.stats.mergeStats(itemAsset.base_stats);
                }
            }

            __instance.s_attackType = __instance.getWeaponAsset().attackType;
            __instance.s_slashType = __instance.getWeaponAsset().path_slash_animation;
            __instance.dirty_sprite_item = true;
            for (int i = 0; i < __instance.data.traits.Count; i++)
            {
                string pID = __instance.data.traits[i];
                ActorTrait actorTrait = AssetManager.traits.get(pID);
                if (actorTrait != null && (!actorTrait.only_active_on_era_flag ||
                                           ((!actorTrait.era_active_moon || World.world_era.flag_moon) &&
                                            (!actorTrait.era_active_night || World.world_era.overlay_darkness))))
                {
                    __instance.stats.mergeStats(actorTrait.base_stats);
                }
            }

            if (__instance.asset.unit)
            {
                __instance.s_personality = null;
                if ((__instance.kingdom != null && __instance.kingdom.isCiv() && __instance.isKing()) ||
                    (__instance.city != null && __instance.city.leader == __instance))
                {
                    string pID2 = "balanced";
                    float num = __instance.stats[S.diplomacy];
                    if (__instance.stats[S.diplomacy] > __instance.stats[S.stewardship])
                    {
                        pID2 = "diplomat";
                        num = __instance.stats[S.diplomacy];
                    }
                    else if (__instance.stats[S.diplomacy] < __instance.stats[S.stewardship])
                    {
                        pID2 = "administrator";
                        num = __instance.stats[S.stewardship];
                    }

                    if (__instance.stats[S.warfare] > num)
                    {
                        pID2 = "militarist";
                    }

                    __instance.s_personality = AssetManager.personalities.get(pID2);
                    __instance.stats.mergeStats(__instance.s_personality.base_stats);
                }
            }

            Clan clan = __instance.getClan();
            if (clan != null && clan.bonus_stats != null)
            {
                __instance.stats.mergeStats(clan.bonus_stats.base_stats);
            }

            stats = __instance.stats;
            pKey = S.health;
            stats[pKey] += (float)((__instance.data.level - 1) * 20);
            stats = __instance.stats;
            pKey = S.damage;
            stats[pKey] += (float)((__instance.data.level - 1) / 2);
            stats = __instance.stats;
            pKey = S.armor;
            stats[pKey] += (float)((__instance.data.level - 1) / 3);
            stats = __instance.stats;
            pKey = S.attack_speed;
            stats[pKey] += (float)(__instance.data.level - 1);
            bool flag = __instance.hasTrait("madness");
            __instance.data.s_traits_ids.Clear();
            __instance.s_action_attack_target = null;
            List<ItemAsset> list = __instance.s_special_effect_items;
            if (list != null)
            {
                list.Clear();
            }

            Dictionary<ItemAsset, double> dictionary = __instance.s_special_effect_items_timers;
            if (dictionary != null)
            {
                dictionary.Clear();
            }

            List<ActorTrait> list2 = __instance.s_special_effect_traits;
            if (list2 != null)
            {
                list2.Clear();
            }

            Dictionary<ActorTrait, double> dictionary2 = __instance.s_special_effect_traits_timers;
            if (dictionary2 != null)
            {
                dictionary2.Clear();
            }

            foreach (string text in __instance.data.traits)
            {
                ActorTrait actorTrait2 = AssetManager.traits.get(text);
                if (actorTrait2 != null)
                {
                    __instance.data.s_traits_ids.Add(text);
                    if (actorTrait2.action_special_effect != null)
                    {
                        if (__instance.s_special_effect_traits == null)
                        {
                            __instance.s_special_effect_traits = new List<ActorTrait>();
                            __instance.s_special_effect_traits_timers = new Dictionary<ActorTrait, double>();
                        }

                        __instance.s_special_effect_traits.Add(actorTrait2);
                    }

                    if (actorTrait2.action_attack_target != null)
                    {
                        __instance.s_action_attack_target =
                            (AttackAction)Delegate.Combine(__instance.s_action_attack_target,
                                actorTrait2.action_attack_target);
                    }
                }
            }

            __instance.has_trait_light = __instance.hasTrait("light_lamp");
            __instance.has_trait_weightless = __instance.hasTrait("weightless");
            __instance.has_status_frozen = __instance.hasStatus("frozen");
            if (!__instance.hasWeapon())
            {
                ItemAsset weaponAsset = __instance.getWeaponAsset();
                __instance.addItemActions(weaponAsset);
                if (weaponAsset.item_modifiers != null)
                {
                    foreach (string pID3 in weaponAsset.item_modifiers)
                    {
                        ItemAsset itemAsset2 = AssetManager.items_modifiers.get(pID3);
                        if (itemAsset2 != null)
                        {
                            __instance.addItemActions(itemAsset2);
                        }
                    }
                }
            }

            if (__instance.asset.use_items)
            {
                List<ActorEquipmentSlot> list3 = ActorEquipment.getList(__instance.equipment);
                for (int j = 0; j < list3.Count; j++)
                {
                    ActorEquipmentSlot actorEquipmentSlot = list3[j];
                    if (actorEquipmentSlot.data != null)
                    {
                        ItemData itemData = actorEquipmentSlot.data;
                        ItemAsset pItemAsset = AssetManager.items.get(itemData.id);
                        __instance.addItemActions(pItemAsset);
                        foreach (string pID4 in itemData.modifiers)
                        {
                            ItemAsset pItemAsset2 = AssetManager.items_modifiers.get(pID4);
                            __instance.addItemActions(pItemAsset2);
                        }
                    }
                }
            }

            bool flag2 = __instance.hasTrait("madness");
            if (__instance.s_special_effect_traits == null || __instance.s_special_effect_traits.Count == 0)
            {
                __instance.s_special_effect_traits = null;
                __instance.s_special_effect_traits_timers = null;
                __instance.batch.c_trait_effects.Remove(__instance.a);
            }
            else
            {
                __instance.batch.c_trait_effects.Add(__instance.a);
            }

            if (__instance.s_special_effect_items == null || __instance.s_special_effect_items.Count == 0)
            {
                __instance.s_special_effect_items = null;
                __instance.s_special_effect_items_timers = null;
                __instance.batch.c_item_effects.Remove(__instance.a);
            }
            else
            {
                __instance.batch.c_item_effects.Add(__instance.a);
            }

            if (flag2 != flag)
            {
                __instance.checkMadness(flag2);
            }

            __instance.has_trait_peaceful = __instance.hasTrait("peaceful");
            __instance.has_trait_fire_resistant = __instance.hasTrait("fire_proof");
            __instance.has_status_burning = __instance.hasStatus("burning");
            __instance.has_trait_madness = __instance.hasTrait("madness");
            if (__instance.asset.use_items)
            {
                List<ActorEquipmentSlot> list4 = ActorEquipment.getList(__instance.equipment);
                for (int k = 0; k < list4.Count; k++)
                {
                    ActorEquipmentSlot actorEquipmentSlot2 = list4[k];
                    if (actorEquipmentSlot2.data != null)
                    {
                        ItemTools.mergeStatsWithItem(__instance.stats, actorEquipmentSlot2.data, false);
                    }
                }
            }

            __instance.stats.normalize();
            __instance.stats.checkMods();
            if (__instance.event_full_heal)
            {
                __instance.event_full_heal = false;
                __instance.stats.normalize();
                __instance.data.health = __instance.getMaxHealth();
            }

            Culture culture = __instance.getCulture();
            if (culture != null)
            {
                stats = __instance.stats;
                pKey = S.damage;
                stats[pKey] += __instance.stats[S.damage] * culture.stats.bonus_damage.value;
                stats = __instance.stats;
                pKey = S.armor;
                stats[pKey] += __instance.stats[S.armor] * culture.stats.bonus_armor.value;
                stats = __instance.stats;
                pKey = S.max_age;
                stats[pKey] += (float)culture.getMaxAgeBonus();
            }

            if (__instance.kingdom != null)
            {
                stats = __instance.stats;
                pKey = S.damage;
                stats[pKey] += __instance.stats[S.damage] * __instance.kingdom.stats.bonus_damage.value;
                stats = __instance.stats;
                pKey = S.armor;
                stats[pKey] += __instance.stats[S.armor] * __instance.kingdom.stats.bonus_armor.value;
            }

            if (__instance.asset.unit)
            {
                __instance.calculateFertility();
            }

            stats = __instance.stats;
            pKey = S.zone_range;
            stats[pKey] += (float)((int)(__instance.stats[S.stewardship] / 10f));
            stats = __instance.stats;
            pKey = S.cities;
            stats[pKey] += (float)((int)__instance.stats[S.stewardship] / 6 + 1);
            stats = __instance.stats;
            pKey = S.bonus_towers;
            stats[pKey] += (float)((int)(__instance.stats[S.warfare] / 10f));
            if (__instance.s_attackType == WeaponType.Range)
            {
                stats = __instance.stats;
                pKey = S.range;
                stats[pKey] += __instance.stats[S.range] * World.world_era.range_weapons_mod;
            }

            __instance.attackTimer = 0f;
            __instance.stats.normalize();
            if (__instance.data.health > __instance.getMaxHealth())
            {
                __instance.data.health = __instance.getMaxHealth();
            }

            __instance.target_scale = __instance.stats[S.scale];
            __instance.s_attackSpeed_seconds =
                (300f - __instance.stats[S.attack_speed]) / (100f + __instance.stats[S.attack_speed]);
            if (__instance.s_attackSpeed_seconds < 0.1f)
            {
                __instance.s_attackSpeed_seconds = 0.1f;
            }

            WorldAction action_recalc_stats = __instance.asset.action_recalc_stats;
            if (action_recalc_stats == null)
            {
                return false;
            }

            action_recalc_stats(__instance, null);
            return false;
        }

        [HarmonyPostfix, HarmonyPatch(typeof(MapBox), "updateObjectAge")]
        public static void updateWorldTime_Postfix(MapBox __instance)
        {
            if (Globals.WorldName != __instance.mapStats.name)
            {
                Globals.Tsotw = 10000;
                Globals.WorldName = __instance.mapStats.name;
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
                Debug.Log(actor.getName());
                Globals.Actors.Add(actor.data.id, actor);
            }

            if (Globals.Tsotw >= actor.stats["xiaohao"])
            {
                Globals.Tsotw += actor.stats["xiaohao"];
                actor.stats["yuanneng"] -=  actor.stats["xiaohao"];
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
    }
}    
