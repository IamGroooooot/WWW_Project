using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class WPUserData {

    public int debt;
    public int money;
    public int level;
    public int weather;
    public string dateTime;

    public List<string> worker = new List<string>();
    public List<int> fertilizer = new List<int>();

    public List<List<List<int>>> newsData = new List<List<List<int>>>();
    public List<List<string>> fieldData = new List<List<string>>();

    public WPUserData()
    {
        debt = 0;
        money = 1000;
        level = 1;
        weather = 1;
        dateTime = WPDateTime.StandardDateTime.ToData();

        for (int i = 0; i < 12; ++i)
        {
            fertilizer.Insert(i, 5);
        }
        
        for(int i = 0; i < 4; ++i)
        {
            List<string> fieldString = new List<string>();
            for(int loop = 0; loop < 6; ++loop)
            {
                fieldString.Insert(loop, string.Empty);
            }
            fieldData.Insert(i, fieldString);
        }
    }

    [JsonConstructor]
    public WPUserData(int _debt, int _money, int _level, int _weather, string _dateTime, List<string> _worker, List<int> _fertilizer, List<List<List<int>>> _newsData, List<List<string>> _fieldData)
    {
        debt = _debt;
        money = _money;
        level = _level;
        weather = _weather;
        dateTime = _dateTime;
        worker = _worker;
        fertilizer = _fertilizer;
        newsData = _newsData;
        fieldData = _fieldData;
    }

    public static void SaveData(string path, WPUserData saveData)
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

    public static WPUserData LoadData(string path)
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

        WPUserData newData = null;

        if (string.IsNullOrEmpty(data))
        {
            WPGameCommon._WPDebug("유저 데이터 초기화");
            newData = new WPUserData();
            SaveData(path, newData);
        }
        else
        {
            WPGameCommon._WPDebug("유저 데이터 불러옴");
            newData = JsonConvert.DeserializeObject<WPUserData>(data);
        }

        return newData;
    }

}
