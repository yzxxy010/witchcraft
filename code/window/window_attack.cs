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
        public static List<Actor> allActors = new List<Actor>();
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

            var closeButton = window.transform.Find("Background/CloseBackgound").gameObject
                .GetComponent<RectTransform>();
            closeButton.localPosition = new Vector3(closeButton.localPosition.x, 130, 0);
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
            drawWindoowUIButton(image);
        }

        public static void OnNormalEnable()
        {
            window.show();
            allActors = World.world.units.getSimpleList();
            sort_age_allActor();
            // 根据单位数量调整内容区域的大小，确保足够容纳所有的 UI 元素。
            RectTransform contentRect = content.GetComponent<RectTransform>();
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, 15 + itemHeight * allActors.Count);
            ScrollRect scrollRect = scrollView.GetComponent<ScrollRect>();
            scrollRect.verticalNormalizedPosition = 1f;
        }

        public static void reOnNormalEnable()
        {
            // 根据单位数量调整内容区域的大小，确保足够容纳所有的 UI 元素。
            RectTransform contentRect = content.GetComponent<RectTransform>();
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, 15 + itemHeight * actors.Count);
            ScrollRect scrollRect = scrollView.GetComponent<ScrollRect>();
            scrollRect.verticalNormalizedPosition = 1f;
            // updateWindow();
        }

        public static void updateWindow()
        {
            ClearContent();
            var curr_position = content.transform.localPosition;
            var view_y_start = curr_position.y; //0
            var view_y_end = curr_position.y + scrollView.GetComponent<RectTransform>().sizeDelta.y;
            //Debug.Log(scrollView.GetComponent<RectTransform>().sizeDelta.y); n早之前测试用的,忘记删掉了
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

        public static void drawWindoowUIButton(GameObject parent)
        {
            #region 底部四个小按钮

            GameObject iconObject = new GameObject("IconButton");
            iconObject.transform.SetParent(parent.transform, false);
            RectTransform iconRect = iconObject.AddComponent<RectTransform>();
            // 添加 Image 组件作为 Button 的背景
            Image buttonImage = iconObject.AddComponent<Image>();
            Color color;
            ColorUtility.TryParseHtmlString("#3e4237", out color);
            buttonImage.color = color;
            // 添加 Button 组件
            Button buttonComponent = iconObject.AddComponent<Button>();
            buttonImage.raycastTarget = true;
            // 配置 RectTransform
            iconRect.pivot = new Vector2(0.5f, 0f);
            iconRect.anchorMin = new Vector2(0.5f, 0f);
            iconRect.anchorMax = new Vector2(0.5f, 0f);
            iconRect.anchoredPosition = new Vector2(-27.2f, 5.6f); //16.4
            iconRect.localScale = Vector3.one;
            iconRect.sizeDelta = new Vector2(9.5f, 4.5f);
            //添加中间的小图标
            GameObject centerImageObject = new GameObject("CenterImage");
            centerImageObject.transform.SetParent(iconObject.transform, false);
            RectTransform centerImageRect = centerImageObject.AddComponent<RectTransform>();
            Image centerImage = centerImageObject.AddComponent<Image>();
            centerImage.sprite = Resources.Load<Sprite>("ui/allActor_Age.png");
            centerImage.raycastTarget = false;
            centerImageRect.anchorMin = new Vector2(0.5f, 0.5f);
            centerImageRect.anchorMax = new Vector2(0.5f, 0.5f);
            centerImageRect.pivot = new Vector2(0.5f, 0.5f);
            centerImageRect.anchoredPosition = Vector2.zero;
            centerImageRect.localScale = new Vector3(0.045f, 0.045f);
            //补充点击
            buttonComponent.onClick.AddListener(() => sort_age_allActor());

            //第二个按钮
            GameObject iconObject2 = new GameObject("IconButton");
            iconObject2.transform.SetParent(parent.transform, false);
            RectTransform iconRect2 = iconObject2.AddComponent<RectTransform>();
            // 添加 Image 组件作为 Button 的背景
            Image buttonImage2 = iconObject2.AddComponent<Image>();
            buttonImage2.color = color;
            // 添加 Button 组件
            Button buttonComponent2 = iconObject2.AddComponent<Button>();
            buttonImage2.raycastTarget = true;
            // 配置 RectTransform
            iconRect2.pivot = new Vector2(0.5f, 0f);
            iconRect2.anchorMin = new Vector2(0.5f, 0f);
            iconRect2.anchorMax = new Vector2(0.5f, 0f);
            iconRect2.anchoredPosition = new Vector2(-16.4f, 5.6f);
            iconRect2.localScale = Vector3.one;
            iconRect2.sizeDelta = new Vector2(9.5f, 4.5f);
            //添加中间的小图标
            GameObject centerImageObject2 = new GameObject("CenterImage");
            centerImageObject2.transform.SetParent(iconObject2.transform, false);
            RectTransform centerImageRect2 = centerImageObject2.AddComponent<RectTransform>();
            Image centerImage2 = centerImageObject2.AddComponent<Image>();
            centerImage2.sprite = Resources.Load<Sprite>("ui/civActor_Age.png");
            centerImage2.raycastTarget = false;
            centerImageRect2.anchorMin = new Vector2(0.5f, 0.5f);
            centerImageRect2.anchorMax = new Vector2(0.5f, 0.5f);
            centerImageRect2.pivot = new Vector2(0.5f, 0.5f);
            centerImageRect2.anchoredPosition = Vector2.zero;
            centerImageRect2.localScale = new Vector3(0.045f, 0.045f);
            //补充点击
            buttonComponent2.onClick.AddListener(() => sort_age_civActor());

            //第三个按钮
            GameObject iconObject3 = new GameObject("IconButton");
            iconObject3.transform.SetParent(parent.transform, false);
            RectTransform iconRect3 = iconObject3.AddComponent<RectTransform>();
            // 添加 Image 组件作为 Button 的背景
            Image buttonImage3 = iconObject3.AddComponent<Image>();
            buttonImage3.color = color;
            // 添加 Button 组件
            Button buttonComponent3 = iconObject3.AddComponent<Button>();
            buttonImage3.raycastTarget = true;
            // 配置 RectTransform
            iconRect3.pivot = new Vector2(0.5f, 0f);
            iconRect3.anchorMin = new Vector2(0.5f, 0f);
            iconRect3.anchorMax = new Vector2(0.5f, 0f);
            iconRect3.anchoredPosition = new Vector2(16.4f, 5.6f);
            iconRect3.localScale = Vector3.one;
            iconRect3.sizeDelta = new Vector2(9.5f, 4.5f);
            //添加中间的小图标
            GameObject centerImageObject3 = new GameObject("CenterImage");
            centerImageObject3.transform.SetParent(iconObject3.transform, false);
            RectTransform centerImageRect3 = centerImageObject3.AddComponent<RectTransform>();
            Image centerImage3 = centerImageObject3.AddComponent<Image>();
            centerImage3.sprite = Resources.Load<Sprite>("ui/allActor_Yuanneng.png");
            centerImage3.raycastTarget = false;
            centerImageRect3.anchorMin = new Vector2(0.5f, 0.5f);
            centerImageRect3.anchorMax = new Vector2(0.5f, 0.5f);
            centerImageRect3.pivot = new Vector2(0.5f, 0.5f);
            centerImageRect3.anchoredPosition = Vector2.zero;
            centerImageRect3.localScale = new Vector3(0.045f, 0.045f);
            //补充点击
            buttonComponent3.onClick.AddListener(() => sort_yuanneng_allActor());

            //第四个按钮
            GameObject iconObject4 = new GameObject("IconButton");
            iconObject4.transform.SetParent(parent.transform, false);
            RectTransform iconRect4 = iconObject4.AddComponent<RectTransform>();
            // 添加 Image 组件作为 Button 的背景
            Image buttonImage4 = iconObject4.AddComponent<Image>();
            buttonImage4.color = color;
            // 添加 Button 组件
            Button buttonComponent4 = iconObject4.AddComponent<Button>();
            buttonImage4.raycastTarget = true;
            // 配置 RectTransform
            iconRect4.pivot = new Vector2(0.5f, 0f);
            iconRect4.anchorMin = new Vector2(0.5f, 0f);
            iconRect4.anchorMax = new Vector2(0.5f, 0f);
            iconRect4.anchoredPosition = new Vector2(27.2f, 5.6f); //16.4
            iconRect4.localScale = Vector3.one;
            iconRect4.sizeDelta = new Vector2(9.5f, 4.5f);
            // 添加中间的小图标
            GameObject centerImageObject4 = new GameObject("CenterImage");
            centerImageObject4.transform.SetParent(iconObject4.transform, false);
            RectTransform centerImageRect4 = centerImageObject4.AddComponent<RectTransform>();
            Image centerImage4 = centerImageObject4.AddComponent<Image>();
            centerImage4.sprite = Resources.Load<Sprite>("ui/civActor_Yuanneng.png");
            centerImage4.raycastTarget = false;
            centerImageRect4.anchorMin = new Vector2(0.5f, 0.5f);
            centerImageRect4.anchorMax = new Vector2(0.5f, 0.5f);
            centerImageRect4.pivot = new Vector2(0.5f, 0.5f);
            centerImageRect4.anchoredPosition = Vector2.zero;
            centerImageRect4.localScale = new Vector3(0.045f, 0.045f);
            // 补充点击
            buttonComponent4.onClick.AddListener(() => sort_yuanneng_civActor());

            #endregion
        }

        public static void sort_age_allActor()
        {
            Debug.Log(allActors.Count);
            actors.Clear();
            actors = allActors.OrderByDescending(actor => actor.getAge()).ToList();
            reOnNormalEnable();
        }

        public static void sort_yuanneng_civActor()
        {
            actors.Clear();
            actors = allActors.Where(actor => actor.race.civilization)
                .OrderByDescending(actor => actor.stats["yuanneng"]).ToList();
            reOnNormalEnable();
        }

        public static void sort_age_civActor()
        {
            actors.Clear();
            actors = allActors.Where(actor => actor.race.civilization).OrderByDescending(actor => actor.getAge())
                .ToList();
            reOnNormalEnable();
        }

        public static void sort_yuanneng_allActor()
        {
            actors.Clear();
            actors = allActors
                .OrderByDescending(actor => actor.stats["yuanneng"]).ToList();
            reOnNormalEnable();
        }
    }
}