using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPActorManager : MonoBehaviour
{
	/////////////////////////////////////////////////////////////////////////
	// Varaibles
	public static WPActorManager instance = null;		// singleton

	public Transform _baseObject;						// baseobject. 인스펙터에서 초기화

	public GameObject _pfTempWorker;					// 임시 워커 프리팹

    private int _workerCount;                           // 일꾼 개수. init 초기화

    private int _farmFieldCount;                        // 밭 개수. init 초기화

	private List<GameObject> _actorList;				// 액터 게임오브젝트를 들고있는 리스트.

    /////////////////////////////////////////////////////////////////////////
    // Methods
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        this.InitValue();

        this.UpdateValue();
    }

    /// <summary>
    /// 초기값 불러오기.
    /// </summary>
    private void InitValue()
    {
		this._actorList = new List<GameObject>();

        this._workerCount = WPGameVariableManager.instance.LoadIntVariable(WPEnum.VariableType.eUserWorkerCount);
        this._farmFieldCount = WPGameVariableManager.instance.LoadIntVariable(WPEnum.VariableType.eFarmFieldCount);

    }

    /// <summary>
    /// 현재 세팅된 변수들로 상태 업데이트.
    /// </summary>
    private void UpdateValue()
	{
		WPGameCommon._WPDebug("!! Actor 전체스폰 시작 !!");

		// 일꾼 세팅 파트
		for (int workerIdx = 0; workerIdx < this._workerCount; workerIdx++)
        {
			this.SpawnActor((int)WPEnum.ActorKey.eActorWorkerTemp);
        }
		/*
		// 밭 세팅 파트, 최대 밭 몇개??
		for (int i = 0; i < this._farmFieldCount; i++)
        {
			this.SpawnActor((int)WPEnum.ActorKey.eActorFarmField);
        }
		*/
	}

	/// <summary>
	/// Actor 타입에 맞게 스폰시키는 함수.
	/// 성공적으로 스폰되었다면 eTypeSuccess 을 반환.
	/// </summary>
	/// <param name="actorKey"></param>
	/// <returns></returns>
	public int SpawnActor(int actorKey)
	{
		if ((int)WPEnum.ActorKey.eActorWorkerTemp == actorKey)
		{
			// 현재는 actorkey 상관없이 그냥 생성시키고 배치만 해줌.
			GameObject go = Instantiate(this._pfTempWorker, this._baseObject);

			if (null == go)
			{
				WPGameCommon._WPDebug("Actor 스폰에 문제발생!!");
				return (int)WPEnum.rvType.eTypeFail;
			}

			float xPos = Random.Range(-WPVariable.currentFieldSizeX / 2f, WPVariable.currentFieldSizeX / 2f);
			float yPos = Random.Range(-WPVariable.currentFieldSizeY / 2f, WPVariable.currentFieldSizeY / 2f);

			// 포지션 세팅, 액터키 세팅
			go.GetComponent<WPActor>().SetActorPos(xPos, yPos);
			go.GetComponent<WPActor>().SetActorKey(actorKey);

			// 리스트에 반영
			this._actorList.Add(go);

			return (int)WPEnum.rvType.eTypeSuccess;
		}
		else if((int)WPEnum.ActorKey.eActorFarmField == actorKey)
		{
			//여기에 밭 스폰 짜야됨 grid? UI어떻게

			return (int)WPEnum.rvType.eTypeSuccess;
		}
		else
		{
			return (int)WPEnum.rvType.eTypeFail;
		}
	}

	/// <summary>
	/// 밭 증가시키기. 임시함수. 아직 안만듬.
	/// </summary>
	public void IncreaseField()
	{
		// 임시워커 스폰 시도
		int rv = this.SpawnActor((int)WPEnum.ActorKey.eActorFarmField);

		// 스폰에 문제가 있나?
		if (0 != rv)
		{
			return;
		}

		// 매니저 메모리에 반영 시도
		this._farmFieldCount++;

		// 유저데이터에 작성.
		WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eFarmFieldCount, this._farmFieldCount);
	}

	/// <summary>
	/// 일꾼 증가시키기. 임시함수
	/// 적당히 참고만 하고 이 함수는 지우자.
	/// </summary>
	public void IncreaseWorker()
    {
		// 임시워커 스폰 시도
		int rv = this.SpawnActor((int)WPEnum.ActorKey.eActorWorkerTemp);

		// 스폰에 문제가 있나?
		if (0 != rv)
		{
			return;
		}

		// 매니저 메모리에 반영 시도
		this._workerCount++;

		// 유저데이터에 작성.
		WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eUserWorkerCount, this._workerCount);
    }

	/// <summary>
	/// 일꾼 모두 죽이기. 임시함수
	/// 적당히 참고만 하고 이 함수는 지우자.
	/// </summary>
	public void KillAllWorker()
	{
		// 걍 모든 리스트를 돌면서 지운다.
		for (int idx = 0; idx < this._actorList.Count; idx++)
		{
			DestroyObject(_actorList[idx]);
		}

		// 메모리에 반영 시도.
		this._workerCount = 0;

		// 유저데이터에 작성.
		WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eUserWorkerCount, this._workerCount);
	}
}
