using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUIManager_News : WPUIManager {

    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUIManager_News instance = null;     // singleton

    private WPImageText imageText_Year;              // 년도
    private Button[] button_Month = new Button[12];     // 월을 표시하는 버튼

    /////////////////////////////////////////////////////////////////////////
    // Methods

    // 초기 설정을 합니다.
    protected override void Init()
    {
        instance = this;

        imageText_Year = transform.Find("ImageText_Year").GetComponent<WPImageText>();
        for(int month = 1; month <= 12; ++month)
        {
            Button targetButton = transform.Find("UI_Month").Find("Button_" + month).GetComponent<Button>();
            // targetButton.onClick.AddListener(delegate { OnClick_Month(month); });
            targetButton.onClick.AddListener(delegate { OnClick_Month(int.Parse(targetButton.transform.Find("Text").GetComponent<Text>().text)); });
            button_Month[month - 1] = targetButton;
        }

        this.transform.Find("Button_Close").GetComponent<Button>().onClick.AddListener(OnClick_Close);

        SetActive(false);
    }

    // Close 버튼을 클릭했을 때 호출합니다.
    private void OnClick_Close()
    {
        SetActive(false);
    }

    /// <summary>
    /// month에 해당하는 버튼을 눌렀을 때 호출합니다.
    /// </summary>
    /// <param name="month"></param>
    private void OnClick_Month(int month)
    {
        WPGameCommon._WPDebug("이번 달 : " + month);
    }

    /// <summary>
    /// Year ( 년도 ) UI를 content로 업데이트합니다.
    /// </summary>
    /// <param name="content"></param>
    public void SetText_Year(string content)
    {
        if (imageText_Year == null) return;
        imageText_Year.SetText(content);
    }

}
