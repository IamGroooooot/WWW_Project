using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUIManager_Book : WPUIManager
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUIManager_Book instance = null;     // singleton

    private static string[] tabName = {"식 물", "비 료" };

    private int tabIndex = 0;
    private int TabIndex
    {
        get
        {
            return tabIndex;
        }
        set
        {
            tabIndex = value;
            if(tabIndex >= tabName.Length)
            {
                tabIndex = 0;
            }
            else if(tabIndex < 0)
            {
                tabIndex = tabName.Length - 1;
            }
            
            if(button_Tab != null)
            {
                button_Tab.GetComponentInChildren<Text>().text = tabName[tabIndex];
            }

            if(scrollView_Book != null)
            {
                switch (tabIndex)
                {
                    case 0:
                        {
                            scrollView_Book.CreateSeedList();
                            break;
                        }
                    case 1:
                        {
                            scrollView_Book.CreateFertilizerList();
                            break;
                        }
                }
            }
        }
    }

    private Button button_Tab;
    private WPScrollView_Book scrollView_Book;
    public WPUI_BookItemInfo ui_BookItemInfo { get; private set; }

    /////////////////////////////////////////////////////////////////////////
    // Methods

    protected override void Init()
    {
        instance = this;

        this.transform.Find("Button_Close").GetComponent<Button>().onClick.AddListener(OnClick_Close);

        button_Tab = transform.Find("Button_Tab").GetComponent<Button>();
        button_Tab.onClick.AddListener(OnClick_Tab);
        button_Tab.GetComponentInChildren<Text>().text = tabName[TabIndex];

        scrollView_Book = transform.Find("ScrollView_Book").GetComponent<WPScrollView_Book>();
        ui_BookItemInfo = transform.Find("UI_BookItemInfo").GetComponent<WPUI_BookItemInfo>();

        ui_BookItemInfo.SetActive(false);

        SetActive(false);
    }

    // Close 버튼을 클릭했을 때 호출합니다.
    private void OnClick_Close()
    {
        SetActive(false);
    }

    private void OnClick_Tab()
    {
        ui_BookItemInfo.SetActive(false);
        TabIndex++;
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
