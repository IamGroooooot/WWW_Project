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
		gameObject.SetActive(false);
	}

	void OnTriggerStay2D(Collider2D Col)
	{
		//Debug.Log(Col.name);
		if (Col.GetComponent<WPFieldCtrl>() != null && Col.GetComponent<WPFieldCtrl>().wpField != null)
		{
            //Worker가 없는 경우
            if (Col.GetComponent<WPFieldCtrl>().wpField.workerIndex == -1)
            {
                //식물 성장을 멈추셈

            }
            else
			{
				//해당하는 Worker의 목적지를 이 병충해가 존재하는 Farm으로 이동시키셈.

			}
		}

		//Worker에 닿인 경우
		if (Col.GetComponent<Transform>().CompareTag("Worker"))
		{
			OnMouseDown();
		}
	}

	private void OnMouseDown()
    {
		StopCoroutine(base.MoveRoutine());
		gameObject.SetActive(false);
    }
}
