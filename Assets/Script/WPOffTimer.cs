using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WPOffTimer : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnApplicationPause(bool pause)
    {
        //현재 시각 저장
        WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eUserDate, WPDateTime.Now.ToData());
        WPGameCommon._WPDebug("Game Paused, 현재 시각 저장 완료");
    }

    private void OnApplicationQuit()
    {
        //현재 시각 저장
        WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eUserDate, WPDateTime.Now.ToData());
        WPGameCommon._WPDebug("Game Quit, 현재 시각 저장 완료");
    }

    DateTime getNow_DateTime()
    {
        DateTime dateTime = DateTime.Now;
        return dateTime;
    }
    
}
