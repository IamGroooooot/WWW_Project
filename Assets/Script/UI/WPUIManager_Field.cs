using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WPUIManager_Field : WPUIManager {
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUIManager_Field instance = null;     // singleton

    Sprite Empty, GreenOnion, Lettuce, Potato, SugarCane, Tabacco, Coffee, KaKao, Corn, Wheat, RicePlant, Barley, Cabbage;
    //작업 중 작물, 남은 시간, 일하는 일꾼, 비료, 일꾼 정보,비료 정보, 골드 표시

    public GameObject test;
   
    private WPImageText imageText_Time;              // 예상 시간 UI
    private WPImageText imageText_Money;             // 필요 금액 UI

    private WPScrollView scrollView_Select;               // 일꾼, 비료, 식물을 선택하는 데 필요한 스크롤 뷰

    private Button button_Seed;                         // 식물 선택 버튼
    private Button button_Plant;                        // 식물 심기 버튼 ( 식물을 선택하지 않으면 비활성화? )
    private Button button_Worker;                       // 일꾼 선택 버튼
    private Button button_Fertilizer;                   // 비료 선택 버튼

    private WPField targetField;                        
    private WPFieldCtrl targetFieldCtrl;                // 활용할 밭 객체

    /////////////////////////////////////////////////////////////////////////
    // Methods

    // 초기 설정을 합니다.
    protected override void Init()
    {
        instance = this;

        imageText_Time = this.transform.Find("ImageText_Time").GetComponent<WPImageText>();
        imageText_Money = this.transform.Find("ImageText_Money").GetComponent<WPImageText>();

        scrollView_Select = this.transform.Find("ScrollView_Select").GetComponent<WPScrollView>();

        button_Seed = this.transform.Find("Button_Seed").GetComponent<Button>();
        button_Plant = this.transform.Find("Button_Plant").GetComponent<Button>();
        button_Worker = this.transform.Find("Button_Worker").GetComponent<Button>();
        button_Fertilizer = this.transform.Find("Button_Fertilizer").GetComponent<Button>();

        this.transform.Find("Button_Close").GetComponent<Button>().onClick.AddListener(OnClick_Close);
        button_Seed.onClick.AddListener(OnClick_Seed);
        button_Plant.onClick.AddListener(OnClick_Plant);
        button_Worker.onClick.AddListener(OnClick_Worker);
        button_Fertilizer.onClick.AddListener(OnClick_Fertilizer);

        getSprite();

        SetActive(false);
    }

    public void GetFieldData(WPField wpField, WPFieldCtrl wpFieldCtrl)
    {
        if(wpField == null) // 이 경우 밭의 정보가 없는 것으로, 이 때 여기서 새로운 밭을 만들어 넘겨주어야 합니다.
        {

        }
        else // 밭의 정보가 있습니다. 이 정보를 활용하여 UI로 표시합니다.
        {

        }
    }

    /// <summary>
    /// Time의 UI를 content로 설정합니다.
    /// </summary>
    /// <param name="content"></param>
    public void SetText_Time(string content)
    {
        if (imageText_Time == null) return;
        imageText_Time.SetText(content);
    }

    /// <summary>
    /// Money의 UI를 content로 설정합니다.
    /// </summary>
    /// <param name="content"></param>
    public void SetText_Money(string content)
    {
        if (imageText_Money == null) return;
        imageText_Money.SetText(content);
    }

    // Seed 버튼을 클릭했을 때 호출합니다.
    public void OnClick_Seed()
    {
        scrollView_Select.AddItem(test.GetComponent<WPScrollViewItem>());
        scrollView_Select.SortItem();
        WPGameCommon._WPDebug("식물을 선택");
    }

    // Plant 버튼을 클릭했을 때 호출합니다.
    public void OnClick_Plant()
    {
        WPGameCommon._WPDebug("식물심기를 선택");
        
    }

    // Worker 버튼을 클릭했을 때 호출합니다.
    public void OnClick_Worker()
    {
        WPGameCommon._WPDebug("일꾼을 선택");
    }

    // Fertilizer 버튼을 클릭했을 때 호출합니다.
    public void OnClick_Fertilizer()
    {
        WPGameCommon._WPDebug("비료를 선택");
    }

    // Close 버튼을 클릭했을 때 호출합니다.
    public void OnClick_Close()
    {
        SetActive(false);
    }

    /// <summary>
    /// Resorces로부터 Sprite 불러옴
    /// </summary>
    private void getSprite()
    {
        Empty = Resources.Load<Sprite>("Image/null.png");
        GreenOnion = Resources.Load<Sprite>("Image/UI/Farm/" + "GreenOnion");
        Lettuce = Resources.Load<Sprite>("Image/UI/Farm/" + "Lettuce");
        Potato = Resources.Load<Sprite>("Image/UI/Farm/" + "Potato");
        SugarCane = Resources.Load<Sprite>("Image/UI/Farm/" + "SugarCane");
        Tabacco = Resources.Load<Sprite>("Image/UI/Farm/" + "Tabacco");
        Coffee = Resources.Load<Sprite>("Image/UI/Farm/" + "Coffee");
        KaKao = Resources.Load<Sprite>("Image/UI/Farm/" + "KaKao");
        Corn = Resources.Load<Sprite>("Image/UI/Farm/" + "Corn");
        Wheat = Resources.Load<Sprite>("Image/UI/Farm/" + "Wheat");
        RicePlant = Resources.Load<Sprite>("Image/UI/Farm/" + "RicePlant");
        Barley = Resources.Load<Sprite>("Image/UI/Farm/" + "Barley");
        Cabbage = Resources.Load<Sprite>("Image/UI/Farm/" + "Cabbage");

        if (Empty || GreenOnion || Lettuce || Potato || SugarCane || Tabacco || Coffee || KaKao || Corn || Wheat || RicePlant || Barley || Cabbage)
        {
            WPGameCommon._WPDebug("Field에서 Sprite 못 불어옴 ㅠ");
        }
    }
}
