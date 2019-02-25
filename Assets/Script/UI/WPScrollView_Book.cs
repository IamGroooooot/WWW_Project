using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPScrollView_Book : WPScrollView {

    private static string DATA_PATH = "Image/";

    private GameObject ui_Book;

    List<Sprite> seedSpriteData = new List<Sprite>();

    protected override void Init()
    {
        base.Init();
        ui_Book = WPResourceManager.instance.GetResource<GameObject>("Prefab/UI/UI_Book");
        seedSpriteData = LoadSeedData();
    }

    private List<Sprite> LoadSeedData()
    {
        List<Sprite> spriteData = new List<Sprite>();
        List<WPData_Seed> seedData = WPGameDataManager.instance.GetData<WPData_Seed>(WPEnum.GameData.eSeed);
        for (int index = 0; index < seedData.Count; ++index)
        {
            string dataName = seedData[index].DataName;
            string dataPath = DATA_PATH + "Seed/" + dataName;

            Sprite newSprite = WPResourceManager.instance.GetResource<Sprite>(dataPath);
            if (newSprite != null)
            {
                spriteData.Insert(index, newSprite);
            }
        }
        return spriteData;
    }

    protected override void OnEnabled()
    {
        CreateSeedList();
    }

    public void CreateSeedList()
    {
        ClearList();
        List<WPData_Seed> seedData = WPGameDataManager.instance.GetData<WPData_Seed>(WPEnum.GameData.eSeed);
        for (int index = 0; index < seedData.Count; ++index)
        {
            WPScrollViewItem_Book newItem = Instantiate(ui_Book).AddComponent<WPScrollViewItem_Book>();
            newItem.SetName(index.ToString());
            newItem.SetSprite(seedSpriteData[index]);
            AddItem(newItem);
        }
        SortItemToItem();
    }
}
