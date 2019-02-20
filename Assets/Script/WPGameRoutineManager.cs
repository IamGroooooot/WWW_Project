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

    public static event System.EventHandler TimePassed;
    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        Init();
    }

    IEnumerator MainRoutine()
    {
        float timeCounter = 0f;

        for(; ; )
        {
            for (timeCounter = 0f; timeCounter < 5f; timeCounter += Time.deltaTime * WPVariable.timeScale_WPDateTime)
            {
                yield return null;
                if (WPVariable.timeScale_WPDateTime <= 0) timeCounter = 0f;
            }

            if (timeCounter >= 5f)
            {
                WPDateTime.Now.AddHour(1);

                //한시간 지날 때마다 등록된 콜백 메소드 호출
                TimePassed(this, new OnOneHourPassed());

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
