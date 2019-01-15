using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUIManager_Farm : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUIManager_Farm instance = null;
    private Transform newsMask = null;                          // 뉴스 애니메이션을 위한 Mask
    private int newsIndex = 0;                                  // 뉴스 애니메이션을 위한 Index
    private List<string> newsContent = new List<string>();      // 뉴스 애니메이션을 위한 List
    private float newsDelay = 2f;                               // 뉴스 사이의 간격 ( 출현할 때 대기하고 애니메이션이 끝날 때 대기합니다. )
    private float newsSpeed = 400f;                             // 뉴스가 움직이는 속도 ( pixel per second )

    private Text timeText;

    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
        StartCoroutine(NewsRoutine());
        StartCoroutine(TimeRoutine());
    }

    // 초기 설정을 합니다.
    private void Init()
    {
        this.transform.Find("Button_News").GetComponent<Button>().onClick.AddListener(OnClick_News);
        this.transform.Find("Button_Bank").GetComponent<Button>().onClick.AddListener(OnClick_Bank);
        this.transform.Find("Button_Shop").GetComponent<Button>().onClick.AddListener(OnClick_Shop);
        this.transform.Find("Button_Choose").GetComponent<Button>().onClick.AddListener(OnClick_Choose);
        newsMask = transform.Find("Image_News");
        timeText = transform.Find("Image_Time").GetComponentInChildren<Text>();
        AddNews("테스트 뉴스 1");
        AddNews("테스트 뉴스 2");
        AddNews("테스트 뉴스 33333333333333333 테스트 뉴스 33333333333333333");
    }

    // News 버튼을 클릭했을 때 호출합니다.
    private void OnClick_News()
    {

    }

    // Bank 버튼을 클릭했을 때 호출합니다.
    private void OnClick_Bank()
    {

    }

    // Shop 버튼을 클릭했을 때 호출합니다.
    private void OnClick_Shop()
    {

    }

    // Choose 버튼을 클릭했을 때 호출합니다.
    private void OnClick_Choose()
    {

    }

    // Event와 Delegate 공부해서 이런 X같은 코드 없에버리자!
    private IEnumerator TimeRoutine()
    {
        for(; ; )
        {
            if(timeText != null)
            {
                timeText.text = WPDateTime.ToString();
            }
            yield return null;
        }
    }

    // News 애니메이션을 위한 Coroutine입니다.
    private IEnumerator NewsRoutine()
    {
        if (newsMask == null) yield break;
        for(; ; )
        {
            if (newsContent == null || newsContent.Count <= 0)                      // newsContent가 없다면 채워질 때 까지 1프레임씩 대기합니다.
            {
                yield return new WaitForEndOfFrame();
                continue;
            }

            if (string.IsNullOrEmpty(newsContent[newsIndex]))                       // newsContent의 newsIndex번 째 내용이 비정상적일 경우 넘어갑니다.
            {
                newsIndex += 1;
                if (newsIndex >= newsContent.Count) newsIndex = 0;                  // newsIndex가 newsContent보다 클 경우 0으로 초기화합니다.
                continue;
            }

            GameObject newsObject = new GameObject("newsContent");

            Vector2 maskSize = newsMask.GetComponent<RectTransform>().rect.size;    // 마스크 크기 저장

            Text newsText = newsObject.AddComponent<Text>();
            newsText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");        // 텍스트 폰트 설정
            newsText.text = newsContent[newsIndex];                                 // 텍스트 내용 설정
            newsText.color = Color.black;                                           // 텍스트 색깔 설정
            newsText.fontSize = 80;                                                 // 텍스트 크기 설정
            newsText.alignment = TextAnchor.MiddleLeft;                             // 텍스트 정렬 설정

            ContentSizeFitter newsSizeFitter =
                newsObject.AddComponent<ContentSizeFitter>();                       
            newsSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            newsSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize; // 텍스트 크기를 내용에 맞게 자동으로 설정합니다.

            yield return new WaitForEndOfFrame();                                   // 사이즈 업데이트에 1프레임이 필요하기 때문에 대기합니다.

            if (newsMask.childCount > 0)
            {
                for (int index = 0; index < newsMask.childCount; ++index)
                {
                    Destroy(newsMask.GetChild(index).gameObject);
                }
            }
            newsObject.transform.parent = newsMask;                                 // EndOfFrame 대기로 인해 발생하는 깜빡임 현상을 해결하기 위한 편법

            RectTransform newsRectTransform = newsText.rectTransform;
            newsRectTransform.pivot = new Vector2(0, 0.5f);                         // 텍스트 피봇 설정 ( 좌측 중간 )
            newsRectTransform.anchorMin = new Vector2(0, 0.5f);
            newsRectTransform.anchorMax = new Vector2(0, 0.5f);                     // 텍스트 앵커 설정 ( 좌측 중간 )
            newsRectTransform.sizeDelta = new Vector2(
                newsRectTransform.sizeDelta.x, maskSize.y * 0.9f);                  // 텍스트 전체 크기 설정
            newsRectTransform.anchoredPosition = new Vector2(30, 0);                // 텍스트 위치 초기화

            yield return new WaitForSeconds(newsDelay);                             // newsDelay 만큼 대기합니다.

            if (newsRectTransform.rect.width > maskSize.x)                          // 텍스트의 길이가 긴 경우 애니메이션을 호출합니다.
            {
                float newsTravel = maskSize.x - newsRectTransform.rect.width - 30;       // 텍스트의 이동거리 newsTravel를 구합니다.
                for(; ; )
                {
                    yield return new WaitForEndOfFrame();
                    if (newsRectTransform.anchoredPosition.x < newsTravel) break;  // newsTravel만큼 움직였으면 끝냅니다.
                    newsRectTransform.anchoredPosition -= new Vector2(
                        newsSpeed * Time.deltaTime, 0f);                            // newsSpeed만큼 움직입니다.
                }
            }

            newsIndex += 1;
            if (newsIndex >= newsContent.Count) newsIndex = 0;                      // newsIndex가 newsContent보다 클 경우 0으로 초기화합니다.

            yield return new WaitForSeconds(newsDelay);                             // newsDelay 만큼 대기합니다.

        }
    }

    /// <summary>
	/// News를 추가합니다. 추가한 News는 UI의 상단 부분에 하나씩 우에서 좌로 흘러가게 됩니다.
	/// </summary>
	/// <param name="content"></param>
    public void AddNews(string content)
    {
        if (newsContent == null) return;
        newsContent.Add(content);
    }

    /// <summary>
	/// News를 추가합니다. 추가한 News는 UI의 상단 부분에 하나씩 우에서 좌로 흘러가게 됩니다.
	/// </summary>
	/// <param name="contents"></param>
    public void AddNews(List<string> contents)
    {
        if (newsContent == null) return;
        newsContent.AddRange(contents);
    }

    /// <summary>
	/// 모든 News를 지웁니다. 주의 : 이 동작은 현재 흐르고 있는 news까지 지워버리지 않습니다.
	/// </summary>
    public void ClearNews()
    {
        if (newsContent == null) return;
        newsContent.Clear();
        newsIndex = 0;
    }

}
