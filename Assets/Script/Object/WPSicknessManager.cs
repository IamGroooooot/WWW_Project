using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPSicknessManager : MonoBehaviour {

	/////////////////////////////////////////////////////////////////////////
	// Varaibles
	public static WPSicknessManager instance = null;     // singleton

	/////////////////////////////////////////////////////////////////////////
	// Methods
	void Awake()
	{
		instance = this;
	}
	
	void Start()
	{
		StartCoroutine(MakeSickness_forTest());

	}

	void Update()
	{
		

	}

	//Test용으로 Sickness만듬
	IEnumerator MakeSickness_forTest()
	{
		while (true)
		{
			yield return new WaitForSeconds(3f);
			WPActorManager.instance.GetSicknessByID(Random.Range(0,5)).SetActive(true);
		}
	}
}