using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이자율 계산, 돈 빌리기

public class WPFundsManager : MonoBehaviour{

    public static WPFundsManager instance;

    void Awake()
    {
        instance = this;


    }

    private void UpdateDebt(WPDateTime content)
    {
        //1달이 지나면 Debt올림

    }

    //이자율 가져오기
    public double GetInterestRate()
    {
        
        return 0.05f;
    }

    public void BorrowMoney(int borrowingMoney)
    {
        WPUserDataManager.instance.Debt += borrowingMoney;
        WPUserDataManager.instance.Money += borrowingMoney;

    }

    public WPEnum.IsAble2Pay UseMoney(int payingMoney)
    {
        if (payingMoney >= WPUserDataManager.instance.Money)
        {
            WPUserDataManager.instance.Money -= payingMoney;
            
            return WPEnum.IsAble2Pay.eEnoughMoney;
        }
        else
        {
            WPGameCommon._WPDebug("돈 부족함");
            return WPEnum.IsAble2Pay.eNoMoney;
        }
    }

    //이자 가져오기(빚의 5프로)
    public int GetInterest()
    {
        int interest = (int)((double)WPUserDataManager.instance.Debt * GetInterestRate());
        
        return interest;
    }

}
