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

    /////////////////////////////////////////////////////////////////////////
    // Methods

    protected override void Init()
    {
        instance = this;

        this.transform.Find("Button_Close").GetComponent<Button>().onClick.AddListener(OnClick_Close);

        button_Tab = transform.Find("Button_Tab").GetComponent<Button>();

        SetActive(false);
    }

    // Close 버튼을 클릭했을 때 호출합니다.
    private void OnClick_Close()
    {
        SetActive(false);
    }
}
