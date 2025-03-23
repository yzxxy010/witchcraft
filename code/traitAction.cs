using System.Collections.Generic;
using System.Linq;
using System;
using ai;
using ReflectionUtility;
using UnityEngine;
using VideoCopilot.code.utils;
using System.IO;

namespace VideoCopilot.code
{
    internal class traitAction
    {
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

        public static bool attack_sorcery07(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
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
                pSelf.a.addStatusEffect("Ring06", 3f); //零环•鹰隼凝视
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

        public static bool attack_sorcery17(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
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
                pSelf.a.addStatusEffect("Ring16", 6f); //一环•气机牵引
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
            pTarget.a.spawnParticle(Toolbox.makeColor("#FF0000"));
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

        public static bool attack_sorcery27(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
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
                pSelf.a.addStatusEffect("Ring26", 8f); //二环•因果印记
            }

            return true;
        }

        public static bool attack_sorcery28(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
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
                pSelf.a.addStatusEffect("Ring27", 8f); //二环•相位星痕步
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

        public static bool Grade91_Attack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if ((float)pSelf.base_data.health < (float)pSelf.getMaxHealth() * 0.8f)
            {
                pSelf.a.addStatusEffect("Ring92", 30f);// 检查血量是否低于80%
            }
            if ((float)pSelf.base_data.health < (float)pSelf.getMaxHealth() * 0.5f)
            {
                pSelf.a.addStatusEffect("Ring93", 30f);// 检查血量是否低于80%
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
            if (Toolbox.randomChance(0.2f))
            {
                EffectsLibrary.spawn("fx_meteorite", pTile, "meteorite", null, 0f, -1f, -1f);
            }
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
        public static bool Grade0_Regen(BaseSimObject pTarget, WorldTile pTile = null)
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
                pTarget.a.restoreHealth(2);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }
        public static bool Grade01_Regen(BaseSimObject pTarget, WorldTile pTile = null)
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
                pTarget.a.restoreHealth(6);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }
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
                pTarget.a.restoreHealth(10);
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
                pTarget.a.restoreHealth(20);
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
                pTarget.a.restoreHealth(30);
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
                pTarget.a.restoreHealth(40);
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
                pTarget.a.restoreHealth(220);
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
                pTarget.a.restoreHealth(420);
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
                pTarget.a.restoreHealth(620);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool Grade7_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(3000);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool Grade8_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(7000);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool Grade9_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(30000);
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
                pTarget.a.restoreHealth(90);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }

        public static bool flair91_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(180);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }
        
        public static bool flair92_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(10);
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
        
