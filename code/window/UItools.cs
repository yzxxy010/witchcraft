using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VideoCopilot.code.window
{
    public class UItools : MonoBehaviour
    {
        private static GameObject avatarRef;

        public static void Init()
        {
            // 查找路径并赋值给 avatarRef
            avatarRef = GameObject.Find(
                $"Canvas Container Main/Canvas - Windows/windows/inspect_unit/Background/Scroll View/Viewport/Content/Part 1/BackgroundAvatar"
            );
        }


        public static void createImageOnUI(GameObject parent, string UIname, string imageName, Vector3 pos,
            Vector3 scale)
        {
            GameObject imageObject = new GameObject(UIname);
            imageObject.transform.SetParent(parent.transform);
            RectTransform rectTransform = imageObject.AddComponent<RectTransform>();
            Image image = imageObject.AddComponent<Image>();
            Sprite imageSprite = Resources.Load<Sprite>(imageName);
            image.sprite = imageSprite; // 设置显示的图片
            image.preserveAspect = true; // 保持图片长宽比
            image.raycastTarget = false; //取消阻挡,不然button全失效了
            rectTransform.localPosition = pos;
            rectTransform.localScale = scale;

            createButtonOnImageUI(UIname, imageObject, new Vector3(-22.6f, -42.3f, 0), "ui/allActor_Age.png",
                Sort_key.allActor_Age_sort);
            createButtonOnImageUI(UIname, imageObject, new Vector3(-13.6f, -42.3f, 0), "ui/civActor_Age.png",
                Sort_key.default_Age_sort);
            createButtonOnImageUI(UIname, imageObject, new Vector3(22.6f, -42.3f, 0), "ui/allActor_Yuanneng.png",
                Sort_key.allActor_yuanneng_sort);
            createButtonOnImageUI(UIname, imageObject, new Vector3(13.6f, -42.3f, 0), "ui/civActor_Yuanneng.png",
                Sort_key.yuanneng_sort);
        }

        public delegate void ButtonClickDelegate(string buttonName);

        private static void createButtonOnImageUI(string buttonName, GameObject parent, Vector3 pos, string imageName,
            string option )
        {
            GameObject buttonObject = new GameObject(buttonName + "_Button");
            buttonObject.transform.SetParent(parent.transform); // 将按钮设置为图像的子物体
            RectTransform buttonRectTransform = buttonObject.AddComponent<RectTransform>();
            buttonObject.AddComponent<Button>(); // 添加按钮组件

            // 创建一个纯白色的Image来作为按钮背景
            Image buttonImage = buttonObject.AddComponent<Image>();
            Color color;
            ColorUtility.TryParseHtmlString("#3e4237", out color);
            buttonImage.color = color; // 设置按钮背景颜色为纯白色


            // 设置按钮的大小（例如，适配图像大小）
            buttonRectTransform.localPosition = pos;
            buttonRectTransform.sizeDelta = new Vector2(8, 4.5f);
            buttonRectTransform.localScale = Vector3.one;

            // 创建按钮图案的Image
            GameObject iconObject = new GameObject(buttonName + "_Icon");
            iconObject.transform.SetParent(buttonObject.transform);
            RectTransform iconRectTransform = iconObject.AddComponent<RectTransform>();

            Image iconImage = iconObject.AddComponent<Image>();
            Sprite iconSprite = Resources.Load<Sprite>(imageName); // 从Resources加载图案
            iconImage.sprite = iconSprite;
            iconImage.preserveAspect = true; // 保持图案的长宽比
            iconImage.raycastTarget = false;
            iconRectTransform.localPosition = Vector3.zero; // 图案居中于按钮
            iconRectTransform.localScale = new Vector3(0.05f, 0.05f, 1);
            buttonObject.GetComponent<Button>().onClick.AddListener(() => reloading_List(option));
        }

        public static void reloading_List(string option)
        {
            WindowAttack.ClearContent();
            WindowAttack.Sort_AttackWinodw(option);
            WindowAttack.drawListOnAttackWindow();
        }
        

        public static void createActorOnUI(Actor actor, GameObject parent, Vector3 pos, string option)
        {
            GameObject GO = Instantiate(avatarRef);
            GO.transform.SetParent(parent.transform);
            var avatarElement = GO.GetComponent<UiUnitAvatarElement>();
            avatarElement.show_banner_clan = true;
            avatarElement.show_banner_kingdom = true;
            avatarElement.show(actor);
            RectTransform GORect = GO.GetComponent<RectTransform>();
            GORect.localPosition = pos;
            GORect.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            Button GOButton = GO.AddComponent<Button>();
            GOButton.onClick.AddListener(() => showActor(actor));

            CreateButtonBackground(GO, "ui/buttonBackground.png", "buttonBackground", Vector3.zero,
                new Vector3(0.4f, 0.4f, 0.6f));
            CreateButtonBackground(GO, "ui/textBackground.png", "textBackground", new Vector3(100, -0.5f),
                new Vector3(1.5f, 1.5f));

            //创建后面的打开页面的按钮
            GameObject openActorButton_GO = CreateButtonBackground(GO, "ui/buttonBackground2.png", "openActorButton",
                new Vector3(200, 0),
                new Vector3(0.4f, 0.4f, 0.6f), true);
            Button openActorButton = openActorButton_GO.AddComponent<Button>();
            openActorButton.onClick.AddListener(() => showActor(actor));

            CreateButtonBackground(openActorButton_GO, "ui/openActorButton.png", "openActorImage",
                new Vector3(0, 0), new Vector3(0.6f, 0.6f, 1f), true);

            switch (option)
            {
                case Sort_key.default_Age_sort:
                case Sort_key.allActor_Age_sort:
                    createActorText(GO, $"<color=#FF9B1C>单位姓名:</color>\t" +
                                        $"{actor.getName()}\n" +
                                        $"<color=#FF9B1C>单位年龄:</color>\t" +
                                        $"{actor.getAge()}", new Vector3(110, 0, 0));
                    break;
                case Sort_key.allActor_yuanneng_sort:
                case Sort_key.yuanneng_sort:
                    createActorText(GO, $"<color=#FF9B1C>单位姓名:</color>\t" +
                                        $"{actor.getName()}\n" +
                                        $"<color=#FF9B1C>单位源能:</color>\t" +
                                        $"{actor.stats["yuanneng"]}", new Vector3(110, 0, 0));
                    break;
            }
        }

        public static void createActorText(GameObject parent, string text, Vector3 pos)
        {
            GameObject textObject = new GameObject("actorText");
            textObject.transform.SetParent(parent.transform);
            Text textComponent = textObject.AddComponent<Text>();
            textComponent.text = text;
            textComponent.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            textComponent.fontSize = 24;
            textComponent.color = Color.white;
            textComponent.alignment = TextAnchor.MiddleLeft;
            RectTransform rectTransform = textObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(300, 100);
            rectTransform.localPosition = pos;
            textComponent.raycastTarget = false;
        }

        public static GameObject CreateButtonBackground(GameObject buttonGO, string imageName, string BackgroundName,
            Vector3 pos, Vector3 scale = default, bool isclick = false)
        {
            // 创建背景 GameObject
            GameObject backgroundGO = new GameObject(BackgroundName);
            backgroundGO.transform.SetParent(buttonGO.transform);


            // 添加背景 Image 组件
            Image backgroundImage = backgroundGO.AddComponent<Image>();
            backgroundImage.raycastTarget = isclick;
            // 加载背景图像并设置
            Sprite backgroundSprite = Resources.Load<Sprite>(imageName);
            backgroundImage.sprite = backgroundSprite;
            backgroundImage.preserveAspect = true; //保持长宽比

            // 设置背景的位置和大小
            RectTransform backgroundRect = backgroundGO.GetComponent<RectTransform>();
            backgroundRect.localPosition = pos;
            backgroundRect.localScale = scale;


            // 设置背景图层级为按钮后面
            backgroundGO.transform.SetAsFirstSibling();
            return backgroundGO;
        }

        public static void showActor(Actor pActor)
        {
            Config.selectedUnit = pActor;
            ScrollWindow.showWindow("inspect_unit");
        }
    }
}