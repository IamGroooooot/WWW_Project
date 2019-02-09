using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class WPImageText : MonoBehaviour {

    private Image image;
    private Text text;

    private void Awake()
    {
        text = transform.Find("Text").GetComponent<Text>();
        image = GetComponent<Image>();
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

}
