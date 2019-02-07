using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUIManager_Bank : WPUIManager {

    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUIManager_Bank instance = null;     // singleton

    private Text text_Money;                            // 자금
    private Text text_Debt;                             // 빚
    private Text text_Interest;                         // 이자
    private Text text_InterestRate;                     // 이자율

    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        instance = this;
    }

    // 초기 설정을 합니다.
    protected override void Init()
    {
        text_Money = transform.Find("UI_Money").Find("Text").GetComponent<Text>();
        text_Debt = transform.Find("UI_Debt").Find("Text").GetComponent<Text>();
        text_Interest = transform.Find("UI_Interest").Find("Text").GetComponent<Text>();
        text_InterestRate = transform.Find("UI_InterestRate").Find("Text").GetComponent<Text>();
        this.transform.Find("Button_Close").GetComponent<Button>().onClick.AddListener(OnClick_Close);
        SetActive(false);
    }

    // Close 버튼을 클릭했을 때 호출합니다.
    private void OnClick_Close()
    {
        SetActive(false);
    }

    /// <summary>
	/// Money ( 돈 ) UI를 content로 업데이트합니다.
	/// </summary>
	/// <param name="content"></param>
    public void SetText_Money(string content)
    {
        if (text_Money == null) return;
        text_Money.text = content;
    }

    /// <summary>
	/// Debt ( 빚 ) UI를 content로 업데이트합니다.
	/// </summary>
	/// <param name="content"></param>
    public void SetText_Debt(string content)
    {
        if (text_Debt == null) return;
        text_Debt.text = content;
    }

    /// <summary>
	/// Interest ( 이자 ) UI를 content로 업데이트합니다.
	/// </summary>
	/// <param name="content"></param>
    public void SetText_Interest(string content)
    {
        if (text_Interest == null) return;
        text_Interest.text = content;
    }

    /// <summary>
	/// InterestRate ( 이자율 ) UI를 content로 업데이트합니다.
	/// </summary>
	/// <param name="content"></param>
    public void SetText_InterestRate(string content)
    {
        if (text_InterestRate == null) return;
        text_InterestRate.text = content;
    }

}
