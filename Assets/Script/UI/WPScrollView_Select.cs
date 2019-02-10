using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPScrollView_Select : WPScrollView {

    private static string DATA_PATH = "Image/UI/Farm/";

    private GameObject ui_Item;

    List<Sprite> seedSpriteData = new List<Sprite>();

    protected override void Init()
    {
        ui_Item = Resources.Load<GameObject>("Prefab/UI/UI_Item");
        seedSpriteData = LoadSeedData();
        base.Init();
    }

    private List<Sprite> LoadSeedData()
    {
        List<Sprite> seedSpriteData = new List<Sprite>();
        List<Dictionary<string, object>> seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.eSeed);
        for (int index = 0; index < seedData.Count; ++index)
        {
            string seedDataName = seedData[index]["eDataName"].ToString();
            string seedDataPath = DATA_PATH + seedDataName.Substring(1);
            WPGameCommon._WPDebug(seedDataPath);
            Sprite seedSprite = Resources.Load<Sprite>(seedDataPath);
            if (seedSprite != null)
            {
                seedSpriteData.Insert(index, seedSprite);
            }
        }
        return seedSpriteData;
    }

    public void CreateSeedList()
    {
        List<Dictionary<string, object>> seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.eSeed);
        for (int index = 0; index < seedData.Count; ++index)
        {
            WPScrollViewItem_Seed newItem = Instantiate(ui_Item).AddComponent<WPScrollViewItem_Seed>();
            newItem.SetName(index.ToString());
            newItem.SetText(seedData[index]["eName"].ToString());
            newItem.SetSprite(seedSpriteData[index]);
            AddItem(newItem);
        }
        SortItem();
    }

}
