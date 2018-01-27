using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionsController : MonoBehaviour {

	[SerializeField] private LineRenderer _transmission;
	private GameSettings _settings;
	public Character _wizard;
	public Character _warrior;

	private Transform _warriorTransform;
	private Transform _wizardTransform;

	private float _dangerZone;
	// Use this for initialization

	void Awake()
	{
		ServiceLocator.Instance.RegisterSingleton(this);
		_settings = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
		_dangerZone = _settings.TransmissionDangerZone;
		_warriorTransform = _warrior.transform;
		_wizardTransform = _wizard.transform;


	}

	
	// Update is called once per frame
	void Update () {
		float distance =Vector3.Distance (_warriorTransform.position, _wizardTransform.position);

		if (distance >= _dangerZone) {
			_transmission.startColor = Color.red;
			_transmission.endColor = Color.red;
		} else {
			_transmission.startColor = Color.blue;
			_transmission.endColor = Color.blue;
		}
	}
}
