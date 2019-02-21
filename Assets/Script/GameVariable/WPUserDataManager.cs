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

    public int Level
    {
        get
        {
            if (userData == null) return 0;
            if (userData.level == 0) userData.level = 1; 
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

    public void SaveData()
    {
        WPUserData.SaveData(DATA_PATH, userData);
    }



}
