using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WPFieldCtrl : WPActor
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    Sprite Empty, GreenOnion, Lettuce, Potato, SugarCane, Tabacco, Coffee, KaKao, Corn, Wheat, RicePlant, Barley, Cabbage;
    int ratio = 2;
    int GrownPercent=0;

    Transform plantTrans;

    WPField wpField = null; // 밭의 정보를 저장하는 변수입니다.

    /////////////////////////////////////////////////////////////////////////
    // Methods

    /// <summary>
    /// InitValues
    /// 오버라이드 해서 사용.
    /// </summary>
    protected override void InitValue()
    {
        base.InitValue();

        // 무빙타입은 NONE.
        base.SetActorMoveType(WPEnum.ActorMoveType.eMoveNone);

        // Empty 상태로 시작
        base._actorState = WPEnum.ActorState.eSeed_Empty;

        //plant Transform
        plantTrans = FindSeed();
        

        //Sprite가져오기
        //getSprite();

        //필드 데이터 가져오기

        /*
        //심은 시기/현재 시간 차로 성장률 계산
        //GrownPercent = 0;
        //30%->localScale Double Once
        if(GrownPercent < 30)
        {
            DoubleScale(null);
        }
        //60%->localScale Double Once again
        if (GrownPercent < 60)
        {
            DoubleScale(null);
        }
        */


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
            WPUIManager_Field.instance.GetFieldData(null, this);
            yield return null; // OnMouseDown을 통한 입력에서 버튼이 바로 눌리는 문제가 있기에 1 프레임 대기
            WPUIManager_Field.instance.SetActive(true);
        }
        else
        {
            if (wpField.CheckIfCompleted()) // 작물이 완성되었습니다.
            {
                
            }
            else // 작물이 완성되지 않았습니다.
            {
                WPUIManager_Field.instance.GetFieldData(wpField, this);
                yield return null; // OnMouseDown을 통한 입력에서 버튼이 바로 눌리는 문제가 있기에 1 프레임 대기
                WPUIManager_Field.instance.SetActive(true);
            }
        }   
    }

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

        if(Empty || GreenOnion || Lettuce || Potato || SugarCane || Tabacco || Coffee || KaKao || Corn || Wheat || RicePlant || Barley || Cabbage)
        {
            WPGameCommon._WPDebug("Field에서 Sprite 못 불어옴 ㅠ");        
        }
    }

    //Plant Scale 2time at(30 60 100) 
    void DoubleScale(Transform targetIMG)
    {
        float curSize = targetIMG.localScale.y;

        targetIMG.localScale = new Vector3(targetIMG.localScale.x, targetIMG.localScale.y * ratio, targetIMG.localScale.z);
        float Up = (ratio - 1) * curSize / 2f;
        targetIMG.localPosition += new Vector3(0, Up, 0);
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

