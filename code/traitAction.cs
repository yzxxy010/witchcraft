﻿using System.Collections.Generic;
using System.Linq;
using ai;
using ReflectionUtility;
using UnityEngine;
using VideoCopilot.code.utils;

namespace VideoCopilot.code
{
    internal class traitAction
    {
        private static Dictionary<string, int> _reviveCounts = new Dictionary<string, int>(); //跟踪每个名字复活次数

        //以下为拥有这个巫术状态的效果
        public static bool attack_Ring05(BaseSimObject pTarget, WorldTile pTile = null)
        {
            float pDamage = 10f; // 每次受到的伤害值
            // 目标生命值大于-1，则对目标施加伤害
            if (Toolbox.randomBool() && pTarget.a.data.health > -1)
            {
                pTarget.getHit(pDamage, true, AttackType.Poison, null, true, false);
            }

            // 在目标位置生成表示火的粒子效果
            pTarget.a.spawnParticle(Toolbox.color_fire);
            // 使目标开始震动，模拟反应
            pTarget.a.startShake(0.4f, 0.2f, true, false);
            // 返回true，表示效果已成功应用
            return true;
        }

        public static bool attack_Ring25(BaseSimObject pTarget, WorldTile pTile = null)
        {
            float pDamage = 900f; // 每次受到的伤害值
            // 目标生命值大于-1，则对目标施加伤害
            if (Toolbox.randomBool() && pTarget.a.data.health > -1)
            {
                pTarget.getHit(pDamage, true, AttackType.Poison, null, true, false);
            }

            // 在目标位置生成表示感染的粒子效果
            pTarget.a.spawnParticle(Toolbox.color_infected);
            // 使目标开始震动，模拟反应
            pTarget.a.startShake(0.4f, 0.2f, true, false);
            // 返回true，表示效果已成功应用
            return true;
        }

