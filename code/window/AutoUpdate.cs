using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using NeoModLoader.General;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace VideoCopilot.code.window
{
    public class AutoUpdate : MonoBehaviour
    {
        public static ScrollWindow window;
        public static GameObject content;
        public static Text textComponent;


        private static readonly string UpdateCheckUrl =
            "https://gh.tryxd.cn/https://raw.githubusercontent.com/yzxxy010/witchcraft/master/mod.json?t=" +
            DateTime.UtcNow.Ticks; //走的cdn,理论上国内可以成功访问!

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

        private static async Task<bool> CheckForUpdates()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(UpdateCheckUrl);
                if (!string.IsNullOrEmpty(response))
                {
                    // 解析 JSON
                    JObject jsonResponse = JObject.Parse(response);
                    remoteVersion = jsonResponse["version"]?.ToString();

                    if (string.IsNullOrEmpty(remoteVersion))
                    {
                        Debug.Log("未能获取到版本信息");
                        return false;
                    }

                    Debug.Log($"远程版本: {remoteVersion}, 当前版本: {currentVersion}");

                    if (CompareVersions(remoteVersion, currentVersion) > 0)
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
            textComponent.text = $"<color=#FF9B1C>当前版本:</color>\t" +
                                 $"{currentVersion}\t" +
                                 $"<color=#FF9B1C>最新版本:</color>\t" +
                                 $"{remoteVersion}\n" +
                                 // $"当前mod已经有了新版本\n如果你的设备可以\n" +
                                 // $"<b><color=#ff0000>直接连接GitHub下载</color></b>\n" +
                                 $"当前mod已经有了新版本" +
                                 $"如若更新,请前往q群:838697221\n" +
                                 $"GitHub项目(国内无法访问):\nhttps://github.com/yzxxy010/witchcraft";
            // $"\n<b><color=#ff0000>否则</color></b>请自行更新,此页面可以关闭";
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
            buttonComponent.onClick.AddListener(() => { Application.OpenURL("https://325477.xyz");});
        }
    }
}