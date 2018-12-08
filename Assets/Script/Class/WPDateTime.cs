using System.Collections;
using System.Collections.Generic;

//게임상의 시간에 대한 Class
//초기화 시점 
public class WPDateTime {

    public int Year { get; private set; }

    private int month;
    public int Month
    {
        get
        {
            return month;
        }
        set
        {
            month = value;
            if(month > 12)
            {
                Year++;
                month = 1;
            }
        }
    }

    private int day;
    public int Day
    {
        get
        {
            return day;
        }
        set
        {
            day = value;
            switch (month)
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
                        day -= 31;
                        Month++;
                    }
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    if (value > 30)
                    {
                        day -= 30;
                        Month++;
                    }
                    break;
                case 2:
                    if (value > 28)
                    {
                        day -= 28;
                        Month++;
                    }
                    break;
            }
        }
    }

    //생성자
    public WPDateTime(int _year, int _month, int _day)
    {
        Year = _year;
        Month = _month;
        Day = _day;
    }

    public override string ToString()
    {
        return "DateTime(" + Year + ":" + Month + ":" + Day + ")";
    }

    public WPEnum.Season GetSeason()
    {
        return GetSeason(Month);
    }

    //Convert String that is time to int32
    public static WPDateTime ParseData(string data)
    {
        //split String
        string[] data_1 = data.Split('(');
        //simple integrity check
        if (data_1[0] != "DateTime") return null;

        string[] dateString = data_1[1].Replace(")", "").Split(':');

        //convert string to int32
        int _year = System.Convert.ToInt32(dateString[0]);
        int _month = System.Convert.ToInt32(dateString[1]);
        int _day = System.Convert.ToInt32(dateString[2]);

        return new WPDateTime(_year, _month, _day);
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
}
