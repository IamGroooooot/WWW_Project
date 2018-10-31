using System.Collections;
using System.Collections.Generic;

//게임상의 시간에 대한 Class

public class WPDateTime {

    public int year;
    public int month;
    public int day;
    // enum으로 날씨 만들자.

    //생성자
    public WPDateTime(int _year, int _month, int _day)
    {
        year = _year;
        month = _month;
        day = _day;
    }

    public string ToString()
    {
        return "DateTime(" + year + ":" + month + ":" + day + ")";
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
}
