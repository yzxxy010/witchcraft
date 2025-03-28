using System.Linq;
using System.Net.Mime;
using NeoModLoader.api.attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VideoCopilot.code.utils;

namespace VideoCopilot.code.window;

public class WindowCreatureInfoPatchHelper
{
    public static CityIcon statIcon;

    public static void Initialize(WindowCreatureInfo window)
    {
        window.gameObject.transform.Find("Background/Scroll View").GetComponent<ScrollRect>().enabled = true;
        window.gameObject.transform.Find("Background/Scroll View/Viewport").GetComponent<Mask>().enabled = true;
        window.gameObject.transform.Find("Background/Scroll View/Viewport").GetComponent<Image>().enabled = true;
        Transform content_transform = window.gameObject.transform.Find("Background/Scroll View/Viewport/Content");
        VerticalLayoutGroup vert_layout_group = content_transform.GetComponent<VerticalLayoutGroup>() ??
                                                content_transform.gameObject.AddComponent<VerticalLayoutGroup>();
        ContentSizeFitter fitter = content_transform.GetComponent<ContentSizeFitter>() ??
                                   content_transform.gameObject.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.MinSize;
        vert_layout_group.childControlHeight = false;
        vert_layout_group.childControlWidth = false;
        vert_layout_group.childForceExpandHeight = false;
        vert_layout_group.childForceExpandWidth = false;
        vert_layout_group.childScaleHeight = false;
        vert_layout_group.childScaleWidth = false;
        vert_layout_group.spacing = 2;

        var bars_rect = window.attackSpeed.transform.parent.GetComponent<RectTransform>();
        fitter = bars_rect.gameObject.AddComponent<ContentSizeFitter>();
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        fitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
        GridLayoutGroup group = window.attackSpeed.transform.parent.GetComponent<GridLayoutGroup>();
        group.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        group.constraintCount = 2;

        statIcon = window.damage;
        CreatCityIcon();
    }


    public static void OnEnable(WindowCreatureInfo window)
    {
        Actor actor = window.actor;
        statIcons[0].cityIcon.setValue(actor.GetYuanNeng());
        statIcons[1].cityIcon.setValue(actor.GetMeditation());
        statIcons[2].cityIcon.setValue(actor.GetResurrection());
        statIcons[3].cityIcon.setValue(actor.GetRresurrection());
        statIcons[4].cityIcon.setValue(actor.stats["Accuracy"]);
        statIcons[5].cityIcon.setValue(actor.stats["Dodge"]);
    }


    public static StatIcon[] statIcons = new StatIcon[]
    {
        new StatIcon()
        {
            name = "YuanNeng",
            iconPath = "ui/YuanNeng.png",
            tip = "源能",
            isShow = true
        },
        new StatIcon()
        {
            name = "Meditation",
            iconPath = "ui/Meditation.png",
            tip = "无根之源",
            isShow = true
        },
        new StatIcon()
        {
            name = "Resurrection",
            iconPath = "ui/Resurrection.png",
            tip = "复活次数",
            isShow = true
        },
        new StatIcon()
        {
            name = "Rresurrection",
            iconPath = "ui/Rresurrection.png",
            tip = "轮回记录",
            isShow = true
        },
        new StatIcon()
        {
            name = "Accuracy",
            iconPath = "ui/Accuracy.png",
            tip = "命中",
            isShow = true
        },
        new StatIcon()
        {
            name = "Dodge",
            iconPath = "ui/Dodge.png",
            tip = "闪避",
            isShow = true
        },
        new StatIcon()
        {
            name = "占位6",
            iconPath = "ui/civActor_Meditation.png",
            tip = "悬浮文本",
            isShow = false
        },
        new StatIcon()
        {
            name = "占位7",
            iconPath = "ui/civActor_Meditation.png",
            tip = "悬浮文本",
            isShow = false
        },
        new StatIcon()
        {
            name = "占位8",
            iconPath = "ui/civActor_Meditation.png",
            tip = "悬浮文本",
            isShow = false
        }
    };


    public static void CreatCityIcon()
    {
        foreach (StatIcon stat in statIcons)
        {
            if (stat.isShow)
            {
                var temp = Object.Instantiate(statIcon, statIcon.transform.parent);
                temp.name = stat.name;
                stat.cityIcon = temp;
                var tmpImage = temp.transform.Find("Icon");
                tmpImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(stat.iconPath);
                temp.transform.GetComponent<TipButton>().textOnClick = stat.tip;
            }
            else
            {
                var temp = Object.Instantiate(statIcon, statIcon.transform.parent);
                temp.name = stat.name;
                stat.cityIcon = temp;
                temp.transform.Find("Icon").gameObject.SetActive(false);
                temp.transform.Find("Text").gameObject.SetActive(false);
                temp.transform.GetComponent<Image>().sprite = Resources.Load<Sprite>("actors/base_head/head_0.png");
                GameObject.Destroy(temp.transform.GetComponent<TipButton>());
            }
        }
    }
}

public class StatIcon
{
    public string name;
    public string iconPath;
    public string tip;
    public bool isShow = true;
    public CityIcon cityIcon;
}