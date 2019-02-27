using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WPUIManager_Field : WPUIManager {
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPUIManager_Field instance = null;     // singleton

    // 작업 중 작물, 남은 시간, 일하는 일꾼, 비료, 일꾼 정보,비료 정보, 골드 표시
    

    private WPImageText imageText_Time;              // 예상 시간 UI
    private WPImageText imageText_Money;             // 필요 금액 UI

    private WPUI_FieldStatus ui_FieldStatus;            // 밭의 상태 표시 UI

    private WPScrollView_Select scrollView_Select;               // 일꾼, 비료, 식물을 선택하는 데 필요한 스크롤 뷰

    private Button button_Seed;                         // 식물 선택 버튼
    private Button button_Action;                       // 뭔갈 하는 버튼
    private Button button_Worker;                       // 일꾼 선택 버튼
    private Button button_Fertilizer;                   // 비료 선택 버튼

    private WPField targetField;                        
    private WPFieldCtrl targetFieldCtrl;                // 활용할 밭 객체

    public bool IsActive
    {
        get
        {
            return gameObject.activeSelf;
        }
    }

    // private string SelectedSeed;                        //선택한 식물
    /////////////////////////////////////////////////////////////////////////
    // Methods

    // 초기 설정을 합니다.
    protected override void Init()
    {
        instance = this;

        imageText_Time = this.transform.Find("ImageText_Time").GetComponent<WPImageText>();
        imageText_Money = this.transform.Find("ImageText_Money").GetComponent<WPImageText>();

        ui_FieldStatus = this.transform.Find("UI_FieldStatus").GetComponent<WPUI_FieldStatus>();

        scrollView_Select = this.transform.Find("ScrollView_Select").GetComponent<WPScrollView_Select>();

        button_Seed = this.transform.Find("Button_Seed").GetComponent<Button>();
        button_Action = this.transform.Find("Button_Action").GetComponent<Button>();
        button_Worker = this.transform.Find("Button_Worker").GetComponent<Button>();
        button_Fertilizer = this.transform.Find("Button_Fertilizer").GetComponent<Button>();

        this.transform.Find("Button_Close").GetComponent<Button>().onClick.AddListener(OnClick_Close);
        button_Seed.onClick.AddListener(OnClick_Seed);
        button_Action.onClick.AddListener(OnClick_Action);
        button_Worker.onClick.AddListener(OnClick_Worker);
        button_Fertilizer.onClick.AddListener(OnClick_Fertilizer);
        WPGameCommon._WPDebug("UIManager_Field 초기화");
        SetActive(false);
    }

    // 로직 구현 방식입니다.
    // 밭을 누르면 GetFieldData로 해당 밭의 WPField 클래스를 받아오려고 합니다.
    // 만약 아무것도 심지 않은 상태라면 그 밭의 WPField 클래스는 null 값입니다. (사실 IsPlanted == false 인 wpfield가 존재하지만, GetFieldData 에 인자로 넘겨주는 과정에서 null 값을 넘겨주게 됩니다.)
    // null 이라면 그 밭은 비어있고, null 이 아니라면 그 클래스를 이용해 ui를 표시합니다.
    // null이라고 해서 새로운 WPField를 바로 생성하지 않습니다. <- 중요.
    // UI에서 선택한 정보는 WPScrollView_Select에 저장됩니다. ( seedIndex, workerIndex, fertilizerIndex )
    // 새로운 WPField를 생성하는 시점은 OnClick_Plant를 클릭했을 때 입니다.
    public void GetFieldData(WPField wpField, WPFieldCtrl wpFieldCtrl)
    {
        if(wpField == null) // 이 경우 밭의 정보가 없는 것으로, 이 때 여기서 새로운 밭을 만들어 넘겨주어야 합니다.
        {
            Text text = button_Action.GetComponentInChildren<Text>();
            if (text != null) text.text = "심기";                        // Action 버튼의 UI 설정

            ui_FieldStatus.SetActive(false);

            button_Seed.interactable = true;                            // Seed 버튼의 활성화

            SetSprite_Seed();
            scrollView_Select.SetActive(true);                          // scrollView UI 활성화
        }
        else                // 밭의 정보가 있습니다. 이 정보를 활용하여 UI로 표시합니다.
        {
            Text text = button_Action.GetComponentInChildren<Text>();
            if (text != null) text.text = "엎기";

            WPGameCommon._WPDebug("Seed Index "+wpField.seedIndex + ":" + "Worker Index " + wpField.workerIndex + ":"+ "Fertilizer Index "+ wpField.fertilizerIndex);

            if(wpField.seedIndex != -1)
            {
                WPData_Seed seedData = WPGameDataManager.instance.GetData<WPData_Seed>(WPEnum.GameData.eSeed)[wpField.seedIndex];

                SetText_Time(string.Format("{0:f2}% 자람", (wpField.GrowthRate * 100f)));
                instance.SetText_Money(
                    Convert.ToInt32(seedData.ComparePrice).ToString());

                string seedDataName = seedData.DataName;
                string seedDataPath = "Image/Seed/" + seedDataName;

                Sprite seedSprite = WPResourceManager.instance.GetResource<Sprite>(seedDataPath);

                SetSprite_Seed(seedSprite);
            }

            ui_FieldStatus.SetActive(true);

            if (wpField.workerIndex != -1)
            {

            }
            else ui_FieldStatus.Worker.SetActive(false);

            if (wpField.fertilizerIndex != -1)
            {
                WPData_Fertilizer fertilizerData = WPGameDataManager.instance.GetData<WPData_Fertilizer>(WPEnum.GameData.eFertilizer)[wpField.fertilizerIndex];

                string fertilizerDataName = fertilizerData.DataName;
                string fertilizerDataPath = "Image/Fertilizer/" + fertilizerDataName;

                Sprite fertilizerSprite = WPResourceManager.instance.GetResource<Sprite>(fertilizerDataPath);

                ui_FieldStatus.Fertilizer.SetActive(true);

                ui_FieldStatus.Fertilizer.SetText(fertilizerData.Name);
                ui_FieldStatus.Fertilizer.SetSprite(fertilizerSprite);

            }
            else ui_FieldStatus.Fertilizer.SetActive(false);

            button_Seed.interactable = false;

            scrollView_Select.SetActive(false);

            this.targetField = wpField;
        }
        this.targetFieldCtrl = wpFieldCtrl;
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
    }

    // Action 버튼을 클릭했을 때 호출합니다.
    public void OnClick_Action()
    {
        if (targetField != null)
        {
            WPGameCommon._WPDebug("식물엎기를 선택");
            targetFieldCtrl.ClearFieldData();
            targetFieldCtrl.SaveFieldData();
            SetActive(false);
        }
        else
        {
            WPGameCommon._WPDebug("식물심기를 선택");
            int seedIndex = scrollView_Select.seedIndex;
            int workerIndex = scrollView_Select.workerIndex;
            int fertilizerIndex = scrollView_Select.fertilizerIndex;
            /*
            if(seedIndex == -1 || workerIndex == -1 || fertilizerIndex == -1)
            {
                string noticeString = string.Empty;
                if (seedIndex == -1) noticeString += "심을 식물";
                if (workerIndex == -1)
                {
                    if (seedIndex == -1) noticeString += ", ";
                    noticeString += "일꾼";
                }
                if (fertilizerIndex == -1)
                {
                    if (seedIndex == -1 || workerIndex == -1) noticeString += ", ";
                    noticeString += "비료";
                }
                noticeString += "을(를) 선택하지 않았습니다!";
                WPUIManager_Toast.instance.MakeToast(noticeString, 3f);
                return;
            }*/
            if (seedIndex == -1)
            {
                string noticeString = string.Empty;
                if (seedIndex == -1) noticeString += "심을 식물";
                noticeString += "을(를) 선택하지 않았습니다!";
                WPUIManager_Toast.instance.MakeToast(noticeString, 3f);
                return;
            }
            // 모두 선택하였다면 새로운 객체를 생성하여 넘겨준다. targetField는 필요하지 않을수도?

            WPUserDataManager.instance.SetFertilizer(fertilizerIndex, WPUserDataManager.instance.GetFertilizer(fertilizerIndex) - 1); // 비료 수의 감소

            targetField = new WPField(seedIndex, workerIndex, fertilizerIndex);
            targetFieldCtrl.SetFieldData(targetField);
            targetFieldCtrl.SaveFieldData();

            //심을 때 Worker도 보내줌
            SendCustomizedWorker();
            
            SetActive(false);
        }
    }

    // Worker 버튼을 클릭했을 때 호출합니다.
    public void OnClick_Worker()
    {
		//Worker가 없는 식물이 심어져있는 경우
		if (targetField != null)
		{
			if(targetField.workerIndex == -1)
			{
				return;
			}
		}

        //If No NullWorker, Spawn Null Worker 
        WPActorManager.instance.IncreaseNullWorker();


        scrollView_Select.CreateWorkerList();
		int fieldId = System.Convert.ToInt32(WPFieldCtrl.justClickedField.Substring(5))%10;
		int farmId = System.Convert.ToInt32(WPFieldCtrl.justClickedField.Substring(5))/10;
		int workerId = UnityEngine.Random.Range(0, 5);

		//Worker가져와서 불러 와야됨
		WPCustomizationManager.instance.SetInvisible(false);
		WPCustomizationManager.instance.setWorkerOnCustomManager(new WPWorker(farmId,fieldId, workerId,0,null));
        WPCustomizationManager.instance.Randomize();
		WPGameCommon._WPDebug("일꾼을 선택");
        //이 UI 멀리 보냄
        SetInvisible(true);
	}

    // Fertilizer 버튼을 클릭했을 때 호출합니다.
    public void OnClick_Fertilizer()
    {
        scrollView_Select.CreateFertilizerList();
        WPGameCommon._WPDebug("비료를 선택");
    }

    // Seed 선택 버튼의 이미지를 초기화합니다.
    private void SetSprite_Seed()
    {
        if (button_Seed == null) return;
        Image seedImage = button_Seed.GetComponent<Image>();
        seedImage.color = new Color(1, 1, 1, 0);
        seedImage.sprite = null;
        seedImage.transform.Find("Text").gameObject.SetActive(true);
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
        seedImage.transform.Find("Text").gameObject.SetActive(false);
        
    }

    /// <summary>
    /// UI를 화면에 param 값에 따라 표시합니다.
    /// </summary>
    /// <param name="param"></param>
    public override void SetActive(bool param)
    {
        if (param)
        {
            WPVariable.timeScale_NewsUI = 0f;
            WPVariable.timeScale_WPDateTime = 0f;
        }
        else
        {
            WPVariable.timeScale_NewsUI = 1f;
            WPVariable.timeScale_WPDateTime = 1f;
            targetField = null;
            targetFieldCtrl = null;
            SetText_Time("예상 시간");
            SetText_Money("예상 금액");
        }
        base.SetActive(param);
    }

    // Close 버튼을 클릭했을 때 호출합니다.
    public void OnClick_Close()
    {
        SetActive(false);
	}

	//커스터마이징 된 워커 정보를 NullWorker라는 태그를 가진 게임 오브젝트에 보내고 기존에 사용한 워커 정보를 초기화 시킨다
	private void SendCustomizedWorker()
	{
        if (targetFieldCtrl == null||targetFieldCtrl.WpField == null || targetFieldCtrl.WpField.seedIndex == -1)
        {
            WPCustomizationManager.instance.setWorkerOnCustomManager(null);
            Debug.Log("밭이 없음 // Worker Set 안함");
            return;
        }

        

		GameObject targetWorker = GameObject.FindGameObjectWithTag("NullWorker");
        
		//Worker전달 해줘야됨// 임시로 NullWorker라는 태그 가진 애한테 전달 함.
		if (targetWorker != null && WPCustomizationManager.instance.worker != null && WPCustomizationManager.instance.worker.appearance != null )
		{
            //targetWorker의 이미지를 SET해줌
            targetWorker.GetComponent<WPWorkerCtrl>().SetWorker(WPCustomizationManager.instance.worker);
            //targetWorker의 태그와 이름 재설정, 위치도 재설정, 움직임도 재설정
			targetWorker.tag = "Worker";
			targetWorker.name = WPFieldCtrl.justClickedField + "_Worker";
            targetWorker.GetComponent<WPWorkerCtrl>().SetActorPos(UnityEngine.Random.Range(-WPVariable.currentFieldSizeX / 2f, WPVariable.currentFieldSizeX / 2f), UnityEngine.Random.Range(-WPVariable.currentFieldSizeY / 2f, WPVariable.currentFieldSizeY / 2f));
            targetWorker.GetComponent<WPWorkerCtrl>().SetActorMoveType(WPEnum.ActorMoveType.eMoveRoaming);

            //WorkerCount 늘려줌
            // 매니저 메모리에 반영 시도 
            WPActorManager.instance._workerCount++;
            // 유저데이터에 작성.
            WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eUserWorkerCount, WPActorManager.instance._workerCount);
        }


        WPCustomizationManager.instance.setWorkerOnCustomManager(null);
    }
}
