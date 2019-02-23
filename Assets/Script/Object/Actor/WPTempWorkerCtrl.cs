using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 지호화이팅
public class WPTempWorkerCtrl : WPActor
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    // 관리 중인 밭
    private int myFieldIndex;
    WPFieldCtrl workingField;
	WPWorker wpWorker;
    //public bool 


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

		//WorkerData가져오기 

		//임시
		if (wpWorker == null)
		{
			wpWorker = new WPWorker();
			wpWorker.workingFarmIndex = 0;
			wpWorker.workingFieldIndex = 0;
		}


		getFieldData();
		//setImage


	}

	/// <summary>
	/// override : 로밍 state 정의
	/// </summary>
	protected override void RoamingMoveFunc()
	{
		this._moveTimeAcc += Time.deltaTime;

		//일하는 field를 가져온다.
		if (workingField == null)
		{
			getFieldData();
			return;
		}
		
		this.transform.Translate(this._currentDir * this.movSpeed * Time.deltaTime);
		
		if (this._currentLimit < _moveTimeAcc)
		{
			//병충해에 걸린 경우 ActorState를 병충해추적 상태로 바꿈.
			if (workingField.GetIsSick())
			{
				base._actorState = WPEnum.ActorState.eActorTrkingSickness;
			}
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
			}else if(WPEnum.ActorState.eActorTrkingSickness==base._actorState){
				// 병충해 제거 하고있을 땐 idle 로 바꾸자.
				base._actorState = WPEnum.ActorState.eActorStateIdle;

				this._currentLimit = 1f;

				//관리하는 밭이 병충해에 걸린 경우 target을 재설정
				if (workingField.GetIsSick())
				{
					if (this._currentDir.Equals(Vector3.zero))
					{
						//Idle상태에서 시작한 경우
						_currentDir = Camera.main.ScreenToWorldPoint(workingField.GetComponent<Transform>().position) - Camera.main.ScreenToWorldPoint(transform.position);
					}
					else
					{
						//move상태에서 시작한 경우
						_currentDir = Camera.main.ScreenToWorldPoint(workingField.GetComponent<Transform>().position) - Camera.main.ScreenToWorldPoint(transform.position) - _currentDir;
					}
					_currentDir = Vector3.Normalize(_currentDir);
				}
			}

            this._moveTimeAcc = 0;
		}
	}

	//WPWorker를 이용해 현재 일하고 있는 field가져옴.
    public void getFieldData()
    {
        if ((this.workingField != null)|| GameObject.Find("Field" + wpWorker.workingFarmIndex.ToString() + wpWorker.workingFieldIndex.ToString())==null) return;
		this.workingField = GameObject.Find("Field"+ wpWorker.workingFarmIndex.ToString()+ wpWorker.workingFieldIndex.ToString()).GetComponent<WPFieldCtrl>();
    }
	


}
