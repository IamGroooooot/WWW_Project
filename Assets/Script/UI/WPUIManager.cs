using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이 클래스는 UI를 관리하는 기본적인 함수들을 구현한 클래스입니다. 상속받아 사용합니다.
/// </summary>
public class WPUIManager : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles


    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 초기화 과정울 수행합니다. override 할 수 있습니다. 이 함수는 Awake()에서 호출합니다.
    /// </summary>
    protected virtual void Init()
    {

    }

    /// <summary>
    /// UI를 화면에 param 값에 따라 표시합니다.
    /// </summary>
    /// <param name="param"></param>
    public virtual void SetActive(bool param)
    {
        gameObject.SetActive(param);
    }
}
