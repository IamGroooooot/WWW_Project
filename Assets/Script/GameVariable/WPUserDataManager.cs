using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

// 유저 데이터를 관리하는 매니저.
// 돈, 빚, 일꾼, 비료 보유 등 유저의 데이터를 저장하고 관리합니다.

public class WPUserDataManager : MonoBehaviour {

    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUserDataManager instance = null;        // for singleton

    private static string DATA_PATH = string.Empty;

    private static string SetPath()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.IPhonePlayer:
                return "file:///" + Application.streamingAssetsPath;
            case RuntimePlatform.WindowsEditor:
                Directory.CreateDirectory(Application.dataPath + "/UserData");
                return Application.dataPath + "/UserData/";
            case RuntimePlatform.Android:
            case RuntimePlatform.WindowsPlayer:
                {
                    Directory.CreateDirectory(Application.persistentDataPath + "/UserData");
                    return Application.persistentDataPath + "/UserData/";
                }
            default:
                return string.Empty;
        }
    }

    private WPUserData userData;

    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        instance = this;

        if (string.IsNullOrEmpty(DATA_PATH)) DATA_PATH = SetPath() + "UserData.dat";

        userData = WPUserData.LoadData(DATA_PATH);


    }

    public int Debt
    {
        get
        {
            if (userData == null) return 0;
            return userData.debt;
        }
        set
        {
            if (userData == null) return;
            userData.debt = value;
            SaveData();
        }
    }

    public int Money
    {
        get
        {
            if (userData == null) return 0;
            return userData.money;
        }
        set
        {
            if (userData == null) return;
            userData.money = value;
            SaveData();
        }
    }

    public int Level
    {
        get
        {
            if (userData == null) return 0;
            if (userData.level <= 0) userData.level = 1;
            return userData.level;
        }
        set
        {
            if (userData == null) return;
            if (value < 1) userData.level = 1;
            else if (value > 4) userData.level = 4;
            else userData.level = value;
            SaveData();
        }
    }

    public WPEnum.Weather Weather
    {
        get
        {
            if (userData == null) return WPEnum.Weather.ERROR;
            if (userData.weather <= 0 || userData.weather > System.Enum.GetValues(typeof(WPEnum.Weather)).Length) return WPEnum.Weather.ERROR;
            return (WPEnum.Weather)userData.weather;
        }
        set
        {
            if (userData == null) return;
            if (value <= 0 || (int)value > System.Enum.GetValues(typeof(WPEnum.Weather)).Length) return;
            userData.weather = (int)value;
            SaveData();
        }
    }

    public WPDateTime DateTime
    {
        get
        {
            if (userData == null) return null;
            if (string.IsNullOrEmpty(userData.dateTime))
            {
                userData.dateTime = WPDateTime.StandardDateTime.ToData();
            }
            return WPDateTime.ParseData(userData.dateTime);
        }
        set
        {
            if (userData == null) return;
            userData.dateTime = value.ToData();
            SaveData();
        }
    }

    public WPWorker GetWorker(int index)
    {
        if (userData == null) return null;
        if (userData.worker == null) return null;
        if (index < 0 || index >= userData.worker.Count) return null;
        return null;
    }

    public int GetWorkerCount()
    {
        if (userData == null) return 0;
        if (userData.worker == null) return 0;
        return userData.worker.Count;
    }

    public void SetWorker(int index, WPWorker value)
    {
        if (userData == null) return;
        if (userData.worker == null) return;
        if (index < 0 || index >= userData.worker.Count) return;
        userData.worker[index] = value.ToString();
        SaveData();
    }

    public int GetFertilizer(int index)
    {
        if (userData == null) return 0;
        if (userData.fertilizer == null) return 0;
        if (index < 0 || index >= userData.fertilizer.Count) return 0;
        return userData.fertilizer[index];
    }

    public int GetFertilizerCount()
    {
        if (userData == null) return 0;
        if (userData.fertilizer == null) return 0;
        return userData.fertilizer.Count;
    }

    public void SetFertilizer(int index, int value)
    {
        if (userData == null) return;
        if (userData.fertilizer == null) return;
        if (index < 0 || index >= userData.fertilizer.Count) return;
        userData.fertilizer[index] = value;
        SaveData();
    }

    public void SetFieldData(int level, int index, string content)
    {
        if (userData == null) return;
        if (userData.fieldData == null) return;
        if (level < 0 || level > userData.fieldData.Count) return;
        if (index < 0 || index >= userData.fieldData[level].Count) return;
        userData.fieldData[level - 1][index] = content;         // level은 초기값이 1이다.
        SaveData();
    }

    public string GetFieldData(int level, int index)
    {
        if (userData == null) return null;
        if (userData.fieldData == null) return null;
        if (level < 0 || level > userData.fieldData.Count) return null;
        if (index < 0 || index >= userData.fieldData[level].Count) return null;
        return userData.fieldData[level - 1][index];
    }

    public List<int> GetNewsDataByDateTime(WPDateTime nowTime)
    {
        if (userData == null) return null;
        if (userData.newsData == null) return null;
        int nowYear = nowTime.Year - WPDateTime.STANDARD_YEAR + 1;
        int nowMonth = nowTime.Month;

        WPGameCommon._WPDebug("현재 년도차 : " + nowYear + " 현재 월차 : " + nowMonth);

        if (userData.newsData.Count < nowYear)
        {
            for (int yearLoop = 0; yearLoop < nowYear; ++yearLoop)
            {
                if (yearLoop >= userData.newsData.Count) userData.newsData.Insert(yearLoop, new List<List<int>>());
                for (int monthLoop = userData.newsData[yearLoop].Count; monthLoop < (yearLoop < (nowYear - 1) ? 12 : nowMonth); ++monthLoop)
                {
                    WPGameCommon._WPDebug(yearLoop + "년차 " + (monthLoop + 1) + "월차 데이터 부재, 새로 생성");
                    userData.newsData[yearLoop].Insert(monthLoop, WPGameDataManager.instance.GetData<WPData_Event>(WPEnum.GameData.eEvent)[monthLoop].GetNewsIDByCount(3));
                }
            }
            SaveData();
        }

        if (userData.newsData[nowYear - 1].Count < nowMonth)
        {
            for (int monthLoop = userData.newsData[nowYear - 1].Count; monthLoop < nowMonth; ++monthLoop)
            {
                WPGameCommon._WPDebug((nowYear - 1) + "년차 " + (monthLoop + 1) + "월차 데이터 부재, 새로 생성");
                userData.newsData[nowYear - 1].Insert(monthLoop, WPGameDataManager.instance.GetData<WPData_Event>(WPEnum.GameData.eEvent)[monthLoop].GetNewsIDByCount(3));
            }
            SaveData();
        }

        return userData.newsData[nowYear - 1][nowMonth - 1];
    }

    public void SaveData()
    {
        WPUserData.SaveData(DATA_PATH, userData);
    }



}
