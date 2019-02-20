using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //저장할 정보는 Experience, customizing 정보(옷 얼굴 상체 등의 인덱스)
    //WorkerIndex는 정보를 담고 외형은 커스터마이징 시스템으로 결정
	
	//아직 ToData랑 parsing하는 것 테스트 안해봄!

public class WPWorker {
    //private static List<Dictionary<string, object>> seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.eWorker);
	public int workingFarmIndex { get; private set; }										
	public int workingFieldIndex { get; private set; }										
    public int workerIndex { get; private set; }											//WorkerData 정보
    public float requiredExperience { get; private set; }									//필요한 경험치
	public Dictionary<WPEnum.WorkerAppearanceDetail, int> appearance { get; set; }			//외형 정보

	//WorkerIndex로 해당하는 Worker 데이터 불러오기, 
	public WPWorker(int _workingFarmIndex,int _workingFieldIndex, int _workerIndex, float _requiredExp, Dictionary<WPEnum.WorkerAppearanceDetail,int> _appearance)
    {
		this.workingFarmIndex = _workingFarmIndex;
		this.workingFieldIndex = _workingFieldIndex;
        this.workerIndex = _workerIndex;
        this.requiredExperience = _requiredExp;
		this.appearance = _appearance;
    }

	public WPWorker()
	{
		this.workingFarmIndex = -1;
		this.workingFieldIndex = -1;
		this.workerIndex = -1;
		this.requiredExperience = -1;
		this.appearance = null;
	}

	public string ToData()
	{
		return string.Format("WPWorker({0}:{1}:{2}:{3}:{4})", workingFarmIndex, workingFieldIndex, workerIndex,requiredExperience, Dic2data());
	}

	public string Dic2data()
	{																			
		return string.Format("Appearance({0}:{1}:{2}:{3}:{4}:{5}:{6})", 
			appearance[WPEnum.WorkerAppearanceDetail.eWorkerName], 
			appearance[WPEnum.WorkerAppearanceDetail.eBasedBody],
			appearance[WPEnum.WorkerAppearanceDetail.eHair],
			appearance[WPEnum.WorkerAppearanceDetail.eHairColor],
			appearance[WPEnum.WorkerAppearanceDetail.eShirt],
			appearance[WPEnum.WorkerAppearanceDetail.ePants],
			appearance[WPEnum.WorkerAppearanceDetail.eShoes]
			);
	}

	public static Dictionary<WPEnum.WorkerAppearanceDetail, int> Data2Dic(string data)
	{
		// split String
		string[] data_1 = data.Split('(');
		// simple integrity check
		if (data_1[0] != "Appearance") return new Dictionary<WPEnum.WorkerAppearanceDetail, int>();

		string[] dateString = data_1[1].Replace(")", "").Split(':');

		// convert string to int32
		int worker_Names_Id = System.Convert.ToInt32(dateString[0]);
		int basedBody_Id = System.Convert.ToInt32(dateString[1]);
		int hair_Id = System.Convert.ToInt32(dateString[2]);
		int hairColor_Id = System.Convert.ToInt32(dateString[3]);
		int shirt_Id = System.Convert.ToInt32(dateString[4]);
		int pants_Id = System.Convert.ToInt32(dateString[5]);
		int shoes_Id = System.Convert.ToInt32(dateString[6]);

		Dictionary<WPEnum.WorkerAppearanceDetail, int> tempAppearance = new Dictionary<WPEnum.WorkerAppearanceDetail, int>();

		tempAppearance.Add(WPEnum.WorkerAppearanceDetail.eWorkerName, worker_Names_Id);
		tempAppearance.Add(WPEnum.WorkerAppearanceDetail.eBasedBody, basedBody_Id);
		tempAppearance.Add(WPEnum.WorkerAppearanceDetail.eHair, hair_Id);
		tempAppearance.Add(WPEnum.WorkerAppearanceDetail.eHairColor, hairColor_Id);
		tempAppearance.Add(WPEnum.WorkerAppearanceDetail.eShirt, shirt_Id);
		tempAppearance.Add(WPEnum.WorkerAppearanceDetail.ePants, pants_Id);
		tempAppearance.Add(WPEnum.WorkerAppearanceDetail.eShoes, shoes_Id);
		
		return tempAppearance;
	}

	public static WPWorker ParseData(string data)
	{
		// split String
		string[] data_1 = data.Split("(".ToCharArray(), 2);
		// simple integrity check
		if (data_1[0] != "WPWorker") return new WPWorker();

		string[] dataString = data_1[1].Substring(0, data_1[1].Length).Split(":".ToCharArray(), 5);

		int workingFarmIndex = System.Convert.ToInt32(dataString[0]);
		int workingFieldIndex = System.Convert.ToInt32(dataString[1]);
		int workerIndex = System.Convert.ToInt32(dataString[2]);
		float requiredExperience = (float)System.Convert.ToDouble(dataString[3]);

		Dictionary<WPEnum.WorkerAppearanceDetail, int> _appearance = WPWorker.Data2Dic(dataString[4]);

		return new WPWorker(workingFarmIndex, workingFieldIndex, workerIndex, requiredExperience, _appearance);
	}
}
