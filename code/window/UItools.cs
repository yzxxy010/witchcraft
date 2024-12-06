using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace InterestingTrait.code.window
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
        }

        public static void createActorOnUI(Actor actor, GameObject parent, Vector3 pos,string option)
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


            createActorText(GO, $"<color=#FF9B1C>单位姓名:</color>\t" +
                                $"{actor.getName()}\n" +
                                $"<color=#FF9B1C>单位源能:</color>\t" +
                                $"100", new Vector3(110, 0, 0));
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