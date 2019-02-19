using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WPActorManager : MonoBehaviour
{
	/////////////////////////////////////////////////////////////////////////
	// Varaibles
	public static WPActorManager instance = null;		// singleton

	public Transform _baseObject;						// baseobject. 인스펙터에서 초기화
	public Transform _baseObject_Farm;                  // baseobject. 인스펙터에서 초기화
	public Transform _baseObject_Sickness;				// baseobject, 인스펙터에서 초기화

	public GameObject _pfTempWorker;					// 임시 워커 프리팹
	public GameObject _field;                           // 밭 프리팹
	public GameObject _sickness;						// 병충해 프리팹

    private int _workerCount;                           // 일꾼 개수. init 초기화
    private int _farmFieldCount;                     // 밭 개수. init 초기화
	private int _sicknessCount;

	private List<GameObject> _actorList_Worker;			// 액터Worker 게임오브젝트를 들고있는 리스트.
	private List<GameObject> _actorList_Field;          // 액터Field 게임오브젝트를 들고있는 리스트.
	private List<GameObject> _actorList_Sickness;          // 액터Field 게임오브젝트를 들고있는 리스트.

	private int fieldIndex;
    private int farmIndex;

    IEnumerator enumerator;
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
        enumerator = GetEnumerator();
        farmIndex = 0;
        fieldIndex = 0;

        this._actorList_Worker = new List<GameObject>();
        this._actorList_Field = new List<GameObject>();

        this._workerCount = WPGameVariableManager.instance.LoadIntVariable(WPEnum.VariableType.eUserWorkerCount);
        //this._farmFieldCount = WPGameVariableManager.instance.LoadIntVariable(WPEnum.VariableType.eFarmFieldCount);
        this._farmFieldCount = 6;
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
		
		// 밭 세팅 파트
		for (int i = 0; i < this._farmFieldCount; i++)
        {
			this.SpawnActor((int)WPEnum.ActorKey.eActorFarmField);
        }
	
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
				WPGameCommon._WPDebug("Actor_Worker 스폰에 문제발생!!");
				return (int)WPEnum.rvType.eTypeFail;
			}

			float xPos = UnityEngine.Random.Range(-WPVariable.currentFieldSizeX / 2f, WPVariable.currentFieldSizeX / 2f);
			float yPos = UnityEngine.Random.Range(-WPVariable.currentFieldSizeY / 2f, WPVariable.currentFieldSizeY / 2f);

			// 포지션 세팅, 액터키 세팅
			go.GetComponent<WPActor>().SetActorPos(xPos, yPos);
			go.GetComponent<WPActor>().SetActorKey(actorKey);

			// 리스트에 반영
			this._actorList_Worker.Add(go);

			return (int)WPEnum.rvType.eTypeSuccess;
		}
		else if((int)WPEnum.ActorKey.eActorFarmField == actorKey)
		{
			// 현재는 actorkey 상관없이 그냥 생성시키고 배치만 해줌.
			GameObject go = Instantiate(this._field, this._baseObject_Farm);

			if (null == go || farmIndex >3 || fieldIndex>5)
			{
				WPGameCommon._WPDebug("Actor_Worker 스폰에 문제발생!!");
				return (int)WPEnum.rvType.eTypeFail;
			}

			float xPos = WPField.FieldPos(fieldIndex).x;
			float yPos = WPField.FieldPos(fieldIndex).y;

			Debug.Log(farmIndex.ToString()+"번째 Farm위치"+ WPField.FieldPos(farmIndex));
		
			// 포지션 세팅, 액터키 세팅
			go.GetComponent<WPActor>().SetActorPos(xPos, yPos);
			go.GetComponent<WPActor>().SetActorKey(actorKey);

			go.name = "Field" + farmIndex.ToString() + fieldIndex.ToString();
			fieldIndex++;

			// 리스트에 반영
			this._actorList_Worker.Add(go);
			
			return (int)WPEnum.rvType.eTypeSuccess;
		}
		else if((int)WPEnum.ActorKey.eActorSickness == actorKey)
		{
			// 병충해를 확률적으로 Respawn함
			GameObject go = Instantiate(this._sickness, this._baseObject_Sickness);

			if (null == go)
			{
				WPGameCommon._WPDebug("Actor_Sickness 스폰에 문제발생!!");
				return (int)WPEnum.rvType.eTypeFail;
			}

			float xPos = UnityEngine.Random.Range(-WPVariable.currentFieldSizeX / 2f, WPVariable.currentFieldSizeX / 2f);
			float yPos = UnityEngine.Random.Range(-WPVariable.currentFieldSizeY / 2f, WPVariable.currentFieldSizeY / 2f);

			// 포지션 세팅, 액터키 세팅
			go.GetComponent<WPActor>().SetActorPos(xPos, yPos);
			go.GetComponent<WPActor>().SetActorKey(actorKey);

			// 리스트에 반영
			this._actorList_Worker.Add(go);
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
		this._farmFieldCount ++;

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
		for (int idx = 0; idx < this._actorList_Worker.Count; idx++)
		{
			DestroyObject(_actorList_Worker[idx]);
		}

		// 메모리에 반영 시도.
		this._workerCount = 0;

		// 유저데이터에 작성.
		WPGameVariableManager.instance.SaveVariable(WPEnum.VariableType.eUserWorkerCount, this._workerCount);
	}

   
}

