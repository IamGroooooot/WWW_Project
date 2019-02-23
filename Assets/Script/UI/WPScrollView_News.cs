using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPScrollView_News : WPScrollView {

    private GameObject ui_News;

    protected override void Init()
    {
        base.Init();
        ui_News = WPResourceManager.instance.GetResource<GameObject>("Prefab/UI/UI_News");
    }

    public void CreateNewsList(WPDateTime currentTime)
    {
        ClearList();
        if(ui_News == null) ui_News = WPResourceManager.instance.GetResource<GameObject>("Prefab/UI/UI_News");
        List<int> newsID = WPUserDataManager.instance.GetNewsDataByDateTime(currentTime);
        List<WPData_News> newsData = WPGameDataManager.instance.GetData<WPData_News>(WPEnum.GameData.eNews);
        for(int i = 0; i < newsID.Count; ++i)
        {
            WPScrollViewItem_News newItem = Instantiate(ui_News).AddComponent<WPScrollViewItem_News>();
            newItem.SetName(newsID[i].ToString());
            newItem.SetText(newsData[newsID[i]].Description);
            AddItem(newItem);
        }
        SortItemToVertical();
    }
}
