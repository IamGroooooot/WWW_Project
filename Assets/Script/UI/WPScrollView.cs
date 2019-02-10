using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 이 클래스는 스크롤 뷰에 필요한 기본적인 함수들을 구현한 것입니다. 상속받아 사용합니다.
/// </summary>
[RequireComponent(typeof(ScrollRect))]
public class WPScrollView : MonoBehaviour {

    private ScrollRect scrollView;

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
        scrollView = GetComponent<ScrollRect>();
    }

    /// <summary>
    /// Item을 추가합니다.
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(WPScrollViewItem item)
    {
        if (scrollView == null) return;
        RectTransform content = scrollView.content;

        item.transform.SetParent(content);
    }

    /// <summary>
    /// Item들을 정렬합니다.
    /// </summary>
    public void SortItem()
    {
        if (scrollView == null) return;
        RectTransform content = scrollView.content;

        float contentWidth = 0;
        for (int index = 0; index < content.childCount; ++index)
        {
            WPScrollViewItem item = content.GetChild(index).GetComponent<WPScrollViewItem>();
            if (item == null) continue;
            item.SetPosition(new Vector2(contentWidth, 0));
            contentWidth += item.GetWidth();
        }
        content.sizeDelta = new Vector2(contentWidth, content.sizeDelta.y);
    }

    /// <summary>
    /// Item들을 모두 제거합니다.
    /// </summary>
    protected void ClearItem()
    {
        if (scrollView == null) return;
        RectTransform content = scrollView.content;

        for (int index = 0; index < content.childCount; ++index)
        {
            Destroy(content.GetChild(index).gameObject);
        }
        content.sizeDelta = new Vector2(0, content.sizeDelta.y);
    }
}
