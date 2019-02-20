using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WPScrollViewItem_Fertilizer : WPScrollViewItem
{

    private static List<WPScrollViewItem_Fertilizer> ITEMS = new List<WPScrollViewItem_Fertilizer>();

    public static WPScrollViewItem_Fertilizer ITEM_FOCUS
    {
        set
        {
            foreach (WPScrollViewItem_Fertilizer item in ITEMS)
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
        ITEMS = new List<WPScrollViewItem_Fertilizer>();
    }

    protected override void Init()
    {
        base.Init();
        AddEvent(delegate { OnClick_Item(); });
        ITEMS.Add(this);
    }

    private void OnClick_Item()
    {
        ITEM_FOCUS = this;
    }

}