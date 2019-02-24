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
                
                timeCounter = 0f;
            }
            
        }
    }

    private void Init()
    {
        instance = this;

        checkTime = WPDateTime.ParseData(WPDateTime.Now.ToData());
        WPDateTime.Now.OnTimeChanged += OnTimeChanged;

        StartCoroutine(MainRoutine());
    }

    private void SaveTimeData(WPDateTime content)
    {
        //WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eUserDate, content.ToData());
        WPUserDataManager.instance.DateTime = content;
    }

    private void ChangeWeather(WPDateTime nowTime)
    {
        WPGameCommon._WPDebug("날씨 변경!");
        int random = Random.Range(0, 100);
        switch (nowTime.Season)
        {
            case WPEnum.Season.eSpring:
                {
                    if(random < 60)
                    {
                        WPUserDataManager.instance.Weather = WPEnum.Weather.eSunny;
                    }
                    else
                    {
                        WPUserDataManager.instance.Weather = WPEnum.Weather.eRain;
                    }
                    break;
                }
            case WPEnum.Season.eSummer:
                {
                    if(random < 30)
                    {
                        WPUserDataManager.instance.Weather = WPEnum.Weather.eSunny;
                    }
                    else if(30 <= random && random < 60)
                    {
                        WPUserDataManager.instance.Weather = WPEnum.Weather.eDrought;
                    }
                    else
                    {
                        WPUserDataManager.instance.Weather = WPEnum.Weather.eRain;
                    }
                    break;
                }
            case WPEnum.Season.eAutumn:
                {
                    if(random < 70)
                    {
                        WPUserDataManager.instance.Weather = WPEnum.Weather.eSunny;
                    }
                    else
                    {
                        WPUserDataManager.instance.Weather = WPEnum.Weather.eRain;
                    }
                    break;
                }
            case WPEnum.Season.eWinter:
                {
                    if(random < 50)
                    {
                        WPUserDataManager.instance.Weather = WPEnum.Weather.eSunny;
                    }
                    else
                    {
                        WPUserDataManager.instance.Weather = WPEnum.Weather.eCold;
                    }
                    break;
                }
        }
    }

    private WPDateTime checkTime;
    private void OnTimeChanged(WPDateTime content)
    {
        SaveTimeData(content);
        if(content.Week != checkTime.Week)
        {
            WPGameCommon._WPDebug("주가 바뀌었습니다!");
            ChangeWeather(content);
            checkTime = WPDateTime.ParseData(content.ToData());
        }
    }

}
