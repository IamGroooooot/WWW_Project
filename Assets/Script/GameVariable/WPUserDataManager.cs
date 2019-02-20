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
        public List<string> worker = new List<string>();
        public List<int> fertilizer = new List<int>();

        public UserData()
        {
            debt = 0;
            money = 1000;
            for(int i = 0; i < 12; ++i)
            {
                fertilizer.Insert(i, 1);
            }
        }

        [JsonConstructor]
        public UserData(int _debt, int _money, List<string> _worker, List<int> _fertilizer)
        {
            debt = _debt;
            money = _money;
            worker = _worker;
            fertilizer = _fertilizer;
        }

        public static void SaveData(string path, UserData saveData)
        {
            try
            {
                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine(JsonConvert.SerializeObject(saveData, Formatting.Indented));
                writer.Close();
            }
            catch (IOException e)
            {
                WPGameCommon._WPDebug(e);
            }
        }

        public static UserData LoadData(string path)
        {
            string data = string.Empty;
            try
            {
                StreamReader streamReader = new StreamReader(path);
                data = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (IOException e)
            {
                WPGameCommon._WPDebug(e);
            }

            UserData newData = null;

            if (string.IsNullOrEmpty(data))
            {
                WPGameCommon._WPDebug("유저 데이터 초기화");
                newData = new UserData();
                SaveData(path, newData);
            }
            else
            {
                WPGameCommon._WPDebug("유저 데이터 불러옴");
                newData = JsonConvert.DeserializeObject<UserData>(data);
            }

            return newData;
        }
    }

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

        if (string.IsNullOrEmpty(DATA_PATH)) DATA_PATH = SetPath() + "UserData.dat";

        userData = UserData.LoadData(DATA_PATH);
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
        UserData.SaveData(DATA_PATH, userData);
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
        UserData.SaveData(DATA_PATH, userData);
    }

}
