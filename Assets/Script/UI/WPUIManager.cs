using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUIManager : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUIManager instance = null;      // singleton

    protected int m_playerMoney = 0;				// 자금
	protected int m_playerDeft = 0;                 // 빚
	
	/////////////////////////////////////////////////////////////////////////
	// Methods

	private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        this.InitValue();
    }

    /// <summary>
    /// 초기값 불러오기.
    /// </summary>
    private void InitValue()
    {
        m_playerMoney = WPGameVariableManager.instance.LoadIntVariable(WPEnum.VariableType.eUserMoney);
		m_playerDeft = WPGameVariableManager.instance.LoadIntVariable(WPEnum.VariableType.eUserDebt);
		//m_playerDate_Year = WPGameVariableManager.instance.LoadIntVariable(WPEnum.VaraibleType.eUserDate_Year);
		//m_playerDate_Month = WPGameVariableManager.instance.LoadIntVariable(WPEnum.VaraibleType.eUserDate_Month);
		//m_playerDate_Day = WPGameVariableManager.instance.LoadIntVariable(WPEnum.VaraibleType.eUserDate_Day);

		this.UpdateMoney();
		this.UpdateDebt();
    }

    /// <summary>
    /// variable 테스트 임시함수. 이 함수가 보이면 바로 지우자
    /// </summary>
    public void IncreaseMoney()
    {
        this.m_playerMoney += 1;
		this.m_playerDeft += 1;

        this.UpdateMoney();
		this.UpdateDebt();
        WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eUserMoney, m_playerMoney);
        WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eUserDebt, m_playerDeft);
    }

    /// <summary>
    /// 자금 출력 함수. 임시로 만든 함수 나중에 추가해야됨.
    /// </summary>
    public void UpdateMoney()
    {
		this.transform.Find("Money").GetComponent<Text>().text = "자금 : " + m_playerMoney.ToString();
    }

	/// <summary>
	/// 빚 출력 함수. 임시로 만든 함수 나중에 추가해야됨.
	/// </summary>
	public void UpdateDebt()
	{
		this.transform.Find("Debt").GetComponent<Text>().text = "빚 : " + m_playerDeft.ToString();
	}

}

