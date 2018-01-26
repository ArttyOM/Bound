using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SettingPopup : MonoBehaviour {

	//[SerializeField] private GameObject popup; 

	public void Open(){
		//popup.SetActive(true);
		gameObject.SetActive(true);
	}

	public void Close(){
		//popup.SetActive (false);
		gameObject.SetActive(false);
	}

}
