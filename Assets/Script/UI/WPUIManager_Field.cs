using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUIManager_Field : MonoBehaviour {
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUIManager_Field instance = null;     // singleton
    //작업 중 작물, 남은 시간, 일하는 일꾼, 비료, 일꾼 정보,비료 정보, 골드 표시
    string[] mSeeds = new string[6];
    


    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
    }

    // 초기 설정을 합니다.
    private void Init()
    {
        
        SetActive(false);
    }

    // Close 버튼을 클릭했을 때 호출합니다.
    public void OnClick_Close()
    {
        SetActive(false);
    }

    void SetActive(bool param)
    {
        if (param)
        {
            transform.localPosition = Vector2.zero;
        }
        else
        {
            transform.localPosition = new Vector2(-10000, 0);
        }
    }
}
