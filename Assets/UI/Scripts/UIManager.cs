using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	[SerializeField] private SettingPopup settingPopup;

	[SerializeField] private Texture scoreLabel;

	void Start(){
		//settingPopup.Open();
	}
	// Update is called once per frame

	private bool isPopupOpen = true;
	void Update () {

		//isPopupOpen = settingPopup.isActiveAndEnabled ();

		if (Input.GetButtonDown ("Cancel")) {
			if (isPopupOpen)
				OnCloseSettings();
			else {
				//Debug.Log ("Trying to open popup...");
				OnOpenSettings();
			}
			//isPopupOpen = !isPopupOpen;
		}
	}

	public void OnOpenSettings(){
		//Debug.Log ("open settings");
		settingPopup.Open();
		isPopupOpen = !isPopupOpen;
	}
	public void OnCloseSettings(){
		//Debug.Log ("open settings");
		settingPopup.Close();
		isPopupOpen = !isPopupOpen;
	}
		

	//public void ExitGame(){
		//Application.Quit ();
	//	SceneManager.Quit();
	//}
		

}
