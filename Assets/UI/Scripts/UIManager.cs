using System.Collections;
using System.Collections.Generic;
using Assets.Core.Characters;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
	[SerializeField] private SettingPopup settingPopup;
	[SerializeField] private SettingPopup startMenu;
	[SerializeField] private SettingPopup dieMenu;

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
		Messenger.AddListener (GameEvent.SCORE_INCREASED, OnScoreIncreased);
		Messenger.AddListener (GameEvent.YOU_DEAD, OnDead);
	}

	void OnDestroy(){
		Messenger.RemoveListener (GameEvent.MAGE_HEALTH_CHANGED,OnMageHealthChanged);
		Messenger.RemoveListener (GameEvent.WARRIOT_HEALTH_CHANGED,OnMageHealthChanged);
		Messenger.RemoveListener (GameEvent.SCORE_INCREASED, OnScoreIncreased);
		Messenger.RemoveListener (GameEvent.YOU_DEAD, OnDead);
	}

	void Start(){
		//settingPopup.Open();
	}
	// Update is called once per frame

	private bool isPopupOpen = false;
	void Update () {

		//isPopupOpen = settingPopup.isActiveAndEnabled ();

		//if (Input.GetButtonDown ("Cancel")) {
		//	if (isPopupOpen)
		//		OnCloseSettings();
		//	else {
				//Debug.Log ("Trying to open popup...");
		//		OnOpenSettings();
		//	}
			//isPopupOpen = !isPopupOpen;
		//}
			
		//if (Input.GetButtonDown ("Fire1")) {
		//	OnMageHealthChanged ();
		//	OnWariorHealthChanged ();
		//	_healthMage -= 30f;
		//	_healthWarrior -= 25f;
		//
		//}
	}

	public void OnStartButton(){
		startMenu.Close ();
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
		healthMageTXT.text = ServiceLocator.Instance.ResolveSingleton<CharactersController>()._wizard.Health + " / 100";
		healthMageSlider.value = ServiceLocator.Instance.ResolveSingleton<CharactersController>()._wizard.Health / 100f;

	}
	public void OnWariorHealthChanged ()
	{
		//_healthWarrior = 65f;
		healthWarriorTXT.text = ServiceLocator.Instance.ResolveSingleton<CharactersController>()._warrior.Health + " / 100";
		healthWarriorSlider.value = ServiceLocator.Instance.ResolveSingleton<CharactersController>()._warrior.Health / 100f;

	}

	public void OnDead(){
		dieMenu.Open ();
	}

	public void OnScoreIncreased()
	{
        var game = ServiceLocator.Instance.ResolveSingleton<Game>();
		scoreLabel.text = System.String.Format("towers: {0} / {1}", game.DestroyedTowers, game.TotalTowers);
    }
	//public void ExitGame(){
		//Application.Quit ();
	//	SceneManager.Quit();
	//}
		

}
