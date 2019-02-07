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
    /// </summary>
    public bool CheckIfCompleted()
    {
        List<Dictionary<string, object>> seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.Seed);
        // Dictionary<string, object> seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.Seed)[0]; // 식물 이름만 따로 enum으로 저장해놓든가 해야할듯.

        Debug.Log(seedData[0]["eName"]);

        return false;
    }
}
