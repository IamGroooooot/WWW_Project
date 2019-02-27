using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUIManager_Bank : WPUIManager {

    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUIManager_Bank instance = null;     // singleton

    private WPImageText imageText_Money;                            // 자금
    private WPImageText imageText_Debt;                             // 빚
    private WPImageText imageText_Interest;                         // 이자
    private WPImageText imageText_InterestRate;                     // 이자율

        public bool IsActive
    {
        get
        {
            return gameObject.activeSelf;
        }
    }

    /////////////////////////////////////////////////////////////////////////
    // Methods

    // 초기 설정을 합니다.
    protected override void Init()
    {
        instance = this;

        imageText_Money = transform.Find("ImageText_Money").GetComponent<WPImageText>();
        imageText_Debt = transform.Find("ImageText_Debt").GetComponent<WPImageText>();
        imageText_Interest = transform.Find("ImageText_Interest").GetComponent<WPImageText>();
        imageText_InterestRate = transform.Find("ImageText_InterestRate").GetComponent<WPImageText>();

        this.transform.Find("Button_Close").GetComponent<Button>().onClick.AddListener(OnClick_Close);


        WPDateTime.Now.OnTimeChanged += UpdateUI;
        SetActive(false);
    }
    

    // Close 버튼을 클릭했을 때 호출합니다.
    private void OnClick_Close()
    {
        SetActive(false);
    }

    private void UpdateUI(WPDateTime content)
    {
        SetText_Money("내 자금: " + WPUserDataManager.instance.Money.ToString());
        SetText_Debt("빚: " + WPUserDataManager.instance.Debt.ToString());
        SetText_Interest("다음달 추가될 이자: "+WPFundsManager.instance.GetInterest().ToString());
        SetText_InterestRate("이자율: " + (5).ToString()+"%");
    }

    public void BorrowClicked1000()
    {
        WPFundsManager.instance.BorrowMoney(1000);
    }

    public void BorrowClicked100()
    {
        WPFundsManager.instance.BorrowMoney(100);
    }

    public void PayBackClicked50()
    {
        WPFundsManager.instance.PayBackDebt(50);
    }

    public void PayBackClicked500()
    {
        WPFundsManager.instance.PayBackDebt(500);
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
