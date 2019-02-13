using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPField
{
	List<Dictionary<string, object>> seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.eSeed);
    int index;
	string currentCrop;
    string startedTime;
    string worker;
    string fertilizer;
    int gold;

    public WPField(int _index,string _currentCrop, string _startedTime, string _worker, string _fertilizer, int _gold)
    {
        this.index=_index;
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
        
        // Dictionary<string, object> seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.Seed)[0]; // 식물 이름만 따로 enum으로 저장해놓든가 해야할듯.
		

        if (GetGrownPercent()>=60)
        {
            return true;
        }
        else
        {
            return false;
        }

        //Debug.Log(seedData[0][WPEnum.CSV_Index.eGrowthTime.ToString()]);
    }

    /// <summary>
    /// 축적된 Score를 이용하여
    /// 밭의 보상을 획득하는 코드
    /// </summary>
    /// <returns>GOLD</returns>
    public int CheckGold()
    {
        //returm gold
        //불변 값 + 가변값
        //gold에 비료나 날씨에 의한 버프 넣은 후 
        //마직막에 불변값 계산해서 넣기
        return 0;
    }

    /// <summary>
    /// StartedTime이랑 현재 시각(게임상의 시간)과 비교
    /// 주의할 것 : 단위는 DAY이다
    /// 
    /// 임시로 10일 반환하게 함. 수정해야됨 !!
    /// </summary>
    /// <returns></returns>
    private int TimeCompare()
    {
		
        return 10;
    }

	public float GetGrownPercent()
	{
		//시간의 줄여주는 비료를 고려하는 코드
		//짜야됨

		//심은 일 수 
		float timePassed = TimeCompare(); //(단위는 Day로 할 것), 일단 임시로 10 넣어둠.
		int targetID = Find_EID();

		float Percent = timePassed / (float)(seedData[targetID][WPEnum.CSV_Index.eGrowthTime.ToString()]);

		if (Percent > 100f)
		{
			return -1;
		}

		return Percent;
	}

	public int Find_EID()
	{
		//Find eID
		int targetID;

		for (targetID = 0; targetID < 13; targetID++)
		{
			if (targetID == 12) //해당하는 작물의 ID를 못찾은 경우
			{
				return -1;
				//WPGameCommon._WPDebug("Cannot Find My Target ID - FieldCtrl");
			}

			//타깃의 Name 이랑 Fieldz클라스의 currentCrop이랑 비교-이렇게 하는거 맞낭?
			if (string.Equals(seedData[targetID][WPEnum.CSV_Index.eName.ToString()].ToString(), currentCrop, System.StringComparison.CurrentCultureIgnoreCase))
			{
				return targetID;
			}
		}
		return targetID;
	}
}