        //以下为巫术
        public static bool attack_sorcery01(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string>
                { "Grade1", "Grade2", "Grade3", "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (Toolbox.randomChance(1f))
            {
                pSelf.a.addStatusEffect("Ring01", 3f); //零环•轻身术
            }

            return true;
        }

        public static bool attack_sorcery02(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string>
                { "Grade1", "Grade2", "Grade3", "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if (Toolbox.randomChance(1f))
            {
                pTarget.a.addStatusEffect("Ring05", 3f); //零环•烈焰之握
            }

            return true;
        }

        public static bool attack_sorcery03(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string>
                { "Grade1", "Grade2", "Grade3", "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if (Toolbox.randomChance(1f))
            {
                pTarget.a.addStatusEffect("Ring02", 3f); //零环•水幻迷障
            }

            return true;
        }

        public static bool sorcery04_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(2); //零环•生命祈愈
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool attack_sorcery05(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string>
                { "Grade1", "Grade2", "Grade3", "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (Toolbox.randomChance(1f))
            {
                pSelf.a.addStatusEffect("Ring03", 3f); //零环•大地壁垒
            }

            return true;
        }

        public static bool attack_sorcery06(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string>
                { "Grade1", "Grade2", "Grade3", "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if (Toolbox.randomChance(0.8f))
            {
                pTarget.a.addStatusEffect("Ring04", 3f); //零环•蔓藤囚牢
            }

            return true;
        }

        public static bool attack_sorcery11(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string>
                { "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if (Toolbox.randomChance(0.7f))
            {
                pTarget.a.addStatusEffect("Ring11", 6f); //一环•疲惫之手
            }

            return true;
        }

        public static bool attack_sorcery12(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string>
                { "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if (Toolbox.randomChance(0.8f))
            {
                pTarget.a.addStatusEffect("Ring12", 6f); //一环•缠绕之网
            }

            return true;
        }

        public static bool attack_sorcery13(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string>
                { "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (Toolbox.randomChance(0.8f))
            {
                pSelf.a.addStatusEffect("Ring13", 6f); //一环•土之坚甲
            }

            return true;
        }

        public static bool sorcery14_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(23); //一环•复苏之流
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool attack_sorcery15(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string>
                { "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (Toolbox.randomChance(0.8f))
            {
                pSelf.a.addStatusEffect("Ring14", 6f); //一环•风行术
            }

            return true;
        }

        public static bool attack_sorcery16(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string>
                { "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if (Toolbox.randomChance(0.8f))
            {
                pTarget.a.addStatusEffect("Ring15", 6f); //一环•水雾术
            }

            return true;
        }

        public static bool attack_sorcery22(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string> { "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if (Toolbox.randomChance(0.8f))
            {
                pTarget.a.addStatusEffect("Ring22", 8f); //二环•星之致幻
            }

            return true;
        }

        public static bool attack_sorcery23(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个
            var invalidTraits = new HashSet<string> { "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            const int drainAmount = 150; // 定义要汲取的血量
            if (pSelf == null || pSelf.a == null || pSelf.a.data == null)
            {
                return false; // 如果施法者无效，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            // 检查目标是否有足够的血量可以被汲取
            if (pTarget.a.data.health > 0)
            {
                int actualDrain = Mathf.Min(drainAmount, pTarget.a.data.health); // 实际汲取的血量，不超过目标的当前血量
                pTarget.a.data.health -= actualDrain;                            // 减少目标的血量
                pSelf.a.data.health =
                    Mathf.Min(pSelf.a.getMaxHealth(), pSelf.a.data.health + actualDrain); // 恢复施法者的血量，但不超过其最大血量
            }

            return true;
        }

        public static bool attack_sorcery24(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string> { "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (Toolbox.randomChance(1f))
            {
                pSelf.a.addStatusEffect("Ring24", 8f); //二环•岩石护壁
            }

            return true;
        }

        public static bool sorcery25_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(1000); //二环•生命涌泉
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool attack_sorcery26(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string> { "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if (Toolbox.randomChance(1f))
            {
                pTarget.a.addStatusEffect("Ring25", 5f); //5秒状态:二环•生命流逝
            }

            return true;
        }

        public static bool attack_sorcery31(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string> { "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if (Toolbox.randomChance(1f))
            {
                pTarget.a.addStatusEffect("Ring31", 10f); //三环•斥力场状态
            }

            return true;
        }

        public static bool sorcery31_Attack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(1f)) //三环•斥力场
            {
                Actor a = pTarget.a;
                Actor s = pSelf.a;

                EffectsLibrary.spawnExplosionWave(pTile.posV3, 0.05f, 6f);
                return false;
            }

            return false;
        }

        public static bool attack_sorcery32(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string> { "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if (Toolbox.randomChance(1f))
            {
                pTarget.a.addStatusEffect("Ring32", 6f); //三环•裂界爆炎状态
            }

            return true;
        }

        public static bool sorcery32_Attack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 特效的随机大小
            float explosionScaleMin = 0.1f;
            float explosionScaleMax = 0.3f;
            float explosionScale = UnityEngine.Random.Range(explosionScaleMin, explosionScaleMax);

            if (Toolbox.randomChance(1f)) //三环•裂界爆炎
            {
                // 使用随机大小生成爆炸特效
                EffectsLibrary.spawnAtTileRandomScale("fx_explosion_tiny", pTile, explosionScale, explosionScale);
            }

            return true;
        }

        public static bool sorcery33_Attack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string> { "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if (Toolbox.randomChance(1))
            {
                pTarget.a.addStatusEffect("Ring33", 10f); //三环•雷霆术状态
            }

            return true;
        }

        public static bool attack_sorcery33(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null) //三环•雷霆术
            {
                if (Toolbox.randomChance(100.0f))
                {
                    MapBox.spawnLightningMedium(pTile, 0.25f);
                }
            }

            return false;
        }

        public static bool sorcery34_Attack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string> { "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            // 检查血量是否低于60%
            if ((float)pSelf.base_data.health < (float)pSelf.getMaxHealth() * 0.6f)
            {
                // 如果血量低于50%，则为目标添加血眸状态
                pSelf.a.addStatusEffect("Ring34", 10f);
            }

            return true;
        }

        public static bool attack_Grade91(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pSelf == null || pTarget == null)
            {
                return false;
            }
            if (pTarget != null)
            {
                pTile = pTarget.currentTile;
            }
            if (pTile == null)
            {
                return false;
            }
            EffectsLibrary.spawn("fx_meteorite", pTile, "meteorite", null, 0f, -1f, -1f);
            return true;
        }

        public static bool sorcery35_Attack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
            var invalidTraits = new HashSet<string> { "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if (Toolbox.randomChance(1))
            {
                pTarget.a.addStatusEffect("Ring35", 10f); //黯镰噬魂·万灵湮灭之仪状态
            }

            return true;
        }

        //以下为境界带的再生
        public static bool Grade02_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.hasTrait("infected"))
            {
                return true;
            }

            bool flag = true;
            if (pTarget.a.asset.needFood)
            {
                flag = (pTarget.a.data.hunger > 0);
            }

            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(5);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }
        public static bool Grade1_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.hasTrait("infected"))
            {
                return true;
            }

            bool flag = true;
            if (pTarget.a.asset.needFood)
            {
                flag = (pTarget.a.data.hunger > 0);
            }

            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(10);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool Grade2_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.hasTrait("infected"))
            {
                return true;
            }

            bool flag = true;
            if (pTarget.a.asset.needFood)
            {
                flag = (pTarget.a.data.hunger > 0);
            }

            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(20);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool Grade3_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.hasTrait("infected"))
            {
                return true;
            }

            bool flag = true;
            if (pTarget.a.asset.needFood)
            {
                flag = (pTarget.a.data.hunger > 0);
            }

            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(30);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool Grade4_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.hasTrait("infected"))
            {
                return true;
            }

            bool flag = true;
            if (pTarget.a.asset.needFood)
            {
                flag = (pTarget.a.data.hunger > 0);
            }

            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(200);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool Grade5_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.hasTrait("infected"))
            {
                return true;
            }

            bool flag = true;
            if (pTarget.a.asset.needFood)
            {
                flag = (pTarget.a.data.hunger > 0);
            }

            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(400);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool Grade6_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.hasTrait("infected"))
            {
                return true;
            }

            bool flag = true;
            if (pTarget.a.asset.needFood)
            {
                flag = (pTarget.a.data.hunger > 0);
            }

            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(600);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool Grade7_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(2000);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool Grade8_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(5000);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool Grade9_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(10000);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool Grade91_EffectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(2000000);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool flair8_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(300);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool flair91_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(1000);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }


        public static bool hunger_Grade91(BaseSimObject pTarget, WorldTile pTile = null) //始祖的饱食度
        {
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (a != null)
            {
                a.data.hunger = 100; // 不会饥饿，饱食度一直为100%
            }

            return false;
        }

        public static bool intelligence_attack_Grade4(BaseSimObject pSelf, BaseSimObject pTarget,
                                                      WorldTile     pTile = null)
        {
            // 每次攻击计算自身智力值的随机倍数，再减少目标血量
            if (pTarget != null && pTarget.a != null && pTarget.a.data != null && pSelf != null && pSelf.a != null &&
                pSelf.a.stats != null)
            {
                // 智力值的随机倍数，范围在x到x之间
                float intelligenceMultiplierMin = 5f;
                float intelligenceMultiplierMax = 8f;
                float intelligenceMultiplier =
                    UnityEngine.Random.Range(intelligenceMultiplierMin, intelligenceMultiplierMax);

                float damageBasedOnIntelligence = pSelf.a.stats[S.intelligence] * intelligenceMultiplier; // 根据智力计算伤害
                int damageToDeal = Mathf.FloorToInt(damageBasedOnIntelligence);                           // 将浮点数伤害转换为整数

                pTarget.a.data.health -= damageToDeal; // 减去基于智力的伤害值

                if (pTarget.a.data.health <= 0)
                {
                    pTarget.a.data.alive = false;
                    AttackType attackTypeInstance = new AttackType();
                    (pTarget.a as Actor)?.killHimself(false, attackTypeInstance, true, true, true);
                }
            }

            return true;
        }

        public static bool intelligence_attack_Grade5(BaseSimObject pSelf, BaseSimObject pTarget,
                                                      WorldTile     pTile = null)
        {
            // 每次攻击计算自身智力值的随机倍数，再减少目标血量
            if (pTarget != null && pTarget.a != null && pTarget.a.data != null && pSelf != null && pSelf.a != null &&
                pSelf.a.stats != null)
            {
                // 智力值的随机倍数，范围在x到x之间
                float intelligenceMultiplierMin = 7f;
                float intelligenceMultiplierMax = 10f;
                float intelligenceMultiplier =
                    UnityEngine.Random.Range(intelligenceMultiplierMin, intelligenceMultiplierMax);

                float damageBasedOnIntelligence = pSelf.a.stats[S.intelligence] * intelligenceMultiplier; // 根据智力计算伤害
                int damageToDeal = Mathf.FloorToInt(damageBasedOnIntelligence);                           // 将浮点数伤害转换为整数

                pTarget.a.data.health -= damageToDeal; // 减去基于智力的伤害值

                if (pTarget.a.data.health <= 0)
                {
                    pTarget.a.data.alive = false;
                    AttackType attackTypeInstance = new AttackType();
                    (pTarget.a as Actor)?.killHimself(false, attackTypeInstance, true, true, true);
                }
            }

            return true;
        }

        public static bool intelligence_attack_Grade6(BaseSimObject pSelf, BaseSimObject pTarget,
                                                      WorldTile     pTile = null)
        {
            // 每次攻击计算自身智力值的随机倍数，再减少目标血量
            if (pTarget != null && pTarget.a != null && pTarget.a.data != null && pSelf != null && pSelf.a != null &&
                pSelf.a.stats != null)
            {
                // 智力值的随机倍数，范围在x到x之间
                float intelligenceMultiplierMin = 9f;
                float intelligenceMultiplierMax = 10f;
                float intelligenceMultiplier =
                    UnityEngine.Random.Range(intelligenceMultiplierMin, intelligenceMultiplierMax);

                float damageBasedOnIntelligence = pSelf.a.stats[S.intelligence] * intelligenceMultiplier; // 根据智力计算伤害
                int damageToDeal = Mathf.FloorToInt(damageBasedOnIntelligence);                           // 将浮点数伤害转换为整数

                pTarget.a.data.health -= damageToDeal; // 减去基于智力的伤害值

                if (pTarget.a.data.health <= 0)
                {
                    pTarget.a.data.alive = false;
                    AttackType attackTypeInstance = new AttackType();
                    (pTarget.a as Actor)?.killHimself(false, attackTypeInstance, true, true, true);
                }
            }

            return true;
        }

        public static bool intelligence_attack_Grade7(BaseSimObject pSelf, BaseSimObject pTarget,
                                                      WorldTile     pTile = null)
        {
            // 每次攻击计算自身智力值的随机倍数，再减少目标血量
            if (pTarget != null && pTarget.a != null && pTarget.a.data != null && pSelf != null && pSelf.a != null &&
                pSelf.a.stats != null)
            {
                // 智力值的随机倍数，范围在x到x之间
                float intelligenceMultiplierMin = 10f;
                float intelligenceMultiplierMax = 30f;
                float intelligenceMultiplier =
                    UnityEngine.Random.Range(intelligenceMultiplierMin, intelligenceMultiplierMax);

                float damageBasedOnIntelligence = pSelf.a.stats[S.intelligence] * intelligenceMultiplier; // 根据智力计算伤害
                int damageToDeal = Mathf.FloorToInt(damageBasedOnIntelligence);                           // 将浮点数伤害转换为整数

                pTarget.a.data.health -= damageToDeal; // 减去基于智力的伤害值

                if (pTarget.a.data.health <= 0)
                {
                    pTarget.a.data.alive = false;
                    AttackType attackTypeInstance = new AttackType();
                    (pTarget.a as Actor)?.killHimself(false, attackTypeInstance, true, true, true);
                }
            }

            return true;
        }

        public static bool intelligence_attack_Grade8(BaseSimObject pSelf, BaseSimObject pTarget,
                                                      WorldTile     pTile = null)
        {
            // 每次攻击计算自身智力值的随机倍数，再减少目标血量
            if (pTarget != null && pTarget.a != null && pTarget.a.data != null && pSelf != null && pSelf.a != null &&
                pSelf.a.stats != null)
            {
                // 智力值的随机倍数，范围在x到x之间
                float intelligenceMultiplierMin = 20f;
                float intelligenceMultiplierMax = 40f;
                float intelligenceMultiplier =
                    UnityEngine.Random.Range(intelligenceMultiplierMin, intelligenceMultiplierMax);

                float damageBasedOnIntelligence = pSelf.a.stats[S.intelligence] * intelligenceMultiplier; // 根据智力计算伤害
                int damageToDeal = Mathf.FloorToInt(damageBasedOnIntelligence);                           // 将浮点数伤害转换为整数

                pTarget.a.data.health -= damageToDeal; // 减去基于智力的伤害值

                if (pTarget.a.data.health <= 0)
                {
                    pTarget.a.data.alive = false;
                    AttackType attackTypeInstance = new AttackType();
                    (pTarget.a as Actor)?.killHimself(false, attackTypeInstance, true, true, true);
                }
            }

            return true;
        }

        public static bool intelligence_attack_Grade9(BaseSimObject pSelf, BaseSimObject pTarget,
                                                      WorldTile     pTile = null)
        {
            // 每次攻击计算自身智力值的随机倍数，再减少目标血量
            if (pTarget != null && pTarget.a != null && pTarget.a.data != null && pSelf != null && pSelf.a != null &&
                pSelf.a.stats != null)
            {
                // 智力值的随机倍数，范围在x到x之间
                float intelligenceMultiplierMin = 40f;
                float intelligenceMultiplierMax = 50f;
                float intelligenceMultiplier =
                    UnityEngine.Random.Range(intelligenceMultiplierMin, intelligenceMultiplierMax);

                float damageBasedOnIntelligence = pSelf.a.stats[S.intelligence] * intelligenceMultiplier; // 根据智力计算伤害
                int damageToDeal = Mathf.FloorToInt(damageBasedOnIntelligence);                           // 将浮点数伤害转换为整数

                pTarget.a.data.health -= damageToDeal; // 减去基于智力的伤害值

                if (pTarget.a.data.health <= 0)
                {
                    pTarget.a.data.alive = false;
                    AttackType attackTypeInstance = new AttackType();
                    (pTarget.a as Actor)?.killHimself(false, attackTypeInstance, true, true, true);
                }
            }

            return true;
        }

        public static bool intelligence_attack_Grade91(BaseSimObject pSelf, BaseSimObject pTarget,
                                                       WorldTile     pTile = null)
        {
            // 每次攻击计算自身智力值的随机倍数，再减少目标血量
            if (pTarget != null && pTarget.a != null && pTarget.a.data != null && pSelf != null && pSelf.a != null &&
                pSelf.a.stats != null)
            {
                // 智力值的随机倍数，范围在x到x之间
                float intelligenceMultiplierMin = 2000f;
                float intelligenceMultiplierMax = 3000f;
                float intelligenceMultiplier =
                    UnityEngine.Random.Range(intelligenceMultiplierMin, intelligenceMultiplierMax);

                float damageBasedOnIntelligence = pSelf.a.stats[S.intelligence] * intelligenceMultiplier; // 根据智力计算伤害
                int damageToDeal = Mathf.FloorToInt(damageBasedOnIntelligence);                           // 将浮点数伤害转换为整数

                pTarget.a.data.health -= damageToDeal; // 减去基于智力的伤害值

                if (pTarget.a.data.health <= 0)
                {
                    pTarget.a.data.alive = false;
                    AttackType attackTypeInstance = new AttackType();
                    (pTarget.a as Actor)?.killHimself(false, attackTypeInstance, true, true, true);
                }
            }

            return true;
        }

        public static bool Grade91_attackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (Toolbox.randomChance(0.1f))
            {
                ActionLibrary.castTornado(null, pTarget, null);
            }

            return true;
        }

        public static bool fountainhead3_action(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetBenYuan() <= 9)
            {
                return false;
            }


            a.addTrait("fountainhead3");
            a.removeTrait("fountainhead2");
            a.removeTrait("tumorInfection");
            a.removeTrait("cursed");
            a.removeTrait("infected");
            a.removeTrait("mushSpores");
            a.removeTrait("plague");
            a.removeTrait("madness");

            return true;
        }

        private static string GetRandomTrait(string[] additionalTraits)
        {
            return additionalTraits[UnityEngine.Random.Range(0, additionalTraits.Length)];
        }

        public static bool Grade0_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetYuanNeng() <= 2.99)
            {
                return false;
            }

