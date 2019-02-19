using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPSicknessCtrl : WPActor {

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

        base.SetActorMoveType(WPEnum.ActorMoveType.eMoveNone);

        // Empty 상태로 시작
        base._actorState = WPEnum.ActorState.eActorStateNone;
		
		//코루틴 멈춰줘야되낭?
		base.startMoveRoutine = false;
		//gameObject.SetActive(false);
	}

    private void OnMouseDown()
    {
		StopCoroutine(base.MoveRoutine());
		gameObject.SetActive(false);
    }
}
