using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

// 유저 데이터를 관리하는 매니저.
// 돈, 빚, 일꾼, 비료 보유 등 유저의 데이터를 저장하고 관리합니다.

public class WPUserDataManager : MonoBehaviour {

    public class UserData
    {
        public int debt;
        public int money;
        public int[] worker;
        public int[] fertilizer;

        public UserData()
        {
            debt = 0;
            money = 0;
            worker = new int[12];
            fertilizer = new int[12];
        }
    }
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUserDataManager instance = null;        // for singleton

    private static string DATA_PATH = SetPath() + "UserData.dat";

    private static string SetPath()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.IPhonePlayer:
                return "file:///" + Application.streamingAssetsPath;
            case RuntimePlatform.WindowsEditor:
                return Application.dataPath + "/UserData";
            case RuntimePlatform.Android:
            case RuntimePlatform.WindowsPlayer:
                {
                    Directory.CreateDirectory(Application.persistentDataPath + "/UserData");
                    return Application.persistentDataPath + "/UserData";
                }
            default:
                return string.Empty;
        }
    }

    private UserData userData;

    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        instance = this;
    }

    public void SaveData()
    {
        try
        {
            StreamWriter writer = new StreamWriter(DATA_PATH, false);
            writer.WriteLine(JsonConvert.SerializeObject(userData, Formatting.Indented));
            writer.Close();
        }
        catch (IOException e)
        {
            WPGameCommon._WPDebug(e);
        }
    }

    private void LoadData()
    {
        string data = string.Empty;
        try
        {
            StreamReader streamReader = new StreamReader(DATA_PATH);
            data = streamReader.ReadToEnd();
            streamReader.Close();
        }
        catch(IOException e)
        {
            WPGameCommon._WPDebug(e);
        }

        UserData newData = JsonConvert.DeserializeObject<UserData>(data);

        if (newData != null) userData = newData;

    }

}
