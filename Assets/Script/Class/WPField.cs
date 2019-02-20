using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPField
{
    // 모든 객체에서 접근하는 데이터는 static으로 합시다.
	private static List<WPData_Seed> seedData = WPGameDataManager.instance.GetData<WPData_Seed>(WPEnum.GameData.eSeed);

	public int seedIndex { get; private set; }
    public int workerIndex { get; private set; }
    public int fertilizerIndex { get; private set; }
	
	//병충해가 붙은지 한시간마다 true
	private bool isSubscribed = false;

	public WPDateTime startedTime { get; private set; }

    public static WPField ParseData(string data)
    {
        // split String
        string[] data_1 = data.Split("(".ToCharArray(), 2);
        // simple integrity check
        if (data_1[0] != "WPField") return new WPField();

        string[] dataString = data_1[1].Substring(0, data_1[1].Length).Split(":".ToCharArray(), 4);

        int seed = System.Convert.ToInt32(dataString[0]);
        int worker = System.Convert.ToInt32(dataString[1]);
        int fertilizer = System.Convert.ToInt32(dataString[2]);

        WPDateTime time = WPDateTime.ParseData(dataString[3]);

        return new WPField(seed, worker, fertilizer, time);
    }

    /// <summary>
    /// 이 밭의 작물이 완성되었는지 확인합니다.
    /// 60퍼센트 이상이면 수확 가능?
    /// </summary>
    public bool IsCompleted
    {
        get
        {
            //GetGrownPecent못구하면 false 반환
            if ((int)GetGrownPercent() == (int)-1)
            {
                return false;
            }
            else
            {
                return GetGrownPercent() >= 0.6f;
            }
        }
    }

    public WPField()
    {
        //Empty Field
        seedIndex = -1;
        workerIndex = -1;
        fertilizerIndex = -1;
        startedTime = null;
    }

    public WPField(int _seedIndex, int _workerIndex, int _fertilizerIndex, WPDateTime _startedTime)
    {
        seedIndex = _seedIndex;
        workerIndex = _workerIndex;
        fertilizerIndex = _fertilizerIndex;
        startedTime = _startedTime;
    }

    public string ToData()
    {
        return string.Format("WPField({0}:{1}:{2}:{3})", seedIndex, workerIndex, fertilizerIndex, startedTime.ToData());
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
        int comparePrice = Convert.ToInt32(((WPData_Seed)seedData[seedIndex]).ComparePrice);
        return 0;
    }

	//병충해 이벤트 구독!
	public void SubscribeSickEvent()
	{
		if (isSubscribed)
		{
			return;
		}
		WPDateTime.Now.OnValueChanged += IsSick;
		isSubscribed = true;
	}

	//병충해 이벤트 구독 취소!
	public void UnsubscribeSickEvent()
	{
		if (isSubscribed == false)
		{
			return;
		}
		WPDateTime.Now.OnValueChanged -= IsSick;
		isSubscribed = false;
	}

	//isSick!! 시작 시각에 1시간 더하기
	private void IsSick(WPDateTime content)
	{
		//병충해에 걸린 경우 시작한 시각 + 1시간
		this.startedTime = this.startedTime.AddTimeData(1);
		WPGameCommon._WPDebug("시작한 시각 : " + startedTime.ToString());
		WPGameCommon._WPDebug("병충해로 식물 성장 멈춤!! |" + seedData[seedIndex].DataName.ToString() + "| 식물의 성장도 성장도: " + ((float)WPDateTime.CompareTime(WPDateTime.Now, startedTime) / seedData[seedIndex].GrowthTime).ToString());
		
	}

	public float GetGrownPercent()
	{
        //시간의 줄여주는 비료를 고려하는 코드
        //짜야됨

        //심은 일 수 

        //WPDateTime.Compare못하는 경우
        if (WPDateTime.CompareTime(WPDateTime.Now, startedTime) == -1)
        {
            return -1f;
        }

        float percent = 
            (float)WPDateTime.CompareTime(WPDateTime.Now, startedTime) /
            seedData[seedIndex].GrowthTime;

        if (percent > 1f)
        {
            percent = 1f;
        }

        return percent;
	}

    public static Vector2 FieldPos(int fieldIndex)
    {
        if (fieldIndex == 0)
        {
			return new Vector2(-WPVariable.currentWorldSizeX / 2, (WPVariable.currentWorldSizeY / 4));
        }
        else if (fieldIndex == 1)
        {
            return new Vector2(WPVariable.currentWorldSizeX / 2, (WPVariable.currentWorldSizeY / 4));
        }
        else if (fieldIndex == 2)
        {
            return new Vector2(-WPVariable.currentWorldSizeX / 2, (-WPVariable.currentWorldSizeY / 4));
        }
        else if (fieldIndex == 3)
        {
            return new Vector2(WPVariable.currentWorldSizeX / 2, (-WPVariable.currentWorldSizeY / 4));
        }
        else if (fieldIndex == 4)
        {
            return new Vector2(-WPVariable.currentWorldSizeX / 2, ((-3f) * WPVariable.currentWorldSizeY / 4));
        }
        else if (fieldIndex == 5)
        {
            return new Vector2(WPVariable.currentWorldSizeX / 2, ((-3f) * WPVariable.currentWorldSizeY / 4));
        }
        else
        {
            return new Vector2(10000, 10000);
        }
    }


}
