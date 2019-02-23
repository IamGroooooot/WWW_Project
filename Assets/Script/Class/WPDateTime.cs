using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPDateTime {

    private static WPDateTime now = null;
    public static WPDateTime Now
    {
        get
        {
            if (now == null)
            {
                WPDateTime userDateTime = WPUserDataManager.instance.DateTime;
                if (userDateTime == null)
                {
                    now = StandardDateTime;
                }
                else now = userDateTime;
            }
            return now;
        }
    }

    public const int STANDARD_YEAR = 2019;                // TimeData를 연도-월-일-시간으로 계산하는 데 필요한 기준년도입니다. TimeData가 0인 WPTimeData 객체는 2019년 1월 1일 0시를 가리킵니다.

    public static bool CheckLeapYear(int year)
    {
        return (((year % 4 == 0 && year % 100 != 0) || year % 400 == 0));
    }

    public static WPDateTime StandardDateTime
    {
        get
        {
            return new WPDateTime((int)WPEnum.InitialDate.eInitYear, (int)WPEnum.InitialDate.eInitMonth, (int)WPEnum.InitialDate.eInitDay, (int)WPEnum.InitialDate.eInitHour);
        }
    }

    public static WPEnum.Season GetSeason(int month)
    {
        switch (month)
        {
            case 3:
            case 4:
            case 5:
                return WPEnum.Season.eSpring;
            case 6:
            case 7:
            case 8:
                return WPEnum.Season.eSummer;
            case 9:
            case 10:
            case 11:
                return WPEnum.Season.eAutumn;
            case 12:
            case 1:
            case 2:
                return WPEnum.Season.eWinter;
            default:
                return 0;
        }
    }

    public static WPDateTime ParseData(string data)
    {
        // split String
        string[] data_1 = data.Split('(');
        // simple integrity check
        if (data_1[0] != "WPDateTime") return new WPDateTime();

        string[] dateString = data_1[1].Replace(")", "").Split(':');

        // convert string to int32
        int year = System.Convert.ToInt32(dateString[0]);
        int month = System.Convert.ToInt32(dateString[1]);
        int day = System.Convert.ToInt32(dateString[2]);
        int hour = System.Convert.ToInt32(dateString[3]);

        return new WPDateTime(year, month, day, hour);
    }

    // 두 WPTimeData 객체가 가리키는 시간의 차이를 비교합니다. t1에 현재 시간을 가리키는 객체를 넣고, t2에 어떤 시점을 가리키는 객체를 넣으면 어떤 시점으로부터 얼마나 시간이 지났는지 그 값을 얻을 수 있습니다.
    public static int CompareTime(WPDateTime t1, WPDateTime t2)
    {
        if(t1==null || t2 == null)
        {
            WPGameCommon._WPDebug("TimeData is missing");
            return -1;
        }
        return t1.TimeData - t2.TimeData;
    }

    public delegate void EventHandler_WPDateTime(WPDateTime dateTime);
    public event EventHandler_WPDateTime OnTimeChanged;

    public delegate void EventHandler_WPDateTime_2(int changedValue);
    public event EventHandler_WPDateTime_2 OnValueChanged;

    private int timeData = -1;
    private int TimeData
    {
        get
        {
            return timeData;
        }
        set
        {
            //WPGameCommon._WPDebug("New Value : " + value);
            UpdateInstance(value);
            if (OnValueChanged != null) OnValueChanged(value - timeData);
            timeData = value;
            if (OnTimeChanged != null) OnTimeChanged(this);
        }
    }

    public int Year { get; private set; }
    public int Month { get; private set; }
    public int Day { get; private set; }
    public int Hour { get; private set; }

    public bool isLeapYear
    {
        get
        {
            return CheckLeapYear(Year);
        }
    }

    public WPDateTime()
    {
        TimeData = 0;
    }

    public WPDateTime(int year, int month, int day, int hour)
    {
        int tempTimeData = 0;
        int tempYear = year - STANDARD_YEAR;
        for(int index = 0; index < tempYear; ++index)
        {
            tempTimeData += CheckLeapYear(STANDARD_YEAR + index) ? 366 * 24 : 365 * 24;
        }
        for(int index = 1; index < month; ++index)
        {
            switch (index)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    {
                        tempTimeData += 31 * 24;
                        break;
                    }
                case 4:
                case 6:
                case 9:
                case 11:
                    {
                        tempTimeData += 30 * 24;
                        break;
                    }
                case 2:
                    {
                        tempTimeData += CheckLeapYear(year) ? 29 * 24 : 28 * 24;
                        break;
                    }
            }
        }
        tempTimeData += (day - 1) * 24;
        tempTimeData += hour;
        TimeData = tempTimeData;
    }

    // ToString()은 해당 객체를 연도-월-일 시간 AMPM으로 나타냅니다. UI에서 사용합니다.
    public new string ToString()
    {
        string dateString = string.Format("{0} / {1} / {2} ", Year, Month, Day);
        string timeString = string.Empty;
        if (0 == Hour)
        {
            timeString = "12 : 00 AM";
        }
        else if (1 <= Hour && Hour < 12)
        {
            timeString = string.Format("{0} : 00 AM", Hour);
        }
        else if (12 == Hour)
        {
            timeString = "12 : 00 PM";
        }
        else if (13 <= Hour && Hour < 24)
        {
            timeString = string.Format("{0} : 00 PM", Hour - 12);
        }
        return string.Concat(dateString, timeString);
    }

    // ToData()는 해당 객체를 PaseData()를 통하여 저장/불러오기를 가능하게 나타냅니다.
    public string ToData()
    {
        return string.Format("WPDateTime({0}:{1}:{2}:{3})", Year, Month, Day, Hour);
    }

    public WPDateTime AddTimeData(int timeData)
    {
        WPDateTime newDateTime = new WPDateTime();
        newDateTime.TimeData = TimeData + timeData;
        return newDateTime;
    }

    public WPDateTime AddTimeData(WPDateTime timeData)
    {
        WPDateTime newDateTime = new WPDateTime();
        newDateTime.TimeData = TimeData + timeData.TimeData;
        return newDateTime;
    }

    // 윤년의 2월 29일에서 1년을 더하면 3월 1일이 되어야 할까요, 2월 28일이 되어야 할까요?
    public void AddYear(int content)
    {
        int tempTimeData = 0;
        if (content >= 0)
        {
            for (int index = 0; index < content; ++index)
            {
                tempTimeData += CheckLeapYear(Year + index) ? 366 * 24 : 365 * 24;
            }
        }
        else
        {
            for(int index = 0; index < -content; ++index)
            {
                tempTimeData -= CheckLeapYear(Year - index) ? 366 * 24 : 365 * 24;
            }
        }
        TimeData += tempTimeData;
    }

    public void AddMonth(int content)
    {
        int tempTimeData = 0;
        bool isNegative = content < 0;

        AddYear(content / 12);

        content = Mathf.Abs(content) % 12;

        for (int index = 0; index < content; ++index)
        {
            if (isNegative)
            {
                int newMonth = Month - index;
                int isOver = 0;
                if(newMonth < 1)
                {
                    newMonth += 12;
                    isOver = 1;
                }
                switch (Month)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        {
                            tempTimeData -= 31 * 24;
                            break;
                        }
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        {
                            tempTimeData -= 30 * 24;
                            break;
                        }
                    case 2:
                        {
                            tempTimeData -= CheckLeapYear(Year - isOver) ? 29 * 24 : 28 * 24;
                            break;
                        }
                }
            }
            else
            {
                int newMonth = Month + index;
                int isOver = 0;
                if(newMonth > 12)
                {
                    newMonth -= 12;
                    isOver = 1;
                }
                switch (Month)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        {
                            tempTimeData += 31 * 24;
                            break;
                        }
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        {
                            tempTimeData += 30 * 24;
                            break;
                        }
                    case 2:
                        {
                            tempTimeData += CheckLeapYear(Year + isOver) ? 29 * 24 : 28 * 24;
                            break;
                        }
                }
            }
        }

        TimeData += tempTimeData;

    }

    public void AddDay(int content)
    {
        TimeData += content * 24;
    }

    public void AddHour(int content)
    {
        TimeData += content;
    }

    private void UpdateInstance(int newData)
    {
        int tempTimeData = newData;

        for (Year = STANDARD_YEAR; ;)                           // 년도 계산 루프
        {
            int yearToHour = isLeapYear ? 366 * 24 : 365 * 24;
            if (tempTimeData >= yearToHour)
            {
                Year++;
                tempTimeData -= yearToHour;
            }
            else break;
        }

        for (Month = 1; Month <= 12; )                      // 월 계산 루프
        {
            int monthToHour = 0;
            switch (Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    {
                        monthToHour = 31 * 24;
                        break;
                    }
                case 4:
                case 6:
                case 9:
                case 11:
                    {
                        monthToHour = 30 * 24;
                        break;
                    }
                case 2:
                    {
                        monthToHour = isLeapYear ? 29 * 24 : 28 * 24;
                        break;
                    }
            }
            if (tempTimeData >= monthToHour)
            {
                Month++;
                tempTimeData -= monthToHour;
            }
            else break;
        }

        for (Day = 1; ;)                                        // 일 계산 루프
        {
            int dayToHour = 24;
            if (tempTimeData >= dayToHour)
            {
                Day++;
                tempTimeData -= dayToHour;
            }
            else break;
        }

        Hour = tempTimeData;

    }

}
