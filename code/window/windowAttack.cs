using System.Collections.Generic;
using NCMS.Utils;
using NeoModLoader.General;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace VideoCopilot.code.window
{
    public class WindowAttack : MonoBehaviour
    {
        private static ScrollWindow window; // 窗口对象
        private static GameObject content; // 内容区域对象
        private static bool isInitialized = false; // 是否已经初始化窗口
        private static float itemHeightWithGap = 35f; // 每个预制件的高度 + 间距
        private static List<Actor> sortedList = new List<Actor>(); // 排序后的列表

        public static string state { get; private set; }

        // 打开窗口的方法
        public static void ShowWindow()
        {
            if (isInitialized)
            {
                ClearContent();
            }

            Init();
            ScrollWindow.showWindow("tilesWindow"); // 显示窗口
        }

        // 初始化窗口的方法
        public static void Init()
        {
            isInitialized = true; // 标记为已初始化

            // 创建窗口并设置窗口名称
            window = Windows.CreateNewWindow("tilesWindow", LM.Get("windowAttack"));

            // 激活滚动视图
            var scrollView =
                GameObject.Find(
                    $"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View");
            scrollView.gameObject.SetActive(true);


            // 获取视口并调整尺寸
            var viewport =
                GameObject.Find(
                    $"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View/Viewport");
            var viewportRect = viewport.GetComponent<RectTransform>();
            viewportRect.sizeDelta = new Vector2(0, 17); // 调整视口大小


            // 获取内容区域对象
            content = GameObject.Find(
                $"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View/Viewport/Content");


            //绘制窗口

            var allChildObjects = window.transform.Find("Background").GetComponentsInChildren<Transform>();
            foreach (var child in allChildObjects)
            {
                if (child.name == "AttackWindow_Main") // 销毁之前的ui
                {
                    GameObject.Destroy(child.gameObject);
                }

                if (child.name == "CloseBackgound")
                {
                    child.transform.localPosition = new Vector3(84, 132, 1);
                    child.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                }
            }

            UItools.createImageOnUI(window.transform.Find("Background").gameObject, "AttackWindow_Main",
                "ui/attackWindow.png",
                new Vector3(0, -3.5f, 2), new Vector3(2.8f, 2.8f, 1));


            Sort_AttackWinodw(Sort_key.default_Age_sort);

            drawListOnAttackWindow();
        }

        public static void Sort_AttackWinodw(string option)
        {
            switch (option)
            {
                case Sort_key.default_Age_sort:
                    state = Sort_key.default_Age_sort;
                    Civ_Sort_Of_Age();
                    break;
                case Sort_key.allActor_Age_sort:
                    state = Sort_key.allActor_Age_sort;
                    AllActor_Sort_Of_Age();
                    break;
                case Sort_key.yuanneng_sort:
                    state = Sort_key.yuanneng_sort;
                    Civ_Sort_Of_Yuanneng();
                    break;
                case Sort_key.allActor_yuanneng_sort:
                    state = Sort_key.allActor_yuanneng_sort;
                    AllActor_Sort_Of_Yuanneng();
                    break;
            }
        }

        public static void drawListOnAttackWindow()
        {
            //根据预制件数量判定列表长度
            float totalHeight = sortedList.Count * itemHeightWithGap;
            RectTransform contentRect = content.GetComponent<RectTransform>();
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, totalHeight+30);
            
            var num = 0;
            foreach (var actor in sortedList)
            {
                UItools.createActorOnUI(actor, content, new Vector3(70, -30 - (num * itemHeightWithGap), 10),
                    state);
                num++;
            }
        }

        public static void ClearContent()
        {
            foreach (Transform child in content.transform)

            {
                Destroy(child.gameObject);
            }
        }

        public static void AllActor_Sort_Of_Age()
        {
            sortedList.Clear();

            // 通过一次遍历来同时处理添加到 sortedList 和收集需要删除的键(年龄降序)
            sortedList.AddRange(Globals.Actors.Values.Where(actor => actor != null)
                .OrderByDescending(actor => actor.getAge()));
            var keysToRemove = Globals.Actors
                .Where(pair => pair.Value == null) // 筛选出值为 null 的项
                .Select(pair => pair.Key) // 选择出键
                .ToList();
            foreach (var key in keysToRemove)
            {
                Globals.Actors.Remove(key);
            }
        }

        public static void Civ_Sort_Of_Age()
        {
            sortedList.Clear();

            // 通过一次遍历来同时处理添加到 sortedList 和收集需要删除的键(年龄降序)
            sortedList.AddRange(Globals.Actors.Values.Where(actor => actor != null && actor.race.civilization)
                .OrderByDescending(actor => actor.getAge()));
            var keysToRemove = Globals.Actors
                .Where(pair => pair.Value == null) // 筛选出值为 null 的项
                .Select(pair => pair.Key) // 选择出键
                .ToList();
            foreach (var key in keysToRemove)
            {
                Globals.Actors.Remove(key);
            }
        }


        public static void Civ_Sort_Of_Yuanneng()
        {
            sortedList.Clear();
            sortedList.AddRange(Globals.Actors.Values.Where(actor => actor != null && actor.race.civilization)
                .OrderByDescending(actor => actor.stats["yuanneng"]));
            var keysToRemove = Globals.Actors
                .Where(pair => pair.Value == null) // 筛选出值为 null 的项
                .Select(pair => pair.Key) // 选择出键
                .ToList();
            foreach (var key in keysToRemove)
            {
                Globals.Actors.Remove(key);
            }
        }

        public static void AllActor_Sort_Of_Yuanneng()
        {
            sortedList.Clear();
            sortedList.AddRange(Globals.Actors.Values.Where(actor => actor != null )
                .OrderByDescending(actor => actor.stats["yuanneng"]));
            var keysToRemove = Globals.Actors
                .Where(pair => pair.Value == null) // 筛选出值为 null 的项
                .Select(pair => pair.Key) // 选择出键
                .ToList();
            foreach (var key in keysToRemove)
            {
                Globals.Actors.Remove(key);
            }
        }
    }

    public static class Sort_key
    {
        public const string default_Age_sort = "age";
        public const string yuanneng_sort = "yuaneng";
        public const string allActor_Age_sort = "allActor_age";
        public const string allActor_yuanneng_sort = "allActor_yuanneng";
    }
}