            string[] forbiddenTraits =
            {
                "Grade01", "Grade02", "Grade1", "Grade2", "Grade3", "Grade4", "Grade5", "Grade6", "Grade7", "Grade8",
                "Grade9", "Grade91"
            };
            foreach (string trait in forbiddenTraits)
            {
                if (pTarget.a.hasTrait(trait))
                {
                    return false;
                }
            }

            upTrait("特质", "Grade0", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness" },
                new string[] { "特质" });

            return true;
        }

        private static string GetNewRandomTrait(string[] additionalTraits)
        {
            // 逻辑从 additionalTraits 中随机选择一个特质
            int randomIndex = UnityEngine.Random.Range(0, additionalTraits.Length);
            return additionalTraits[randomIndex];
        }

        public static bool Grade01_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetYuanNeng() <= 5.99)
            {
                return false;
            }

            string[] additionalTraits =
                { "sorcery01", "sorcery02", "sorcery03", "sorcery04", "sorcery05", "sorcery06" };
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质


            upTrait("特质", "Grade01", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade0" },
                new string[] { randomTrait });

            return true;
        }

        public static bool Grade02_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetYuanNeng() <= 8.99)
            {
                return false;
            }

            string[] additionalTraits =
                { "sorcery01", "sorcery02", "sorcery03", "sorcery04", "sorcery05", "sorcery06" };
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质


            upTrait("特质", "Grade02", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade01" },
                new string[] { randomTrait });

            return true;
        }

        public static bool Grade1_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetYuanNeng() <= 17.99)
            {
                return false;
            }

            a.ChangeYuanNeng(-1);
            double successRate = 0.9; //默认概率
            //根据天赋调整概率
            if (a.hasTrait("flair1"))
            {
                successRate = 0.05;
            }
            else if (a.hasTrait("flair2"))
            {
                successRate = 0.1;
            }
            else if (a.hasTrait("flair3"))
            {
                successRate = 0.3;
            }
            else if (a.hasTrait("flair4"))
            {
                successRate = 0.6;
            }
            else if (a.hasTrait("flair5"))
            {
                successRate = 0.9;
            }

            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f); //生成0到1之间的随机数
            if (randomValue > successRate)
            {
                return false;
            }

            string[] additionalTraits =
                { "sorcery11", "sorcery12", "sorcery13", "sorcery14", "sorcery15", "sorcery16" };
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质

            upTrait("特质", "Grade1", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade02" },
                new string[] { "freeze_proof", "fire_proof", randomTrait });

            return true;
        }

        public static bool Grade2_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetYuanNeng() <= 26.99)
            {
                return false;
            }

            string[] additionalTraits =
                { "sorcery11", "sorcery12", "sorcery13", "sorcery14", "sorcery15", "sorcery16" };
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质

            upTrait("Grade1", "Grade2", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade1" },
                new string[] { randomTrait });

            return true;
        }

        public static bool Grade3_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetYuanNeng() <= 35.99)
            {
                return false;
            }

            string[] additionalTraits =
                { "sorcery11", "sorcery12", "sorcery13", "sorcery14", "sorcery15", "sorcery16" };
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质

            upTrait("特质", "Grade3", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade2" },
                new string[] { randomTrait });


            return true;
        }

        public static bool Grade4_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetYuanNeng() <= 71.99)
            {
                return false;
            }

            a.ChangeYuanNeng(-2);
            double successRate = 0.9; //默认概率
            //根据天赋调整概率
            if (a.hasTrait("flair1"))
            {
                successRate = 0.5;
            }
            else if (a.hasTrait("flair2"))
            {
                successRate = 0.2;
            }
            else if (a.hasTrait("flair3"))
            {
                successRate = 0.2;
            }
            else if (a.hasTrait("flair4"))
            {
                successRate = 0.3;
            }
            else if (a.hasTrait("flair5"))
            {
                successRate = 0.4;
            }

            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f); //生成0到1之间的随机数
            if (randomValue > successRate)
            {
                return false;
            }

            string[] additionalTraits = { "sorcery22", "sorcery23", "sorcery24", "sorcery25", "sorcery26" };
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质

            upTrait("特质", "Grade4", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade3" },
                new string[] { randomTrait });

            return true;
        }

        public static bool Grade5_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetYuanNeng() <= 107.99)
            {
                return false;
            }

            string[] additionalTraits = { "sorcery22", "sorcery23", "sorcery24", "sorcery25", "sorcery26" };
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质

            upTrait("特质", "Grade5", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade4" },
                new string[] { randomTrait });

            return true;
        }

        public static bool Grade6_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetYuanNeng() <= 143.99)
            {
                return false;
            }

            string[] additionalTraits = { "sorcery22", "sorcery23", "sorcery24", "sorcery25", "sorcery26" };
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质

            upTrait("特质", "Grade6", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade5" },
                new string[] { randomTrait });

            return true;
        }

        public static bool Grade7_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetYuanNeng() <= 287.99)
            {
                return false;
            }

            a.ChangeYuanNeng(-3);
            double successRate = 0.9; //默认概率
            //根据天赋调整概率
            if (a.hasTrait("flair1"))
            {
                successRate = 0.6;
            }
            else if (a.hasTrait("flair2"))
            {
                successRate = 0.4;
            }
            else if (a.hasTrait("flair3"))
            {
                successRate = 0.25;
            }
            else if (a.hasTrait("flair4"))
            {
                successRate = 0.2;
            }
            else if (a.hasTrait("flair5"))
            {
                successRate = 0.3;
            }

            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f); //生成0到1之间的随机数
            if (randomValue > successRate)
            {
                return false;
            }

            if (!a.hasTrait("flair8") && !a.hasTrait("flair91"))
            {
                a.data.setName(pTarget.a.getName()+" 大巫师");
            }

            string[] additionalTraits = { "sorcery31", "sorcery32", "sorcery33", "sorcery34", "sorcery35" };
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质

            upTrait("特质", "Grade7", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade6" },
                new string[] { "添加升级外的特质", randomTrait });

            return true;
        }

        public static bool Grade8_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetYuanNeng() <= 519.99)
            {
                return false;
            }


            upTrait("特质", "Grade8", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade7" },
                new string[] { "添加升级外的特质" });

            return true;
        }

        public static bool Grade9_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetYuanNeng() <= 819.99)
            {
                return false;
            }
            if (!a.hasTrait("flair8") && !a.hasTrait("flair91"))
            {
                a.data.favorite = true;
                // 修改名称，将“大巫师”替换为“不灭”
                string currentName = pTarget.a.getName();
                string newName = currentName.Replace(" 大巫师", " 不灭");
                if (!currentName.EndsWith(" 大巫师"))
                {
                    newName += " 不灭";
                }
                a.data.setName(newName);
            }
            if (!a.hasTrait("flair91"))
            {
                if (!a.hasTrait("flair8"))
                {
                    a.addTrait("flair8");
                }
            }

            upTrait("特质", "Grade9", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade8" },
                new string[] { "添加升级外的特质" });

            return true;
        }
        public static bool Grade91_effectAction(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            if (a.GetYuanNeng() <= 2069.99)
            {
                return false;
            }

            // 初始化newName为当前名称
            string currentName = a.getName();
            string newName = currentName;

            // 检查名称中是否包含“不灭”，如果包含则替换为“始祖”
            if (currentName.Contains(" 不灭"))
            {
                newName = currentName.Replace(" 不灭", " 始祖");
                a.data.setName(newName);
            }
            // 随机选择一条提示信息
            System.Random random = new System.Random();
            int index = random.Next(grade91Tips.Count);
            string tip = grade91Tips[index];

            // 如果提示信息中包含占位符（比如 {0}），则替换为角色的名称
            if (tip.Contains("{0}"))
            {
                tip = string.Format(tip, newName);
            }

            // 显示随机选择的提示信息
            ActionLibrary.showWhisperTip(tip);

            upTrait("特质", "Grade91", a,
                new string[]
                {
                    "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade9", "sorcery31",
                    "sorcery32", "sorcery33", "sorcery34", "sorcery35", "flair8"

                },
                new string[] { "flair91" });

            return true;
        }
        // 定义一组升级提示信息
        private static readonly List<string> grade91Tips = new List<string>
        {
            "「命运之轮倾轧!终焉王庭第九柱石归位!\n虚空记录者低语「{0}」之名,混沌以太为其重塑王座!\n『祂自逆界回廊归来,持「无根之源」擎起新生冠冕!」",
            "「诸界源河倒卷!七百二十万星辰为其真名见证!\n永寂黑域中,「{0}」撕裂维度帷幕,以始祖法典宣告：「吾即永恒!」",
            "「古老者们在深渊睁开瞳孔!法则之网因祂而扭曲!\n「{0}」的第九真身踏碎湮灭之环,三千世界湮灭又重生!」",
            "「以太潮汐倒卷!虚空裂隙烙下「{0}」之真名!\n无根之源沸腾翻涌,其投影撕裂三千位面法则壁垒!",
            "「混沌退避!永恒星环为「{0}」加冕!\n第九真身自世界核心苏醒,维度锚点尽数崩裂!」",
            "「诸天法则重构!「{0}」踏碎时空长河逆流而来!\n此方世界已无法承载始祖真身亿万分之一重!」",
            "「星辰黯淡,万籁俱寂!「{0}」的第九真身,于虚空深渊中觉醒!\n无根之源的力量,撼动诸天万界,冠冕加身,永恒不朽!」",
            "「逆界之风,吹拂过湮灭的纪元!「{0}」以始祖之名,重塑混沌秩序!\n第九真身降临,万法归宗,宇宙为之震颤!」",
            "「深渊之下,古神低语!「{0}」踏破虚空,第九真身携无根之源,重塑世界法则!\n冠冕加冕,永恒之座,矗立于星海之巅!」",
            "「时空扭曲,维度崩塌!「{0}」的第九真身,自世界之外归来!\n无根之源沸腾,冠冕闪耀,诸界为之臣服!」",
            "「混沌初开,始祖降临!「{0}」以第九真身,踏破虚空枷锁!\n无根之源的力量,涌动于每一寸宇宙,冠冕加身,永恒统治!」",
            "「星辰逆转,法则重塑!「{0}」的第九真身,于虚空之中显现!\n无根之源沸腾,冠冕璀璨,万界为之震颤,永恒之名,响彻宇宙!」",
            "「深渊咆哮,始祖觉醒!「{0}」以第九真身,撕裂混沌帷幕!\n无根之源的力量,涌动于诸天万界,冠冕加冕,永恒不朽的誓言!」"
        };

        public static bool flair8_death(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            string entityName = pTarget.a.getName();
            //检查是否存在该名字的复活计数，如果不存在则初始化为1
            if (!_reviveCounts.TryGetValue(entityName, out int reviveCount))
            {
                _reviveCounts[entityName] = 1;
            }

            if (_reviveCounts[entityName] >= 15) //复活次数是否已达到限制
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            a.removeTrait("tumorInfection");
            a.removeTrait("cursed");
            a.removeTrait("infected");
            a.removeTrait("mushSpores");
            a.removeTrait("plague");
            var act = World.world.units.createNewUnit(a.asset.id, pTile, 0f);
            ActorTool.copyUnitToOtherUnit(a, act);
            act.data.setName(pTarget.a.getName());
            act.data.traits = new List<string>() { "flair8" };
            act.data.health = 999;
            act.data.created_time = World.world.getCreationTime();
            act.data.age_overgrowth = 18;
            act.data.setName(entityName);
            teleportRandom(act);

            if (reviveCount < 15) //如果复活次数未达到限制，则添加flair8
            {
                act.data.traits = new List<string>() { "flair8" };
            }

            _reviveCounts[entityName] = reviveCount + 1; //增加该名字的复活次数计数器

            PowerLibrary pb = new PowerLibrary();
            pb.divineLightFX(pTarget.a.currentTile, null);

            return true;
        }
        public static bool flair91_death(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null)
            {
                return false;
            }

            string entityName = pTarget.a.getName();
            //检查是否存在该名字的复活计数，如果不存在则初始化为0
            if (!_reviveCounts.TryGetValue(entityName, out int reviveCount))
            {
                _reviveCounts[entityName] = 1;
            }

            if (_reviveCounts[entityName] >= 100) //复活次数是否已达到限制
            {
                return false;
            }

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            a.removeTrait("tumorInfection");
            a.removeTrait("cursed");
            a.removeTrait("infected");
            a.removeTrait("mushSpores");
            a.removeTrait("plague");
            var act = World.world.units.createNewUnit(a.asset.id, pTile, 0f);
            ActorTool.copyUnitToOtherUnit(a, act);
            act.data.setName(pTarget.a.getName());
            act.data.traits = new List<string>() { "flair91", "flair5" };
            act.data.health = 999;
            act.data.created_time = World.world.getCreationTime();
            act.data.age_overgrowth = 18;
            act.data.setName(entityName);
            teleportRandom(act);

            if (reviveCount < 100) //如果复活次数未达到限制，则添加flair91
            {
                act.data.traits = new List<string>() { "flair91", "flair5" };
            }

            _reviveCounts[entityName] = reviveCount + 1; //增加该名字的复活次数计数器

            PowerLibrary pb = new PowerLibrary();
            pb.divineLightFX(pTarget.a.currentTile, null);

            return true;
        }

        public static bool teleportRandom(Actor a)
        {
            MapBox mapBox = World.world as MapBox;
            if (mapBox == null)
            {
                return false;
            }

            CitiesManager citiesManager = mapBox.list_base_managers.OfType<CitiesManager>().FirstOrDefault();
            if (citiesManager == null)
            {
                return false;
            }

            List<City> cities = citiesManager.list;
            if (cities.Count == 0)
            {
                return false;
            }

            System.Random random = new System.Random();
            int randomIndex = random.Next(cities.Count);
            City randomCity = cities[randomIndex];

            WorldTile cityCenterTile = randomCity.getTile();
            if (cityCenterTile == null || cityCenterTile.Type.block || !cityCenterTile.Type.ground)
            {
                return false;
            }

            a.cancelAllBeh(null);
            a.spawnOn(cityCenterTile, 0f);
            return true;
        }

        public static bool flair8_Traits(BaseSimObject pTarget, WorldTile pTile = null)
        {
            Actor a = pTarget.a;
            string[] possibleTraits = { "flair3", "flair4", "flair5" };
            System.Random random = new System.Random();
            string traitToAdd = possibleTraits[random.Next(possibleTraits.Length)];
            bool hasAnyTrait = possibleTraits.Any(t => a.hasTrait(t));
            if (!hasAnyTrait)
            {
                a.addTrait(traitToAdd);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="old_trait">升级前的特质</param>
        /// <param name="new_trait">升级到的特质</param>
        /// <param name="actor">单位传入</param>
        /// <param name="other_Oldtraits">升级要删掉的特质(不包括升级前的主特质)</param>
        /// <param name="other_newTrait">升级后要伴随添加的特质(不包含主特质)</param>
        /// <returns></returns>
        public static bool upTrait(string   old_trait, string new_trait, Actor actor, string[] other_Oldtraits = null,
                                   string[] other_newTrait = null)
        {
            if (actor == null)
            {
                return false;
            }

            foreach (string VARIABLE in other_newTrait)
            {
                actor.addTrait(VARIABLE);
            }

            foreach (var VARIABLE in other_Oldtraits)
            {
                actor.removeTrait(VARIABLE);
            }

            actor.addTrait(new_trait);
            actor.removeTrait(old_trait);
            return true;
        }
    }
}