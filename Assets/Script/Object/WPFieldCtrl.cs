using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        //밭 작업 중 창
        WPGameCommon._WPDebug("clicked");
        //작업 중 작물, 남은 시간, 일하는 일꾼, 비료, 일꾼 정보,비료 정보, 골드 표시
    }

}

