using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPActor : MonoBehaviour
{
	/////////////////////////////////////////////////////////////////////////
	// Varaibles

	// 움직임 상태 저장. 기본은 None
	private WPEnum.ActorMoveType _moveType = WPEnum.ActorMoveType.eMoveNone;

	// 액터키
	private int _actorKey;

	// 현재 액터의 상태. 기본 None
	protected WPEnum.ActorState _actorState = WPEnum.ActorState.eActorStateNone;

	/////////////////////////////////////////////////////////////////////////
	// Methods
	protected void Start()
    {
		this.InitValue();

        StartCoroutine(MoveRoutine());
    }

	/// <summary>
	/// InitValues
	/// 이걸 상속받아서 사용하자.
	/// </summary>
	protected virtual void InitValue() { }

    /// <summary>
    /// 현재 Actor 의 포지션 세팅.
    /// </summary>
    /// <param name="xPos"></param>
    /// <param name="yPos"></param>
    public void SetActorPos(float xPos, float yPos)
    {
        this.transform.position = new Vector3(xPos, yPos);
    }

	/// <summary>
	/// ActorKey 를 세팅해준다.
	/// </summary>
	/// <param name="key"></param>
	public void SetActorKey(int key)
	{
		this._actorKey = key;
	}

	/// <summary>
	/// ActorKey 반환.
	/// </summary>
	/// <returns></returns>
	public int GetActorKey()
	{
		return this._actorKey;
	}

	/// <summary>
	/// ActorState 반환.
	/// actorstate 는 내부에서만 set 가능해야한다. 외부에서는 get 만 가능해야 한다.
	/// </summary>
	/// <returns></returns>
	public WPEnum.ActorState GetActorState()
	{
		return this._actorState;
	}

    /// <summary>
    /// 현재 ActorMoveType 세팅
    /// </summary>
    /// <param name="type"></param>
    public void SetActorMoveType(WPEnum.ActorMoveType type)
    {
		WPGameCommon._WPDebug("ActorMoveType 이 변경됩니다! : " + type.ToString());

        this._moveType = type;
    }

    /// <summary>
    /// Actor 의 움직임 루틴 moveType 으로 움직임을 제어가능하게 만드는 것이 목적.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator MoveRoutine()
    {
		// 무한루프
		for (;;)
		{
			// TODO : 각 상태일 떄 호출하는 함수를 버츄얼 함수로 상속받는 곳에서 정의하게 하자.
			if (WPEnum.ActorMoveType.eMoveNone == this._moveType)
			{
				// none 일땐 움직이지 않음.
				yield return new WaitForFixedUpdate();
			}
			else if (WPEnum.ActorMoveType.eMoveRoaming == this._moveType)
			{
				RoamingMoveFunc();
				yield return new WaitForFixedUpdate();
			}
			else
			{
				WPGameCommon._WPDebug("올바르지 않은 ActorMoveType! 확인해야한다! : " + this._moveType.ToString());
				yield return new WaitForFixedUpdate();
			}
		}
	}

	/// <summary>
	/// actor 타입별 움직임 함수. 상속받는 actor 스크립트에서 재정의해서 쓰자.
	/// </summary>
	protected virtual void RoamingMoveFunc() {}
}
