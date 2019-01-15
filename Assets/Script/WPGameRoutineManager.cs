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
        instance = this;
    }

    private void Start()
    {
        InitValue();
        StartCoroutine(MainRoutine());
    }

    IEnumerator MainRoutine()
    {
        for(; ; )
        {
            yield return new WaitForSeconds(5f);
            WPDateTime.Hour++;
        }
    }

    private void InitValue()
    {
        WPDateTime.Init();
    }

}
