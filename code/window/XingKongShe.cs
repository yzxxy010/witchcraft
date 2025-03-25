using System.IO;
using NeoModLoader.General;
using UnityEngine;
using UnityEngine.UI;

namespace VideoCopilot.code.window;

public class XingKongShe
{
    public static ScrollWindow window;
    public static GameObject content;
    public static Text textComponent;

    public static bool Initialized = false;

    public static void Init()
    {
        Initialized = true;
        window = WindowCreator.CreateEmptyWindow("xingkong", "xingkong");
        var scrollView =
            GameObject.Find(
                $"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View");
        scrollView.gameObject.SetActive(true);
        content = GameObject.Find(
            $"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View/Viewport/Content");


            Draw_Text();
    }

    private static void Draw_Text()
    {
        GameObject textObject = new GameObject("actorText");
        textObject.transform.SetParent(content.transform);
        textComponent = textObject.AddComponent<Text>();
        textComponent.text = $"<b><color=#ff4757>本mod由星空社提供</color></b>" +
                             $"\n<color=#1abc9c>欢迎大家加入星空社</color>" +
                             $"\nQQ群:941774543" +
                             $"\n星空社期待你的到来!";
        textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        textComponent.fontSize = 50;
        textComponent.color = Color.white;
        textComponent.alignment = TextAnchor.MiddleCenter;
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(800, 600);
        rectTransform.localPosition = new Vector3(130, -65);
        textComponent.raycastTarget = false;

        //在文字下方插一个图片
        GameObject imageObject = new GameObject("actorImage");
        imageObject.transform.SetParent(content.transform);
        Image imageComponent = imageObject.AddComponent<Image>();
        imageComponent.sprite = Resources.Load<Sprite>("ui/qun.jpg");
        imageComponent.preserveAspect = true;
        imageComponent.raycastTarget = false;
        rectTransform = imageObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(800, 600);
        rectTransform.localPosition = new Vector3(130, -172);
        rectTransform.localScale = new Vector3(0.2f, 0.2f, 1);
        imageComponent.color = Color.white;

    }

    public static void showWindow()
    {
        if (!Initialized)
        {
            Init();
        }

        OnNormalEnable();
    }

    public static void OnNormalEnable()
    {
        window.show();
    }
}
