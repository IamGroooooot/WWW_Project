using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPData_Event : WPData
{
    public int ID { get; private set; }
    public int Month { get; private set; }
    public string NewsInfo { get; private set; }

    public WPData_Event(Dictionary<string, object> args)
    {
        ID = System.Convert.ToInt32(args["eID"]);
        Month = System.Convert.ToInt32(args["eMonth"]);
        NewsInfo = args["eNewsInfo"].ToString();
    }

    public new static bool CheckDataIntegrity(Dictionary<string, object> args)
    {
        if (args.Count != 3) return false;
        int checkCount = 0;
        foreach (KeyValuePair<string, object> keyValuePair in args)
        {
            WPEnum.Header_Event header = (WPEnum.Header_Event)System.Enum.Parse(typeof(WPEnum.Header_Event), keyValuePair.Key);
            switch (header)
            {
                case WPEnum.Header_Event.ERROR:
                    {
                        WPGameCommon._WPDebug("WPData_News // Wrong Header! :" + keyValuePair.Key);
                        return false;
                    }
                case WPEnum.Header_Event.eID:
                case WPEnum.Header_Event.eMonth:
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
                case WPEnum.Header_Event.eNewsInfo:
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

    /// <summary>
    /// NewsID를 원하는 개수만큼 가져옵니다. 
    /// </summary>
    public List<int> GetNewsIDByCount(int count)
    {
        List<int> newsIndex = new List<int>();

        List<string> newsItem = new List<string>();

        newsItem.AddRange(NewsInfo.Split(','));

        WPGameCommon._WPDebug("뉴스 데이터 개수 : " + newsItem.Count);

        int maxChance = 0;

        for (int loop = 0; loop < count; ++loop)
        {
            int[] chanceArray = new int[newsItem.Count];
            for (int i = 0; i < newsItem.Count; ++i)
            {
                int itemChance = System.Convert.ToInt32(newsItem[i].Split(':')[1]);
                maxChance += itemChance;
                chanceArray[i] = itemChance;
            }
            int random = Random.Range(0, maxChance);

            for(int i = 0; i < newsItem.Count; ++i)
            {
                if(random < chanceArray[i])
                {
                    if (newsItem[i].Contains("^"))
                    {
                        string[] items = newsItem[i].Split(':')[0].Split('^');
                        newsIndex.Add(System.Convert.ToInt32(items[Random.Range(0, items.Length)]));
                    }
                    else
                    {
                        newsIndex.Add(System.Convert.ToInt32(newsItem[i].Split(':')[0]));
                    }
                    maxChance = 0;
                    newsItem.RemoveAt(i);
                    break;
                }
                else random -= chanceArray[i];
            }
        }
        return newsIndex;
    }
}
