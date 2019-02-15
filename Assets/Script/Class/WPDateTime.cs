using System;
using System.Collections;
using System.Collections.Generic;

// 게임상의 시간에 대한 Class
// 초기화 시점 
public class WPDateTime {

    private int year;
    private int month;
    private int day;
    private int hour;

    public static int Year
    {
        get
        {
            if (wpDateTime == null) return 0;
            return wpDateTime.year;
        }
    }
    public static int Month
    {
        get
        {
            if (wpDateTime == null) return 0;
            return wpDateTime.month;
        }
        set
        {
            if (wpDateTime == null) return;
            wpDateTime.month = value;
            if(wpDateTime.month > 12)
            {
                wpDateTime.year++;
                wpDateTime.month = 1;
            }
        }
    }
    public static int Day
    {
        get
        {
            if (wpDateTime == null) return 0;
            return wpDateTime.day;
        }
        set
        {
            if (wpDateTime == null) return;
            wpDateTime.day = value;
            switch (wpDateTime.month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    if (value > 31)
                    {
                        wpDateTime.day -= 31;
                        Month++;
                    }
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    if (value > 30)
                    {
                        wpDateTime.day -= 30;
                        Month++;
                    }
                    break;
                case 2:
                    if (value > 28)
                    {
                        wpDateTime.day -= 28;
                        Month++;
                    }
                    break;
            }
        }
    }
    public static int Hour
    {
        get
        {
            if (wpDateTime == null) return 0;
            return wpDateTime.hour;
        }
        set
        {
            if (wpDateTime == null) return;
            wpDateTime.hour = value;
            if(wpDateTime.hour >= 24)
            {
                wpDateTime.hour = 0;
                Day++;
            }
        }
    }

    private static WPDateTime wpDateTime = null;

    // 생성자
    private WPDateTime(int _year, int _month, int _day, int _hour)
    {
        year = _year;
        month = _month;
        day = _day;
        hour = _hour;
    }

    public new static string ToString()
    {
        string dateString = string.Format("{0} / {1} / {2} ", Year, Month, Day);
        string timeString = string.Empty;
        if(0 == Hour)
        {
            timeString = "12 : 00 AM";
        }
        else if(1 <= Hour && Hour < 12)
        {
            timeString = string.Format("{0} : 00 AM", Hour);
        }
        else if(12 == Hour)
        {
            timeString = "12 : 00 PM";
        }
        else if(13 <= Hour && Hour < 24)
        {
            timeString = string.Format("{0} : 00 PM", Hour);
        }

        return string.Concat(dateString, timeString);
    }

    public static WPEnum.Season GetSeason()
    {
        return GetSeason(Month);
    }

    public static WPEnum.Season GetSeason(int _month)
    {
        switch (_month)
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
        if (data_1[0] != "DateTime") return null;

        string[] dateString = data_1[1].Replace(")", "").Split(':');

        // convert string to int32
        int _year = System.Convert.ToInt32(dateString[0]);
        int _month = System.Convert.ToInt32(dateString[1]);
        int _day = System.Convert.ToInt32(dateString[2]);
        int _hour = System.Convert.ToInt32(dateString[3]);

        return new WPDateTime(_year, _month, _day, _hour);
    }

	//ParseDate을 DateTime형식 Return해주는 것 만들기
	//농장 Field에서 심은 작물의 심은 일자를 구하기 위함.
	public static DateTime ParseData2DateTime(string data)
	{
		// split String
		string[] data_1 = data.Split('(');
		// simple integrity check
		if (data_1[0] != "DateTime") return new DateTime(0,0,0,0,0,0,0,DateTimeKind.Local);

		string[] dateString = data_1[1].Replace(")", "").Split(':');

		// convert string to int32
		int _year = System.Convert.ToInt32(dateString[0]);
		int _month = System.Convert.ToInt32(dateString[1]);
		int _day = System.Convert.ToInt32(dateString[2]);
		int _hour = System.Convert.ToInt32(dateString[3]);

		return new DateTime(_year, _month, _day, _hour,0,0,DateTimeKind.Local);
	}


	private static string ToData()
    {
        return string.Format("DateTime({0}:{1}:{2}:{3})", Year, Month, Day, Hour);
    }

    public static void Init()
    {
        string data = WPGameVariableManager.instance.LoadStringVariable(WPEnum.VariableType.eUserDate);
        if (string.IsNullOrEmpty(data))
        {
            wpDateTime = new WPDateTime(
                (int)WPEnum.InitialDate.eInitYear,
                (int)WPEnum.InitialDate.eInitMonth,
                (int)WPEnum.InitialDate.eInitDay,
                (int)WPEnum.InitialDate.eInitHour);
            return;
        }
        wpDateTime = ParseData(data);
    }

    public static void Save()
    {
        if (wpDateTime == null)
        {
            WPGameCommon._WPDebug("wpDateTime == NULL!");
            return;
        }
        WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eUserDate, ToString());
    }

	public static void Save_FieldTime(WPEnum.VariableType myField,int FarmNum)
	{
		//FarmNum은 농장 인덱스, 일단 0으로 설정
		FarmNum = 0;

		if (wpDateTime == null)
		{
			WPGameCommon._WPDebug("wpDateTime == NULL!, FieldTime");
			return;
		}
		
		WPGameVariableManager.instance.SaveVariable((myField.ToString()+FarmNum.ToString()), ToString());
	}


}
