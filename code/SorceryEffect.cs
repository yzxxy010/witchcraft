using System;
using System.Threading;
using NCMS;
using UnityEngine;
using ReflectionUtility;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace InterestingTrait.code
{
    internal class SorceryEffect
    {
        public static void Init()
        {
            //零环
            StatusEffect Ring01 = new StatusEffect();
            Ring01.id = "Ring01";//零环•轻身术
            Ring01.name = "status_title_Ring01";
            Ring01.description = "status_desc_Ring01";
            Ring01.path_icon = "Ring/Ring01";
            Ring01.base_stats[S.speed] = 10f;//速度+10
            Ring01.base_stats[S.attack_speed] = 10f;//攻击速度+10
            Ring01.base_stats[S.armor] = -5f;//防御-5
            Ring01.base_stats[S.knockback_reduction] = -0.05f;//抗击退-5%
            AssetManager.status.add(pAsset:Ring01);

            StatusEffect Ring02 = new StatusEffect();
            Ring02.id = "Ring02";//零环•水幻迷障
            Ring02.name = "status_title_Ring02";
            Ring02.description = "status_desc_Ring02";
            Ring02.path_icon = "Ring/Ring02";
            Ring02.base_stats[S.range] = -1f;//射程-1
            Ring02.base_stats[S.area_of_effect] = -0.5f;//近战范围-5%
            AssetManager.status.add(pAsset:Ring02);

            StatusEffect Ring03 = new StatusEffect();
            Ring03.id = "Ring03";//零环•大地壁垒
            Ring03.name = "status_title_Ring03";
            Ring03.description = "status_desc_Ring03";
            Ring03.path_icon = "Ring/Ring03";
            Ring03.base_stats[S.armor] = 10f;//防御+10
            Ring03.base_stats[S.speed] = -5f;//速度-5
            Ring03.base_stats[S.attack_speed] = -5f;//攻击速度-5
            Ring03.base_stats[S.knockback_reduction] = 0.05f;//抗击退+5%
            AssetManager.status.add(pAsset:Ring03);

            StatusEffect Ring04 = new StatusEffect();
            Ring04.id = "Ring04";//零环•蔓藤囚牢
            Ring04.name = "status_title_Ring04";
            Ring04.description = "status_desc_Ring04";
            Ring04.path_icon = "Ring/Ring04";
            Ring04.base_stats[S.speed] = -10f;//速度-10
            Ring04.base_stats[S.attack_speed] = -10f;//攻击速度-10
            AssetManager.status.add(pAsset:Ring04);

            StatusEffect Ring05 = new StatusEffect();
            Ring05.id = "Ring05";//零环•烈焰之握
            Ring05.name = "status_title_Ring05";
            Ring05.description = "status_desc_Ring05";
            Ring05.allow_timer_reset = false;// 不允许重置计时器
            Ring05.action_interval = 0.5f;// 动作触发的时间间隔秒
            Ring05.path_icon = "Ring/Ring05";
            Ring05.action = new WorldAction(traitAction.attack_Ring05);
            AssetManager.status.add(pAsset:Ring05);
            //一环
            StatusEffect Ring11 = new StatusEffect();
            Ring11.id = "Ring11";//一环•疲惫之手
            Ring11.name = "status_title_Ring11";
            Ring11.description = "status_desc_Ring11";
            Ring11.path_icon = "Ring/Ring11";
            Ring11.base_stats[S.damage] = -120f;//伤害-120
            Ring11.base_stats[S.speed] = -5f;//速度-5
            Ring11.base_stats[S.attack_speed] = -5f;//攻击速度-5
            AssetManager.status.add(pAsset:Ring11);

            StatusEffect Ring12 = new StatusEffect();
            Ring12.id = "Ring12";//一环•缠绕之网
            Ring12.name = "status_title_Ring12";
            Ring12.description = "status_desc_Ring12";
            Ring12.path_icon = "Ring/Ring12";
            Ring12.base_stats[S.damage] = -30f;//伤害-50
            Ring12.base_stats[S.speed] = -15f;//速度-15
            Ring12.base_stats[S.attack_speed] = -30f;//攻击速度-20
            AssetManager.status.add(pAsset:Ring12);

            StatusEffect Ring13 = new StatusEffect();
            Ring13.id = "Ring13";//一环•土之坚甲
            Ring13.name = "status_title_Ring13";
            Ring13.description = "status_desc_Ring13";
            Ring13.path_icon = "Ring/Ring13";
            Ring13.base_stats[S.damage] = 100f;//伤害+100
            Ring13.base_stats[S.armor] = 20f;//防御+20
            Ring13.base_stats[S.speed] = -10f;//速度-10
            Ring13.base_stats[S.attack_speed] = -10f;//攻击速度-10
            Ring13.base_stats[S.knockback_reduction] = 0.10f;//抗击退+10%
            AssetManager.status.add(pAsset:Ring13);

            StatusEffect Ring14 = new StatusEffect();
            Ring14.id = "Ring14";//一环•风行术
            Ring14.name = "status_title_Ring14";
            Ring14.description = "status_desc_Ring14";
            Ring14.path_icon = "Ring/Ring14";
            Ring14.base_stats[S.speed] = 40f;//速度+40
            Ring14.base_stats[S.attack_speed] = 60f;//攻击速度+60
            AssetManager.status.add(pAsset:Ring14);

            StatusEffect Ring15 = new StatusEffect();
            Ring15.id = "Ring15";//一环•水雾术
            Ring15.name = "status_title_Ring15";
            Ring15.description = "status_desc_Ring15";
            Ring15.path_icon = "Ring/Ring15";
            Ring15.base_stats[S.range] = -3f;//射程-3
            Ring15.base_stats[S.area_of_effect] = -3f;//近战范围-3
            AssetManager.status.add(pAsset:Ring15);

            StatusEffect Ring22 = new StatusEffect();
            Ring22.id = "Ring22";//二环•星之致幻
            Ring22.name = "status_title_Ring22";
            Ring22.description = "status_desc_Ring22";
            Ring22.path_icon = "Ring/Ring22";
            Ring22.base_stats[S.intelligence] = -50f;//智力-50
            Ring22.base_stats[S.damage] = -1200f;//伤害-1200
            Ring22.base_stats[S.speed] = -10f;//速度-10
            Ring22.base_stats[S.attack_speed] = -10f;//攻击速度-10
            AssetManager.status.add(pAsset:Ring22);

            StatusEffect Ring24 = new StatusEffect();
            Ring24.id = "Ring24";//二环•岩石护壁
            Ring24.name = "status_title_Ring24";
            Ring24.description = "status_desc_Ring24";
            Ring24.path_icon = "Ring/Ring24";
            Ring24.base_stats[S.damage] = 2000f;//伤害+3000
            Ring24.base_stats[S.health] = 3000f;//生命+3000
            Ring24.base_stats[S.critical_chance] = 0.1f;//暴击+10%
            Ring24.base_stats[S.area_of_effect] = 1f;//近战范围+1
            Ring24.base_stats[S.armor] = 40f;//防御+40
            Ring24.base_stats[S.scale] = 0.10f;//身材+100%
            Ring24.base_stats[S.speed] = -15f; //-移动速度15
            Ring24.base_stats[S.attack_speed] = -30f;//-攻击速度15
            Ring24.base_stats[S.knockback_reduction] = 1f;//抗击退+100%
            AssetManager.status.add(pAsset:Ring24);

            StatusEffect Ring25 = new StatusEffect();
            Ring25.id = "Ring25";//二环•生命流逝
            Ring25.name = "status_title_Ring25";
            Ring25.description = "status_desc_Ring25";
            Ring25.allow_timer_reset = false;// 不允许重置计时器
            Ring25.action_interval = 0.5f;// 动作触发的时间间隔秒
            Ring25.path_icon = "Ring/Ring25";
            Ring25.base_stats[S.health] = -5000f;//-生命值5000
            Ring25.base_stats[S.speed] = -10f;//-移动速度10
            Ring25.base_stats[S.attack_speed] = -10f;//-攻击速度10
            Ring25.action = new WorldAction(traitAction.attack_Ring25);
            AssetManager.status.add(pAsset:Ring25);

            StatusEffect Ring31 = new StatusEffect();
            Ring31.id = "Ring31";//三环•斥力场
            Ring31.name = "status_title_Ring31";
            Ring31.description = "status_desc_Ring31";
            Ring31.path_icon = "Ring/Ring31";
            Ring31.base_stats[S.armor] = -20f;//防御-20
            //Ring31.base_stats[S.damage] = -50f;//伤害-50
            //Ring31.base_stats[S.critical_chance] = -0.10f;//暴击-10%
            //Ring31.base_stats[S.speed] = -30f;//速度-30
            //Ring31.base_stats[S.attack_speed] = -30f;//攻击速度-30
            AssetManager.status.add(pAsset:Ring31);

            StatusEffect Ring32 = new StatusEffect();
            Ring32.id = "Ring32";//三环•裂界爆炎
            Ring32.name = "status_title_Ring32";
            Ring32.description = "status_desc_Ring32";
            Ring32.path_icon = "Ring/Ring32";
            Ring32.base_stats[S.armor] = -20f;//防御-20
            AssetManager.status.add(pAsset:Ring32);

            StatusEffect Ring33 = new StatusEffect();
            Ring33.id = "Ring33";//三环•雷霆术
            Ring33.name = "status_title_Ring33";
            Ring33.description = "status_desc_Ring33";
            Ring33.path_icon = "Ring/Ring33";
            Ring33.base_stats[S.armor] = -20f;//防御20
            AssetManager.status.add(pAsset:Ring33);
        }
    }
}