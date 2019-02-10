using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPScrollView_Select : WPScrollView {

    private static string DATA_PATH = "Image/UI/Farm/";

    private GameObject ui_Item;

    List<Sprite> seedSpriteData = new List<Sprite>();
    List<Sprite> workerSpriteData = new List<Sprite>();
    List<Sprite> fertilizerSpriteData = new List<Sprite>();

    private int seedIndex = -1;             // 선택한 식물의 Index
    private int workerIndex = -1;           // 선택한 일꾼의 Index
    private int fertilizerIndex = -1;       // 선택한 비료의 Index

    private int selectionState = 0;         // 0 = default, 1 = seed, 2 = worker, 3 = fertilizer

    protected override void Init()
    {
        base.Init();
        ui_Item = Resources.Load<GameObject>("Prefab/UI/UI_Item");
        selectionState = 0;
        seedSpriteData = LoadSeedData();
        workerSpriteData = LoadWorkerData();
        fertilizerSpriteData = LoadFertilizerData();
    }

    public void OnEnabled()
    {
        seedIndex = -1;
        workerIndex = -1;
        fertilizerIndex = -1;
        selectionState = 0;
    }

    public void OnDisabled()
    {
        ClearList();
    }

    private List<Sprite> LoadSeedData()
    {
        List<Sprite> seedSpriteData = new List<Sprite>();
        List<Dictionary<string, object>> seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.eSeed);
        for (int index = 0; index < seedData.Count; ++index)
        {
            string seedDataName = seedData[index]["eDataName"].ToString();
            string seedDataPath = DATA_PATH + seedDataName.Substring(1);
            //WPGameCommon._WPDebug(seedDataPath);
            Sprite seedSprite = Resources.Load<Sprite>(seedDataPath);
            if (seedSprite != null)
            {
                seedSpriteData.Insert(index, seedSprite);
            }
        }
        return seedSpriteData;
    }

    private List<Sprite> LoadWorkerData()
    {
        return null;
    }

    private List<Sprite> LoadFertilizerData()
    {
        return null;
    }

    public void OnClick_Seed(int index)
    {
        WPGameCommon._WPDebug(WPGameDataManager.instance.GetData(WPEnum.GameData.eSeed)[index]["eName"] + "을(를) 선택하였습니다.");
        WPUIManager_Field.instance.SetSprite_Seed(seedSpriteData[index]);
        seedIndex = index;
    }

    public void OnClick_Worker(int index)
    {

    }

    public void OnClick_Fertilizer(int index)
    {

    }

    public void CreateSeedList()
    {
        if (selectionState == 1) return;
        selectionState = 1;
        ClearList();
        WPScrollViewItem_Seed.Initalize();
        List<Dictionary<string, object>> seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.eSeed);
        for (int index = 0; index < seedData.Count; ++index)
        {
            WPScrollViewItem_Seed newItem = Instantiate(ui_Item).AddComponent<WPScrollViewItem_Seed>();
            newItem.SetName(index.ToString());
            newItem.AddEvent(delegate { OnClick_Seed(Convert.ToInt32(newItem.name)); });
            newItem.SetText(seedData[index]["eName"].ToString());
            newItem.SetSprite(seedSpriteData[index]);
            AddItem(newItem);
        }
        SortItem();
        if(seedIndex != -1)
        {
            // 예전에 선택한 Item이 있을 경우

        }
    }

    public void CreateWorkerList()
    {

    }

    public void CreateFertilizerList()
    {

    }
    

}
