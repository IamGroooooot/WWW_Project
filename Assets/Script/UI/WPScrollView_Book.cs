using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPScrollView_Book : WPScrollView {

    private static string DATA_PATH = "Image/";

    private GameObject ui_Book;

    Sprite lockSprite;
    List<Sprite> seedSpriteData = new List<Sprite>();
    List<Sprite> fertilizerSpriteData = new List<Sprite>();

    protected override void Init()
    {
        base.Init();
        ui_Book = WPResourceManager.instance.GetResource<GameObject>("Prefab/UI/UI_Book");
        lockSprite = WPResourceManager.instance.GetResource<Sprite>(DATA_PATH + "lock");
        seedSpriteData = LoadSeedData();
        fertilizerSpriteData = LoadFertilizerData();
        
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

    private List<Sprite> LoadFertilizerData()
    {
        List<Sprite> spriteData = new List<Sprite>();
        List<WPData_Fertilizer> fertilizerData = WPGameDataManager.instance.GetData<WPData_Fertilizer>(WPEnum.GameData.eFertilizer);
        for (int index = 0; index < fertilizerData.Count; ++index)
        {
            string dataName = fertilizerData[index].DataName;
            string dataPath = DATA_PATH + "Fertilizer/" + dataName;

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

    private void OnClick_Seed(int index)
    {

    }

    private void OnClick_Fertilizer(int index)
    {

    }

    public void CreateSeedList()
    {
        ClearList();
        List<WPData_Seed> seedData = WPGameDataManager.instance.GetData<WPData_Seed>(WPEnum.GameData.eSeed);
        for (int index = 0; index < seedData.Count; ++index)
        {
            WPScrollViewItem_Book newItem = Instantiate(ui_Book).AddComponent<WPScrollViewItem_Book>();
            newItem.SetName(index.ToString());
            if(WPUserDataManager.instance.Level >= seedData[index].UnlockLevel)
            {
                newItem.SetSprite(seedSpriteData[index]);
                newItem.AddEvent(delegate { OnClick_Seed(System.Convert.ToInt32(newItem.name)); });
            }
            else
            {
                newItem.SetSprite(lockSprite);
            }
            AddItem(newItem);
        }
        SortItemToItem();
    }

    public void CreateFertilizerList()
    {
        ClearList();
        List<WPData_Fertilizer> fertilizerData = WPGameDataManager.instance.GetData<WPData_Fertilizer>(WPEnum.GameData.eFertilizer);
        for(int index = 0; index < fertilizerData.Count; ++index)
        {
            WPScrollViewItem_Book newItem = Instantiate(ui_Book).AddComponent<WPScrollViewItem_Book>();
            newItem.SetName(index.ToString());
            newItem.SetSprite(fertilizerSpriteData[index]);
            newItem.AddEvent(delegate { OnClick_Fertilizer(System.Convert.ToInt32(newItem.name)); });
            AddItem(newItem);
        }
        SortItemToItem();
    }
}
