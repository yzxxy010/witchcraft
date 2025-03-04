using System.Linq;
using NeoModLoader.api.attributes;
using UnityEngine;
using UnityEngine.UI;
using VideoCopilot.code.utils;

namespace VideoCopilot.code.window;

public class WindowCreatureInfoPatchHelper
{
    public static CityIcon attack;

    public static void Initialize(WindowCreatureInfo window)
    {
        window.gameObject.transform.Find("Background/Scroll View").GetComponent<ScrollRect>().enabled = true;
        window.gameObject.transform.Find("Background/Scroll View/Viewport").GetComponent<Mask>().enabled = true;
        window.gameObject.transform.Find("Background/Scroll View/Viewport").GetComponent<Image>().enabled = true;
        Transform content_transform = window.gameObject.transform.Find("Background/Scroll View/Viewport/Content");
        VerticalLayoutGroup vert_layout_group = content_transform.GetComponent<VerticalLayoutGroup>() ??
                                                content_transform.gameObject.AddComponent<VerticalLayoutGroup>();
        vert_layout_group.childControlHeight = false;
        vert_layout_group.childControlWidth = false;
        vert_layout_group.childForceExpandHeight = false;
        vert_layout_group.childForceExpandWidth = false;
        vert_layout_group.childScaleHeight = false;
        vert_layout_group.childScaleWidth = false;
        vert_layout_group.spacing = 4;

        var contentFitter = content_transform.gameObject.AddComponent<ContentSizeFitter>();
        contentFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;

        GridLayoutGroup group = window.attackSpeed.transform.parent.GetComponent<GridLayoutGroup>();
        group.constraintCount = 2;

        CityIcon attack = Object.Instantiate(window.damage, window.attackSpeed.transform.parent);
        Object.Instantiate(window.damage, window.attackSpeed.transform.parent);
        Object.Instantiate(window.damage, window.attackSpeed.transform.parent);
        Object.Instantiate(window.damage, window.attackSpeed.transform.parent);
        Object.Instantiate(window.damage, window.attackSpeed.transform.parent);
        Object.Instantiate(window.damage, window.attackSpeed.transform.parent);
        Object.Instantiate(window.damage, window.attackSpeed.transform.parent);
        Object.Instantiate(window.damage, window.attackSpeed.transform.parent);
        Object.Instantiate(window.damage, window.attackSpeed.transform.parent);
        attack.setValue(window.actor.GetYuanNeng());
    }


    public static void OnEnable(WindowCreatureInfo window)
    {
        Actor actor = window.actor;
    }
}