using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 예시로 만든 Event입니다.
public class WPTestEvent : WPEvent {

    protected override void Init()
    {
        SetCondition<int>(WPEnum.VariableType.eUserMoney, WPEnum.CompareType.eMore, 50);
    }

    protected override void Execution()
    {
        WPGameCommon._WPDebug("돈이 50원을 넘어섰습니다!");
        base.Execution();
    }
}
