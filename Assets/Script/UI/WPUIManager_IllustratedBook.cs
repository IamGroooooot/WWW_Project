using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPUIManager_IllustratedBook : WPUIManager {

	/////////////////////////////////////////////////////////////////////////
	// Varaibles
	public static WPUIManager_IllustratedBook instance = null;     // singleton
	
	[SerializeField] private GameObject prefab;

	/////////////////////////////////////////////////////////////////////////
	// Methods

	// 초기 설정을 합니다.
	protected override void Init()
	{
		instance = this;

		CreateItemList();

		//SetActive(false);
	}

	//임시로 짠 함수----WPScrollViewItem 
	void CreateItemList()
	{
		GameObject go;
		for(int i =0; i < 12; i++)
		{
			go = Instantiate(prefab, transform.GetChild(0).GetChild(0).GetChild(0)) as GameObject;

		}


	}
}
