using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPField
{
    // 모든 객체에서 접근하는 데이터는 static으로 합시다.
	private static List<Dictionary<string, object>> seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.eSeed);

	public int seedIndex { get; private set; }
    public int workerIndex { get; private set; }
    public int fertilizerIndex { get; private set; }
    WPDateTime startedTime;

    /// <summary>
    /// 이 밭의 작물이 완성되었는지 확인합니다.
    /// 60퍼센트 이상이면 수확 가능?
    /// </summary>
    public bool IsCompleted
    {
        get
        {
            return GetGrownPercent() >= 0.6f;
        }
    }

    public WPField()
    {

    }

    public WPField(int _seedIndex, int _workerIndex, int _fertilizerIndex, WPDateTime _startedTime)
    {
        seedIndex = _seedIndex;
        workerIndex = _workerIndex;
        fertilizerIndex = _fertilizerIndex;
        startedTime = _startedTime;
    }

    /// <summary>
    /// 축적된 Score를 이용하여
    /// 밭의 보상을 획득하는 코드
    /// </summary>
    /// <returns>GOLD</returns>
    public int CheckGold()
    {
        //return gold
        //불변 값 + 가변값
        //gold에 비료나 날씨에 의한 버프 넣은 후 
        //마직막에 불변값 계산해서 넣기
        int gold = Convert.ToInt32(seedData[seedIndex]["eComparePrice"]);
        return 0;
    }

	public float GetGrownPercent()
	{
        //시간의 줄여주는 비료를 고려하는 코드
        //짜야됨

        //심은 일 수 

		float percent = 
            WPDateTime.CompareTime(WPDateTime.Now, startedTime) /
            (Convert.ToInt32(seedData[seedIndex]["eGrowthTime"]) * 24);

        if (percent > 1f) percent = 1f;

        return percent;
	}
}
