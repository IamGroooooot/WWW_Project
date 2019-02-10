using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUIManager_Toast : WPUIManager {

    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUIManager_Toast instance = null;     // singleton

    private GameObject toastObject;
    private Transform toastTransform;

    protected override void Init()
    {
        instance = this;

        toastObject = transform.Find("ToastObject").gameObject;
        toastTransform = transform.Find("ToastTransform");
    }

    /// <summary>
    /// content를 time 동안 띄우는 Toast를 생성합니다.
    /// </summary>
    public void MakeToast(string content, float time)
    {
        GameObject newToast = Instantiate(toastObject);
        newToast.SetActive(true);
        newToast.transform.SetParent(toastTransform);
        newToast.GetComponent<WPToast>().MakeToast(content, time);
    }

}
