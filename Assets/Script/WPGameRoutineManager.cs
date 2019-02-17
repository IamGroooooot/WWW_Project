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
        float timeCounter = 0f;
        WaitUntil waitUntil = new WaitUntil(() => WPVariable.deltaTime_WPDateTime > 0);
        for(; ; )
        {
            if (WPVariable.deltaTime_WPDateTime > 0)
            {
                timeCounter += Time.deltaTime;
                yield return null;
            }
            else
            {
                timeCounter = 0f;
                yield return waitUntil;
            }

            if (timeCounter >= 5f)
            {
                WPDateTime.Now.AddHour(1);
                timeCounter = 0f;
            }
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
