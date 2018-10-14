using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 지호화이팅
public class WPTempWorkerCtrl : WPActor
{
	/////////////////////////////////////////////////////////////////////////
	// Varaibles

	// 움직임 관련 변수. 일단 하드코딩
	private float movSpeed = 10f;
	private float stopDuration = 2f;
	private float movDuration = 3f;

	private float _moveTimeAcc = 0f;
	private float _currentLimit = 0f;
	private Vector3 _currentDir;

	/////////////////////////////////////////////////////////////////////////
	// Methods

	/// <summary>
	/// InitValues
	/// 오버라이드 해서 사용.
	/// </summary>
    protected override void InitValue()
    {
		base.InitValue();

        // 무빙타입은 로밍.
        base.SetActorMoveType(WPEnum.ActorMoveType.eMoveRoaming);

		// idle 상태로 시작
		base._actorState = WPEnum.ActorState.eActorStateIdle;

		// 초기 방향 정해주기.
		this._currentDir = Vector3.zero;
    }

	/// <summary>
	/// override : 로밍 state 정의
	/// </summary>
	protected override void RoamingMoveFunc()
	{
		this._moveTimeAcc += Time.deltaTime;

		this.transform.Translate(this._currentDir * this.movSpeed * Time.deltaTime);
		
		if (this._currentLimit < _moveTimeAcc)
		{

			// 이건 좀 이상하다. 일단 참고만하고 바꾸도록 하자
			if (WPEnum.ActorState.eActorStateIdle == base._actorState)
			{
				//Idle 인 상태였을 떈 move 상태로 바꿔주자.
				base._actorState = WPEnum.ActorState.eActorStateMoving;

				this._currentLimit = Random.Range(0f, movDuration);
				
				this._currentDir = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
			}
			else if (WPEnum.ActorState.eActorStateMoving == base._actorState)
			{
				// move 하고있을 땐 idle 로 바꾸자.
				base._actorState = WPEnum.ActorState.eActorStateIdle;

				this._currentLimit = Random.Range(0f, stopDuration);

				this._currentDir = Vector3.zero;
			}

			this._moveTimeAcc = 0;
		}
	}
}
