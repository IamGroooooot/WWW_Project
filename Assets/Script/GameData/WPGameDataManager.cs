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

    // rivate string jsonString;
    // private JsonData seedJsonData;

    // private List<Seed> SeedDB = new List<Seed>();           // construct Seed Database
    /////////////////////////////////////////////////////////////////////////
    // Methods

    
    private void Awake()
    {
        instance = this;
        // jsonString = File.ReadAllText(path,System.Text.Encoding.UTF8);
        // seedJsonData = JsonMapper.ToObject(jsonString);
        // ConstructSeedDatabase(); // Seed DB Construct하는 것은 Awake에서 돼야한가용??
    }

    private void Start()
    {
        
    }

    private void Init()
    {
        
    }

    /*

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
                    seedJsonData[i]["specie"].ToString(), 
                    (int)seedJsonData[i]["growthTime"],
                    (int)seedJsonData[i]["comparePrice"],
                    //(int)seedJsonData[i]["salePrice"],  기준 판매가 * 0.8 ,(반올림)
                    //(int)seedJsonData[i]["scoreIncreaseRatio"],
                    seedJsonData[i]["buffWeather"].ToString(), 
                    seedJsonData[i]["debuffWeather"].ToString(), 
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
    public string specie { get; set; }
    public int growthTime { get; set; }
    public int comparePrice { get; set; }
    public int salePrice { get; set; }
    public int scoreIncreaseRatio { get; set; }
    public string buffWeather { get; set; }
    public string debuffWeather { get; set; }
    public string description { get; set; }
    public int unlockLevel { get; set; }
    public string slug { get; set; }// 여기에 이미지 파일 path저장 
    /// <summary>
    /// ID : 작물 순서대로 번호 부여
    /// specie : 종목
    /// growthTime : 성장기간(개월)
    /// comparePrice : 기준판매가(비교용)
    /// salePrice : 모종 가격 == Round(기준판매가 * 0.8)
    /// scoreIncreaseRatio : 1일 점수증가량 == (기준 판매가 / 일성장기간)
    /// buffWeather : 버프 날씨
    /// debuffWeather : 디버프 날씨
    /// description : 도감 설명
    /// unlockLevel : 해금 렙
    /// slug : 작물 이미지 경로
    /// </summary>

    public Seed(int _id, string _specie, int _growthTime,int _comparePrice, string _buffWeather,string _debuffWeather,string _description,int _unlockLevel, string _slug)
    {
        this.ID = _id;
        this.specie = _specie;
        this.growthTime = -growthTime;
        this.comparePrice = _comparePrice;
        this.salePrice = Mathf.RoundToInt(_comparePrice*0.8f); //반올림 이거 맞낭?
        this.scoreIncreaseRatio = Mathf.RoundToInt(comparePrice/(30f*growthTime)); 
        this.buffWeather = _buffWeather;
        this.debuffWeather = _debuffWeather;
        this.description = _description;
        this.unlockLevel = _unlockLevel;
        this.slug = _slug;
        
    }

    //Empty Seed
    public Seed()
    {
        this.ID = -1;
    }*/

}