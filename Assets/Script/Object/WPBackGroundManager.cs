using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPBackGroundManager : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPBackGroundManager instance = null;      // singleton

    public GameObject _baseObject;                      // baseobject. 인스펙터에서 초기화

    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        this.InitValue();
    }

    /// <summary>
    /// 초기값 불러오기.
    /// </summary>
    private void InitValue()
    {
        //스크린 사이즈 계산 - 월드 기준
        Vector3 screen2WorldSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        WPVariable.currentWorldSizeX = screen2WorldSize.x;
        WPVariable.currentWorldSizeY = screen2WorldSize.y;

    }


}
