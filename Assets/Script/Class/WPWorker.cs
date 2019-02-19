using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //저장할 정보는 Experience, customizing 정보(옷 얼굴 상체 등의 인덱스)
    //WorkerIndex는 정보를 담고 외형은 커스터마이징 시스템으로 결정
    //커스터마이징 시스템으로 할건지 결정해야됨

public class WPWorker {
    //private static List<Dictionary<string, object>> seedData = WPGameDataManager.instance.GetData(WPEnum.GameData.eWorker);
    public int workerIndex { get; private set; }
    public float requiredExperience { get; private set; }
	public Dictionary<WPEnum.WorkerAppearanceDetail, int> appearance { get; set; }

	//WorkerIndex로 해당하는 Worker 데이터 불러오기, 
	public WPWorker(int _workerIndex, float _requiredExp, Dictionary<WPEnum.WorkerAppearanceDetail,int> _appearance)
    {
        this.workerIndex = _workerIndex;
        this.requiredExperience = _requiredExp;
		this.appearance = _appearance;
    }

    
}
