using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WPOffTimer : MonoBehaviour {

    private void OnApplicationPause(bool pause)
    {
        //현재 시각 저장
        WPUserDataManager.instance.SaveData();
        WPGameCommon._WPDebug("Game Paused, 현재 시각 저장 완료");
    }

    private void OnApplicationQuit()
    {
        //현재 시각 저장
        WPUserDataManager.instance.SaveData();
        WPGameCommon._WPDebug("Game Quit, 현재 시각 저장 완료");
    }

}
