using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ai;
using UnityEngine;
using ReflectionUtility;

namespace VideoCopilot.code
{
    internal class traitAction
    {
        //以下为拥有这个巫术状态的效果
        public static bool attack_Ring05(BaseSimObject pTarget, WorldTile pTile = null)
		{
            float pDamage = 10f;// 每次受到的伤害值
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
            float pDamage = 900f;// 每次受到的伤害值
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
             var invalidTraits = new HashSet<string> { "Grade1", "Grade2", "Grade3", "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }
            if(Toolbox.randomChance(1f))
            {
                pSelf.a.addStatusEffect("Ring01", 3f);//零环•轻身术
            }
            return true;
        }
        public static bool attack_sorcery02(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
             var invalidTraits = new HashSet<string> { "Grade1", "Grade2", "Grade3", "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if(Toolbox.randomChance(1f))
            {
                pTarget.a.addStatusEffect("Ring05", 3f);//零环•烈焰之握
            }
            return true;
        }
        public static bool attack_sorcery03(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
             var invalidTraits = new HashSet<string> { "Grade1", "Grade2", "Grade3", "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if(Toolbox.randomChance(1f))
            {
                pTarget.a.addStatusEffect("Ring02", 3f);//零环•水幻迷障
            }
            return true;
        }
        public static bool sorcery04_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {

            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(2);//零环•生命祈愈
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }
        public static bool attack_sorcery05(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
             var invalidTraits = new HashSet<string> { "Grade1", "Grade2", "Grade3", "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }
            if(Toolbox.randomChance(1f))
            {
                pSelf.a.addStatusEffect("Ring03", 3f);//零环•大地壁垒
            }
            return true;
        }
        public static bool attack_sorcery06(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
             var invalidTraits = new HashSet<string> { "Grade1", "Grade2", "Grade3", "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if(Toolbox.randomChance(0.8f))
            {
                pTarget.a.addStatusEffect("Ring04", 3f);//零环•蔓藤囚牢
            }
            return true;
        }
        public static bool attack_sorcery11(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
             var invalidTraits = new HashSet<string> { "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if(Toolbox.randomChance(0.7f))
            {
                pTarget.a.addStatusEffect("Ring11", 6f);//一环•疲惫之手
            }

            return true;
        }
        public static bool attack_sorcery12(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
             var invalidTraits = new HashSet<string> { "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if(Toolbox.randomChance(0.8f))
            {
                pTarget.a.addStatusEffect("Ring12", 6f);//一环•缠绕之网
            }

            return true;
        }
        public static bool attack_sorcery13(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
             var invalidTraits = new HashSet<string> { "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if(Toolbox.randomChance(0.8f))
            {
                pSelf.a.addStatusEffect("Ring13", 6f);//一环•土之坚甲
            }
            return true;
        }
        public static bool sorcery14_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {

            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(23);//一环•复苏之流
                pTarget.a.spawnParticle(Toolbox.color_heal);
            }

            return true;
        }
        public static bool attack_sorcery15(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
             var invalidTraits = new HashSet<string> { "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }
            
            if(Toolbox.randomChance(0.8f))
            {
                pSelf.a.addStatusEffect("Ring14", 6f);//一环•风行术
            }
            return true;
        }
        public static bool attack_sorcery16(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 定义一个包含无效特质的HashSet
             var invalidTraits = new HashSet<string> { "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91" };
            // 检查pTarget和pTarget.a是否不为null，并且pTarget.a是否具有无效特质中的任何一个
            if (pTarget != null && pTarget.a != null && invalidTraits.Any(trait => pTarget.a.hasTrait(trait)))
            {
                return false; // 如果具有无效特质，则返回false
            }

            if (pTarget.isBuilding())
            {
                return false;
            }

            if(Toolbox.randomChance(0.8f))
            {
                pTarget.a.addStatusEffect("Ring15", 6f);//一环•水雾术
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

            if(Toolbox.randomChance(0.8f))
            {
                pTarget.a.addStatusEffect("Ring22", 8f);//二环•星之致幻
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
            const int drainAmount = 325;// 定义要汲取的血量
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
                pTarget.a.data.health -= actualDrain; // 减少目标的血量
                pSelf.a.data.health = Mathf.Min(pSelf.a.getMaxHealth(), pSelf.a.data.health + actualDrain); // 恢复施法者的血量，但不超过其最大血量
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

            if(Toolbox.randomChance(1f))
            {
                pSelf.a.addStatusEffect("Ring24", 8f);//二环•岩石护壁
            }
            return true;
        }
        public static bool sorcery25_Regen(BaseSimObject pTarget, WorldTile pTile = null)
        {

            if (pTarget.a.data.health != pTarget.getMaxHealth())
            {
                pTarget.a.restoreHealth(1000);//二环•生命涌泉
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
            if(Toolbox.randomChance(1f))
            {
                pTarget.a.addStatusEffect("Ring25", 5f);//5秒状态:二环•生命流逝
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

            if(Toolbox.randomChance(1f))
            {
                pTarget.a.addStatusEffect("Ring31", 10f);//三环•斥力场状态
            }

            return true;
        }
        public static bool sorcery31_Attack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {
            if (Toolbox.randomChance(1f))//三环•斥力场
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

            if(Toolbox.randomChance(1f))
            {
                pTarget.a.addStatusEffect("Ring32", 6f);//三环•裂界爆炎状态
            }

            return true;
        }
        public static bool sorcery32_Attack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 特效的随机大小
            float explosionScaleMin = 0.1f;
            float explosionScaleMax = 0.3f;
            float explosionScale = UnityEngine.Random.Range(explosionScaleMin, explosionScaleMax);

            if (Toolbox.randomChance(1f))//三环•裂界爆炎
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

            if(Toolbox.randomChance(1))
            {
                pTarget.a.addStatusEffect("Ring33", 10f);//三环•雷霆术状态
            }

            return true;
        }
        public static bool attack_sorcery33(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null)//三环•雷霆术
            {
                if (Toolbox.randomChance(100.0f))
                {
                    MapBox.spawnLightningMedium(pTile, 0.25f);
                }
            }

            return false;
        }
        public static bool attack_Grade91(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            if (pTarget != null && pTile != null)
            {
                float effectChance = 1f / 3f; // 每个效果触发的概率为1/3

                if (Toolbox.randomChance(effectChance))//三环•雷霆术
                {
                    MapBox.spawnLightningMedium(pTile, 0.25f);
                }
                if (Toolbox.randomChance(effectChance))//三环•裂界爆炎
                {
                    EffectsLibrary.spawnAtTileRandomScale("fx_explosion_tiny", pTile, 0.3f, 0.6f);
                }
                if (Toolbox.randomChance(effectChance))//三环•斥力场
                {
                    EffectsLibrary.spawnExplosionWave(pTile.posV3, 0.05f, 6f);
                    return false; // 如果这个效果触发了，函数返回false
                }
            }

            return true;
        }

        //以下为境界带的再生
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
        public static bool hunger_Grade91(BaseSimObject pTarget, WorldTile pTile = null)//始祖的饱食度
        {
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (a != null)
            {
                a.data.hunger = 100;// 不会饥饿，饱食度一直为100%
            }
            return false;
        }
        public static bool intelligence_attack_Grade4(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 每次攻击计算自身智力值的随机倍数，再减少目标血量
            if (pTarget != null && pTarget.a != null && pTarget.a.data != null && pSelf != null && pSelf.a != null && pSelf.a.stats != null)
            {
                // 智力值的随机倍数，范围在x到x之间
                float intelligenceMultiplierMin = 5f;
                float intelligenceMultiplierMax = 8f;
                float intelligenceMultiplier = UnityEngine.Random.Range(intelligenceMultiplierMin, intelligenceMultiplierMax);

                float damageBasedOnIntelligence = pSelf.a.stats[S.intelligence] * intelligenceMultiplier; // 根据智力计算伤害
                int damageToDeal = Mathf.FloorToInt(damageBasedOnIntelligence); // 将浮点数伤害转换为整数

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
        public static bool intelligence_attack_Grade5(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 每次攻击计算自身智力值的随机倍数，再减少目标血量
            if (pTarget != null && pTarget.a != null && pTarget.a.data != null && pSelf != null && pSelf.a != null && pSelf.a.stats != null)
            {
                // 智力值的随机倍数，范围在x到x之间
                float intelligenceMultiplierMin = 7f;
                float intelligenceMultiplierMax = 10f;
                float intelligenceMultiplier = UnityEngine.Random.Range(intelligenceMultiplierMin, intelligenceMultiplierMax);

                float damageBasedOnIntelligence = pSelf.a.stats[S.intelligence] * intelligenceMultiplier; // 根据智力计算伤害
                int damageToDeal = Mathf.FloorToInt(damageBasedOnIntelligence); // 将浮点数伤害转换为整数

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
        public static bool intelligence_attack_Grade6(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 每次攻击计算自身智力值的随机倍数，再减少目标血量
            if (pTarget != null && pTarget.a != null && pTarget.a.data != null && pSelf != null && pSelf.a != null && pSelf.a.stats != null)
            {
                // 智力值的随机倍数，范围在x到x之间
                float intelligenceMultiplierMin = 9f;
                float intelligenceMultiplierMax = 10f;
                float intelligenceMultiplier = UnityEngine.Random.Range(intelligenceMultiplierMin, intelligenceMultiplierMax);

                float damageBasedOnIntelligence = pSelf.a.stats[S.intelligence] * intelligenceMultiplier; // 根据智力计算伤害
                int damageToDeal = Mathf.FloorToInt(damageBasedOnIntelligence); // 将浮点数伤害转换为整数

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
        public static bool intelligence_attack_Grade7(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 每次攻击计算自身智力值的随机倍数，再减少目标血量
            if (pTarget != null && pTarget.a != null && pTarget.a.data != null && pSelf != null && pSelf.a != null && pSelf.a.stats != null)
            {
                // 智力值的随机倍数，范围在x到x之间
                float intelligenceMultiplierMin = 10f;
                float intelligenceMultiplierMax = 30f;
                float intelligenceMultiplier = UnityEngine.Random.Range(intelligenceMultiplierMin, intelligenceMultiplierMax);

                float damageBasedOnIntelligence = pSelf.a.stats[S.intelligence] * intelligenceMultiplier; // 根据智力计算伤害
                int damageToDeal = Mathf.FloorToInt(damageBasedOnIntelligence); // 将浮点数伤害转换为整数

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
        public static bool intelligence_attack_Grade8(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 每次攻击计算自身智力值的随机倍数，再减少目标血量
            if (pTarget != null && pTarget.a != null && pTarget.a.data != null && pSelf != null && pSelf.a != null && pSelf.a.stats != null)
            {
                // 智力值的随机倍数，范围在x到x之间
                float intelligenceMultiplierMin = 20f;
                float intelligenceMultiplierMax = 40f;
                float intelligenceMultiplier = UnityEngine.Random.Range(intelligenceMultiplierMin, intelligenceMultiplierMax);

                float damageBasedOnIntelligence = pSelf.a.stats[S.intelligence] * intelligenceMultiplier; // 根据智力计算伤害
                int damageToDeal = Mathf.FloorToInt(damageBasedOnIntelligence); // 将浮点数伤害转换为整数

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
        public static bool intelligence_attack_Grade9(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 每次攻击计算自身智力值的随机倍数，再减少目标血量
            if (pTarget != null && pTarget.a != null && pTarget.a.data != null && pSelf != null && pSelf.a != null && pSelf.a.stats != null)
            {
                // 智力值的随机倍数，范围在x到x之间
                float intelligenceMultiplierMin = 40f;
                float intelligenceMultiplierMax = 50f;
                float intelligenceMultiplier = UnityEngine.Random.Range(intelligenceMultiplierMin, intelligenceMultiplierMax);

                float damageBasedOnIntelligence = pSelf.a.stats[S.intelligence] * intelligenceMultiplier; // 根据智力计算伤害
                int damageToDeal = Mathf.FloorToInt(damageBasedOnIntelligence); // 将浮点数伤害转换为整数

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
        public static bool intelligence_attack_Grade91(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
        {
            // 每次攻击计算自身智力值的随机倍数，再减少目标血量
            if (pTarget != null && pTarget.a != null && pTarget.a.data != null && pSelf != null && pSelf.a != null && pSelf.a.stats != null)
            {
                // 智力值的随机倍数，范围在x到x之间
                float intelligenceMultiplierMin = 2000f;
                float intelligenceMultiplierMax = 3000f;
                float intelligenceMultiplier = UnityEngine.Random.Range(intelligenceMultiplierMin, intelligenceMultiplierMax);

                float damageBasedOnIntelligence = pSelf.a.stats[S.intelligence] * intelligenceMultiplier; // 根据智力计算伤害
                int damageToDeal = Mathf.FloorToInt(damageBasedOnIntelligence); // 将浮点数伤害转换为整数

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
            if (a.stats["benyuan"] <= 9)
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
            if (a.stats["yuanneng"] <= 2.99)
            {
                return false;
            }

            // 添加成功概率检查-暂时不考虑
            //double successRate = 0.3; // 30%的概率
            //System.Random random = new System.Random();
            //double randomValue = random.NextDouble(); // 生成0到1之间的随机数
            //if (randomValue > successRate)
            //{
                // 随机数大于successRate，表示执行失败
                //return false;
            //}

            string[] forbiddenTraits = { "Grade01", "Grade02", "Grade1", "Grade2", "Grade3", "Grade4", "Grade5", "Grade6", "Grade7", "Grade8", "Grade9", "Grade91"};
            foreach (string trait in forbiddenTraits)
            {
                if (pTarget.a.hasTrait(trait))
                {
                    return false;
                }
            }

            upTrait("特质", "Grade0", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness"},
                new string[] {"特质"});

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
            if (a.stats["yuanneng"] <= 5.99)
            {
                return false;
            }
            string[] additionalTraits = { "sorcery01", "sorcery02", "sorcery03", "sorcery04", "sorcery05", "sorcery06"};
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质


            upTrait("特质", "Grade01", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade0"},
                new string[] {randomTrait});

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
            if (a.stats["yuanneng"] <= 8.99)
            {
                return false;
            }
            string[] additionalTraits = { "sorcery01", "sorcery02", "sorcery03", "sorcery04", "sorcery05", "sorcery06"};
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质


            upTrait("特质", "Grade02", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade01"},
                new string[] {randomTrait});

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
            if (a.stats["yuanneng"] <= 17.99)
            {
                return false;
            }
            string[] additionalTraits = { "sorcery11", "sorcery12", "sorcery13", "sorcery14", "sorcery15", "sorcery16"};
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质

            upTrait("特质", "Grade1", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade02"},
                new string[] {randomTrait});

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
            if (a.stats["yuanneng"] <= 26.99)
            {
                return false;
            }
            string[] additionalTraits = { "sorcery11", "sorcery12", "sorcery13", "sorcery14", "sorcery15", "sorcery16"};
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质

            upTrait("Grade1", "Grade2", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade1"},
                new string[] {randomTrait});

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
            if (a.stats["yuanneng"] <= 35.99)
            {
                return false;
            }
            string[] additionalTraits = { "sorcery11", "sorcery12", "sorcery13", "sorcery14", "sorcery15", "sorcery16"};
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质

            upTrait("特质", "Grade3", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade2"},
                new string[] {randomTrait});


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
            if (a.stats["yuanneng"] <= 71.99)
            {
                return false;
            }
            string[] additionalTraits = { "sorcery22", "sorcery23", "sorcery24", "sorcery25", "sorcery26" };
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质

            upTrait("特质", "Grade4", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade3"},
                new string[] {randomTrait});

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
            if (a.stats["yuanneng"] <= 107.99)
            {
                return false;
            }
            string[] additionalTraits = { "sorcery22", "sorcery23", "sorcery24", "sorcery25", "sorcery26" };
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质

            upTrait("特质", "Grade5", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade4"},
                new string[] {randomTrait});

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
            if (a.stats["yuanneng"] <= 143.99)
            {
                return false;
            }
            string[] additionalTraits = { "sorcery22", "sorcery23", "sorcery24", "sorcery25", "sorcery26" };
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质

            upTrait("特质", "Grade6", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade5"},
                new string[] {randomTrait});

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
            if (a.stats["yuanneng"] <= 287.99)
            {
                return false;
            }
            string[] additionalTraits = { "sorcery31", "sorcery32", "sorcery33" };
            string randomTrait = GetNewRandomTrait(additionalTraits); //获取新随机特质

            upTrait("特质", "Grade7", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade6"},
                new string[] {"freeze_proof", "fire_proof", randomTrait});

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
            if (a.stats["yuanneng"] <= 519.99)
            {
                return false;
            }


            upTrait("特质", "Grade8", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade7"},
                new string[] {"添加升级外的特质"});

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
            if (a.stats["yuanneng"] <= 819.99)
            {
                return false;
            }

            upTrait("特质", "Grade9", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade8"},
                new string[] {"flair8"});

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
            if (a.stats["yuanneng"] <= 2069.99)
            {
                return false;
            }

            upTrait("特质", "Grade91", a,
                new string[] { "tumorInfection", "cursed", "infected", "mushSpores", "plague", "madness", "Grade9", "sorcery31", "sorcery32", "sorcery33"},
                new string[] {"添加升级外的特质"});

            return true;
        }
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
            teleportRandom(act);


            PowerLibrary pb = new PowerLibrary();
            pb.divineLightFX(pTarget.a.currentTile, null);

            return true;
        }
        public static bool teleportRandom(Actor a)
        {
            TileIsland randomIslandGround = World.world.islandsCalculator.getRandomIslandGround(true);
            WorldTile worldTile;
            if (randomIslandGround == null)
            {
                worldTile = null;
            }
            else
            {
                MapRegion random = randomIslandGround.regions.GetRandom();
                worldTile = (random != null) ? random.tiles.GetRandom<WorldTile>() : null;
            }

            WorldTile worldTile2 = worldTile;
            if (worldTile2 == null)
            {
                return false;
            }

            if (worldTile2.Type.block)
            {
                return false;
            }

            if (!worldTile2.Type.ground)
            {
                return false;
            }

            a.cancelAllBeh(null);
            a.spawnOn(worldTile2, 0f);
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
        public static bool upTrait(string old_trait, string new_trait, Actor actor, string[] other_Oldtraits = null,
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