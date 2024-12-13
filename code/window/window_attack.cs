using System.Collections.Generic;
using NeoModLoader.General;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime;
using System.Linq;
using System.Runtime.Serialization;
using VideoCopilot.code.window.prafab;

namespace VideoCopilot.code.window
{
    public class window_attack : MonoBehaviour
    {
        public static List<Actor> actors = new List<Actor>();


        public static GameObject avatarRef = GameObject.Find(
            $"Canvas Container Main/Canvas - Windows/windows/inspect_unit/Background/Scroll View/Viewport/Content/Part 1/BackgroundAvatar");

        private static ScrollWindow window;
        private static GameObject scrollView;
        public static GameObject content;
        public static bool Initialized = false;
        public static float itemHeight = 35f;
        
        public static void Init()
        {
            Initialized = true;
            window = WindowCreator.CreateEmptyWindow("tilesWindow", "windowAttack");
            scrollView = window.transform.GetChild(0).Find("Scroll View").gameObject;
            scrollView.SetActive(true);
            content = window.transform.Find("Background/Scroll View/Viewport/Content").gameObject;

            ScrollRect scrollRect = scrollView.GetComponent<ScrollRect>();
            scrollRect.onValueChanged.AddListener((v2) => { updateWindow(); });
            drawWindowUI();
        }


        public static void drawWindowUI()
        {
            GameObject image = new GameObject();
            image.transform.SetParent(window.transform.GetChild(0).transform);
            RectTransform rectTransform = image.AddComponent<RectTransform>();
            Image imageComponent = image.AddComponent<Image>();
            Sprite imageSprite = Resources.Load<Sprite>("ui/attackWindow.png");
            imageComponent.sprite = imageSprite;
            imageComponent.raycastTarget = false;
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            rectTransform.localScale = new Vector3(2.4f, 2.7f, 0f);
            rectTransform.localPosition = new Vector3(0f, -1f, 0f);
        }

        public static void OnNormalEnable()
        {
            actors = World.world.units.getSimpleList();
            // 根据单位数量调整内容区域的大小，确保足够容纳所有的 UI 元素。
            RectTransform contentRect = content.GetComponent<RectTransform>();
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, 15 + itemHeight * actors.Count);
            ScrollRect scrollRect = scrollView.GetComponent<ScrollRect>();
            scrollRect.verticalNormalizedPosition = 1f;
            window.show();
        }

        public static void updateWindow()
        {
            ClearContent();
            var curr_position = content.transform.localPosition;
            var view_y_start = curr_position.y; //0
            var view_y_end = curr_position.y + scrollView.GetComponent<RectTransform>().sizeDelta.y;
            Debug.Log(scrollView.GetComponent<RectTransform>().sizeDelta.y);
            var view_start_idx = (int)(view_y_start / itemHeight);
            var view_end_idx = Math.Min((int)(view_y_end / itemHeight) + 1, actors.Count - 1);
            // var view_end_idx = (int)(view_y_end / itemHeight);测试用的
            for (int i = view_start_idx; i <= view_end_idx; i++)
            {
                GameObject prefab = AttackPrefab.InstantiatePrefab(content.transform, actors[i]);
                prefab.transform.localPosition = new(prefab.transform.localPosition.x, -15 - i * itemHeight);
            }
        }

        public static void showWindow()
        {
            if (!Initialized)
            {
                Init();
            }

            OnNormalEnable();
        }

        public static void ClearContent()
        {
            foreach (Transform child in content.transform)

            {
                Destroy(child.gameObject);
            }
        }
    }
}