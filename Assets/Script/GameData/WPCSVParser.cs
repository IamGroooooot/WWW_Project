using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;
using System.IO;

public class WPCSVParser : MonoBehaviour
{
    private static bool isLoaded = false;
    public static bool IsLoaded
    {
        get
        {
            return isLoaded;
        }
        private set
        {
            isLoaded = value;
        }
    }

    public static int Count
    {
        get
        {
            if (IsLoaded)
            {
                return csvData.Count;
            }
            else return 0;
        }
    }

    static string DATA_PATH;

    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    static List<Dictionary<string, object>> csvData = new List<Dictionary<string, object>>();

    public static Dictionary<string, object> GetDataByIndex(int index)
    {
        if (IsLoaded)
        {
            if (csvData != null)
            {
                if (0 <= index && index < csvData.Count && csvData[index] != null)
                {
                    return csvData[index];
                }
                else return null;
            }
            else return null;
        }
        else return null;
    }

    public static void LoadData()
    {
        string csvString = File.ReadAllText(DATA_PATH, System.Text.Encoding.UTF8);

        csvData = new List<Dictionary<string, object>>();

        string[] lines = Regex.Split(csvString, LINE_SPLIT_RE);

        if (lines.Length > 1)
        {
            string[] header = Regex.Split(lines[0], SPLIT_RE);
            for (int i = 1; i < lines.Length; ++i)
            {
                string[] values = Regex.Split(lines[i], SPLIT_RE);

                // string testString = "Index : " + i + "\n";

                if (values.Length == 0) continue;

                Dictionary<string, object> entry = new Dictionary<string, object>();
                for (int j = 0; j < header.Length && j < values.Length; ++j)
                {
                    string value = values[j].TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                    entry[header[j]] = value;
                    // testString += header[j] + " : " + value + "\n";
                }
                csvData.Add(entry);
                // WPGameCommon._WPDebug(testString);
            }
        }
        IsLoaded = true;
    }

    private void Awake()
    {
        DATA_PATH = Application.streamingAssetsPath + "/News.csv";
        LoadData();
    }
}