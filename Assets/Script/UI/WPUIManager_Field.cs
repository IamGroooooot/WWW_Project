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
    

    private WPImageText imageText_Time;              // 예상 시간 UI
    private WPImageText imageText_Money;             // 필요 금액 UI

    private WPScrollView_Select scrollView_Select;               // 일꾼, 비료, 식물을 선택하는 데 필요한 스크롤 뷰

    private Button button_Seed;                         // 식물 선택 버튼
    private Button button_Plant;                        // 식물 심기 버튼 ( 식물을 선택하지 않으면 비활성화? )
    private Button button_Worker;                       // 일꾼 선택 버튼
    private Button button_Fertilizer;                   // 비료 선택 버튼

    private WPField targetField;                        
    private WPFieldCtrl targetFieldCtrl;                // 활용할 밭 객체

    private string SelectedSeed;                        //선택한 식물
    /////////////////////////////////////////////////////////////////////////
    // Methods

    // 초기 설정을 합니다.
    protected override void Init()
    {
        instance = this;

        imageText_Time = this.transform.Find("ImageText_Time").GetComponent<WPImageText>();
        imageText_Money = this.transform.Find("ImageText_Money").GetComponent<WPImageText>();

        scrollView_Select = this.transform.Find("ScrollView_Select").GetComponent<WPScrollView_Select>();

        button_Seed = this.transform.Find("Button_Seed").GetComponent<Button>();
        button_Plant = this.transform.Find("Button_Plant").GetComponent<Button>();
        button_Worker = this.transform.Find("Button_Worker").GetComponent<Button>();
        button_Fertilizer = this.transform.Find("Button_Fertilizer").GetComponent<Button>();

        this.transform.Find("Button_Close").GetComponent<Button>().onClick.AddListener(OnClick_Close);
        button_Seed.onClick.AddListener(OnClick_Seed);
        button_Plant.onClick.AddListener(OnClick_Plant);
        button_Worker.onClick.AddListener(OnClick_Worker);
        button_Fertilizer.onClick.AddListener(OnClick_Fertilizer);

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
        scrollView_Select.CreateSeedList();
        WPGameCommon._WPDebug("식물을 선택");
        //SelectedSeed에 값 저장
        //SelectedSeed = "Potato";
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

    // Seed 선택 버튼의 이미지를 초기화합니다.
    private void SetSprite_Seed()
    {
        if (button_Seed == null) return;
        Image seedImage = button_Seed.GetComponent<Image>();
        seedImage.color = new Color(1, 1, 1, 0);
        seedImage.sprite = null;
    }

    /// <summary>
    /// Seed 버튼의 Sprite를 content로 변경합니다.
    /// </summary>
    /// <param name="content"></param>
    public void SetSprite_Seed(Sprite content)
    {
        if (button_Seed == null) return;
        Image seedImage = button_Seed.GetComponent<Image>();
        seedImage.color = new Color(1, 1, 1, 1);
        seedImage.sprite = content;
    }

    /// <summary>
    /// UI를 화면에 param 값에 따라 표시합니다.
    /// </summary>
    /// <param name="param"></param>
    public override void SetActive(bool param)
    {
        if (param)
        {
            SetSprite_Seed();
            scrollView_Select.OnEnabled();
            scrollView_Select.CreateSeedList();
        }
        else
        {
            scrollView_Select.OnDisabled();
        }
        base.SetActive(param);
    }

    // Close 버튼을 클릭했을 때 호출합니다.
    public void OnClick_Close()
    {
        SetActive(false);
    }
}
