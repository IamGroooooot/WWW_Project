using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPGameVariableManager : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPGameVariableManager instance = null;        // for singleton

    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 타입 이넘값으로 값을 저장해준다.
    /// string value 전용
    /// </summary>
    public void SaveVariable(WPEnum.VariableType type, string value)
    {
        // 이넘값 변환
        string key = type.ToString();

        // 변환한 값을 그대로 넣어주자.
        PlayerPrefs.SetString(key, value);
    }

    /// <summary>
    /// 타입 이넘값으로 값을 저장해준다.
    /// Int value 전용
    /// </summary>
    public void SaveVariable(WPEnum.VariableType type, int value)
    {
        // 이넘값 변환
        string key = type.ToString();

        // 변환한 값을 그대로 넣어주자.
        PlayerPrefs.SetInt(key, value);
    }

    /// <summary>
    /// 타입 이넘값으로 값을 저장해준다.
    /// Float value 전용
    /// </summary>
    public void SaveVariable(WPEnum.VariableType type, float value)
    {
        // 이넘값 변환
        string key = type.ToString();

        // 변환한 값을 그대로 넣어주자.
        PlayerPrefs.SetFloat(key, value);
    }

    /// <summary>
    /// 타입 이넘값을 바탕으로 저장된 값을 불러온다.
    /// </summary>
    public string LoadStringVariable(WPEnum.VariableType type)
    {
        // 이넘값 변환
        string key = type.ToString();

        // 불러오기
        string returnValue = PlayerPrefs.GetString(key);

        WPGameCommon._WPDebug("LoadValue From //" + key + "// : //" + returnValue + "//");

        return returnValue;
    }

    /// <summary>
    /// 타입 이넘값을 바탕으로 저장된 값을 불러온다.
    /// </summary>
    public int LoadIntVariable(WPEnum.VariableType type)
    {
        // 이넘값 변환
        string key = type.ToString();

        // 불러오기
        int returnValue = PlayerPrefs.GetInt(key);

        WPGameCommon._WPDebug("LoadValue From //" + key + "// : //" + returnValue.ToString() + "//");

        return returnValue;
    }

    /// <summary>
    /// 타입 이넘값을 바탕으로 저장된 값을 불러온다.
    /// </summary>
    public float LoadFloatVariable(WPEnum.VariableType type)
    {
        // 이넘값 변환
        string key = type.ToString();

        // 불러오기
        float returnValue = PlayerPrefs.GetFloat(key);

        WPGameCommon._WPDebug("LoadValue From //" + key + "// : //" + returnValue.ToString() + "//");

        return returnValue;
    }
}
