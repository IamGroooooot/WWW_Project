using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class WPFieldCtrl : WPActor
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    private static string DATA_PATH = "Image/Seed/";
    private static List<WPData_Seed> seedData;

    private int fieldIndex;                                         // 현재 밭의 인덱스
    private WPEnum.VariableType fieldKey;                           // 데이터 저장을 위한 현재 밭의 키값

    private bool isSick;
    private bool IsSick
    {
        get
        {
            return isSick;
        }
        set
        {
            SetSprite_Sickness(value);
            isSick = value;
            if (wpField != null) wpField.isSick = value;
        }
    }// 병충해에 걸려 있는가?

    private Transform graphic_Seed;                                 // Seed 표현을 위한 Transform

    private GameObject graphic_Sickness;                            // Sickness 표현을 위한 GameObject

    public WPField wpField;                                        // 밭의 정보를 저장하는 변수입니다.

    // 원래 wpField가 null인지 아닌지를 통해 밭에 작물이 심어져 있는지 확인했으나, 이러면 밭에 작물을 심지 않았을 때 병충해의 유무를 저장할 수가 없게 됩니다.
    // 그래서 밭에 작물이 있는지 없는지 유무를 WPField 객체의 IsPlanted를 통해 판별합니다. (IsPlanted = seedIndex != -1)
    // 일단 작물이 완성되면 클릭하면 식물을 없애도록 만들었습니다.

    private WPDateTime checkTime;
    /////////////////////////////////////////////////////////////////////////
    // Methods

    /// <summary>
    /// InitValues
    /// 오버라이드 해서 사용.
    /// </summary>
    protected override void InitValue()
    {
        base.InitValue();

        // static data의 설정
        if(seedData == null) seedData = WPGameDataManager.instance.GetData<WPData_Seed>(WPEnum.GameData.eSeed);

        //현재 밭의 인덱스 설정
        fieldIndex = Convert.ToInt32(this.transform.name.Substring(5));

        // 무빙타입은 NONE.
        base.SetActorMoveType(WPEnum.ActorMoveType.eMoveNone);

        // Empty 상태로 시작
        base._actorState = WPEnum.ActorState.eSeed_Empty;

        graphic_Seed = transform.Find("Graphic_Seed");

        graphic_Sickness = transform.Find("Graphic_Sickness").gameObject;

        fieldKey = (WPEnum.VariableType)System.Enum.Parse(typeof(WPEnum.VariableType), "eField" + fieldIndex.ToString());

        string data = WPGameVariableManager.instance.LoadStringVariable(fieldKey);

        if (string.IsNullOrEmpty(data))
            SetFieldData(new WPField());
        else
            SetFieldData(WPField.ParseData(data));

        IsSick = wpField.isSick;

        checkTime = WPDateTime.ParseData(WPDateTime.Now.ToData());

        WPDateTime.Now.OnValueChanged += OnTimeChanged;

    }

    private void OnMouseDown()
    {
        //밭 작업 중 창
        if (EventSystem.current.IsPointerOverGameObject()) return; // UI를 통과해 클릭하는 것을 방지
        StartCoroutine(OpenUI()); 
    }

    private IEnumerator OpenUI()
    {
        WPGameCommon._WPDebug("밭을 클릭 : " + gameObject.name);
        // 작업 중 작물, 남은 시간, 일하는 일꾼, 비료, 일꾼 정보,비료 정보, 골드 표시
        if(!wpField.IsPlanted) // 밭이 비어 있습니다.
        {
            if (IsSick)     // 병충해에 걸려있습니다.
            {
                IsSick = false;
                yield return null;
            }
            else
            {
                WPUIManager_Field.instance.GetFieldData(null, this);
                yield return null; // OnMouseDown을 통한 입력에서 버튼이 바로 눌리는 문제가 있기에 1 프레임 대기
                WPUIManager_Field.instance.SetActive(true);
            }
        }
        else
        {
            if (IsSick)     // 병충해에 걸려있습니다.
            {
                IsSick = false;
                yield return null;
            }
            else
            {
                if (wpField.IsCompleted) // 작물이 완성되었습니다.
                {
                    // 보상 획득하는 코드 짤 것.
                    WPGameCommon._WPDebug("작물이 완성됨.");
                    wpField.CheckGold();
                    ClearFieldData();
                    SaveFieldData();
                    yield return null;
                }
                else // 작물이 완성되지 않았습니다.
                {
                    WPGameCommon._WPDebug("밭 정보 있음");
                    WPUIManager_Field.instance.GetFieldData(wpField, this);
                    yield return null; // OnMouseDown을 통한 입력에서 버튼이 바로 눌리는 문제가 있기에 1 프레임 대기
                    WPUIManager_Field.instance.SetActive(true);
                }
            }
        }   
    }

    private void SetScale_Seed(float scale)
    {
        if (graphic_Seed == null) return;
        if (scale < 0 || scale > 1) return;
        for(int i = 0; i < graphic_Seed.childCount; ++i)
        {
            graphic_Seed.GetChild(i).localScale = new Vector2(scale, scale);
        }
    }

    private void SetSprite_Seed(Sprite sprite)
    {
        if (graphic_Seed == null) return;
        for(int i = 0; i < graphic_Seed.childCount; ++i)
        {
            SpriteRenderer spriteRenderer = graphic_Seed.GetChild(i).GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) continue;
            spriteRenderer.sprite = sprite;
        }
    }

    private void SetSprite_Sickness(bool isActive)
    {
        graphic_Sickness.gameObject.SetActive(isActive);
    }

    public void SetFieldData(WPField _wpField)
    {
        if (_wpField == null) return;

        wpField = _wpField;

        if(growRoutine == null && wpField.IsPlanted)
        {
            growRoutine = GrowRoutine();
            StartCoroutine(growRoutine);
        }
    }

    public void SaveFieldData()
    {
        if (wpField != null)
        {
            WPGameVariableManager.instance.SaveVariable(fieldKey, wpField.ToData());
        }
        else
        {
            WPGameVariableManager.instance.SaveVariable(fieldKey, "");
        }
    }

    public void ClearFieldData()
    {
        if (growRoutine != null)
        {
            StopCoroutine(growRoutine);
            growRoutine = null;
        }
        SetSprite_Seed(null);
        wpField = new WPField();
    }

    private void OnTimeChanged(WPDateTime nowTime)
    {
        for(int timeChangedValue = WPDateTime.CompareTime(nowTime, checkTime);
            timeChangedValue >= 6;
            timeChangedValue -= 6)
        {               // 게임 상의 시간이 6시간이 지날 때마다 병충해 계산 (25%)을 시행하고, 데이터를 저장합니다.
            if (!IsSick)
            {
                int randomValue = UnityEngine.Random.Range(0, 100);
                IsSick = randomValue < 25;
            }
            SaveFieldData();
            checkTime = WPDateTime.ParseData(nowTime.ToData());
        }
    }

    private IEnumerator growRoutine;
    private IEnumerator GrowRoutine()
    {
        string seedDataName = seedData[wpField.seedIndex].DataName;
        string seedDataPath = DATA_PATH + seedDataName;

        Sprite seedSprite = WPResourceManager.instance.GetResource<Sprite>(seedDataPath);

        SetSprite_Seed(seedSprite);

        SetScale_Seed(0.15f);

        yield return new WaitUntil(() => wpField.GrowthRate >= 0.3f);

        SetScale_Seed(0.3f);

        yield return new WaitUntil(() => wpField.GrowthRate >= 0.6f);

        SetScale_Seed(0.6f);

        yield return new WaitUntil(() => wpField.GrowthRate >= 1f);

        SetScale_Seed(1f);
    }

}

