using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 지호화이팅
public class WPWorkerCtrl : WPActor
{
    /////////////////////////////////////////////////////////////////////////
    // Varaibles
    // 관리 중인 밭
    WPFieldCtrl workingField;
    WPWorker wpWorker;

    //Sprite
    SpriteRenderer hair;
    SpriteRenderer basedBody;
    SpriteRenderer shirts;
    SpriteRenderer pants;
    SpriteRenderer shoes;
	TextMesh worker_name;


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
    /// 

    protected override void InitValue()
    {
        base.InitValue();

        //정보가 없는 워커( NullWorker )의 무빙 타입은 None
        if (gameObject.tag == "NullWorker")
        {
            base.SetActorMoveType(WPEnum.ActorMoveType.eMoveNone);
        }
        else // 워커인 경우 무빙타입은 로밍.
        {
            base.SetActorMoveType(WPEnum.ActorMoveType.eMoveRoaming);
        }

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
            wpWorker.appearance = new Dictionary<WPEnum.WorkerAppearanceDetail, int>();

            wpWorker.appearance.Add(WPEnum.WorkerAppearanceDetail.eWorkerName, 0);
            wpWorker.appearance.Add(WPEnum.WorkerAppearanceDetail.eBasedBody, 0);
            wpWorker.appearance.Add(WPEnum.WorkerAppearanceDetail.eHair, 0);
            wpWorker.appearance.Add(WPEnum.WorkerAppearanceDetail.eHairColor, 0);
            wpWorker.appearance.Add(WPEnum.WorkerAppearanceDetail.eShirt, 0);
            wpWorker.appearance.Add(WPEnum.WorkerAppearanceDetail.ePants, 0);
            wpWorker.appearance.Add(WPEnum.WorkerAppearanceDetail.eShoes, 0);

        }


        GetFieldData();



		//setImage
		getWorkerComponents();

		SetImage(wpWorker.appearance);

    }

	//GetComponents
	void getWorkerComponents()
	{
		hair = transform.GetChild(0).GetComponent<SpriteRenderer>();
		basedBody = transform.GetChild(1).GetComponent<SpriteRenderer>();
		shirts = transform.GetChild(2).GetComponent<SpriteRenderer>();
		pants = transform.GetChild(3).GetComponent<SpriteRenderer>();
		shoes = transform.GetChild(4).GetComponent<SpriteRenderer>();
		worker_name = transform.GetChild(5).GetComponent<TextMesh>();
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
            GetFieldData();
            return;
        }

        this.transform.Translate(this._currentDir * this.movSpeed * Time.deltaTime);

        if (this._currentLimit < _moveTimeAcc)
        {
            //병충해에 걸린 경우 ActorState를 병충해추적 상태로 바꿈.
            if (workingField.IsSick)
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
            }
            else if (WPEnum.ActorState.eActorTrkingSickness == base._actorState)
            {
                // 병충해 제거 하고있을 땐 idle 로 바꾸자.
                base._actorState = WPEnum.ActorState.eActorStateIdle;

                this._currentLimit = 1f;

                //관리하는 밭이 병충해에 걸린 경우 target을 재설정
                if (workingField.IsSick)
                {
                    if (this._currentDir.Equals(Vector3.zero))
                    {
                        //Idle상태에서 시작한 경우
                        _currentDir = workingField.GetComponent<Transform>().position - transform.position;
                    }
                    else
                    {
                        //move상태에서 시작한 경우
                        _currentDir = workingField.GetComponent<Transform>().position - transform.position - _currentDir;
                    }
                    _currentDir = Vector3.Normalize(_currentDir);
                }
            }

            this._moveTimeAcc = 0;
        }
    }

    //WPWorker를 이용해 현재 일하고 있는 field가져옴.
    private void GetFieldData()
    {
        if ((this.workingField != null) || GameObject.Find("Field" + wpWorker.workingFarmIndex.ToString() + wpWorker.workingFieldIndex.ToString()) == null) return;
        this.workingField = GameObject.Find("Field" + wpWorker.workingFarmIndex.ToString() + wpWorker.workingFieldIndex.ToString()).GetComponent<WPFieldCtrl>();
    }

    //Dictionary로 일꾼 이미지 바꾸기
    private void SetImage(Dictionary<WPEnum.WorkerAppearanceDetail, int> _appearance)
    {

        if (wpWorker != null)
        {

            hair.sprite = WPCustomizationManager.instance.hairPrefabs[_appearance[WPEnum.WorkerAppearanceDetail.eHair]].GetComponent<SpriteRenderer>().sprite;
            basedBody.sprite = WPCustomizationManager.instance.basedBodyPrefabs[_appearance[WPEnum.WorkerAppearanceDetail.eBasedBody]].GetComponent<SpriteRenderer>().sprite;
            shirts.sprite = WPCustomizationManager.instance.shirtPrefabs[_appearance[WPEnum.WorkerAppearanceDetail.eShirt]].GetComponent<SpriteRenderer>().sprite;
            pants.sprite = WPCustomizationManager.instance.pantsPrefabs[_appearance[WPEnum.WorkerAppearanceDetail.ePants]].GetComponent<SpriteRenderer>().sprite;
            shoes.sprite = WPCustomizationManager.instance.shoesPrefabs[_appearance[WPEnum.WorkerAppearanceDetail.eShoes]].GetComponent<SpriteRenderer>().sprite;
			worker_name.text = WPCustomizationManager.instance.worker_Names[_appearance[WPEnum.WorkerAppearanceDetail.eWorkerName]];

        }
        else
        {
            WPGameCommon._WPDebug("Worker Data 불러오기 실패");
        }
    }

    //Worker를 가져옴
    public void SetWorker(WPWorker _worker)
    {
        this.wpWorker = _worker;
        SetImage(wpWorker.appearance);
        WPGameCommon._WPDebug("Worker SET!!");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Field"))
        {
            if (col.GetComponent<WPFieldCtrl>().WpField != null && col.GetComponent<WPFieldCtrl>().WpField.seedIndex != -1)
            {
                col.GetComponent<WPFieldCtrl>().IsSick = false;
            }
        }
    }

}
