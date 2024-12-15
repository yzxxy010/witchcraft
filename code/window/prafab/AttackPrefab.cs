using NeoModLoader.General;
using UnityEngine;
using UnityEngine.UI;
using VideoCopilot.code.utils;

namespace VideoCopilot.code.window.prafab;

public class AttackPrefab : MonoBehaviour
{
    public static GameObject InstantiatePrefab(Transform parent, Actor actor)
    {
        // 创建根对象
        GameObject rootObject = new GameObject("AttackPrefab");
        rootObject.transform.SetParent(parent, false);

        // 设置 RectTransform
        RectTransform rectTransform = rootObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(180, 30);
        rectTransform.pivot = new Vector2(0.5f, 1f);
        rectTransform.anchorMin = new Vector2(0.5f, 1f);
        rectTransform.anchorMax = new Vector2(0.5f, 1f);
        rectTransform.anchoredPosition = new Vector2(0, -15);

        #region 单位头像设置

        // 添加单位他头像
        GameObject actorImage = Instantiate(window_attack.avatarRef, rootObject.transform);
        var avatarElement = actorImage.GetComponent<UiUnitAvatarElement>();
        avatarElement.show_banner_clan = true;
        avatarElement.show_banner_kingdom = true;
        avatarElement.show(actor);
        RectTransform GORect = actorImage.GetComponent<RectTransform>();
        GORect.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        GORect.pivot = new Vector2(0f, 0.5f);
        GORect.anchorMin = new Vector2(0f, 0.5f);
        GORect.anchorMax = new Vector2(0f, 0.5f);
        GORect.anchoredPosition = new Vector2(0, 0f);

        Button GOButton = actorImage.AddComponent<Button>();
        GOButton.onClick.AddListener(() => showActor(actor));

        actorImage.SetActive(true);


        // 添加背景图片
        GameObject ActorImageBackground = new GameObject("ActorImage");
        ActorImageBackground.transform.SetParent(actorImage.transform, false);
        Image backgroundImage = ActorImageBackground.AddComponent<Image>();
        backgroundImage.sprite = Resources.Load<Sprite>("ui/buttonBackground.png");
        backgroundImage.preserveAspect = true;

        // 设置背景 RectTransform
        RectTransform backgroundRect = ActorImageBackground.GetComponent<RectTransform>();
        backgroundRect.pivot = new Vector2(0.5f, 0.5f);
        backgroundRect.anchorMin = new Vector2(0.5f, 0.5f);
        backgroundRect.anchorMax = new Vector2(0.5f, 0.5f);
        backgroundRect.localScale = new Vector3(0.4f, 0.4f, 0f);

        #endregion

        #region 单位打开窗口

        //创建按钮
        GameObject buttonObject = new GameObject("AttackButton");
        buttonObject.transform.SetParent(rootObject.transform, false);

        //设置按钮 RectTransform
        RectTransform buttonRect = buttonObject.AddComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(30, 30);
        buttonRect.anchoredPosition = new Vector2(0, 0);

        //添加按钮组件和样式
        Button attackButton = buttonObject.AddComponent<Button>();
        Image buttonImage = buttonObject.AddComponent<Image>();

        //添加背景图片
        buttonImage.sprite = Resources.Load<Sprite>("ui/buttonBackground2.png");
        buttonImage.preserveAspect = true;
        //设置背景 RectTransform
        RectTransform buttonbackgroundRect = buttonObject.GetComponent<RectTransform>();
        buttonbackgroundRect.pivot = new Vector2(0.5f, 0.5f);
        buttonbackgroundRect.anchorMin = new Vector2(0.5f, 0.5f);
        buttonbackgroundRect.anchorMax = new Vector2(0.5f, 0.5f);
        buttonbackgroundRect.anchoredPosition = new Vector2(75, 0);
        buttonbackgroundRect.localScale = Vector3.one;

        //在按钮上添加图片
        GameObject iconObject = new GameObject("IconImage");
        iconObject.transform.SetParent(buttonImage.transform, false);
        RectTransform iconRect = iconObject.AddComponent<RectTransform>();
        Image iconImage = iconObject.AddComponent<Image>();
        iconImage.sprite = Resources.Load<Sprite>("ui/openActorButton.png");
        iconImage.preserveAspect = true;
        iconRect.pivot = new Vector2(0.5f, 0.5f);
        iconRect.anchorMin = new Vector2(0.5f, 0.5f);
        iconRect.anchorMax = new Vector2(0.5f, 0.5f);
        iconRect.anchoredPosition = new Vector2(0, 0);
        iconRect.localScale = new Vector3(0.2f, 0.2f);
        iconImage.raycastTarget = false;

        //内置按钮事件
        attackButton.onClick.AddListener(() => showActor(actor));

        #endregion

        #region 文本显示区域

        GameObject textBackground = new GameObject("textBackground");
        textBackground.transform.SetParent(rootObject.transform, false);
        Image textBackgroundImage = textBackground.AddComponent<Image>();
        textBackgroundImage.sprite = Resources.Load<Sprite>("ui/textBackground.png");
        textBackgroundImage.preserveAspect = true;

        // 设置背景 RectTransform
        RectTransform textBackgroundRect = textBackground.GetComponent<RectTransform>();
        textBackgroundRect.pivot = new Vector2(0.5f, 0.5f);
        textBackgroundRect.anchorMin = new Vector2(0.5f, 0.5f);
        textBackgroundRect.anchorMax = new Vector2(0.5f, 0.5f);
        textBackgroundRect.localScale = new Vector3(1.2f, 1f, 0f);

        // 创建文本组件并将其设置为背景的子物体
        GameObject textObject = new GameObject("TextObject");
        textObject.transform.SetParent(textBackground.transform, false);
        Text textComponent = textObject.AddComponent<Text>();

        // 设置文本属性
        textComponent.text = $"<color=#FF9B1C>姓名:</color>\t{actor.getName()}\n" +
                             $"<color=#FF9B1C>年龄:</color>\t{actor.getAge()}\n" +
                             $"<color=#FF9B1C>{LM.Get("yuanneng")}:</color>\t{(int)actor.GetYuanNeng()}";
        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textComponent.fontSize = 6;
        textComponent.alignment = TextAnchor.MiddleLeft;
        textComponent.color = Color.white;
        textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
        textComponent.verticalOverflow = VerticalWrapMode.Truncate;

        // 获取文本 RectTransform，调整其大小以适应背景
        RectTransform textRect = textComponent.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(1f, 0.5f);
        textRect.anchorMax = new Vector2(1f, 0.5f);
        textRect.pivot = new Vector2(1f, 0.5f);
        textRect.anchoredPosition = new Vector2(2, 0);
        // textRect.sizeDelta = new Vector2(80, 35);

        #endregion

        return rootObject;
    }


    public static void showActor(Actor actor)
    {
        Config.selectedUnit = actor;
        ScrollWindow.showWindow("inspect_unit");
    }
}

