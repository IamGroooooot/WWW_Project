using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System;
using System.IO;
using LitJson;
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

    private Dictionary<WPEnum.GameData, List<Dictionary<string, object>>> gameData = new Dictionary<WPEnum.GameData, List<Dictionary<string, object>>>();

    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        foreach (WPEnum.GameData gameDataType in Enum.GetValues(typeof(WPEnum.GameData)))
        {
            LoadData(gameDataType);
        }
    }

    public List<Dictionary<string, object>> GetData(WPEnum.GameData _gameData)
    {
        if (gameData != null)
        {
            if (gameData.ContainsKey(_gameData))
            {
                return gameData[_gameData];
            }
        }
        return null;
    }

    private void LoadData(WPEnum.GameData _gameData)
    {
        string dataName = _gameData.ToString();
        string dataPath = Application.streamingAssetsPath + "/" + dataName + ".csv";

        if (!File.Exists(dataPath)) return;

        string csvString = File.ReadAllText(dataPath, System.Text.Encoding.UTF8);
        List<Dictionary<string, object>> csvData = new List<Dictionary<string, object>>();

        string[] lines = Regex.Split(csvString, LINE_SPLIT_RE);

        if (lines.Length > 1)
        {
            string[] header = Regex.Split(lines[0], SPLIT_RE);
            for (int i = 1; i < lines.Length; ++i)
            {
                string[] values = Regex.Split(lines[i], SPLIT_RE);

                if (values.Length <= 0) continue;

                Dictionary<string, object> entry = new Dictionary<string, object>();
                for (int j = 0; j < header.Length && j < values.Length; ++j)
                {
                    string value = values[j].TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                    entry[header[j]] = value;

                }
                csvData.Add(entry);
            }
            gameData.Add(_gameData, csvData);
        }
    }

}