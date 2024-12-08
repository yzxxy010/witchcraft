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
            "https://cdn.jsdmirror.com/gh/yzxxy010/witchcraft/mod.json"; //走的cdn,理论上国内可以成功访问!

        // 当前版本号
        public static string currentVersion = VideoCopilotClass.modDeclare.Version;
        public static string remoteVersion;
        public static string parentPath;

        public static async void Init()
        {
            DeleteFile();
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
                                 $"当前mod已经有了新版本\n如果你的设备可以\n" +
                                 $"<b><color=#ff0000>直接连接GitHub下载</color></b>\n" +
                                 $"请点击下方的更新" +
                                 $"\n<b><color=#ff0000>否则</color></b>请自行更新,此页面可以关闭";
            textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            textComponent.fontSize = 50;
            textComponent.color = Color.white;
            textComponent.alignment = TextAnchor.MiddleCenter;
            RectTransform rectTransform = textObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(800, 600);
            rectTransform.localPosition = new Vector3(130, -45);
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
            textComponent.text = "<b><color=#ffffff>GitHub一键下载</color></b>";
            textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            textComponent.fontSize = 40;
            textComponent.alignment = TextAnchor.MiddleCenter;
            textComponent.color = Color.white;

            // 调整文字的大小和位置
            RectTransform textRectTransform = textObject.GetComponent<RectTransform>();
            textRectTransform.sizeDelta = rectTransform.sizeDelta; // 与按钮大小一致
            textRectTransform.localPosition = Vector3.zero;

            // 添加按钮点击事件
            buttonComponent.onClick.AddListener(() =>
            {

                string downloadUrl = "https://api.github.com/repos/yzxxy010/witchcraft/zipball"; 
                string savePath = Path.Combine(Directory.GetParent(VideoCopilotClass.modDeclare.FolderPath).FullName, "witchcraft_latest.zip");
                // 开始下载文件
                content.AddComponent<AutoUpdate>().StartCoroutine(DownloadFileCoroutine(downloadUrl, savePath));
            });
        }

        private static IEnumerator DownloadFileCoroutine(string downloadUrl, string savePath)
        {
            
            // 创建异步任务并使用协程等待
            Task downloadTask = DownloadFileAsync(downloadUrl, savePath);

            GameObject buttonObject = GameObject.Find("DownloadButton");
            if (buttonObject != null)
            {
                Destroy(buttonObject);
            }
            var closeButton = GameObject.Find("CloseBackgound");
            if (closeButton != null)
            {
                Destroy(closeButton);
            }
            
            while (!downloadTask.IsCompleted)
            {
                
                yield return null;
            }

            if (downloadTask.Exception != null)
            {
                // 处理下载失败的情况
                Debug.LogError("下载失败: " + downloadTask.Exception.InnerException.Message);
                textComponent.text = "下载失败，请重试。";
            }
            else
            {

                ExtractZipFileToRoot(Path.Combine(parentPath,"witchcraft_latest.zip"), parentPath);
                DeleteFile();
                DeleteFolder(VideoCopilotClass.modDeclare.FolderPath);
                textComponent.text = "下载完成，请重启游戏。";

               
            }
        }

        private static async Task DownloadFileAsync(string downloadUrl, string savePath)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // 设置GitHub API请求的User-Agent头部，GitHub要求所有API请求必须包含这个头部
                    client.DefaultRequestHeaders.Add("User-Agent", "Unity-Downloader");

                    HttpResponseMessage response = await client.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode();

                    long totalBytes = response.Content.Headers.ContentLength ?? 0;
                    long downloadedBytes = 0;

                    // 创建一个流来接收数据并保存为文件
                    using (Stream contentStream = await response.Content.ReadAsStreamAsync(),
                           fileStream = new FileStream(savePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        byte[] buffer = new byte[ 1024 *1024];
                        int bytesRead;
                        while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead);
                            downloadedBytes += bytesRead;

                            // 更新下载进度
                            // long progress = (downloadedBytes / totalBytes)*100;
                            textComponent.text = $"正在下载\n<color=#373b31>下载较慢请稍等</color>";
                        }
                    }

                    Debug.Log("下载完成！");
                }
                catch (Exception ex)
                {
                    Debug.LogError("下载文件时出错: " + ex.Message);
                    textComponent.text = "下载失败，请重试。";
                }
            }
        }

        private static void ExtractZipFileToRoot(string zipFilePath, string destinationDirectory)
        {
            try
            {
                if (!File.Exists(zipFilePath))
                {
                    Debug.LogError("ZIP 文件不存在: " + zipFilePath);
                    return;
                }

                // 确保目标目录存在
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                // 解压整个 ZIP 文件到目标目录
                ZipFile.ExtractToDirectory(zipFilePath, destinationDirectory);

                Debug.Log("文件解压完成");
            }
            catch (Exception ex)
            {
                Debug.LogError("解压文件时出错: " + ex.Message);
            }

        }


        public static void DeleteFolder(string folderPath)
        {
            try
            {
                 parentPath = Directory.GetParent(folderPath).FullName;

                Directory.Delete(folderPath, true);
                Debug.Log($"成功删除文件夹: {folderPath}");

                Debug.Log($"上一级目录: {parentPath}");
            }
            catch (IOException ioEx)
            {
                Debug.LogError($"删除文件夹时发生IO错误: {ioEx.Message}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"删除文件夹时发生错误: {ex.Message}");
            }
        }

        public static void DeleteFile()
        {
            try
            {

                parentPath = Directory.GetParent(VideoCopilotClass.modDeclare.FolderPath).FullName;
                Debug.Log(parentPath);
                if (!File.Exists(Path.Combine(parentPath, "witchcraft_latest.zip")))
                {
                    return;
                }

                File.Delete(Path.Combine(parentPath, "witchcraft_latest.zip"));

            }
            catch (IOException ioEx)
            {
                Debug.LogError($"删除文件时发生IO错误: {ioEx.Message}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"删除文件时发生错误: {ex.Message}");
            }
        }
    }
}