using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이 클래스에선 개발버전 여부, 유틸함수 등 여러 클래스에서 공통적으로 사용하는 함수를 정의한다.
/// </summary>
public class WPGameCommon : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public bool isDevelopment = true;

    /////////////////////////////////////////////////////////////////////////
    // Methods
    public void Awake()
    {
        this.InitValue();
    }

    /// <summary>
    /// GameCommon 에서 초기화되는 값들을 전달해준다.
    /// </summary>
    protected void InitValue()
    {
        WPVariable.isDevelopment = this.isDevelopment;
    }

    /// <summary>
    /// WWWProject 에서 사용할 debug 함수.
    /// 개발버전일 때만 디버그를 찍어준다.
    /// </summary>
    /// <param name="str"></param>
    public static void _WPDebug(string str)
    {
        // 개발버전이 아니면 debug 를 찍지 않는다.
        if (false == WPVariable.isDevelopment)
        {
            return;
        }

        Debug.Log(str);
    }

    /// <summary>
    /// Obj 널체크용 함수.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static GameObject _WPAssert(GameObject obj)
    {
        if (null == obj)
        {
            _WPDebug("Null 인 GameObejct가 있습니다! 확인이 필요합니다! : " + obj.ToString());
        }

        return obj;
    }
}
