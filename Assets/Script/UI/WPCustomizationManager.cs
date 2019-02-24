using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//hair Color도 바꿈? - 지호한테 묻기
//User Data에 어떻게 넣징

public class WPCustomizationManager : WPUIManager
{
	/////////////////////////////////////////////////////////////////////////
	// Varaibles
	public static WPCustomizationManager instance = null;     // singleton

	//이 정보를 저장할 Worker
	public WPWorker worker;
	WPFieldCtrl targetFieldCtrl;
	
	//Prefabs
	[SerializeField]
	public GameObject[] hairPrefabs;
	[SerializeField]
	public GameObject[] basedBodyPrefabs;
	[SerializeField]
	public GameObject[] shirtPrefabs;
	[SerializeField]
	public GameObject[] pantsPrefabs;
	[SerializeField]
	public GameObject[] shoesPrefabs;

	//Hair Colors
	[SerializeField]
	private Color32[] hairColors;

	//Random Worker Name
	[SerializeField] private string[] worker_Names;

	//Anchors
	[SerializeField]
	private Transform hairAchor;
	[SerializeField]
	private Transform basedBodyAchor;
	[SerializeField]
	private Transform shirtsAchor;
	[SerializeField]
	private Transform pantsAchor;
	[SerializeField]
	private Transform shoesAchor;

	GameObject currentHair;
	GameObject currentBasedBody;
	GameObject currentShirt;
	GameObject currentPants;
	GameObject currentShoes;
	string currentWorkerName;

	private Dictionary<WPEnum.WorkerAppearanceDetail, int> selectedAppearance;

	int worker_Names_Id ;
	int basedBody_Id ;
	int hair_Id ;
	int shirt_Id ;
	int pants_Id ;
	int hairColor_Id ;
	int shoes_Id ;

	/////////////////////////////////////////////////////////////////////////
	// Methods
	protected override void Init()
	{
		instance = this;

		Randomize();

    }

