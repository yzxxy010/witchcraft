using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ReflectionUtility;
using ai;
using System.Numerics;

namespace VideoCopilot.code
{
    internal class traits
    {
        public static void Init()
        {                          //特质id↓ //贴图路径↓ //诞生率↓ //每年-x点世界源力+x点源能↓ //排斥的特质↓
            flair12345_AddActorTrait("flair1", "trait/flair1", 10f, -0.3f, "flair2,flair3,flair4,flair5,flair6,flair7");//D天赋
            flair12345_AddActorTrait("flair2", "trait/flair2", 7f, -0.6f, "flair1,flair3,flair4,flair5,flair6,flair7");//C天赋
            flair12345_AddActorTrait("flair3", "trait/flair3", 3f, -0.9f, "flair1,flair2,flair4,flair5,flair6,flair7");//B天赋
            flair12345_AddActorTrait("flair4", "trait/flair4", 1.5f, -1.2f, "flair1,flair2,flair3,flair5,flair6,flair7");//A天赋
            flair12345_AddActorTrait("flair5", "trait/flair5", 0.5f, -1.5f, "flair1,flair2,flair3,flair4,flair6,flair7");//S天赋
            flair12345_AddActorTrait("flair6", "trait/flair6", 0f, -10f, "flair1,flair2,flair3,flair4,flair5,flair7");//SS天赋
            flair12345_AddActorTrait("flair7", "trait/flair7", 0f, -30f, "flair1,flair2,flair3,flair4,flair5,flair6");//SSS天赋

            ActorTrait flair8 = new ActorTrait();
            flair8.id = "flair8";//大巫师•灵魂
            flair8.path_icon = "trait/flair8";
            flair8.needs_to_be_explored = false;
            flair8.group_id = "interesting3";
            flair8.base_stats[S.max_age] = 10f;//寿命+10
            flair8.base_stats[S.fertility] = -100f;//生育力-100
            flair8.base_stats[S.max_children] = -100f;//可生育子女数-100
            flair8.base_stats["xiaohao"] = -2f;//每年-2点世界源力+2点源能
            flair8.action_death = traitAction.flair8_death;//无限重生效果
            flair8.action_special_effect = traitAction.flair8_Traits;//随机天赋
            AssetManager.traits.add(flair8);
            //巫师境界↓
            ActorTrait Grade0 = new ActorTrait();
            Grade0.id = "Grade0"; //学徒•初识
            Grade0.path_icon = "trait/Grade0";
            Grade0.needs_to_be_explored = false;
            Grade0.group_id = "interesting2";
            Grade0.base_stats[S.damage] = 20f;//伤害+20
            Grade0.base_stats[S.health] = 200f;//生命+40
            Grade0.base_stats[S.intelligence] = 1f;//智力+1
            Grade0.action_special_effect = traitAction.Grade01_effectAction; //学徒•掌控的升级条件
            AssetManager.traits.add(Grade0);

            ActorTrait Grade01 = new ActorTrait();
            Grade01.id = "Grade01"; //学徒•掌控
            Grade01.path_icon = "trait/Grade01";
            Grade01.needs_to_be_explored = false;
            Grade01.group_id = "interesting2";
            Grade01.base_stats[S.damage] = 40f;//伤害+40
            Grade01.base_stats[S.health] = 400f;//生命+400
            Grade01.base_stats[S.intelligence] = 2f;//智力+2
            Grade01.action_special_effect = traitAction.Grade02_effectAction; //学徒•精通的升级条件
            AssetManager.traits.add(Grade01);

            ActorTrait Grade02 = new ActorTrait();
            Grade02.id = "Grade02"; //学徒•精通
            Grade02.path_icon = "trait/Grade02";
            Grade02.needs_to_be_explored = false;
            Grade02.group_id = "interesting2";
            Grade02.base_stats[S.damage] = 60f;
            Grade02.base_stats[S.health] = 600f;
            Grade02.base_stats[S.intelligence] = 3f;
            Grade02.action_special_effect = traitAction.Grade1_effectAction; //正式巫师•塑造的升级条件
            AssetManager.traits.add(Grade02);

            ActorTrait Grade1 = new ActorTrait();
            Grade1.id = "Grade1"; //正式巫师•塑造
            Grade1.path_icon = "trait/Grade1";
            Grade1.needs_to_be_explored = false;
            Grade1.group_id = "interesting2";
            Grade1.base_stats[S.max_age] = 20f;//寿命+20
            Grade1.base_stats[S.damage] = 200f;//
            Grade1.base_stats[S.health] = 2000f;
            Grade1.base_stats[S.armor] = 10f;
            Grade1.base_stats[S.intelligence] = 10f;
            Grade1.base_stats[S.range] = 3f;//射程+3
            Grade1.base_stats[S.area_of_effect] = 3f;//近战范围+3
            Grade1.base_stats[S.targets] = 1f;//可攻击目标+1
            Grade1.base_stats[S.knockback_reduction] = 1f;//抗击退+100%
            Grade1.base_stats[S.knockback] = 1f;//击退+1
            Grade1.action_special_effect = traitAction.Grade2_effectAction; //升正式巫师•元素的条件
            Grade1.action_special_effect = (WorldAction)Delegate.Combine(Grade1.action_special_effect,
new WorldAction(traitAction.Grade1_Regen)); //正式巫师•塑造的再生效果
            AssetManager.traits.add(Grade1);

            ActorTrait Grade2 = new ActorTrait();
            Grade2.id = "Grade2"; //正式巫师•元素
            Grade2.path_icon = "trait/Grade2";
            Grade2.needs_to_be_explored = false;
            Grade2.group_id = "interesting2";
            Grade2.base_stats[S.max_age] = 25f;
            Grade2.base_stats[S.damage] = 400f;
            Grade2.base_stats[S.health] = 4000f;
            Grade2.base_stats[S.armor] = 10f;
            Grade2.base_stats[S.intelligence] = 20f;
            Grade2.base_stats[S.range] = 3f;
            Grade2.base_stats[S.area_of_effect] = 3f;
            Grade2.base_stats[S.targets] = 1f;
            Grade2.base_stats[S.knockback_reduction] = 1f;//抗击退+100%
            Grade2.base_stats[S.knockback] = 1f;//击退+1
            Grade2.action_special_effect = traitAction.Grade3_effectAction; //正式巫师•冥想的条件
            Grade2.action_special_effect = (WorldAction)Delegate.Combine(Grade2.action_special_effect,
                new WorldAction(traitAction.Grade2_Regen)); //正式巫师•元素的再生效果
            AssetManager.traits.add(Grade2);

            ActorTrait Grade3 = new ActorTrait();
            Grade3.id = "Grade3"; //正式巫师•冥想
            Grade3.path_icon = "trait/Grade3";
            Grade3.needs_to_be_explored = false;
            Grade3.group_id = "interesting2";
            Grade3.base_stats[S.max_age] = 30f;
            Grade3.base_stats[S.damage] = 600f;
            Grade3.base_stats[S.health] = 6000f;
            Grade3.base_stats[S.armor] = 15f;
            Grade3.base_stats[S.intelligence] = 30f;
            Grade3.base_stats[S.range] = 5f;
            Grade3.base_stats[S.area_of_effect] = 5f;
            Grade3.base_stats[S.targets] = 1f;
            Grade3.base_stats[S.knockback_reduction] = 1f;
            Grade3.base_stats[S.knockback] = 1f;//击退+1
            Grade3.action_special_effect = traitAction.Grade4_effectAction; //升高级巫师•黎明的条件
            Grade3.action_special_effect = (WorldAction)Delegate.Combine(Grade3.action_special_effect,
                new WorldAction(traitAction.Grade3_Regen)); //正式巫师•冥想的再生效果
            AssetManager.traits.add(Grade3);

            ActorTrait Grade4 = new ActorTrait();
            Grade4.id = "Grade4"; //高级巫师•黎明
            Grade4.path_icon = "trait/Grade4";
            Grade4.needs_to_be_explored = false;
            Grade4.group_id = "interesting2";
            Grade4.base_stats[S.max_age] = 90f;
            Grade4.base_stats[S.damage] = 2000f;
            Grade4.base_stats[S.health] = 20000f;
            Grade4.base_stats[S.armor] = 25f;
            Grade4.base_stats[S.intelligence] = 50f;
            Grade4.base_stats[S.range] = 10f;
            Grade4.base_stats[S.area_of_effect] = 10f;
            Grade4.base_stats[S.targets] = 3f;
            Grade4.base_stats[S.knockback_reduction] = 2f;
            Grade4.base_stats[S.knockback] = 2f;
            Grade4.base_stats["xiaohao"] = -1f;//每年-1点世界源力+1点源能
            Grade4.action_attack_target = traitAction.intelligence_attack_Grade4;//法伤
            Grade4.action_special_effect = traitAction.Grade5_effectAction; //升高级巫师•扩展的条件
            Grade4.action_special_effect = (WorldAction)Delegate.Combine(Grade4.action_special_effect,
                new WorldAction(traitAction.Grade4_Regen)); //高级巫师•黎明的再生效果
            AssetManager.traits.add(Grade4);


            ActorTrait Grade5 = new ActorTrait();
            Grade5.id = "Grade5"; //高级巫师•扩展
            Grade5.path_icon = "trait/Grade5";
            Grade5.needs_to_be_explored = false;
            Grade5.group_id = "interesting2";
            Grade5.base_stats[S.max_age] = 105f;
            Grade5.base_stats[S.damage] = 4000f;
            Grade5.base_stats[S.health] = 40000f;
            Grade5.base_stats[S.armor] = 30f;
            Grade5.base_stats[S.intelligence] = 80f;
            Grade5.base_stats[S.range] = 10f;
            Grade5.base_stats[S.area_of_effect] = 10f;
            Grade5.base_stats[S.targets] = 3f;
            Grade5.base_stats[S.knockback_reduction] = 2f;
            Grade5.base_stats[S.knockback] = 2f;//击退+1
            Grade5.base_stats["xiaohao"] = -1f;
            Grade5.action_attack_target = traitAction.intelligence_attack_Grade5;//法伤
            Grade5.action_special_effect = traitAction.Grade6_effectAction; //升高级巫师•巅峰的条件
            Grade5.action_special_effect = (WorldAction)Delegate.Combine(Grade5.action_special_effect,
                new WorldAction(traitAction.Grade5_Regen)); //高级巫师•扩展的再生效果
            AssetManager.traits.add(Grade5);


            ActorTrait Grade6 = new ActorTrait();
            Grade6.id = "Grade6"; //高级巫师•巅峰
            Grade6.path_icon = "trait/Grade6";
            Grade6.needs_to_be_explored = false;
            Grade6.group_id = "interesting2";
            Grade6.base_stats[S.max_age] = 120f;
            Grade6.base_stats[S.damage] = 6000f;
            Grade6.base_stats[S.health] = 60000f;
            Grade6.base_stats[S.armor] = 40f;
            Grade6.base_stats[S.intelligence] = 100f;
            Grade6.base_stats[S.attack_speed] = 10f;
            Grade6.base_stats[S.range] = 10f;
            Grade6.base_stats[S.area_of_effect] = 10f;
            Grade6.base_stats[S.targets] = 3f;
            Grade6.base_stats[S.knockback_reduction] = 2f;
            Grade6.base_stats[S.knockback] = 2f;
            Grade6.base_stats["xiaohao"] = -1f;
            Grade6.action_attack_target = traitAction.intelligence_attack_Grade6;//法伤
            Grade6.action_special_effect = traitAction.Grade7_effectAction; //升大巫师•铭刻的条件
            Grade6.action_special_effect = (WorldAction)Delegate.Combine(Grade6.action_special_effect,
                new WorldAction(traitAction.Grade6_Regen)); //高级巫师•巅峰三阶的再生效果
            AssetManager.traits.add(Grade6);


            ActorTrait Grade7 = new ActorTrait();
            Grade7.id = "Grade7"; //大巫师•铭刻
            Grade7.path_icon = "trait/Grade7";
            Grade7.needs_to_be_explored = false;
            Grade7.group_id = "interesting2";
            Grade7.base_stats[S.max_age] = 170f;
            Grade7.base_stats[S.damage] = 20000f;
            Grade7.base_stats[S.health] = 200000f;
            Grade7.base_stats[S.intelligence] = 200f;
            Grade7.base_stats[S.critical_chance] = 0.10f;
            Grade7.base_stats[S.armor] = 60f;
            Grade7.base_stats[S.mod_damage] = 0.20f;
            Grade7.base_stats[S.mod_health] = 0.20f;
            Grade7.base_stats[S.range] = 12f;
            Grade7.base_stats[S.area_of_effect] = 12f;
            Grade7.base_stats[S.targets] = 5f;
            Grade7.base_stats[S.knockback_reduction] = 3f;
            Grade7.base_stats[S.knockback] = 3f;
            Grade7.base_stats["xiaohao"] = -2f;
            Grade7.action_attack_target = traitAction.intelligence_attack_Grade7;//法伤
            Grade7.action_special_effect = traitAction.Grade8_effectAction; //大巫师•巅峰的条件
            Grade7.action_special_effect = (WorldAction)Delegate.Combine(Grade7.action_special_effect,
                new WorldAction(traitAction.Grade7_Regen)); //大巫师•铭刻的再生效果
            AssetManager.traits.add(Grade7);


            ActorTrait Grade8 = new ActorTrait();
            Grade8.id = "Grade8"; //大巫师•巅峰
            Grade8.path_icon = "trait/Grade8";
            Grade8.needs_to_be_explored = false;
            Grade8.group_id = "interesting2";
            Grade8.base_stats[S.max_age] = 220f;
            Grade8.base_stats[S.damage] = 50000f;
            Grade8.base_stats[S.health] = 500000f;
            Grade8.base_stats[S.intelligence] = 300f;
            Grade8.base_stats[S.critical_chance] = 0.10f;
            Grade8.base_stats[S.armor] = 70f;
            Grade8.base_stats[S.mod_damage] = 0.20f;
            Grade8.base_stats[S.mod_health] = 0.20f;
            Grade8.base_stats[S.range] = 12f;
            Grade8.base_stats[S.area_of_effect] = 12f;
            Grade8.base_stats[S.targets] = 5f;
            Grade8.base_stats[S.knockback_reduction] = 5f;
            Grade8.base_stats[S.knockback] = 5f;
            Grade8.base_stats["xiaohao"] = -2f;
            Grade8.action_attack_target = traitAction.intelligence_attack_Grade8;//法伤
            Grade8.action_special_effect = traitAction.Grade9_effectAction; //升大巫师•不死的条件
            Grade8.action_special_effect = (WorldAction)Delegate.Combine(Grade8.action_special_effect,
                new WorldAction(traitAction.Grade8_Regen)); //大巫师•巅峰的再生效果
            AssetManager.traits.add(Grade8);


            ActorTrait Grade9 = new ActorTrait();
            Grade9.id = "Grade9"; //大巫师•不死
            Grade9.path_icon = "trait/Grade9";
            Grade9.needs_to_be_explored = false;
            Grade9.group_id = "interesting2";
            Grade9.base_stats[S.max_age] = 330f;
            Grade9.base_stats[S.damage] = 100000f;
            Grade9.base_stats[S.health] = 1000000f;
            Grade9.base_stats[S.armor] = 100f;
            Grade9.base_stats[S.intelligence] = 500f;
            Grade9.base_stats[S.critical_chance] = 0.10f;
            Grade9.base_stats[S.mod_damage] = 0.20f;
            Grade9.base_stats[S.mod_health] = 0.20f;
            Grade9.base_stats[S.range] = 15f;
            Grade9.base_stats[S.area_of_effect] = 15f;
            Grade9.base_stats[S.targets] = 5f;
            Grade9.base_stats[S.knockback_reduction] = 8f;
            Grade9.base_stats[S.knockback] = 8f;
            Grade9.base_stats["xiaohao"] = -2f;
            Grade9.action_attack_target = traitAction.intelligence_attack_Grade9;//法伤      
            Grade9.action_special_effect = traitAction.Grade91_effectAction; //升始祖的条件
            Grade9.action_special_effect = (WorldAction)Delegate.Combine(Grade9.action_special_effect,
                new WorldAction(traitAction.Grade9_Regen)); //大巫师•不死的再生效果
            AssetManager.traits.add(Grade9);


            ActorTrait Grade91 = new ActorTrait();
            Grade91.id = "Grade91"; //始祖
            Grade91.path_icon = "trait/Grade10";
            Grade91.needs_to_be_explored = false;
            Grade91.group_id = "interesting2";
            Grade91.base_stats[S.max_age] = float.PositiveInfinity;//无限寿命
            Grade91.base_stats[S.damage] = 10000000f;//1000w伤害
            Grade91.base_stats[S.health] = 100000000f;//1E血量
            Grade91.base_stats[S.armor] = 300f;
            Grade91.base_stats[S.intelligence] = 999f;
            Grade91.base_stats[S.critical_chance] = 0.30f;
            Grade91.base_stats[S.mod_damage] = 0.30f;
            Grade91.base_stats[S.mod_health] = 0.30f;
            Grade91.base_stats[S.range] = 20f;
            Grade91.base_stats[S.area_of_effect] = 20f;
            Grade91.base_stats[S.targets] = 5f;
            Grade91.base_stats[S.knockback_reduction] = 10f;
            Grade91.base_stats[S.knockback] = 10f;
            Grade91.base_stats["xiaohao"] = -10f;
            Grade91.action_attack_target += traitAction.intelligence_attack_Grade91;//法伤
            Grade91.action_attack_target += new AttackAction((traitAction.attack_Grade91));//随机三环巫术
            Grade91.action_special_effect = new WorldAction((traitAction.hunger_Grade91));//不会饥饿
            Grade91.action_special_effect = (WorldAction)Delegate.Combine(Grade91.action_special_effect,
                new WorldAction(traitAction.Grade91_EffectAction)); //始祖的再生效果
            AssetManager.traits.add(Grade91);


            ActorTrait sorcery01 = new ActorTrait();
            sorcery01.id = "sorcery01"; //零环•轻身术
            sorcery01.path_icon = "trait/sorcery01";
            sorcery01.needs_to_be_explored = false;
            sorcery01.group_id = "interesting4";
            sorcery01.action_attack_target = new AttackAction((traitAction.attack_sorcery01));
            AssetManager.traits.add(sorcery01);

            ActorTrait sorcery02 = new ActorTrait();
            sorcery02.id = "sorcery02"; //零环•烈焰之握
            sorcery02.path_icon = "trait/sorcery02";
            sorcery02.needs_to_be_explored = false;
            sorcery02.group_id = "interesting4";
            sorcery02.action_attack_target = new AttackAction((traitAction.attack_sorcery02));
            AssetManager.traits.add(sorcery02);

            ActorTrait sorcery03 = new ActorTrait();
            sorcery03.id = "sorcery03"; //零环•水幻迷障
            sorcery03.path_icon = "trait/sorcery03";
            sorcery03.needs_to_be_explored = false;
            sorcery03.group_id = "interesting4";
            sorcery03.action_attack_target = new AttackAction((traitAction.attack_sorcery03));
            AssetManager.traits.add(sorcery03);

            ActorTrait sorcery04 = new ActorTrait();
            sorcery04.id = "sorcery04"; //零环•生命祈愈
            sorcery04.path_icon = "trait/sorcery04";
            sorcery04.needs_to_be_explored = false;
            sorcery04.group_id = "interesting4";
            sorcery04.base_stats[S.health] = 100f;
            sorcery04.action_special_effect = (WorldAction)Delegate.Combine(sorcery04.action_special_effect,
                new WorldAction(traitAction.sorcery04_Regen)); //再生效果
            AssetManager.traits.add(sorcery04);

            ActorTrait sorcery05 = new ActorTrait();
            sorcery05.id = "sorcery05"; //零环•大地壁垒
            sorcery05.path_icon = "trait/sorcery05";
            sorcery05.needs_to_be_explored = false;
            sorcery05.group_id = "interesting4";
            sorcery05.action_attack_target = new AttackAction((traitAction.attack_sorcery05));
            AssetManager.traits.add(sorcery05);

            ActorTrait sorcery06 = new ActorTrait();
            sorcery06.id = "sorcery06"; //零环•蔓藤囚牢
            sorcery06.path_icon = "trait/sorcery06";
            sorcery06.needs_to_be_explored = false;
            sorcery06.group_id = "interesting4";
            sorcery06.action_attack_target = new AttackAction((traitAction.attack_sorcery06));
            AssetManager.traits.add(sorcery06);

            ActorTrait sorcery11 = new ActorTrait();
            sorcery11.id = "sorcery11"; //一环•肌肉松弛术
            sorcery11.path_icon = "trait/sorcery11";
            sorcery11.needs_to_be_explored = false;
            sorcery11.group_id = "interesting4";
            sorcery11.action_attack_target = new AttackAction((traitAction.attack_sorcery11));
            AssetManager.traits.add(sorcery11);

            ActorTrait sorcery12 = new ActorTrait();
            sorcery12.id = "sorcery12"; //一环•缠绕之网
            sorcery12.path_icon = "trait/sorcery12";
            sorcery12.needs_to_be_explored = false;
            sorcery12.group_id = "interesting4";
            sorcery12.action_attack_target = new AttackAction((traitAction.attack_sorcery12));
            AssetManager.traits.add(sorcery12);

            ActorTrait sorcery13 = new ActorTrait();
            sorcery13.id = "sorcery13"; //一环•土之坚甲
            sorcery13.path_icon = "trait/sorcery13";
            sorcery13.needs_to_be_explored = false;
            sorcery13.group_id = "interesting4";
            sorcery13.action_attack_target = new AttackAction((traitAction.attack_sorcery13));
            AssetManager.traits.add(sorcery13);
            
            ActorTrait sorcery14 = new ActorTrait();
            sorcery14.id = "sorcery14"; //一环•复苏之流
            sorcery14.path_icon = "trait/sorcery14";
            sorcery14.needs_to_be_explored = false;
            sorcery14.group_id = "interesting4";
            sorcery14.base_stats[S.health] = 250f;
            sorcery14.action_special_effect = (WorldAction)Delegate.Combine(sorcery14.action_special_effect,
                new WorldAction(traitAction.sorcery14_Regen)); //再生效果
            AssetManager.traits.add(sorcery14);

            ActorTrait sorcery15 = new ActorTrait();
            sorcery15.id = "sorcery15"; //一环•风行术
            sorcery15.path_icon = "trait/sorcery15";
            sorcery15.needs_to_be_explored = false;
            sorcery15.group_id = "interesting4";
            sorcery15.action_attack_target = new AttackAction((traitAction.attack_sorcery15));
            AssetManager.traits.add(sorcery15);

            ActorTrait sorcery16 = new ActorTrait();
            sorcery16.id = "sorcery16"; //一环•水雾术
            sorcery16.path_icon = "trait/sorcery16";
            sorcery16.needs_to_be_explored = false;
            sorcery16.group_id = "interesting4";
            sorcery16.action_attack_target = new AttackAction((traitAction.attack_sorcery16));
            AssetManager.traits.add(sorcery16);

            ActorTrait sorcery22 = new ActorTrait();
            sorcery22.id = "sorcery22"; //二环•星之致幻
            sorcery22.path_icon = "trait/sorcery22";
            sorcery22.needs_to_be_explored = false;
            sorcery22.group_id = "interesting4";
            sorcery22.action_attack_target = new AttackAction((traitAction.attack_sorcery22));
            AssetManager.traits.add(sorcery22);
            
            ActorTrait sorcery23 = new ActorTrait();
            sorcery23.id = "sorcery23"; //二环•血液汲取
            sorcery23.path_icon = "trait/sorcery23";
            sorcery23.needs_to_be_explored = false;
            sorcery23.group_id = "interesting4";
            sorcery23.action_attack_target = new AttackAction((traitAction.attack_sorcery23));
            AssetManager.traits.add(sorcery23);

            ActorTrait sorcery24 = new ActorTrait();
            sorcery24.id = "sorcery24"; //二环•岩石护壁
            sorcery24.path_icon = "trait/sorcery24";
            sorcery24.needs_to_be_explored = false;
            sorcery24.group_id = "interesting4";
            sorcery24.action_attack_target = new AttackAction((traitAction.attack_sorcery24));
            AssetManager.traits.add(sorcery24);

            ActorTrait sorcery25 = new ActorTrait();
            sorcery25.id = "sorcery25"; //二环•生命涌泉
            sorcery25.path_icon = "trait/sorcery25";
            sorcery25.needs_to_be_explored = false;
            sorcery25.group_id = "interesting4";
            sorcery25.base_stats[S.health] = 2000f;
            sorcery25.action_special_effect = (WorldAction)Delegate.Combine(sorcery25.action_special_effect,
                new WorldAction(traitAction.sorcery25_Regen)); //再生效果
            AssetManager.traits.add(sorcery25);

            ActorTrait sorcery26 = new ActorTrait();
            sorcery26.id = "sorcery26"; //二环•生命流逝
            sorcery26.path_icon = "trait/sorcery26";
            sorcery26.needs_to_be_explored = false;
            sorcery26.group_id = "interesting4";
            sorcery26.action_attack_target = new AttackAction((traitAction.attack_sorcery26));
            AssetManager.traits.add(sorcery26);

            ActorTrait sorcery31 = new ActorTrait();
            sorcery31.id = "sorcery31"; //三环•斥力场
            sorcery31.path_icon = "trait/sorcery31";
            sorcery31.needs_to_be_explored = false;
            sorcery31.group_id = "interesting4";
            sorcery14.base_stats[S.damage] = 100f;
            sorcery31.action_attack_target = new AttackAction((traitAction.attack_sorcery31));
            sorcery31.action_attack_target = (AttackAction)Delegate.Combine(sorcery31.action_attack_target,
                new AttackAction(traitAction.sorcery31_Attack)); //三环•斥力场攻击效果
            AssetManager.traits.add(sorcery31);

            ActorTrait sorcery32 = new ActorTrait();
            sorcery32.id = "sorcery32"; //三环•裂界爆炎
            sorcery32.path_icon = "trait/sorcery32";
            sorcery32.needs_to_be_explored = false;
            sorcery32.group_id = "interesting4";
            sorcery32.base_stats[S.damage] = 100f;
            sorcery32.action_attack_target = new AttackAction((traitAction.attack_sorcery32));
            sorcery32.action_attack_target = (AttackAction)Delegate.Combine(sorcery32.action_attack_target,
                new AttackAction(traitAction.sorcery32_Attack));
            AssetManager.traits.add(sorcery32);

            ActorTrait sorcery33 = new ActorTrait();
            sorcery33.id = "sorcery33"; //三环•雷霆术
            sorcery33.path_icon = "trait/sorcery33";
            sorcery33.needs_to_be_explored = false;
            sorcery33.group_id = "interesting4";
            sorcery33.action_attack_target = new AttackAction((traitAction.attack_sorcery33));
            sorcery33.action_attack_target = (AttackAction)Delegate.Combine(sorcery33.action_attack_target,
                new AttackAction(traitAction.sorcery33_Attack));
            AssetManager.traits.add(sorcery33);
        }
        
        public static void flair12345_AddActorTrait(string id, string pathIcon, float birth, float base_stat_value, string opposite_stats_value)
        {
            ActorTrait flair = new ActorTrait();
            flair.id = id;
            flair.path_icon = pathIcon;
            flair.needs_to_be_explored = false;
            flair.birth = birth;
            flair.group_id = "interesting3";
            flair.base_stats["xiaohao"] = base_stat_value;
            flair.opposite = opposite_stats_value;
            flair.action_special_effect = traitAction.Grade0_effectAction; //觉醒一阶的升价条件
            AssetManager.traits.add(flair);
        }
    }
}