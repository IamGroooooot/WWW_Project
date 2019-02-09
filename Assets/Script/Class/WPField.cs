using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPField
{
    string currentCrop;
    string startedTime;
    string worker;
    string fertilizer;
    int gold;

    public WPField(string _currentCrop, string _startedTime, string _worker, string _fertilizer, int _gold)
    {
        this.currentCrop = _currentCrop;
        this.startedTime = _startedTime;
        this.worker = _worker;
        this.fertilizer = _fertilizer;
        this.gold = _gold;
    }

    /// <summary>
    /// 이 밭의 작물이 완성되었는지 확인합니다.
    /// 60퍼센트 이상이면 됨.
    /// </summary>
    public bool CheckIfCompleted()
    {
        List<Dictionary<string, object>> seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.Seed);
        // Dictionary<string, object> seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.Seed)[0]; // 식물 이름만 따로 enum으로 저장해놓든가 해야할듯.

        //시간의 줄여주는 비료를 고려하는 코드
        //짜야됨

        //StartedTime과 현재 시간 비교 
        float TimePassed=10; //(단위는 Day로 할 것), 일단 임시로 10 넣어둠.

        //Find eID
        int targetID;
        for(targetID = 0; targetID < 13; targetID++)
        {
            if (targetID == 12) //해당하는 작물의 ID를 못찾은 경우
            {
                return false;
                //WPGameCommon._WPDebug("Cannot Find My Target ID - FieldCtrl");
            }

            //타깃의 Name 이랑 Fieldz클라스의 currentCrop이랑 비교-이렇게 하는거 맞낭?
            if (string.Equals(seedData[targetID][WPEnum.CSV_Index.eName.ToString()].ToString(), currentCrop,System.StringComparison.CurrentCultureIgnoreCase)) 
            {
                break;
            }      
        }

        if (TimePassed/ (float)(seedData[targetID][WPEnum.CSV_Index.eGrowthTime.ToString()]) >=60)
        {
            return true;
        }
        else
        {
            return false;
        }

        //Debug.Log(seedData[0][WPEnum.CSV_Index.eGrowthTime.ToString()]);
                
    }
}
