using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WPActorManager : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    public static WPActorManager instance = null;       // singleton

    public Transform _baseObject_Worker;                // baseobject. 인스펙터에서 초기화
    public Transform _baseObject_Field;                  // baseobject. 인스펙터에서 초기화

    [SerializeField] private GameObject _pfNullWorker;	// Null 워커 프리팹
    [SerializeField] private GameObject _pfWorker;      // 워커 프립팹
    [SerializeField] private GameObject _field;         // 밭 프리팹

    private int _workerCount;                           // 일꾼 개수. init 초기화
    private int _farmFieldCount;                        // 밭 개수. init 초기화

    private List<GameObject> _actorList_Worker;         // 액터Worker 게임오브젝트를 들고있는 리스트.
    private List<GameObject> _actorList_Field;          // 액터Field 게임오브젝트를 들고있는 리스트.

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

        // 밭 세팅 파트// 밭이 제일 먼저 세팅돼야 됩니다.
        for (int i = 0; i < this._farmFieldCount; i++)
        {
            this.SpawnActor((int)WPEnum.ActorKey.eActorFarmField);
        }

        // 일꾼 세팅 파트
        for (int workerIdx = 0; workerIdx < this._workerCount; workerIdx++)
        {
            this.SpawnActor((int)WPEnum.ActorKey.eActorWorker);
        }


    }

    public void SpawnNullWorker()
    {
        IncreaseNullWorker();
    }

    /// <summary>
    /// Actor 타입에 맞게 스폰시키는 함수.
    /// 성공적으로 스폰되었다면 eTypeSuccess 을 반환.
    /// </summary>
    /// <param name="actorKey"></param>
    /// <returns></returns>
    public int SpawnActor(int actorKey)
    {
        if ((int)WPEnum.ActorKey.eActorNullWorker == actorKey)
        {
            GameObject go = Instantiate(this._pfNullWorker, this._baseObject_Worker) as GameObject;

            if (null == go)
            {
                WPGameCommon._WPDebug("Actor_Worker 스폰에 문제발생!!");
                return (int)WPEnum.rvType.eTypeFail;
            }

            /*
			float xPos = UnityEngine.Random.Range(-WPVariable.currentFieldSizeX / 2f, WPVariable.currentFieldSizeX / 2f);
			float yPos = UnityEngine.Random.Range(-WPVariable.currentFieldSizeY / 2f, WPVariable.currentFieldSizeY / 2f);
            */

            float xPos = 10000;
            float yPos = 10000;

            // 포지션 세팅, 액터키 세팅
            go.GetComponent<WPActor>().SetActorPos(xPos, yPos);
            go.GetComponent<WPActor>().SetActorKey(actorKey);

            // 리스트에 반영
            _actorList_Worker.Add(go);

            return (int)WPEnum.rvType.eTypeSuccess;
        }
        else if ((int)WPEnum.ActorKey.eActorFarmField == actorKey)
        {
            GameObject go = Instantiate(this._field, this._baseObject_Field) as GameObject;

            if (null == go || farmIndex > 3 || fieldIndex > 5)
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

            if (fieldIndex > 5)
            {
                fieldIndex = 0;
                WPGameCommon._WPDebug(farmIndex.ToString() + "번째 농장 스폰 완료!!");
            }

            // 리스트에 반영
            _actorList_Worker.Add(go);

            return (int)WPEnum.rvType.eTypeSuccess;
        }
        else if ((int)WPEnum.ActorKey.eActorWorker == actorKey)
        {
            GameObject go = Instantiate(this._pfWorker, this._baseObject_Worker) as GameObject;

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
    /// Null일꾼 증가시키기. 
    /// </summary>
    public void IncreaseNullWorker()
    {
        //Null Worker가 이미 존재하나? 그러면 더 Spawn안함.
        if (GameObject.FindGameObjectWithTag("NullWorker") != null)
        {
            WPGameCommon._WPDebug("이미 널 워커가 존재합니다!");
            return;
        }
        // 임시워커 스폰 시도
        int rv = this.SpawnActor((int)WPEnum.ActorKey.eActorNullWorker);

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
	/// </summary>
	public void IncreaseWorker()
    {
        // 임시워커 스폰 시도
        int rv = this.SpawnActor((int)WPEnum.ActorKey.eActorWorker);

        // 스폰에 문제가 있나?
        if (0 != rv)
        {
            return;
        }
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

