using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPData_Seed : WPData
{
    public int ID { get; private set; }
    public string Name { get; private set; }
    public string DataName { get; private set; }
    public int GrowthTime { get; private set; }
    public int ComparePrice { get; private set; }
    public int SalePrice { get; private set; }
    public int ScoreIncreaseRatio { get; private set; }
    public WPEnum.Weather BuffWeather { get; private set; }
    public WPEnum.Weather DebuffWeather { get; private set; }
    public int UnlockLevel { get; private set; }
    public string Description { get; private set; }

    public WPData_Seed(Dictionary<string, object> args)
    {
        ID = System.Convert.ToInt32(args["eID"]);
        Name = args["eName"].ToString();
        DataName = args["eDataName"].ToString();
        GrowthTime = System.Convert.ToInt32(args["eGrowthTime"]);
        ComparePrice = System.Convert.ToInt32(args["eComparePrice"]);
        SalePrice = System.Convert.ToInt32(args["eSalePrice"]);
        ScoreIncreaseRatio = System.Convert.ToInt32(args["eScoreIncreaseRatio"]);
        BuffWeather = (WPEnum.Weather)System.Enum.Parse(typeof(WPEnum.Weather), args["eBuffWeather"].ToString());
        DebuffWeather = (WPEnum.Weather)System.Enum.Parse(typeof(WPEnum.Weather), args["eDebuffWeather"].ToString());
        UnlockLevel = System.Convert.ToInt32(args["eUnlockLevel"]);
        Description = args["eDescription"].ToString();
    }

    public new static bool CheckDataIntegrity(Dictionary<string, object> args)
    {
        if (args.Count != 11) return false;

        int checkCount = 0;

        foreach(KeyValuePair<string, object> keyValuePair in args)
        {
            WPEnum.Header_Seed header = (WPEnum.Header_Seed)System.Enum.Parse(typeof(WPEnum.Header_Seed), keyValuePair.Key);
            switch (header)
            {
                case WPEnum.Header_Seed.ERROR:
                    {
                        WPGameCommon._WPDebug("WPData_Seed // Wrong Header! :" + keyValuePair.Key);
                        return false;
                    }
                case WPEnum.Header_Seed.eID:
                case WPEnum.Header_Seed.eGrowthTime:
                case WPEnum.Header_Seed.eComparePrice:
                case WPEnum.Header_Seed.eSalePrice:
                case WPEnum.Header_Seed.eScoreIncreaseRatio:
                case WPEnum.Header_Seed.eUnlockLevel:
                    {
                        int checkInt;
                        string datum = keyValuePair.Value.ToString();
                        if (string.IsNullOrEmpty(datum))
                        {
                            WPGameCommon._WPDebug("WPData_Seed // Empty Value! :" + keyValuePair.Key);
                            return false;
                        }
                        if (!int.TryParse(datum, out checkInt))
                        {
                            WPGameCommon._WPDebug("WPData_Seed // Wrong Value! :" + keyValuePair.Key + " : " + datum);
                            return false;
                        }
                        checkCount++;
                        break;
                    }
                case WPEnum.Header_Seed.eDescription:
                case WPEnum.Header_Seed.eName:
                case WPEnum.Header_Seed.eDataName:
                    {
                        string datum = keyValuePair.Value.ToString();
                        if (string.IsNullOrEmpty(datum))
                        {
                            WPGameCommon._WPDebug("WPData_Seed // Empty Value! :" + keyValuePair.Key);
                            return false;
                        }
                        checkCount++;
                        break;
                    }
                case WPEnum.Header_Seed.eBuffWeather:
                case WPEnum.Header_Seed.eDebuffWeather:
                    {
                        string datum = keyValuePair.Value.ToString();
                        if (string.IsNullOrEmpty(datum))
                        {
                            WPGameCommon._WPDebug("WPData_Seed // Empty Value! :" + keyValuePair.Key);
                            return false;
                        }
                        if ((WPEnum.Weather)System.Enum.Parse(typeof(WPEnum.Weather), datum) == WPEnum.Weather.ERROR)
                        {
                            WPGameCommon._WPDebug("WPData_Seed // Wrong Value! :" + keyValuePair.Key + " : " + datum);
                            return false;
                        }
                        checkCount++;
                        break;
                    }
            }
        }

        if (checkCount < 11) WPGameCommon._WPDebug("WPData_Seed // Invalid Data! :" + checkCount);

        return checkCount == 11;
    }

}
