using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	[SerializeField] private SettingPopup settingPopup;

	[SerializeField] private Text scoreLabel;

	[SerializeField] private Slider healthMageSlider;
	[SerializeField] private Text healthMageTXT;

	[SerializeField] private Slider healthWarriorSlider;
	[SerializeField] private Text healthWarriorTXT;


	private float _healthMage=100;
	private float _healthWarrior=100;

	void Awake(){
		Messenger.AddListener(GameEvent.MAGE_HEALTH_CHANGED, OnMageHealthChanged);
		Messenger.AddListener (GameEvent.WARRIOT_HEALTH_CHANGED, OnWariorHealthChanged);
	}

	void OnDestroy(){
		Messenger.RemoveListener (GameEvent.MAGE_HEALTH_CHANGED,OnMageHealthChanged);
		Messenger.RemoveListener (GameEvent.WARRIOT_HEALTH_CHANGED,OnMageHealthChanged);
	}

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

		if (Input.GetButtonDown ("Fire1")) {
			OnMageHealthChanged ();
			OnWariorHealthChanged ();
			_healthMage -= 30f;
			_healthWarrior -= 25f;

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
		
	public void OnMageHealthChanged ()
	{
		//_healthMage = 30f;
		healthMageTXT.text = _healthMage + " / 100";
		healthMageSlider.value = _healthMage / 100f;

	}
	public void OnWariorHealthChanged ()
	{
		//_healthWarrior = 65f;
		healthWarriorTXT.text = _healthWarrior + " / 100";
		healthWarriorSlider.value = _healthWarrior / 100f;

	}

	//public void ExitGame(){
		//Application.Quit ();
	//	SceneManager.Quit();
	//}
		

}
