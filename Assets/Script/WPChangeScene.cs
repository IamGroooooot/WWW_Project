using UnityEngine;
using UnityEngine.SceneManagement;

public class WPChangeScene : MonoBehaviour{

	/// <summary>
	/// 게임 시작 
	/// </summary>
	public void StartBtnPressed()
	{
		SceneManager.LoadScene(1);
	}
}
