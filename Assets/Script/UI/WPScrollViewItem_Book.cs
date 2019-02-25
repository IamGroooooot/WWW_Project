using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPScrollViewItem_Book : WPScrollViewItem {

    protected override void Init()
    {
        image = transform.Find("Image").GetComponent<Image>();
        transform.Find("Image").GetComponent<Button>().enabled = false;
        button = transform.Find("Button").GetComponent<Button>();
        focus = transform.Find("Focus").gameObject;
        rectTransform = GetComponent<RectTransform>();
    }
}