        public static bool Grade91_attackAction(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (Toolbox.randomChance(0.1f))
            {
                ActionLibrary.castTornado(null, pTarget, null);
            }

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
            string actorName = a.getName();
            if (!actorName.Contains("学徒") && !actorName.Contains("正式") && !actorName.Contains("高级") && !actorName.Contains("大巫师") && !actorName.Contains("不灭") && !actorName.Contains("始祖") && !a.hasTrait("flair8") && !a.hasTrait("flair91"))
            {
                a.data.setName(pTarget.a.getName()+" 学徒");
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
                { "sorcery01", "sorcery02", "sorcery03", "sorcery04", "sorcery05", "sorcery06", "sorcery07" };
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
                { "sorcery01", "sorcery02", "sorcery03", "sorcery04", "sorcery05", "sorcery06", "sorcery07" };
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
            double successRate = 0.6; //默认概率
            //根据天赋调整概率
            string currentName = a.getName();
            if (a.hasTrait("flair91"))
            {
                successRate = 0.9;
            }
            else if (a.hasTrait("flair92"))
            {
                if (currentName.Contains(" 始祖"))
                {
                    successRate = 0.9;
                }
                if (currentName.Contains(" 不灭"))
                {
                    successRate = 0.8;
                }
            }
            else if (a.hasTrait("flair8"))
            {
                successRate = 0.8;
            }
            else if (a.hasTrait("flair1"))
            {
                successRate = 0.01;
            }
            else if (a.hasTrait("flair2"))
            {
                successRate = 0.1;
            }
            else if (a.hasTrait("flair3"))
            {
                successRate = 0.2;
            }
            else if (a.hasTrait("flair4"))
            {
                successRate = 0.4;
            }
            else if (a.hasTrait("flair5"))
            {
                successRate = 0.6;
            }

            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f); //生成0到1之间的随机数
            if (randomValue > successRate)
            {
                return false;
            }

            // 初始化newName为当前名称
            string newName = currentName;
            // 检查名称中是否包含“学徒”，如果包含则替换为“正式”
            if (currentName.Contains(" 学徒"))
            {
                newName = currentName.Replace(" 学徒", " 正式");
                a.data.setName(newName);
            }

            string[] additionalTraits =
                { "sorcery11", "sorcery12", "sorcery13", "sorcery14", "sorcery15", "sorcery16", "sorcery17" };
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
            if (a.GetYuanNeng() <= 29.99)
            {
                return false;
            }
            a.ChangeYuanNeng(-2);

            string[] additionalTraits =
                { "sorcery11", "sorcery12", "sorcery13", "sorcery14", "sorcery15", "sorcery16", "sorcery17" };
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
            if (a.GetYuanNeng() <= 44.99)
            {
                return false;
            }
            a.ChangeYuanNeng(-3);

            string[] additionalTraits =
                { "sorcery11", "sorcery12", "sorcery13", "sorcery14", "sorcery15", "sorcery16", "sorcery17" };
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
            if (a.GetYuanNeng() <= 69.99)
            {
                return false;
            }
            a.ChangeYuanNeng(-6);
            double successRate = 0.5; //默认概率
            //根据天赋调整概率
            string currentName = a.getName();
            if (a.hasTrait("flair91"))
            {
                successRate = 0.8;
            }
            else if (a.hasTrait("flair92"))
            {
                if (currentName.Contains(" 始祖"))
                {
                    successRate = 0.8;
                }
                if (currentName.Contains(" 不灭"))
                {
                    successRate = 0.7;
                }
            }
            else if (a.hasTrait("flair8"))
            {
                successRate = 0.7;
            }
            else if (a.hasTrait("flair1"))
            {
                successRate = 0.001;
            }
            else if (a.hasTrait("flair2"))
            {
                successRate = 0.01;
            }
            else if (a.hasTrait("flair3"))
            {
                successRate = 0.1;
            }
            else if (a.hasTrait("flair4"))
            {
                successRate = 0.3;
            }
            else if (a.hasTrait("flair5"))
            {
                successRate = 0.5;
            }

            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f); //生成0到1之间的随机数
            if (randomValue > successRate)
            {
                return false;
            }
            // 初始化newName为当前名称
            string newName = currentName;
            // 检查名称中是否包含“正式”，如果包含则替换为“高级”
            if (currentName.Contains(" 正式"))
            {
                newName = currentName.Replace(" 正式", " 高级");
                a.data.setName(newName);
            }

            string[] additionalTraits = { "sorcery22", "sorcery23", "sorcery24", "sorcery25", "sorcery26", "sorcery27", "sorcery28" };
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
            if (a.GetYuanNeng() <= 139.99)
            {
                return false;
            }
            a.ChangeYuanNeng(-10);

            string[] additionalTraits = { "sorcery22", "sorcery23", "sorcery24", "sorcery25", "sorcery26", "sorcery27", "sorcery28" };
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
            if (a.GetYuanNeng() <= 209.99)
            {
                return false;
            }
            a.ChangeYuanNeng(-20);

