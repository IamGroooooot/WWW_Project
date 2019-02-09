using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 이 클래스는 스크롤 뷰에 추가되는 Item을 구현합니다. 상속받아 사용합니다.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class WPScrollViewItem : MonoBehaviour {

    private Text text;
    private Image image;
    private RectTransform rectTransform;
    
    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 초기화 과정울 수행합니다. override 할 수 있습니다. 이 함수는 Awake()에서 호출합니다.
    /// 사용할 시 base.Init()을 반드시 호출해야 합니다.
    /// </summary>
    protected virtual void Init()
    {
        text = transform.Find("Text").GetComponent<Text>();
        image = transform.Find("Image").GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
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
        rectTransform.localPosition = position;
    }

    /// <summary>
    /// Item의 너비를 리턴합니다.
    /// </summary>
    public float GetWidth()
    {
        if (rectTransform == null) return 0;
        return rectTransform.sizeDelta.x;
    }

}
