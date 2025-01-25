using System.Collections.Generic;
using NeoModLoader.General;
using UnityEngine;
using UnityEngine.UI;
using NeoModLoader.General.UI.Tab;

namespace VideoCopilot.code.window
{
    public class UiPanelInfo
    {
        public static GameObject tabObj = UI.tab.gameObject;
        private static List<EntryData> dataList = new List<EntryData>()
        {
            new EntryData(LM.Get("TSOTW"), Globals.Tsotw.ToString()),
            new EntryData(LM.Get("TSOTW_ADD Description"),Globals.TsotwAdd.ToString())
        };

        public class EntryData
        {
            public string Label;
            public string Value;

            public EntryData(string label, string value)
            {
                Label = label;
                Value = value;
            }
        }

        public static void Init()
        {
            GameObject baseImage = new GameObject("UiPanelInfoImage");
            baseImage.transform.SetParent(tabObj.transform);
            RectTransform imageRect = baseImage.AddComponent<RectTransform>();
            Image image = baseImage.AddComponent<Image>();
            image.sprite = Resources.Load<Sprite>("ui/UiPanelInfo.png"); 
            image.preserveAspect = true;
            imageRect.pivot = new Vector2(0.5f, 0.5f);
            imageRect.anchorMin = new Vector2(0.5f, 0.5f);
            imageRect.anchorMax = new Vector2(0.5f, 0.5f);
            imageRect.localPosition = new Vector3(210, 0, 0);
            imageRect.localScale = new Vector3(1.3f, 1.3f);
        }
        
       
    }
}