using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using NeoModLoader.General;
using System;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace VideoCopilot.code.window
{
    public class AutoUpdate : MonoBehaviour
    {
        public static ScrollWindow window;
        public static GameObject content;
        public static Text textComponent;


        // 当前版本号
        public static string currentVersion = VideoCopilotClass.modDeclare.Version;
        public static string remoteVersion;
        public static string parentPath;

        public static async void Init()
        {
            parentPath = Directory.GetParent(VideoCopilotClass.modDeclare.FolderPath).FullName;
            window = WindowCreator.CreateEmptyWindow("AutoUpdateWindow", "AutoUpdateWindow");
            var scrollView =
                GameObject.Find(
                    $"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View");
            scrollView.gameObject.SetActive(true);
            content = GameObject.Find(
                $"/Canvas Container Main/Canvas - Windows/windows/{window.name}/Background/Scroll View/Viewport/Content");

            bool isUpdateAvailable = await CheckForUpdates();
            if (isUpdateAvailable)
            {
                Draw_Text();
                Draw_RedButton();
                window.show();
            }
        }
        private static readonly string[] DnsUrls =
        {
            "https://dns.alidns.com/resolve?name={0}&type=TXT",
            "https://dns.google/resolve?name={0}&type=TXT"
        };

        public static async Task<string> ResolveTxtRecord(string domain)
        {
            foreach (var url in DnsUrls)
            {
                string formattedUrl = string.Format(url, domain);
                string data = await GetTxtRecord(formattedUrl);
                if (data != null)
                {
                    return data;
                }
            }
            return null;
        }

        private static async Task<string> GetTxtRecord(string url)
        {
            using HttpClient client = new HttpClient();
            using CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
            try
            {
                string response = await client.GetStringAsync(url);
                JObject json = JObject.Parse(response);
            
                if (json["Status"]?.ToObject<int>() == 0)
                {
                    JArray answers = (JArray)json["Answer"];
                    if (answers != null && answers.Count > 0)
                    {
                        foreach (JObject answer in answers)
                        {
                            string data = answer["data"]?.ToString().Trim('"');
                            if (!string.IsNullOrEmpty(data))
                            {
                                return data;
                            }
                        }
                    }
                }
            }
            catch (TaskCanceledException)
            {
                return null; // 超时返回 null
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }


        private static async Task<bool> CheckForUpdates()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await ResolveTxtRecord("witchcraft.325477.xyz");
                if (!string.IsNullOrEmpty(response))
                {
                    if (string.IsNullOrEmpty(response))
                    {
                        Debug.Log("未能获取到版本信息");
                        return false;
                    }

                    remoteVersion = response;
                    Debug.Log($"远程版本: {response}, 当前版本: {currentVersion}");

                    if (CompareVersions(response, currentVersion) > 0)
                    {
                        Debug.Log("有新版本可用");

                        return true;
                    }
                    else
                    {
                        Debug.Log("当前版本已是最新");
                        return false;
                    }
                }
            }

            return false;
        }

        private static int CompareVersions(string remoteVersion, string currentVersion)
        {
            var remoteParts = remoteVersion.Split('.');
            var currentParts = currentVersion.Split('.');

            for (int i = 0; i < Math.Min(remoteParts.Length, currentParts.Length); i++)
            {
                int remote = int.Parse(remoteParts[i]);
                int current = int.Parse(currentParts[i]);

                if (remote > current)
                    return 1;
                if (remote < current)
                    return -1;
            }

            return 0;
        }

        private static void Draw_Text()
        {
            GameObject textObject = new GameObject("actorText");
            textObject.transform.SetParent(content.transform);
            textComponent = textObject.AddComponent<Text>();
            textComponent.text = $"{LM.Get("update_txt_1")}{currentVersion}\t" +
                                 $"{LM.Get("update_txt_2")}{remoteVersion}\n" +
                                 $"{LM.Get("update_txt_3")}" +
                                 $"{LM.Get("update_txt_4")}838697221\n" +
                                 $"{LM.Get("update_txt_5")}\n" +
                                 $"{LM.Get("update_txt_6")}";
            textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            textComponent.fontSize = 50;
            textComponent.color = Color.white;
            textComponent.alignment = TextAnchor.MiddleCenter;
            RectTransform rectTransform = textObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(800, 600);
            rectTransform.localPosition = new Vector3(130, -65);
            textComponent.raycastTarget = false;
        }

        private static void Draw_RedButton()
        {
            // 创建按钮对象
            GameObject buttonObject = new GameObject("DownloadButton");
            buttonObject.transform.SetParent(content.transform);

            // 添加按钮组件
            Button buttonComponent = buttonObject.AddComponent<Button>();
            Image buttonImage = buttonObject.AddComponent<Image>();

            // 设置按钮的颜色
            buttonImage.color = new Color(255, 0, 0);

            // 设置按钮大小和位置
            RectTransform rectTransform = buttonObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(400, 80);
            rectTransform.localPosition = new Vector3(130, -200);

            // 添加文字到按钮
            GameObject textObject = new GameObject("ButtonText");
            textObject.transform.SetParent(buttonObject.transform);
            Text textComponent = textObject.AddComponent<Text>();
            // textComponent.text = "<b><color=#ffffff>GitHub一键下载</color></b>";
            textComponent.text = "<b><color=#ffffff>前往GitHub下载</color></b>";
            textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            textComponent.fontSize = 40;
            textComponent.alignment = TextAnchor.MiddleCenter;
            textComponent.color = Color.white;

            // 调整文字的大小和位置
            RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
            textRectTransform.sizeDelta = rectTransform.sizeDelta; // 与按钮大小一致
            textRectTransform.localPosition = Vector3.zero;

            // 添加按钮点击事件
            buttonComponent.onClick.AddListener(
                () => { Application.OpenURL("https://github.com/yzxxy010/witchcraft"); });
        }
    }
}