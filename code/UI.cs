using NCMS.Utils;
using NeoModLoader.General;
using NeoModLoader.General.UI.Tab;
using System.Collections.Generic;
using InterestingTrait.code.window;
using UnityEngine;
using UnityEngine.UI;

namespace InterestingTrait.code;

public class UI
{
    public const string INFO = "info";
    public const string DISPLAY = "display";
    public static PowersTab tab;

    public static void Init()
    {
        tab = TabManager.CreateTab("tab_witchcraft" ,"tab_witchcraft", "hotkey_tip_tab_other",Resources.Load<Sprite>("ui/TabIcon.png"));
        tab.SetLayout(new List<string>()
        {
            INFO, DISPLAY
        });

        _addButtons();

        tab.UpdateLayout();
    }

    private static void _addButtons()
    {
        
        var buttons = PowerButtonCreator.CreateSimpleButton("windowAttack",
            ()=>{WindowAttack.ShowWindow();}, Resources.Load<Sprite>("ui/openAttackWindow.png"));
        tab.AddPowerButton(INFO,buttons );

    }
    
}