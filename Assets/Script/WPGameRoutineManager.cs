using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이 클래스매니저는 게임의 흐름을 관리합니다.
/// </summary>
public class WPGameRoutineManager : MonoBehaviour {
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPGameRoutineManager instance = null;

    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        Init();
    }

    IEnumerator MainRoutine()
    {
        WaitForSeconds waitFiveSeconds = new WaitForSeconds(5f);
        for(; ; )
        {
            if (WPVariable.deltaTime_WPDateTime > 0) yield return waitFiveSeconds;
            else yield return null;
            if (WPVariable.deltaTime_WPDateTime > 0) WPDateTime.Now.AddHour(1);
        }
    }

    private void Init()
    {
        instance = this;

        WPDateTime.Now.OnValueChanged += SaveTimeData;

        StartCoroutine(MainRoutine());
    }

    private void SaveTimeData(WPDateTime content)
    {
        WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eUserDate, content.ToData());
    }

}
