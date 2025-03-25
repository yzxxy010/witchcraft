using NCMS.Utils;
using NeoModLoader.General;
using NeoModLoader.General.UI.Tab;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VideoCopilot.code.window;

namespace VideoCopilot.code;

public class UI
{
    public const string INFO = "info";
    public const string DISPLAY = "display";
    public static PowersTab tab;

    public static void Init()
    {
        tab = TabManager.CreateTab("witchcraft", "tab_witchcraft", "hotkey_tip_tab_other",
                                   Resources.Load<Sprite>("ui/TabIcon.png"));
        tab.SetLayout(new List<string>()
        {
            INFO, DISPLAY
        });

        _addButtons();
        tab.UpdateLayout();
    }

    private static void _addButtons()
    {
        var XingKongShe = PowerButtonCreator.CreateSimpleButton("windowXingKongShe",
                                                                () => { window.XingKongShe.showWindow(); },
                                                                Resources.Load<Sprite>("ui/xingkonglogo.jpg"));
        var buttons = PowerButtonCreator.CreateSimpleButton("windowAttack",
                                                            () => { window_attack.showWindow(); },
                                                            Resources.Load<Sprite>("ui/openAttackWindow.png"));
        tab.AddPowerButton(INFO, XingKongShe);
        tab.AddPowerButton(INFO, buttons);
    }
}