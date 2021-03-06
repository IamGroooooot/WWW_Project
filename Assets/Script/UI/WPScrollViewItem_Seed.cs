﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WPScrollViewItem_Seed : WPScrollViewItem
{

    private static List<WPScrollViewItem_Seed> ITEMS = new List<WPScrollViewItem_Seed>();

    public static WPScrollViewItem_Seed ITEM_FOCUS
    {
        set
        {
            foreach(WPScrollViewItem_Seed item in ITEMS)
            {
                item.SetFocus(item.name == value.name);
            }
        }
    }

    /// <summary>
    /// 이 객체를 활용하여 새로운 객체를 만드려고 시도할 때 반드시 호출해야 합니다.
    /// </summary>
    public static void Initalize()
    {
        ITEMS = new List<WPScrollViewItem_Seed>();
    }

    protected override void Init()
    {
        base.Init();
        transform.Find("ImageText_Count").GetComponent<WPImageText>().SetActive(false);
        AddEvent(delegate { OnClick_Item(); });
        ITEMS.Add(this);
    }

    private void OnClick_Item()
    {
        ITEM_FOCUS = this;
    }
    
}
