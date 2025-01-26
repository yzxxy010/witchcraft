using System.Collections.Generic;
using NeoModLoader.api.attributes;
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

        // 存储文本组件的列表
        private static List<Text> valueTextComponents = new List<Text>();
        private static List<Text> labelTextComponents = new List<Text>();

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
            // 清空旧组件引用
            valueTextComponents.Clear();
            labelTextComponents.Clear();

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
            DrawText(baseImage);
        }
        
        public static void DrawText(GameObject parent)
        {
            for (int i = 0; i < dataList.Count; i++)
            {
                DrawYText_Label(parent, i);
                DrawYText_Value(parent, i);
            }
        }
        
        public static void DrawYText_Label(GameObject parent, int index)
        {
            GameObject gameObject = new GameObject("UiPanelInfoText");
            gameObject.transform.SetParent(parent.transform);
    
            RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = new Vector2(300, 30);
            rectTransform.anchorMin = new Vector2(0f, 0.5f);
            rectTransform.anchorMax = new Vector2(0f, 0.5f);
            rectTransform.pivot = new Vector2(0f, 0.5f);
            rectTransform.localPosition = new Vector3(-48, 20-(index * 8));
            rectTransform.localScale = new Vector3(0.24f, 0.24f);

            Text text = gameObject.AddComponent<Text>();
            text.text = dataList[index].Label + ": ";
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = 24;
            text.color = Color.white;
            text.alignment = TextAnchor.UpperLeft;
            
            labelTextComponents.Add(text);
        }

        public static void DrawYText_Value(GameObject parent, int index)
        {
            GameObject gameObject = new GameObject("UiPanelInfoText");
            gameObject.transform.SetParent(parent.transform);
    
            RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = new Vector2(300, 30);
            rectTransform.anchorMin = new Vector2(1f, 0.5f);
            rectTransform.anchorMax = new Vector2(1f, 0.5f);
            rectTransform.pivot = new Vector2(1f, 0.5f);
            rectTransform.localPosition = new Vector3(48, 20-(index*8));
            rectTransform.localScale = new Vector3(0.24f, 0.24f);

            Text text = gameObject.AddComponent<Text>();
            text.text = dataList[index].Value;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontSize = 20;
            text.color = Color.yellow;
            text.alignment = TextAnchor.MiddleRight;
            
            valueTextComponents.Add(text);
        }

        public static void UpdateText()
        {
            dataList[0].Value = Globals.Tsotw.ToString();
            dataList[1].Value = Globals.TsotwAdd.ToString();

            // 更新所有文本组件
            for (int i = 0; i < valueTextComponents.Count; i++)
            {
                if (i < dataList.Count)
                {
                    valueTextComponents[i].text = dataList[i].Value;
                }
            }
        }
    }
}