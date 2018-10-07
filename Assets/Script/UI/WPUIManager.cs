using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUIManager : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUIManager instance = null;      // singleton

    protected int m_playerMoney = 0;

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
        m_playerMoney = WPGameVariableManager.instance.LoadIntVariable(WPEnum.VaraibleType.eUserMoney);

        this.UpdateMoney();
    }

    /// <summary>
    /// variable 테스트 임시함수. 이 함수가 보이면 바로 지우자
    /// </summary>
    public void IncreaseMoney()
    {
        this.m_playerMoney += 1;

        this.UpdateMoney();

        WPGameVariableManager.instance.SaveVariable(WPEnum.VaraibleType.eUserMoney, m_playerMoney);
    }

    /// <summary>
    /// variable 테스트 임시함수. 이 함수가 보이면 바로 지우자
    /// </summary>
    public void UpdateMoney()
    {
        this.gameObject.GetComponentInChildren<Text>().text = "Count : " + m_playerMoney.ToString();
    }
}
