using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUIManager_Bank : WPUIManager {

    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUIManager_Bank instance = null;     // singleton

    private WPUI_ImageText imageText_Money;                            // 자금
    private WPUI_ImageText imageText_Debt;                             // 빚
    private WPUI_ImageText imageText_Interest;                         // 이자
    private WPUI_ImageText imageText_InterestRate;                     // 이자율

    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        instance = this;
    }

    // 초기 설정을 합니다.
    protected override void Init()
    {
        imageText_Money = transform.Find("ImageText_Money").GetComponent<WPUI_ImageText>();
        imageText_Debt = transform.Find("ImageText_Debt").GetComponent<WPUI_ImageText>();
        imageText_Interest = transform.Find("ImageText_Interest").GetComponent<WPUI_ImageText>();
        imageText_InterestRate = transform.Find("ImageText_InterestRate").GetComponent<WPUI_ImageText>();
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
        if (imageText_Money == null) return;
        imageText_Money.SetText(content);
    }

    /// <summary>
	/// Debt ( 빚 ) UI를 content로 업데이트합니다.
	/// </summary>
	/// <param name="content"></param>
    public void SetText_Debt(string content)
    {
        if (imageText_Debt == null) return;
        imageText_Debt.SetText(content);
    }

    /// <summary>
	/// Interest ( 이자 ) UI를 content로 업데이트합니다.
	/// </summary>
	/// <param name="content"></param>
    public void SetText_Interest(string content)
    {
        if (imageText_Interest == null) return;
        imageText_Interest.SetText(content);
    }

    /// <summary>
	/// InterestRate ( 이자율 ) UI를 content로 업데이트합니다.
	/// </summary>
	/// <param name="content"></param>
    public void SetText_InterestRate(string content)
    {
        if (imageText_InterestRate == null) return;
        imageText_InterestRate.SetText(content);
    }

}
