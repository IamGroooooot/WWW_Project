using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPScrollView_Select : WPScrollView {

    private static string DATA_PATH = "Image/";

    private GameObject ui_Item;

    List<Sprite> seedSpriteData = new List<Sprite>();
    List<Sprite> workerSpriteData = new List<Sprite>();
    List<Sprite> fertilizerSpriteData = new List<Sprite>();

    public int seedIndex { get; private set; }             // 선택한 식물의 Index
    public int workerIndex { get; private set; }           // 선택한 일꾼의 Index
    public int fertilizerIndex { get; private set; }       // 선택한 비료의 Index

    private int selectionState = 0;         // 0 = default, 1 = seed, 2 = worker, 3 = fertilizer

    protected override void Init()
    {
        base.Init();
        ui_Item = WPResourceManager.instance.GetResource<GameObject>("Prefab/UI/UI_Item");
        selectionState = 0;
        seedSpriteData = LoadSeedData();
        workerSpriteData = LoadWorkerData();
        fertilizerSpriteData = LoadFertilizerData();
    }

    protected override void OnEnabled()
    {
        seedIndex = -1;
        workerIndex = -1;
        fertilizerIndex = -1;
        selectionState = 0;
        CreateSeedList();
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

    private List<Sprite> LoadWorkerData()
    {
        return null;
    }

    private List<Sprite> LoadFertilizerData()
    {
        List<Sprite> spriteData = new List<Sprite>();
        List<WPData_Fertilizer> fertilizerData = WPGameDataManager.instance.GetData<WPData_Fertilizer>(WPEnum.GameData.eFertilizer);
        for(int index = 0; index < fertilizerData.Count; ++index)
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

    //식물 눌렀을 때
    public void OnClick_Seed(int index)
    {
        WPData_Seed seedData = WPGameDataManager.instance.GetData<WPData_Seed>(WPEnum.GameData.eSeed)[index];
        
        if (seedData != null)
        {
            WPGameCommon._WPDebug(seedData.Name + "을(를) 선택하였습니다.");
            int growthTime = seedData.GrowthTime;
            if (fertilizerIndex != -1)
            {
                WPData_Fertilizer fertilizerData = WPGameDataManager.instance.GetData<WPData_Fertilizer>(WPEnum.GameData.eFertilizer)[fertilizerIndex];
                if (fertilizerData != null)
                {
                    switch (fertilizerData.ItemType)
                    {
                        case WPEnum.FertilizerType.eGrowth:
                            {
                                growthTime -= fertilizerData.Value;
                                break;
                            }
                    }
                }
            }
            WPUIManager_Field.instance.SetText_Time(
                WPDateTime.Now.AddTimeData(
                    growthTime
                ).ToString());
            WPUIManager_Field.instance.SetText_Money((
                    seedData.ComparePrice
                ).ToString());
            WPUIManager_Field.instance.SetSprite_Seed(seedSpriteData[index]);
        }
        seedIndex = index;
    }

    public void OnClick_Worker(int index)
    {

    }

    public void OnClick_Fertilizer(int index)
    {
        WPData_Fertilizer fertilizerData = WPGameDataManager.instance.GetData<WPData_Fertilizer>(WPEnum.GameData.eFertilizer)[index];
        if(fertilizerData != null)
        {
            WPGameCommon._WPDebug(fertilizerData.Name + "을(를) 선택하였습니다.");
            int growthTime = 0;
            if(seedIndex != -1)
            {
                WPData_Seed seedData = WPGameDataManager.instance.GetData<WPData_Seed>(WPEnum.GameData.eSeed)[seedIndex];
                if(seedData != null)
                {
                    growthTime = seedData.GrowthTime;
                }

                switch (fertilizerData.ItemType)
                {
                    case WPEnum.FertilizerType.eGrowth:
                        {
                            growthTime -= fertilizerData.Value;
                            break;
                        }
                }

                WPUIManager_Field.instance.SetText_Time(
                    WPDateTime.Now.AddTimeData(
                        growthTime
                    ).ToString());
            }
        }
        fertilizerIndex = index;
    }

    public void CreateSeedList()
    {
        if (selectionState == 1) return;
        selectionState = 1;
        ClearList();
        WPScrollViewItem_Seed.Initalize();
        List<WPData_Seed> seedData = WPGameDataManager.instance.GetData<WPData_Seed>(WPEnum.GameData.eSeed);
        for (int index = 0; index < seedData.Count; ++index)
        {
            if (WPUserDataManager.instance.Level >= seedData[index].UnlockLevel)
            {
                WPScrollViewItem_Seed newItem = Instantiate(ui_Item).AddComponent<WPScrollViewItem_Seed>();
                newItem.SetName(index.ToString());
                newItem.AddEvent(delegate { OnClick_Seed(Convert.ToInt32(newItem.name)); });
                newItem.SetText(seedData[index].Name);
                newItem.SetFocus(index == seedIndex);
                newItem.SetSprite(seedSpriteData[index]);
                AddItem(newItem);
            }
        }
        if (seedIndex <= -1) SortItemToHorizontal();
        else SortItemToHorizontal(seedIndex);
    }

    public void CreateWorkerList()
    {
        if (selectionState == 2) return;
        selectionState = 2;
        ClearList();

    }

    public void CreateFertilizerList()
    {
        if (selectionState == 3) return;
        selectionState = 3;
        ClearList();
        WPScrollViewItem_Fertilizer.Initalize();
        List<WPData_Fertilizer> fertilizerData = WPGameDataManager.instance.GetData<WPData_Fertilizer>(WPEnum.GameData.eFertilizer);
        for(int index = 0; index < fertilizerData.Count; ++index)
        {
            WPGameCommon._WPDebug(index+"번 비료 수 : " +WPUserDataManager.instance.GetFertilizer(index));
            if(WPUserDataManager.instance.GetFertilizer(index) > 0)
            {
                WPScrollViewItem_Fertilizer newItem = Instantiate(ui_Item).AddComponent<WPScrollViewItem_Fertilizer>();
                newItem.SetName(index.ToString());
                newItem.AddEvent(delegate { OnClick_Fertilizer(Convert.ToInt32(newItem.name)); });
                newItem.SetText(fertilizerData[index].Name);
                newItem.SetFocus(index == fertilizerIndex);
                newItem.SetSprite(fertilizerSpriteData[index]);
                AddItem(newItem);
            }
        }
        if (fertilizerIndex <= -1) SortItemToHorizontal();
        else SortItemToHorizontal(seedIndex);
    }
    

}
