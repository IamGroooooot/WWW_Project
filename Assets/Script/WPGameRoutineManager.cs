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
            yield return waitFiveSeconds;
            WPDateTime_New.Now.AddHour(1);
        }
    }

    private void Init()
    {
        instance = this;

        WPDateTime_New.Now.OnValueChanged += SaveTimeData;

        StartCoroutine(MainRoutine());
    }

    private void SaveTimeData(WPDateTime_New content)
    {
        WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eUserDate, content.ToData());
    }

}
