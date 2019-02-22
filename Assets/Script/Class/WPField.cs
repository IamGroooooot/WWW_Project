using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPField
{
    // 모든 객체에서 접근하는 데이터는 static으로 합시다.
	private static List<WPData_Seed> seedData = WPGameDataManager.instance.GetData<WPData_Seed>(WPEnum.GameData.eSeed);
    private static List<WPData_Fertilizer> fertilizerData = WPGameDataManager.instance.GetData<WPData_Fertilizer>(WPEnum.GameData.eFertilizer);

    public static WPField ParseData(string data)
    {
        // split String
        string[] data_1 = data.Split("(".ToCharArray(), 2);
        // simple integrity check
        if (data_1[0] != "WPField") return new WPField();

        string[] dataString = data_1[1].Substring(0, data_1[1].Length - 1).Split(":".ToCharArray(), 5);

        WPField newField = new WPField(
            Convert.ToInt32(dataString[0]),
            Convert.ToInt32(dataString[1]),
            Convert.ToInt32(dataString[2]));

        newField.isSick = Convert.ToBoolean(dataString[3]);
        newField.progress = Convert.ToSingle(dataString[4]);

        return newField;
    }

    public int seedIndex { get; private set; }
    public int workerIndex { get; private set; }
    public int fertilizerIndex { get; private set; }

    public bool isSick;             // 병충해에 감염된 상태인 지 판별하는 변수.

    private float progress;
    public float Progress
    {
        get
        {
            return progress;
        }
        set
        {
            progress = value;
        }
    }

    public float GrowthRate         // 성장률
    {
        get
        {
            if (seedIndex == -1) return -1f;
            int completeValue = seedData[seedIndex].GrowthTime; // 여기서 비료 등의 적용을 하면 됩니다.
            if(fertilizerIndex != -1)
            {
                if(fertilizerData[fertilizerIndex].ItemType == WPEnum.FertilizerType.eGrowth)
                {
                    completeValue -= fertilizerData[fertilizerIndex].Value;
                }
            }
            if (completeValue <= 0) return 1f;
            float growthRate = progress / completeValue;
            if (growthRate > 1) return 1f;
            return growthRate;
        }
    }

    private WPDateTime checkTime;

    /// <summary>
    /// 이 밭에 뭐가 심어져 있긴 한건지 확인합니다.
    /// </summary>
    public bool IsPlanted
    {
        get
        {
            return seedIndex != -1;
        }
    }

    /// <summary>
    /// 이 밭의 작물이 완성되었는지 확인합니다.
    /// 60퍼센트 이상이면 수확 가능?
    /// </summary>
    public bool IsCompleted
    {
        get
        {
            if (GrowthRate == -1f)
            {
                return false;
            }
            else
            {
                return GrowthRate >= 0.6f;
            }
        }
    }

    public WPField()
    {
        //Empty Field
        seedIndex = -1;
        workerIndex = -1;
        fertilizerIndex = -1;
    }

    public WPField(int _seedIndex, int _workerIndex, int _fertilizerIndex)
    {
        seedIndex = _seedIndex;
        workerIndex = _workerIndex;
        fertilizerIndex = _fertilizerIndex;

        isSick = false;
        progress = 0f;
    }

    public string ToData()
    {
        return string.Format("WPField({0}:{1}:{2}:{3}:{4})",
            seedIndex,
            workerIndex,
            fertilizerIndex,
            isSick,
            progress);
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
        int comparePrice = seedData[seedIndex].ComparePrice;
        return 0;
    }
    /*
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

		this.startedTime = this.startedTime.AddTimeData(1);
		//WPGameCommon._WPDebug("시작한 시각 : " + startedTime.ToString());

		//WPGameCommon._WPDebug("병충해로 식물 성장 멈춤!! |" + seedData[seedIndex].DataName.ToString() + "| 식물의 성장도 성장도: " + ((float)WPDateTime.CompareTime(WPDateTime.Now, startedTime) / seedData[seedIndex].GrowthTime).ToString());
		
	}
    */
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
