using ReflectionUtility;
using UnityEngine;
using System.Collections.Generic;


namespace VideoCopilot.code.window;

public class WindowManager
{
    public static void Init()
    {
        AutoUpdate.Init();
        window_init();
        window_attack.Init(); 
        UiPanelInfo.Init();
        
    }

    public static void window_init()
    {
        Dictionary<string, ScrollWindow> allWindows = (Dictionary<string, ScrollWindow>)Reflection.GetField(typeof(ScrollWindow), null, "allWindows");
        Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "inspect_unit");
        allWindows["inspect_unit"].gameObject.SetActive(false);
        Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "village");
        allWindows["village"].gameObject.SetActive(false);
        Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "debug");
        allWindows["debug"].gameObject.SetActive(false);
        Reflection.CallStaticMethod(typeof(ScrollWindow), "checkWindowExist", "kingdom");
        allWindows["kingdom"].gameObject.SetActive(false);
    }
}