            string[] additionalTraits = { "sorcery22", "sorcery23", "sorcery24", "sorcery25", "sorcery26", "sorcery27", "sorcery28" };
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
            a.ChangeYuanNeng(-30);
            double successRate = 0.2; //默认概率 
            //根据天赋调整概率
            string currentName = a.getName();
            if (a.hasTrait("flair91"))
            {
                successRate = 0.5;
            }
            else if (a.hasTrait("flair92"))
            {
                if (currentName.Contains(" 始祖"))
                {
                    successRate = 0.5;
                }
                if (currentName.Contains(" 不灭"))
                {
                    successRate = 0.4;
                }
            }
            else if (a.hasTrait("flair8"))
            {
                successRate = 0.4;
            }
            else if (a.hasTrait("flair1"))
            {
                successRate = 0.001;
            }
            else if (a.hasTrait("flair2"))
            {
                successRate = 0.005;
            }
            else if (a.hasTrait("flair3"))
            {
                successRate = 0.01;
            }
            else if (a.hasTrait("flair4"))
            {
                successRate = 0.05;
            }
            else if (a.hasTrait("flair5"))
            {
                successRate = 0.2;
            }
            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f); //生成0到1之间的随机数
            if (randomValue > successRate)
            {
                return false;
            }
            // 初始化newName为当前名称
            string newName = currentName;

            // 检查名称中是否包含“高级”，如果包含则替换为“不灭”
            if (currentName.Contains(" 高级"))
            {
                newName = currentName.Replace(" 高级", " 大巫师");
                a.data.setName(newName);
            }

            string[] sorceryTraits = { "sorcery31", "sorcery32", "sorcery33", "sorcery34", "sorcery35" };
            string[] meditationTraits = { "meditation1", "meditation2", "meditation3" };
            string randomSorceryTrait = GetNewRandomTrait(sorceryTraits);
            bool hasMeditationTrait = meditationTraits.Any(trait => a.hasTrait(trait));
            string randomMeditationTrait = "";
            if (!hasMeditationTrait)
            {
                randomMeditationTrait = GetNewRandomTrait(meditationTraits); //获取新随机特质
            }

            upTrait("特质", "Grade7", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade6", "flair1", "flair2", "flair3", "flair4", "flair5", "flair6", "flair7" },
                new string[] { "添加升级外的特质", randomSorceryTrait, randomMeditationTrait });

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
            if (a.GetMeditation() <= 199.99)//突破需求
            {
                return false;
            }
            a.ChangeMeditation(-30);//突破消耗
            double successRate = 0.1; //突破默认概率
            string currentName = a.getName();
            if (a.hasTrait("flair91"))
            {
                successRate = 0.5;
            }
            else if (a.hasTrait("flair92"))
            {
                if (currentName.Contains(" 始祖"))
                {
                    successRate = 0.5;
                }
                if (currentName.Contains(" 不灭"))
                {
                    successRate = 0.4;
                }
            }
            else if (a.hasTrait("flair8"))
            {
                successRate = 0.4;
            }
            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f); //生成0到1之间的随机数
            if (randomValue > successRate)
            {
                return false;
            }


            upTrait("特质", "Grade8", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade7", "flair81" },
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
            if (a.GetMeditation() <= 379.99)//突破需求
            {
                return false;
            }
            a.ChangeMeditation(-50);//突破消耗
            double successRate = 0.1; //突破默认概率
            string currentName = a.getName();
            if (a.hasTrait("flair91"))
            {
                successRate = 0.35;
            }
            else if (a.hasTrait("flair92"))
            {
                if (currentName.Contains(" 始祖"))
                {
                    successRate = 0.35;
                }
                if (currentName.Contains(" 不灭"))
                {
                    successRate = 0.3;
                }
            }
            else if (a.hasTrait("flair8"))
            {
                successRate = 0.3;
            }
            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f); //生成0到1之间的随机数
            if (randomValue > successRate)
            {
                return false;
            }
            // 初始化newName为当前名称
            string newName = currentName;
            // 检查名称中是否包含“大巫师”，如果包含则替换为“不灭”
            if (currentName.Contains(" 大巫师"))
            {
                a.data.favorite = true;
                newName = currentName.Replace(" 大巫师", " 不灭");
                a.data.setName(newName);
            }
            if (!a.hasTrait("flair91") && !a.hasTrait("flair92"))
            {
                if (!a.hasTrait("flair8"))
                {
                    a.addTrait("flair8");
                    a.ChangeResurrection(15);
                }
            }
            PlayWavDirectly.Instance.PlaySoundFromFile(VideoCopilotClass.modDeclare.FolderPath + @"\code\Sound\Sound_1.wav");

            // 随机选择一条提示信息
            System.Random random = new System.Random();
            int index = random.Next(grade9Tips.Count);
            string tip = grade9Tips[index];

            // 如果提示信息中包含占位符（比如 {0}），则替换为角色的名称
            if (tip.Contains("{0}"))
            {
                tip = string.Format(tip, newName);
            }

            // 显示随机选择的提示信息
            ActionLibrary.showWhisperTip(tip);

            upTrait("特质", "Grade9", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade8", "talent7", "flair81" },
                new string[] { "添加升级外的特质" });

            return true;
        }
        private static readonly List<string> grade9Tips = new List<string>
        {
            "「轮回之渊震颤！星海魂火为「{0}」重燃！\n不灭真灵穿透三千湮灭劫波，其名已镌刻于永恒碑界————\n『纵使法则崩殒，吾魂仍将踏碎终焉之路！』」",
            "「诸界法则轰鸣！虚空回响九万次魂裂之音！\n「{0}」携破碎冠冕立于星骸之巅，向始祖王座发出第七次叩问————\n『此身不烬，此誓不渝！』",
            "「时空裂隙中亮起亘古魂焰！维度壁垒烙印「{0}」之真印！\n混沌观测者见证：其灵历经七百二十次寂灭，仍执剑劈开归墟长夜！」",
            "「星海潮汐逆流！永劫试炼场第九万阶被踏碎！\n「{0}」撕开裂魂风暴长啸：「纵无冠冕加身，此魂亦当焚尽诸天桎梏！」",
            "「深渊回廊震荡！不朽魂核迸发第七重涅槃辉光！\n「{0}」以破碎王座为基，在湮灭长河中重构三千次登神阶梯！」",
            "「诸界观测塔震颤！灵魂波长突破永恒阈值！\n「{0}」的涅槃烙印照亮维度裂隙，虚空回响着第卅六次冲冠宣言————\n『此魂不熄，此道不绝！』」"
        };
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
            if (a.GetMeditation() <= 999.99)
            {
                return false;
            }

            a.ChangeMeditation(-700);
            double successRate = 0.1; //突破默认概率
            double randomValue = UnityEngine.Random.Range(0.0f, 1.0f); //生成0到1之间的随机数
            if (randomValue > successRate)
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
            if (!a.hasTrait("flair92"))
            {
                if (!a.hasTrait("flair91"))
                {
                    a.addTrait("flair91");
                    a.ChangeResurrection(100);
                }
            }
            PlayWavDirectly.Instance.PlaySoundFromFile(VideoCopilotClass.modDeclare.FolderPath + @"\code\Sound\Sound_1.wav");
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
                    "sorcery32", "sorcery33", "sorcery34", "sorcery35", "flair8", "flair81"

                },
                new string[] { "特质" });

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
            "「逆界之风,吹拂过湮灭的纪元!「{0}」以始祖之名,重塑混沌秩序!」",
            "「星海震颤！诸天法则为之改写！混沌退避，万界共鸣。\n「{0}」已铸就第九真身，其名即是无根之源！\n永恒之冠冕加诸星海，始祖真身投影降临现世！」",
            "「时空扭曲,维度崩塌!「{0}」的第九真身,自世界之外归来!\n无根之源沸腾,冠冕闪耀,诸界为之臣服!」"
        };

        public static bool flair8_death(BaseSimObject pTarget, WorldTile pTile = null)
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
            int currentRresurrectionCount = (int)a.GetRresurrection();
            int currentResurrectionCount = (int)a.GetResurrection();
            // 检查GetResurrection值，如果为1则不执行复活
            if (a.GetResurrection() <= 1)
            {
                return false;
            }

            a.removeTrait("tumorInfection");
            a.removeTrait("cursed");
            a.removeTrait("infected");
            a.removeTrait("mushSpores");
            a.removeTrait("plague");
            var meditationTraits = new[] { "meditation1", "meditation2", "meditation3" };// 定义要检查的 meditation 特质列表
            var traitsToAdd = new List<string> { "flair8" };// 创建一个新的列表来存储新对象将要添加的特质
            foreach (var trait in meditationTraits)
            {
                if (a.hasTrait(trait))
                {
                    traitsToAdd.Add(trait);// 检查目标对象是否具有 meditation 特质，并将其添加到新列表中
                }
            }
            string[] flairTraits = { "flair3", "flair4", "flair5" };
            int randomIndex = UnityEngine.Random.Range(0, flairTraits.Length);
            traitsToAdd.Add(flairTraits[randomIndex]);
            var act = World.world.units.createNewUnit(a.asset.id, pTile, 0f);
            ActorTool.copyUnitToOtherUnit(a, act);
            act.data.setName(pTarget.a.getName());
            act.data.traits = traitsToAdd;
            act.data.health = 999;
            act.data.created_time = World.world.getCreationTime();
            act.data.age_overgrowth = 8;
            teleportRandom(act);
            act.ChangeResurrection(currentResurrectionCount - 1);
            act.ChangeRresurrection(currentRresurrectionCount + 1);

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

            if (!pTarget.isActor())
            {
                return false;
            }

            Actor a = pTarget.a;
            int currentRresurrectionCount = (int)a.GetRresurrection();
            int currentResurrectionCount = (int)a.GetResurrection();
            // 检查GetResurrection值，如果为1则不执行复活
            if (a.GetResurrection() <= 1)
            {
                return false;
            }

            a.removeTrait("tumorInfection");
            a.removeTrait("cursed");
            a.removeTrait("infected");
            a.removeTrait("mushSpores");
            a.removeTrait("plague");
            var meditationTraits = new[] { "meditation1", "meditation2", "meditation3" }; // 定义要检查的 meditation 特质列表
            var traitsToAdd = new List<string> { "flair91", "flair5" }; // 创建一个新的列表来存储新对象将要添加的特质，初始包含 "flair8"
            foreach (var trait in meditationTraits)
            {
                if (a.hasTrait(trait))
                {
                    traitsToAdd.Add(trait); // 检查目标对象是否具有 meditation 特质，并将其添加到新列表中
                }
            }
            var act = World.world.units.createNewUnit(a.asset.id, pTile, 0f);
            ActorTool.copyUnitToOtherUnit(a, act);
            act.data.setName(pTarget.a.getName());
            act.data.traits = traitsToAdd;
            act.data.health = 999;
            act.data.created_time = World.world.getCreationTime();
            act.data.age_overgrowth = 8;
            teleportRandom(act);
            act.ChangeResurrection(currentResurrectionCount - 1);
            act.ChangeRresurrection(currentRresurrectionCount + 1);

            PowerLibrary pb = new PowerLibrary();
            pb.divineLightFX(pTarget.a.currentTile, null);

            return true;
        }
        public static bool flair92_death(BaseSimObject pTarget, WorldTile pTile = null)
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
            int currentRresurrectionCount = (int)a.GetRresurrection();

            a.removeTrait("tumorInfection");
            a.removeTrait("cursed");
            a.removeTrait("infected");
            a.removeTrait("mushSpores");
            a.removeTrait("plague");
            var meditationTraits = new[] { "meditation1", "meditation2", "meditation3" }; // 定义要检查的 meditation 特质列表
            var traitsToAdd = new List<string> { "flair92" };// 创建一个新的列表来存储新对象将要添加的特质
            foreach (var trait in meditationTraits)
            {
                if (a.hasTrait(trait))
                {
                    traitsToAdd.Add(trait); // 检查目标对象是否具有 meditation 特质，并将其添加到新列表中
                }
            }
            string[] flairTraits = { "flair1", "flair2", "flair3", "flair4", "flair5", "flair6", "talent1", "talent2", "talent3", "talent4", "blessed", "flower_prints" };
            int randomIndex = UnityEngine.Random.Range(0, flairTraits.Length);
            traitsToAdd.Add(flairTraits[randomIndex]);
            var act = World.world.units.createNewUnit(a.asset.id, pTile, 0f);
            ActorTool.copyUnitToOtherUnit(a, act);
            string newName = pTarget.a.getName();// 设置名字并检查是否包含"2S"
            if (newName.Contains("2S"))
            {
                newName = newName.Replace("2S", ""); // 删除"2S"
            }
            act.data.setName(newName);
            act.data.traits = traitsToAdd;
            act.data.health = 999;
            act.data.created_time = World.world.getCreationTime();
            act.data.age_overgrowth = 8;
            teleportRandom(act);
            act.ChangeRresurrection(currentRresurrectionCount + 1);

            PowerLibrary pb = new PowerLibrary();
            pb.divineLightFX(pTarget.a.currentTile, null);

            return true;
        }
        public static bool Grade91_death(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget == null || !pTarget.isActor())
            {
                return false;
            }
            Actor a = pTarget.a;
            Grade91_Action(a);
            return true;
        }
        private static bool Grade91_Action(Actor a)
        {
            // 首先计算所有存活的小人总数
            int totalAliveCount = 0;
            List<Actor> simpleList = World.world.units.getSimpleList();
            foreach (Actor actor in simpleList)
            {
                if (actor.isAlive())
                {
                    totalAliveCount++;
                    // 所有存活且有相应特质的增加源能和无根之源
                    if (actor.hasTrait("flair1") || actor.hasTrait("flair2") || actor.hasTrait("flair3") ||
                        actor.hasTrait("flair4") || actor.hasTrait("flair5") || actor.hasTrait("flair6") || actor.hasTrait("flair7"))
                    {
                        // 随机增加50~100源能
                        int yuanNengIncrease = UnityEngine.Random.Range(50, 101);
                        actor.ChangeYuanNeng(yuanNengIncrease);
                    }
                    if (actor.hasTrait("meditation1") || actor.hasTrait("meditation2") || actor.hasTrait("meditation3"))
                    {
                        // 随机增加500~1000无根之源
                        int meditationIncrease = UnityEngine.Random.Range(500, 1001);
                        actor.ChangeMeditation(meditationIncrease);
                    }
                }
            }
            
            int num = 0;
            foreach (Actor actor in simpleList)
            {
                if (actor.isAlive() && !actor.data.favorite && !actor.asset.ignoredByInfinityCoin)
                {
                    num++;
                }
            }

            int num2 = (int)Math.Ceiling(num * 0.30); // 计算30%的小人数量
            int num3 = 0;
            foreach (City city in World.world.cities.list)
            {
                num3 += city.killHalfPopPoints();
            }

            EffectInfinityCoin.temp_list.Clear();
            EffectInfinityCoin.temp_list.AddRange(World.world.units);
            EffectInfinityCoin.temp_list.Shuffle<Actor>();
            foreach (Actor actor2 in EffectInfinityCoin.temp_list)
            {
                if (num2 == 0)
                {
                    break;
                }
                if (actor2.isAlive() && !actor2.data.favorite && !actor2.asset.ignoredByInfinityCoin)
                {
                    num3++;
                    num2--;
                    actor2.getHit((float)(actor2.data.health * 1000 + 1), true, AttackType.Other, null, false, false);
                }
            }

            // 初始化newName为当前名称
            string currentName = a.getName();
            string newName = currentName;

            // 随机选择一条提示信息
            System.Random random = new System.Random();
            int index = random.Next(Grade91deathTips.Count);
            string tip = Grade91deathTips[index];

            // 如果提示信息中包含占位符（比如 {0}），则替换为角色的名称
            if (tip.Contains("{0}"))
            {
                tip = string.Format(tip, newName);
            }

            // 显示随机选择的提示信息
            ActionLibrary.showWhisperTip(tip);
            return true;
        }

        private static readonly List<string> Grade91deathTips = new List<string>
        {
            "「告死星轨贯穿苍穹！「{0}」真身崩解于血狱回廊！\n——时空长河冻结其陨落刹那，三千世界剥离始祖真名！",
            "「星海哀鸣！「{0}」冠冕碎裂于混沌战争残响！\n——光之祖陨落余烬中，唯留无根之源刻印轮回坐标！」",
            "「维度墓碑降临！「{0}」永寂于被污染的始祖王座！\n——然其真灵已遁入第七混沌纪，万劫后当重燃战旗！」",
            "「终末之钟响彻万渊！以太基石在「{0}」脚下崩裂！\n——群星为其举行葬仪，深渊遗骸化作第七十三柱混沌丰碑！『然真灵坠入湮灭洪流，静待逆时针的荣光…」",
            "「永夜尖塔倾塌！时空经纬线在「{0}」陨落处断裂！\n——九重奏哀歌于深渊回响，血月沉沦处唯留真名烙印！『无根之源将铭记此次轮回溃灭』」",
            "「群鸦衔来腐朽冠冕！万界之血在「{0}」陨星下凝固！\n——虚空档案官用灰烬撰写：「第十九纪元末，又一位永恒者堕入重生之涡」『混沌终焉之战再添残响』」"
        };
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
        public static bool flair6_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(30);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }
        public static bool SS_Collection(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a != null)
            {
                string actorName = pTarget.a.getName();
                if (!actorName.Contains("2S"))
                {
                    if (!actorTipsShown.ContainsKey(pTarget.a))
                    {
                        actorTipsShown[pTarget.a] = (false, false); // 初始化状态
                    }

                    var tipsShown = actorTipsShown[pTarget.a];
                    if (!tipsShown.hasShownSSTip)
                    {
                        pTarget.a.data.favorite = true;
                        PlayWavDirectly.Instance.PlaySoundFromFile(VideoCopilotClass.modDeclare.FolderPath + @"\code\Sound\Sound_1.wav");
                        ActionLibrary.showWhisperTip("叮,SS出世");
                        pTarget.a.data.setName(pTarget.a.getName() + " 2S");
                        actorTipsShown[pTarget.a] = (true, tipsShown.hasShownSSSTip); // 更新状态
                    }
                    return false;
                }
            }
            return true;
        }
        public static bool flair7_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(60);
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }
        public static bool SSS_Collection(BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget.a != null)
            {
                string actorName = pTarget.a.getName();
                if (!actorName.Contains("3S"))
                {
                    if (!actorTipsShown.ContainsKey(pTarget.a))
                    {
                        actorTipsShown[pTarget.a] = (false, false); // 初始化状态
                    }

                    var tipsShown = actorTipsShown[pTarget.a];
                    if (!tipsShown.hasShownSSSTip)
                    {
                        pTarget.a.data.favorite = true;
                        PlayWavDirectly.Instance.PlaySoundFromFile(VideoCopilotClass.modDeclare.FolderPath + @"\code\Sound\Sound_1.wav");
                        ActionLibrary.showWhisperTip("叮,SSS出世");
                        pTarget.a.data.setName(pTarget.a.getName() + " 3S");
                        actorTipsShown[pTarget.a] = (tipsShown.hasShownSSTip, true); // 更新状态
                        return true;
                    }
                    return false;
                }
            }
            return false;
        }
        private static Dictionary<Actor, (bool hasShownSSTip, bool hasShownSSSTip)> actorTipsShown = new Dictionary<Actor, (bool, bool)>();
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