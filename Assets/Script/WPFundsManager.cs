using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이자율 계산, 돈 빌리기

public class WPFundsManager : MonoBehaviour{

    public static WPFundsManager instance;
    private static int timer;

    void Awake()
    {
        instance = this;

        timer = WPGameVariableManager.instance.LoadIntVariable(WPEnum.VariableType.eDebtTimer);

        WPDateTime.Now.OnValueChanged += UpdateDebt;
    }

    private void UpdateDebt(int changedValue)
    {
        timer += changedValue;
        WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eDebtTimer, timer);
        if(timer>= 1 * 24 *30)
        {
            WPUserDataManager.instance.Debt += GetInterest();
            WPGameCommon._WPDebug("이자율 적용! 빚 늘어남");
            timer = 0;
            WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eDebtTimer, timer);
        }
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

    /// <summary>
    /// 돈을 쓸 때
    /// </summary>
    /// <param name="payingMoney"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 돈을 갚을 때 
    /// </summary>
    /// <param name="payBackMoney"></param>
    public int PayBackDebt(int payBackMoney)
    {
        if (payBackMoney > WPUserDataManager.instance.Money)
        {
            //잔고 부족
            int neededMoney= payBackMoney-WPUserDataManager.instance.Money;
            WPGameCommon._WPDebug(neededMoney.ToString()+"원 부족함!!");
            return (-1) * neededMoney;
        }

        if (payBackMoney > WPUserDataManager.instance.Debt)
        {
            //빚보다 돈을 더 많이 낸 경우
            int refund = payBackMoney - WPUserDataManager.instance.Debt ;

            WPUserDataManager.instance.Debt = 0;
            WPUserDataManager.instance.Money = WPUserDataManager.instance.Money - payBackMoney + refund;
            WPGameCommon._WPDebug("돈 갚고 돈이 남았다! 거스름돈 :" + refund.ToString());
            return refund;
        }
        else
        {
            //빚을 감소 시켜준다
            WPUserDataManager.instance.Debt -= payBackMoney;
            WPUserDataManager.instance.Money -= payBackMoney;

            //정상적으로 종료
            return 0;
        }
    }

    //이자 가져오기(빚의 5프로)
    public int GetInterest()
    {
        int interest = (int)((double)WPUserDataManager.instance.Debt * GetInterestRate());
        
        return interest;
    }

}
