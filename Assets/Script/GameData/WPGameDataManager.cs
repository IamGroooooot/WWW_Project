using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System;
using System.IO;

// 게임데이터 : (무결성 검사), json, 읽기만 할 수 있음. static
// 파, 상추, 감자, 배추, 30, 50, 150, 500
// 게임데이터 매니저 파서 구현(싱글톤) 
// awake 단계에서 
//(SW)데이터 불러오기 끝나면 값하나 true로 던져주기

public class WPGameDataManager : MonoBehaviour {

    /////////////////////////////////////////////////////////////////////////
    // Varaibles

    public static WPGameDataManager instance = null;        // for singleton

    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    private Dictionary<WPEnum.GameData, List<WPData>> gameData = new Dictionary<WPEnum.GameData, List<WPData>>();

    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        instance = this;
        foreach (WPEnum.GameData gameDataType in Enum.GetValues(typeof(WPEnum.GameData)))
        {
            LoadData(gameDataType);
        }
    }

    public List<T> GetData<T>(WPEnum.GameData _gameData) where T : WPData
    {
        if (gameData != null)
        {
            if (gameData.ContainsKey(_gameData))
            {
                List<T> newList = new List<T>();
                for(int i = 0; i < gameData[_gameData].Count; ++i)
                {
                    newList.Add((T)gameData[_gameData][i]);
                }
                return newList;
            }
        }
        return null;
    }

    private void LoadData(WPEnum.GameData _gameData)
    {
        string dataName = _gameData.ToString().Substring(1);
        string dataPath = Application.streamingAssetsPath + "/" + dataName + ".csv";

        if (!File.Exists(dataPath))
        {
            WPGameCommon._WPDebug("해당하는 파일이 존재하지 않습니다! : " + dataPath);
            return;
        }

        string csvString = File.ReadAllText(dataPath, System.Text.Encoding.UTF8);
        List<WPData> csvData = new List<WPData>();

        string[] lines = Regex.Split(csvString, LINE_SPLIT_RE);

        if (lines.Length > 1)
        {
            string[] header = Regex.Split(lines[0], SPLIT_RE);
            
            for (int i = 1; i < lines.Length; ++i)
            {
                string[] values = Regex.Split(lines[i], SPLIT_RE);

                if (values.Length <= 0) continue;

                string testString = string.Empty;

                Dictionary<string, object> entry = new Dictionary<string, object>();
                for (int j = 0; j < header.Length && j < values.Length; ++j)
                {
                    string value = values[j].TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                    entry[header[j]] = value;
                    testString += header[j] + ":" + value + "//";
                }

                switch (_gameData)
                {
                    case WPEnum.GameData.eNews:
                        {
                            if (WPData_News.CheckDataIntegrity(entry))
                            {
                                WPData_News newsData = new WPData_News(entry);
                                csvData.Add(newsData);
                            }
                            break;
                        }
                    case WPEnum.GameData.eSeed:
                        {
                            if (WPData_Seed.CheckDataIntegrity(entry))
                            {
                                WPData_Seed seedData = new WPData_Seed(entry);
                                csvData.Add(seedData);
                            } 
                            break;
                        }
                    case WPEnum.GameData.eFertilizer:
                        {
                            if (WPData_Fertilizer.CheckDataIntegrity(entry))
                            {
                                WPData_Fertilizer fertilizerData = new WPData_Fertilizer(entry);
                                csvData.Add(fertilizerData);
                            }
                            break;
                        }
                    case WPEnum.GameData.eEvent:
                        {
                            if (WPData_Event.CheckDataIntegrity(entry))
                            {
                                WPData_Event eventData = new WPData_Event(entry);
                                csvData.Add(eventData);
                            }
                            break;
                        }
                }
                
                WPGameCommon._WPDebug(testString);
            }
            gameData.Add(_gameData, csvData);
        }
    }

}