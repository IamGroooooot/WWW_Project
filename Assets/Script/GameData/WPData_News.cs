using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPData_News : WPData
{
    public int ID { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public WPData_News(Dictionary<string, object> args)
    {
        ID = System.Convert.ToInt32(args["eID"]);
        Name = args["eName"].ToString();
        Description = args["eDescription"].ToString();
    }

    public new static bool CheckDataIntegrity(Dictionary<string, object> args)
    {
        if (args.Count != 3) return false;
        int checkCount = 0;
        foreach(KeyValuePair<string, object> keyValuePair in args)
        {
            WPEnum.Header_News header = (WPEnum.Header_News)System.Enum.Parse(typeof(WPEnum.Header_News), keyValuePair.Key);
            switch (header)
            {
                case WPEnum.Header_News.ERROR:
                    {
                        WPGameCommon._WPDebug("WPData_News // Wrong Header! :" + keyValuePair.Key);
                        return false;
                    }
                case WPEnum.Header_News.eID:
                    {
                        int checkInt;
                        string datum = keyValuePair.Value.ToString();
                        if (string.IsNullOrEmpty(datum))
                        {
                            WPGameCommon._WPDebug("WPData_News // Empty Value! :" + keyValuePair.Key);
                            return false;
                        }
                        if (!int.TryParse(datum, out checkInt))
                        {
                            WPGameCommon._WPDebug("WPData_News // Wrong Value! :" + keyValuePair.Key + " : " + datum);
                            return false;
                        }
                        checkCount++;
                        break;
                    }
                case WPEnum.Header_News.eDescription:
                case WPEnum.Header_News.eName:
                    {
                        string datum = keyValuePair.Value.ToString();
                        if (string.IsNullOrEmpty(datum))
                        {
                            WPGameCommon._WPDebug("WPData_News // Empty Value! :" + keyValuePair.Key);
                            return false;
                        }
                        checkCount++;
                        break;
                    }
            }
        }

        if (checkCount < 3) WPGameCommon._WPDebug("WPData_News // Invalid Data! :" + checkCount);

        return checkCount == 3;
    }
}
