using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPEventManager : MonoBehaviour {

    /////////////////////////////////////////////////////////////////////////
    // Varaibles

    public static WPEventManager instance = null;

    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        instance = this;
        

    }

    private void Start()
    {
        AddEvent<WPTestEvent>();
    }

    public void AddEvent<T>() // 이 코드는 매우 위험합니다! 재검토와 도움이 필요합니다.
    {
       // if (false == typeof(T).IsAssignableFrom(typeof(WPEvent))) return; // 만일 Generic 이 WPEvent를 상속하지 않는다면 중지합니다.
        string eventName = typeof(T).ToString();
        /*
        if (GetComponent(typeof(T)) != null)
        {
            GameObject eventObject = new GameObject(eventName);
            eventObject.AddComponent(typeof(T));
            WPGameCommon._WPDebug("이벤트 추가 : " + eventName);
        }
        else
        {
            WPGameCommon._WPDebug("존재하지 않는 이벤트 이름입니다! : " + eventName);
        }*/
        GameObject eventObject = new GameObject(eventName);
        eventObject.AddComponent(typeof(T));
        WPGameCommon._WPDebug("이벤트 추가 : " + eventName);
    }


}
