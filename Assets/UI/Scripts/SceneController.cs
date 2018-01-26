using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	//[SerializeField] private TextMesh scoreLabel;

	[SerializeField] private string SceneName= "SceneUI";

	public void RestartGame()
	{
		SceneManager.LoadScene (SceneName); 
	}
		
	public void ExitGame()
	{
		Application.Quit ();
	}
}
