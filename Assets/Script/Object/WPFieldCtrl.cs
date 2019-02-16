﻿using System.Collections;
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
    private List<Sprite> seedSpriteData = new List<Sprite>();
    private int fieldIndex;                                          //현재 밭의 인덱스
    Transform plantTrans;//현재 심어진 plant 

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

        //현재 밭의 인덱스 설정
        fieldIndex = Convert.ToInt32(this.transform.name.Substring(5));

        // 무빙타입은 NONE.
        base.SetActorMoveType(WPEnum.ActorMoveType.eMoveNone);

        // Empty 상태로 시작
        base._actorState = WPEnum.ActorState.eSeed_Empty;

        //plant Transform
        plantTrans = FindSeed();

        //Sprite가져오기
        seedSpriteData = LoadSeedData();
        
        //필드 데이터 가져오기
        //비어있으면 필드에 널
        //있으면 필드에 해당wpField넣기

        //심은 시기/현재 시간 차로 성장률 계산
        //GrownPercent = 0;
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


        //Sprite 설정하기 
        //this.GetComponent<SpriteRenderer>().sprite = Empty;
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
            //여기가 문제 빨리 고쳐야 됨.
            //WPUIManager_Field.instance.GetFieldData(null, this);
            yield return null; // OnMouseDown을 통한 입력에서 버튼이 바로 눌리는 문제가 있기에 1 프레임 대기
            WPUIManager_Field.instance.SetActive(true);
        }
        else
        {
            if (wpField.CheckIfCompleted()) // 작물이 완성되었습니다.
            {
                // 보상 획득하는 코드 짤 것.
                wpField.CheckGold();
            }
            else // 작물이 완성되지 않았습니다.
            {
                WPUIManager_Field.instance.GetFieldData(wpField, this);
                yield return null; // OnMouseDown을 통한 입력에서 버튼이 바로 눌리는 문제가 있기에 1 프레임 대기
                WPUIManager_Field.instance.SetActive(true);
            }
        }   
    }

    private List<Sprite> LoadSeedData()
    {
        List<Sprite> spriteData = new List<Sprite>();
        List<Dictionary<string, object>> seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.eSeed);
        for (int index = 0; index < seedData.Count; ++index)
        {
            string seedDataName = seedData[index]["eDataName"].ToString();
            string seedDataPath = DATA_PATH + seedDataName.Substring(1);

            Sprite seedSprite = WPResourceManager.instance.GetResource<Sprite>(seedDataPath);
            if (seedSprite != null)
            {
                spriteData.Insert(index, seedSprite);
            }
        }
        return spriteData;
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

    private Transform FindSeed()
    {
        Transform transform =this.transform.Find("Graphic_Seed");
        //WPGameCommon._WPAssert(transform);
        return transform;
    }

    public void SetFieldData(WPField _wpField)
    {
        wpField = _wpField;
    }
}