    public override void SetInvisible(bool param)
    {
        if (param)
        {
            gameObject.GetComponent<RectTransform>().localPosition = new Vector3(10000, 10000);
        }
        else
        {
            gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 377);
        }
    }

    void Start()
    {
        SetInvisible(true);
        
    }

    //for Debug
    void Update()
    {

        if (worker != null && worker.appearance != null)
        {
            Debug.Log("worker Data"+hairPrefabs[worker.appearance[WPEnum.WorkerAppearanceDetail.eHair]].ToString());
            Debug.Log("UI Data"+hairPrefabs[selectedAppearance[WPEnum.WorkerAppearanceDetail.eHair]].ToString());
        }
    }

	// Close 버튼을 클릭했을 때 호출합니다.
	public void OnClick_Close()
	{
		WPAnchorCtrl.instance.SetActive(false);
		base.SetInvisible(true);
		WPUIManager_Field.instance.SetInvisible(false);


	}

	

	// Save 버튼을 클릭했을 때 호출합니다.
	public void OnClick_Save()
	{
		SaveWorker2Dictionary();
	}


	/// <summary>
	/// Customizing 하는 부분/////////////////////////////////////////////////
	/// </summary>
	//이름 설정
	public void OnClick_Name_NextBtn()
	{
		if (worker_Names_Id < worker_Names.Length - 1)
		{
			worker_Names_Id++;
		}
		else
		{
			worker_Names_Id = 0;
		}
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eWorkerName, worker_Names_Id);
	}
	public void OnClick_Name_BackBtn()
	{
		if (worker_Names_Id > 0)
		{
			worker_Names_Id--;
		}
		else
		{
			worker_Names_Id = worker_Names.Length - 1;
		}
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eWorkerName, worker_Names_Id);
	}

	//몸체 설정
	public void OnClick_BasedBody_NextBtn()
	{
		if (basedBody_Id < basedBodyPrefabs.Length - 1)
		{
			basedBody_Id++;
		}
		else
		{
			basedBody_Id = 0;
		}
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eBasedBody, basedBody_Id);
	}
	public void OnClick_BasedBody_BackBtn()
	{
		if (basedBody_Id > 0)
		{
			basedBody_Id--;
		}
		else
		{
			basedBody_Id = basedBodyPrefabs.Length - 1;
		}
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eBasedBody, basedBody_Id);
	}

	//머리 설정
	public void OnClick_Hair_NextBtn()
	{
		if (hair_Id < hairPrefabs.Length - 1)
		{
			hair_Id++;
		}
		else
		{
			hair_Id = 0;
		}
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eHair, hair_Id);
	}
	public void OnClick_Hair_BackBtn()
	{
		if (hair_Id > 0)
		{
			hair_Id--;
		}
		else
		{
			hair_Id = hairPrefabs.Length - 1;
		}
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eHair, hair_Id);
	}

	//상의 설정
	public void OnClick_Shirt_NextBtn()
	{
		if (shirt_Id < shirtPrefabs.Length - 1)
		{
			shirt_Id++;
		}
		else
		{
			shirt_Id = 0;
		}
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eShirt, shirt_Id);
	}
	public void OnClick_Shirt_BackBtn()
	{
		if (shirt_Id > 0)
		{
			shirt_Id--;
		}
		else
		{
			shirt_Id = shirtPrefabs.Length - 1;
		}
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eShirt, shirt_Id);
	}

	//하의 설정
	public void OnClick_Pants_NextBtn()
	{
		if (pants_Id < pantsPrefabs.Length - 1)
		{
			pants_Id++;
		}
		else
		{
			pants_Id = 0;
		}
		ApplyCostume(WPEnum.WorkerAppearanceDetail.ePants, pants_Id);
	}
	public void OnClick_Pants_BackBtn()
	{
		if (pants_Id > 0)
		{
			pants_Id--;
		}
		else
		{
			pants_Id = pantsPrefabs.Length - 1;
		}
		ApplyCostume(WPEnum.WorkerAppearanceDetail.ePants, pants_Id);
	}

	//머리색 설정
	public void OnClick_HairColor_NextBtn()
	{
		if (hairColor_Id < hairColors.Length - 1)
		{
			hairColor_Id++;
		}
		else
		{
			hairColor_Id = 0;
		}
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eHairColor, hairColor_Id);
	}
	public void OnClick_HairColor_BackBtn()
	{
		if (hairColor_Id > 0)
		{
			hairColor_Id--;
		}
		else
		{
			hairColor_Id = hairColors.Length - 1;
		}
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eHairColor, hairColor_Id);
	}

	//신발 설정
	public void OnClick_Shoes_NextBtn()
	{
		if (shoes_Id < shoesPrefabs.Length - 1)
		{
			shoes_Id++;
		}
		else
		{
			shoes_Id = 0;
		}
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eShoes, shoes_Id);
	}
	public void OnClick_Shoes_BackBtn()
	{
		if (shoes_Id > 0)
		{
			shoes_Id--;
		}
		else
		{
			shoes_Id = shoesPrefabs.Length - 1;
		}
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eShoes, shoes_Id);
	}

	//Id에 해당하는 Custom울 적용합니당
	void ApplyCostume(WPEnum.WorkerAppearanceDetail _detail, int id)
	{
		switch (_detail)
		{
			case WPEnum.WorkerAppearanceDetail.eWorkerName:
				//이름 설정하는 코드 짜기
				currentWorkerName = worker_Names[id];
				break;

			case WPEnum.WorkerAppearanceDetail.eBasedBody:
				if(currentBasedBody != null)
				{
					//WPGameCommon._WPDebug("BasedBody 이미 set됨");
					Destroy(currentBasedBody);
				}
				currentBasedBody = Instantiate(basedBodyPrefabs[id]);
				currentBasedBody.transform.SetParent(basedBodyAchor);
				ResetTransform(currentBasedBody.transform);
				break;

			case WPEnum.WorkerAppearanceDetail.eHair:
				if (currentHair != null)
				{
					//WPGameCommon._WPDebug("Hair 이미 set됨");
					Destroy(currentHair);
				}
				currentHair = Instantiate(hairPrefabs[id]);
				currentHair.transform.SetParent(hairAchor);
				ResetTransform(currentHair.transform);
				break;

			case WPEnum.WorkerAppearanceDetail.eShirt:
				if (currentShirt!= null)
				{
					//WPGameCommon._WPDebug("Shirt 이미 set됨");
					Destroy(currentShirt);
				}
				currentShirt = Instantiate(shirtPrefabs[id]);
				currentShirt.transform.SetParent(shirtsAchor);
				ResetTransform(currentShirt.transform);

				break;

			case WPEnum.WorkerAppearanceDetail.ePants:
				if (currentPants != null)
				{
					//WPGameCommon._WPDebug("Pants 이미 set됨");
					Destroy(currentPants);
				}
				currentPants = Instantiate(pantsPrefabs[id]);
				currentPants.transform.SetParent(pantsAchor);
				ResetTransform(currentPants.transform);
				break;
			case WPEnum.WorkerAppearanceDetail.eHairColor:
				if (currentHair!=null && currentHair.GetComponent<MeshRenderer>() !=null)
				{
					currentHair.GetComponent<MeshRenderer>().material.color = hairColors[id];
				}
				break;
			case WPEnum.WorkerAppearanceDetail.eShoes:
				if (currentShoes!=null)
				{
					//WPGameCommon._WPDebug("Shoes 이미 set됨");
					Destroy(currentShoes);
				}
				currentShoes = Instantiate(shoesPrefabs[id]);
				currentShoes.transform.SetParent(shoesAchor);
				ResetTransform(currentShoes.transform);
				break;

			default:
				break;
		}
	}

	//Worker 찾아오는 코드 짜야돼
	public void setWorkerOnCustomManager(WPWorker _worker)
	{
		worker = _worker;

		//이미지 보이게 하기
		if (worker != null)
		{
			WPAnchorCtrl.instance.SetActive(true);
		}
	}

	private void SaveWorker2Dictionary()
	{
		selectedAppearance = new Dictionary<WPEnum.WorkerAppearanceDetail, int>();

		selectedAppearance.Add(WPEnum.WorkerAppearanceDetail.eWorkerName, worker_Names_Id);
		selectedAppearance.Add(WPEnum.WorkerAppearanceDetail.eBasedBody, basedBody_Id);
		selectedAppearance.Add(WPEnum.WorkerAppearanceDetail.eHair, hair_Id);
		selectedAppearance.Add(WPEnum.WorkerAppearanceDetail.eHairColor, hairColor_Id);
		selectedAppearance.Add(WPEnum.WorkerAppearanceDetail.eShirt, shirt_Id);
		selectedAppearance.Add(WPEnum.WorkerAppearanceDetail.ePants, pants_Id);
		selectedAppearance.Add(WPEnum.WorkerAppearanceDetail.eShoes, shoes_Id);

		//Worker에 저장하는 코드
		if (worker != null)
		{
			WPGameCommon._WPDebug("커스터 마이징 저장 완료!!"+hairPrefabs[selectedAppearance[WPEnum.WorkerAppearanceDetail.eHair]].ToString());
			worker.appearance = selectedAppearance;
		}else
		{
			WPGameCommon._WPDebug("현재 가진 worker instance없음!! ");
		}

	}

	public void Randomize()
	{
        this.worker_Names_Id = Random.Range(0, worker_Names.Length);
        this.basedBody_Id = Random.Range(0, basedBodyPrefabs.Length);
        this.hair_Id = Random.Range(0, hairPrefabs.Length);
        this.hairColor_Id = Random.Range(0, hairColors.Length);
        this.shirt_Id = Random.Range(0, shirtPrefabs.Length);
        this.pants_Id = Random.Range(0, pantsPrefabs.Length);
        this.shoes_Id = Random.Range(0, shoesPrefabs.Length);

        ApplyCostume(WPEnum.WorkerAppearanceDetail.eWorkerName, worker_Names_Id);
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eBasedBody, basedBody_Id);
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eHair, hair_Id);
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eHairColor, hairColor_Id);
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eShirt, shirt_Id);
		ApplyCostume(WPEnum.WorkerAppearanceDetail.ePants, pants_Id);
		ApplyCostume(WPEnum.WorkerAppearanceDetail.eShoes, shoes_Id);
	}

	private void ResetTransform(Transform transform)
	{
		transform.localPosition = Vector3.zero;
		transform.localEulerAngles = Vector3.zero;
		transform.localScale = Vector3.one;
	}
}
