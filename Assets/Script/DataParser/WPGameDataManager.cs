using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
// 게임데이터 : (무결성 검사), json, 읽기만 할 수 있음. static
// 파, 상추, 감자, 배추, 30, 50, 150, 500
// 게임데이터 매니저 파서 구현(싱긅ㄴ오) 
// awake 단계에서 
//(SW)데이터 불러오기 끝나면 값하나true로 던져주기

public class WPGameDataManager : MonoBehaviour {

    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPGameDataManager instance = null;        // for singleton

    private string path;                                    // for JSON 
    private string jsonString;
    private JsonData seedJsonData;

    private List<Seed> SeedDB = new List<Seed>();           // construct Seed Database
    /////////////////////////////////////////////////////////////////////////
    // Methods

    private void Awake()
    {
        instance = this;
        path = Application.streamingAssetsPath + "/Seed.json";
        jsonString = File.ReadAllText(path,System.Text.Encoding.UTF8);
        seedJsonData = JsonMapper.ToObject(jsonString);
        ConstructSeedDatabase(); // Seed DB Construct하는 것은 Awake에서 돼야한가용??
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
       
	}
    
    void ConstructSeedDatabase()
    {
        for (int i = 0; i < seedJsonData.Count; i++)
        {
            SeedDB.Add(
                new Seed(
                    (int)seedJsonData[i]["id"],
                    seedJsonData[i]["specialization"].ToString(),
                    seedJsonData[i]["title"].ToString(), 
                    (int)seedJsonData[i]["time"],
                    (int)seedJsonData[i]["purchasePrice"],
                    (int)seedJsonData[i]["salePrice"],
                    (int)seedJsonData[i]["selfDebuffRatio"],
                    (int)seedJsonData[i]["debuffIncreasmentRatio"][0],
                    (int)seedJsonData[i]["debuffIncreasmentRatio"][1],
                    (int)seedJsonData[i]["debuffIncreasmentRatio"][2],
                    seedJsonData[i]["description"].ToString(), 
                    (int)seedJsonData[i]["unlockLevel"],
                    seedJsonData[i]["slug"].ToString()));
            
        }
    }
    

    //Enum값으로 Seed 찾을 수 있도록 구현
    public Seed FindSeedByID(int id)
    {
        if (id < 0 || id >= SeedDB.Count)
        {
            return null;
        }
       
        for (int i = 0; i < SeedDB.Count; i++)
        {
            if (SeedDB[i].ID == id)
            {
                return SeedDB[i];
            }
        }

        return null;
    }
}

[System.Serializable]
public class Seed
{
    public int ID { get; set; }
    public string Specialization;
    public string Title { get; set; }
    public int Time { get; set; }
    public int PurchasePrice { get; set; }
    public int SalePrice { get; set; }
    public int SelfDebuffRatio { get; set; }
    public int DebuffIncreasmentRatio0 { get; set; }
    public int DebuffIncreasmentRatio1 { get; set; }
    public int DebuffIncreasmentRatio2 { get; set; }
    public string Description { get; set; }
    public int UnlockLevel { get; set; }
    public string Slug { get; set; }// 여기에 이미지 파일 path저장 


    public Seed(int _id, string _specialization, string _title,int _time, int _purchasePrice,int _salePrice, int _selfDebuffRatio, int _debuffIncreasmentRatio0, int _debuffIncreasmentRatio1, int _debuffIncreasmentRatio2, string _description,int _unlockLevel, string _slug)
    {
        this.ID = _id;
        this.Specialization = _specialization;
        this.Title = _title;
        this.Time = _time;
        this.PurchasePrice = _purchasePrice;
        this.SalePrice = _salePrice;
        this.SelfDebuffRatio = _selfDebuffRatio;
        this.DebuffIncreasmentRatio0 = _debuffIncreasmentRatio0;
        this.DebuffIncreasmentRatio1 = _debuffIncreasmentRatio1;
        this.DebuffIncreasmentRatio2 = _debuffIncreasmentRatio2;
        this.Description = _description;
        this.UnlockLevel = _unlockLevel;
        this.Slug = _slug;
    }

    public Seed()
    {
        this.ID = -1;
    }
    
}