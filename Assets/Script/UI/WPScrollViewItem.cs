using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 이 클래스는 스크롤 뷰에 추가되는 Item을 구현합니다. 상속받아 사용합니다.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class WPScrollViewItem : MonoBehaviour {

    protected Text text;
    protected Image image;
    protected Button button;
    protected GameObject focus;
    protected RectTransform rectTransform;
    
    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 초기화 과정울 수행합니다. override 할 수 있습니다. 이 함수는 Awake()에서 호출합니다.
    /// 사용할 시 base.Init()을 반드시 가장 먼저 호출해야 합니다.
    /// </summary>
    protected virtual void Init()
    {
        text = transform.Find("Text").GetComponent<Text>();
        image = transform.Find("Image").GetComponent<Image>();
        button = transform.Find("Image").GetComponent<Button>();
        focus = transform.Find("Focus").gameObject;
        rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// GameObject의 이름을 content로 설정합니다. 이는 하나의 변수로 활용할 수 있습니다.
    /// </summary>
    /// <param name="content"></param>
    public void SetName(string content)
    {
        gameObject.name = content;
    }

    /// <summary>
    /// Item을 강조할 지 결정합니다.
    /// </summary>
    /// <param name="isFocus"></param>
    public void SetFocus(bool isFocus)
    {
        if (focus == null) return;
        focus.SetActive(isFocus);
    }

    /// <summary>
    /// Text를 content로 설정합니다.
    /// </summary>
    /// <param name="content"></param>
    public void SetText(string content)
    {
        if (text == null) return;
        text.text = content;
    }

    /// <summary>
    /// Sprite를 content로 설정합니다.
    /// </summary>
    /// <param name="content"></param>
    public void SetSprite(Sprite content)
    {
        if (image == null) return;
        image.sprite = content;
    }

    /// <summary>
    /// Item의 위치를 position으로 지정합니다.
    /// </summary>
    /// /// <param name="position"></param>
    public void SetPosition(Vector2 position)
    {
        if (rectTransform == null) return;
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, position.x, GetWidth());
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, position.y, GetHeight());
        //rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, position.y);
        //rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, position.y);
    }

    /// <summary>
    /// Item 클릭 혹은 터치 시 발생하는 이벤트를 추가합니다.
    /// </summary>
    /// /// <param name="unityAction"></param>
    public void AddEvent(UnityAction unityAction)
    {
        WPGameCommon._WPDebug("이벤트 추가! " + button.name);
        button.onClick.AddListener(unityAction);
    }

    /// <summary>
    /// Item의 너비를 리턴합니다.
    /// </summary>
    public float GetWidth()
    {
        if (rectTransform == null) return 0;
        return rectTransform.rect.width;
    }

    /// <summary>
    /// Item의 높이를 리턴합니다.
    /// </summary>
    public float GetHeight()
    {
        if (rectTransform == null) return 0;
        return rectTransform.rect.height;
    }

}
