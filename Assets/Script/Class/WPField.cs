using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Field
{
    string CurrentCrop;
    string StartedTime;
    string Worker;
    string Fertilizer;
    int Gold;

    public Field(string currentCrop, string startedTime, string worker, string fertilizer, int gold)
    {
        this.CurrentCrop = currentCrop;
        this.StartedTime = startedTime;
        this.Worker = worker;
        this.Fertilizer = fertilizer;
        this.Gold = gold;
    }
};

public class WPField
{
    private Field[] testList = new Field[]
    {
        new Field ("GreenOnion", "DateTime(2019:3:4:3)","지호1","버드나무표 비료",1 ),
        new Field ("", "","","",0 ),
        new Field ("", "","","",0 ),
        new Field ("", "","","",0 ),
        new Field ("", "","","",0 ),
        new Field ("", "","","",0 ),

    };

}
