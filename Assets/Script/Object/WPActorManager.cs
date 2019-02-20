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
    private int _farmFieldCount;						// 밭 개수. init 초기화
	private int _sicknessCount;

	private List<GameObject> _actorList_Worker;			// 액터Worker 게임오브젝트를 들고있는 리스트.
	private List<GameObject> _actorList_Field;          // 액터Field 게임오브젝트를 들고있는 리스트.
	private List<GameObject> _actorList_Sickness;		// 액터Sickness 게임오브젝트를 들고있는 리스트.

	private int sicknessIndex;
	private int fieldIndex;
    public static int farmIndex;

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
		//농장 값을 불러 오도록 수정해야됨
        farmIndex = 0;

		//0으로 초기화
        fieldIndex = 0;
		sicknessIndex = 0;

		this._actorList_Worker = new List<GameObject>();
        this._actorList_Field = new List<GameObject>();
		this._actorList_Sickness = new List<GameObject>();

        this._workerCount = WPGameVariableManager.instance.LoadIntVariable(WPEnum.VariableType.eUserWorkerCount);
        //this._farmFieldCount = WPGameVariableManager.instance.LoadIntVariable(WPEnum.VariableType.eFarmFieldCount);
        this._farmFieldCount = 6;
		this._sicknessCount = 6;
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

		// 병충해 세팅 파트
		for (int i = 0; i < this._sicknessCount; i++)
		{
			this.SpawnActor((int)WPEnum.ActorKey.eActorSickness);
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
			GameObject go = Instantiate(this._pfTempWorker, this._baseObject) as GameObject;

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
			_actorList_Worker.Add(go);

			return (int)WPEnum.rvType.eTypeSuccess;
		}
		else if((int)WPEnum.ActorKey.eActorFarmField == actorKey)
		{
			GameObject go = Instantiate(this._field, this._baseObject_Farm) as GameObject;

			if (null == go || farmIndex >3 || fieldIndex>5)
			{
				WPGameCommon._WPDebug("Actor_Worker 스폰에 문제발생!!");
				return (int)WPEnum.rvType.eTypeFail;
			}

			float xPos = WPField.FieldPos(fieldIndex).x;
			float yPos = WPField.FieldPos(fieldIndex).y;

			// 포지션 세팅, 액터키 세팅
			go.GetComponent<WPActor>().SetActorPos(xPos, yPos);
			go.GetComponent<WPActor>().SetActorKey(actorKey);

			go.name = "Field" + farmIndex.ToString() + fieldIndex.ToString();
			fieldIndex++;

			if(fieldIndex > 5)
			{
				fieldIndex = 0;
				WPGameCommon._WPDebug(farmIndex.ToString()+"번째 농장 스폰 완료!!");
			}

			// 리스트에 반영
			_actorList_Worker.Add(go);
			
			return (int)WPEnum.rvType.eTypeSuccess;
		}
		else if((int)WPEnum.ActorKey.eActorSickness == actorKey)
		{
			// 병충해를 모든 밭에 Respawn함
			GameObject go = Instantiate(this._sickness, this._baseObject_Sickness) as GameObject;
			go.name = "병충해"+sicknessIndex.ToString(); 

			if (null == go)
			{
				WPGameCommon._WPDebug("Actor_Sickness 스폰에 문제발생!!");
				return (int)WPEnum.rvType.eTypeFail;
			}

			float xPos = WPField.FieldPos(sicknessIndex).x;
			float yPos = WPField.FieldPos(sicknessIndex).y;

			sicknessIndex++;

			// 포지션 세팅, 액터키 세팅
			go.GetComponent<WPActor>().SetActorPos(xPos, yPos);
			go.GetComponent<WPActor>().SetActorKey(actorKey);
			go.transform.position += new Vector3(0,0,-1);			//밭에 터치 안하고 병충해 먼저 터치하도록 pos-1로 바꿈


			// 리스트에 반영
			_actorList_Sickness.Add(go);

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
	/// 일꾼 증가시키기. 임시함수
	/// 적당히 참고만 하고 이 함수는 지우자.
	/// </summary>
	public void IncreaseSickness()
	{
		// 임시워커 스폰 시도
		int rv = this.SpawnActor((int)WPEnum.ActorKey.eActorSickness);

		// 스폰에 문제가 있나?
		if (0 != rv)
		{
			return;
		}

		// 매니저 메모리에 반영 시도
		this._sicknessCount++;

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

	public GameObject GetSicknessByID(int id)
	{
		GameObject target = null;
		foreach (GameObject go in _actorList_Sickness)
		{
			if(go.name.Substring(3) == id.ToString())
			{
				target = go;
			}
		}


		return target;
	}
}

