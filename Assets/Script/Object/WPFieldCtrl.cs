using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class WPFieldCtrl : WPActor
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    private int ratio = 2;
    private int GrownPercent=0;
    private static string DATA_PATH = "Image/UI/Farm/";
    private static List<Dictionary<string, object>> seedData;

    private int fieldIndex;                                          //현재 밭의 인덱스
    Transform transform_Seed;                           // Seed 표현을 위한 transform
    public WPField wpField = null; // 밭의 정보를 저장하는 변수입니다.
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
        if(seedData == null) seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.eSeed);

        //현재 밭의 인덱스 설정
        fieldIndex = Convert.ToInt32(this.transform.name.Substring(5));

        // 무빙타입은 NONE.
        base.SetActorMoveType(WPEnum.ActorMoveType.eMoveNone);

        // Empty 상태로 시작
        base._actorState = WPEnum.ActorState.eSeed_Empty;

        transform_Seed = transform.Find("Seed");

        //필드 데이터 가져오기
        //비어있으면 필드에 널
        //있으면 필드에 해당wpField넣기
        /*
        if (wpField != null)
        {
            //30%->localScale Double Once
            if (wpField.GetGrownPercent() >= 30)
            {
                if (DoubleScale(plantTrans) == -1) WPGameCommon._WPDebug("지정된 작물 없음");
            }
            //60%->localScale Double Once again
            if (wpField.GetGrownPercent() >= 60)
            {
                if (DoubleScale(plantTrans) == -1) WPGameCommon._WPDebug("지정된 작물 없음");
            }
        }
        */

        // wpField의 seedIndex에 따른 sprite를 가져오는 방법입니다.
        /*
        string seedDataName = seedData[wpField.seedIndex]["eDataName"].ToString();
        string seedDataPath = DATA_PATH + seedDataName.Substring(1);

        Sprite seedSprite = WPResourceManager.instance.GetResource<Sprite>(seedDataPath);
        */

        //Sprite 설정하기 
        //this.GetComponent<SpriteRenderer>().sprite = Empty;
        StartCoroutine(GrowRoutine());
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
        if(wpField == null) // 밭이 비어 있습니다.
        {
            WPUIManager_Field.instance.GetFieldData(null, this);
            yield return null; // OnMouseDown을 통한 입력에서 버튼이 바로 눌리는 문제가 있기에 1 프레임 대기
            WPUIManager_Field.instance.SetActive(true);
        }
        else
        {
            if (wpField.IsCompleted) // 작물이 완성되었습니다.
            {
                // 보상 획득하는 코드 짤 것.
                wpField.CheckGold();
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

    //Plant Scale 2times at(30 60 100) 
    int DoubleScale(Transform targetIMG)
    {
		if(targetIMG==null) return -1;

        float curSize = targetIMG.localScale.y;

        targetIMG.localScale = new Vector3(targetIMG.localScale.x, targetIMG.localScale.y * ratio, targetIMG.localScale.z);
        float Up = (ratio - 1) * curSize / 2f;
        targetIMG.localPosition += new Vector3(0, Up, 0);
		return 0;
    }

    private void SetScale(float scale)
    {
        if (transform_Seed == null) return;
        if (scale < 0 || scale > 1) return;
        for(int i = 0; i < transform_Seed.childCount; ++i)
        {
            transform_Seed.GetChild(i).localScale = new Vector2(1, scale);
        }
    }

    private void SetSprite(Sprite sprite)
    {
        if (transform_Seed == null) return;
        if (sprite == null) return;
        for(int i = 0; i < transform_Seed.childCount; ++i)
        {
            SpriteRenderer spriteRenderer = transform_Seed.GetChild(i).GetComponent<SpriteRenderer>();
            if (spriteRenderer == null) continue;
            spriteRenderer.sprite = sprite;
        }
    }

    public void SetFieldData(WPField _wpField)
    {
        wpField = _wpField;
    }

    private IEnumerator GrowRoutine()
    {
        yield return new WaitUntil(() => wpField != null);

        string seedDataName = seedData[wpField.seedIndex]["eDataName"].ToString();
        string seedDataPath = DATA_PATH + seedDataName.Substring(1);

        Sprite seedSprite = WPResourceManager.instance.GetResource<Sprite>(seedDataPath);

        SetSprite(seedSprite);

        SetScale(0.15f);

        yield return new WaitUntil(() => wpField.GetGrownPercent() >= 0.3f);

        SetScale(0.3f);

        yield return new WaitUntil(() => wpField.GetGrownPercent() >= 0.6f);

        SetScale(0.6f);

        yield return new WaitUntil(() => wpField.GetGrownPercent() >= 1f);

        SetScale(1f);

    }
}

