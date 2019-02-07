using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class WPFieldCtrl : WPActor
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    
    

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

        
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return; // UI를 통과해 클릭하는 것을 방지
        StartCoroutine(OpenUI());
    }

    private IEnumerator OpenUI()
    {
        WPGameCommon._WPDebug("밭을 클릭");
        // 작업 중 작물, 남은 시간, 일하는 일꾼, 비료, 일꾼 정보,비료 정보, 골드 표시
        // UI의 업데이트, WPUIManager_Field를 참조
        yield return null; // OnMouseDown을 통한 입력에서 버튼이 바로 눌리는 문제가 있기에 1 프레임 대기
        WPUIManager_Field.instance.SetActive(true);
    }

}

