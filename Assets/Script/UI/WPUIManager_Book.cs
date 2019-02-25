using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUIManager_Book : WPUIManager
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUIManager_Book instance = null;     // singleton

    private Button button_Tab;
    private WPScrollView_Book scrollView_Book;

    /////////////////////////////////////////////////////////////////////////
    // Methods

    protected override void Init()
    {
        instance = this;

        this.transform.Find("Button_Close").GetComponent<Button>().onClick.AddListener(OnClick_Close);

        button_Tab = transform.Find("Button_Tab").GetComponent<Button>();
        scrollView_Book = transform.Find("ScrollView_Book").GetComponent<WPScrollView_Book>();

        SetActive(false);
    }

    // Close 버튼을 클릭했을 때 호출합니다.
    private void OnClick_Close()
    {
        SetActive(false);
    }


    /// <summary>
    /// UI를 화면에 param 값에 따라 표시합니다.
    /// </summary>
    /// <param name="param"></param>
    public override void SetActive(bool param)
    {
        if (param)
        {
            WPVariable.timeScale_NewsUI = 0f;
            WPVariable.timeScale_WPDateTime = 0f;
            scrollView_Book.SetActive(true);
        }
        else
        {
            WPVariable.timeScale_NewsUI = 1f;
            WPVariable.timeScale_WPDateTime = 1f;
        }
        base.SetActive(param);
    }
}
