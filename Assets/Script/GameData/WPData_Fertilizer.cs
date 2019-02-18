using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPData_Fertilizer : WPData
{
    public int ID { get; private set; }
    public string Name { get; private set; }
    public string DataName { get; private set; }
    public WPEnum.FertilizerType ItemType { get; private set; }
    public int Tier { get; private set; }
    public int Value { get; private set; }
    public int Price { get; private set; }
    public string Description { get; private set; }

    public WPData_Fertilizer(Dictionary<string, object> args)
    {
        ID = System.Convert.ToInt32(args["eID"]);
        Name = args["eName"].ToString();
        DataName = args["eDataName"].ToString();
        ItemType = (WPEnum.FertilizerType)System.Enum.Parse(typeof(WPEnum.FertilizerType), args["eItemType"].ToString());
        Tier = System.Convert.ToInt32(args["eTier"]);
        if(ItemType == WPEnum.FertilizerType.eWeatherResist)
        {
            Value = System.Convert.ToInt32(System.Enum.Parse(typeof(WPEnum.Weather), args["eValue"].ToString()));
        }
        else
        {
            Value = System.Convert.ToInt32(args["eValue"]);
        }
        Price = System.Convert.ToInt32(args["ePrice"]);
        Description = args["eDescription"].ToString();
    }

    public new static bool CheckDataIntegrity(Dictionary<string, object> args)
    {
        if (args.Count != 8) return false;

        int checkCount = 0;
        WPEnum.FertilizerType itemType = WPEnum.FertilizerType.ERROR;

        foreach (KeyValuePair<string, object> keyValuePair in args)
        {
            WPEnum.Header_Fertilizer header = (WPEnum.Header_Fertilizer)System.Enum.Parse(typeof(WPEnum.Header_Fertilizer), keyValuePair.Key);
            switch (header)
            {
                case WPEnum.Header_Fertilizer.ERROR:
                    {
                        WPGameCommon._WPDebug("WPData_Fertilizer // Wrong Header! :" + keyValuePair.Key);
                        return false;
                    }
                case WPEnum.Header_Fertilizer.eID:
                case WPEnum.Header_Fertilizer.eTier:
                case WPEnum.Header_Fertilizer.ePrice:
                    {
                        int checkInt;
                        string datum = keyValuePair.Value.ToString();
                        if (string.IsNullOrEmpty(datum))
                        {
                            WPGameCommon._WPDebug("WPData_Fertilizer // Empty Value! :" + keyValuePair.Key);
                            return false;
                        }
                        if (!int.TryParse(datum, out checkInt))
                        {
                            WPGameCommon._WPDebug("WPData_Fertilizer // Wrong Value! :" + keyValuePair.Key + " : " + datum);
                            return false;
                        }
                        checkCount++;
                        break;
                    }
                case WPEnum.Header_Fertilizer.eName:
                case WPEnum.Header_Fertilizer.eDataName:
                case WPEnum.Header_Fertilizer.eDescription:
                    {
                        string datum = keyValuePair.Value.ToString();
                        if (string.IsNullOrEmpty(datum))
                        {
                            WPGameCommon._WPDebug("WPData_Fertilizer // Empty Value! :" + keyValuePair.Key);
                            return false;
                        }
                        checkCount++;
                        break;
                    }
                case WPEnum.Header_Fertilizer.eItemType:
                    {
                        string datum = keyValuePair.Value.ToString();
                        if (string.IsNullOrEmpty(datum))
                        {
                            WPGameCommon._WPDebug("WPData_Fertilizer // Empty Value! :" + keyValuePair.Key);
                            return false;
                        }

                        itemType = (WPEnum.FertilizerType)System.Enum.Parse(typeof(WPEnum.FertilizerType), datum);

                        if (itemType == WPEnum.FertilizerType.ERROR)
                        {
                            WPGameCommon._WPDebug("WPData_Fertilizer // Wrong Value! :" + keyValuePair.Key + " : " + datum);
                            return false;
                        }
                        checkCount++;
                        break;
                    }
            }
        }

        int valueInt = -1;
        string valueDatum = args["eValue"].ToString();

        if (string.IsNullOrEmpty(valueDatum))
        {
            WPGameCommon._WPDebug("WPData_Fertilizer // Empty Value! :" + "eValue");
            return false;
        }
        if (itemType == WPEnum.FertilizerType.eWeatherResist)
        {
            if ((WPEnum.Weather)System.Enum.Parse(typeof(WPEnum.Weather), valueDatum) == WPEnum.Weather.ERROR)
            {
                WPGameCommon._WPDebug("WPData_Fertilizer // Wrong Value! :" + "eValue" + " : " + valueDatum);
                return false;
            }
        }
        else
        {
            if (!int.TryParse(valueDatum, out valueInt))
            {
                WPGameCommon._WPDebug("WPData_Fertilizer // Wrong Value! :" + "eValue" + " : " + valueDatum);
                return false;
            }
        }
        checkCount++;

        if (checkCount < 8) WPGameCommon._WPDebug("WPData_Fertilizer // Invalid Data! :" + checkCount);

        return checkCount == 8;
    }

}
