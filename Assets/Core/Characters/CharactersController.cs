using UnityEngine;

namespace Assets.Core.Characters
{
	public class CharactersController: MonoBehaviour
	{
		[SerializeField] private Character _wizard;
		[SerializeField] private Character _warrior;
		[SerializeField] private LineRenderer _transmission;
		[SerializeField] private Transform _cameraTransform;

		private Transform _warriorTransform;
		private Transform _wizardTransform;
		private GameSettings _settings;
		
		public void Awake()
		{
			_settings = ServiceLocator.Instance.Resolve<GameSettingsProvider>().GetSettings();
			_warriorTransform = _warrior.transform;
			_wizardTransform = _wizard.transform;
		}

		private void Update()
		{
			UpdateFirstCharacterPosition();
			UpdateSecondCharacterPosition();
			UpdateTransmission();
			UpdateCamera();
		}

		private void UpdateCamera()
		{
			var targetPosition = Vector3.Lerp(_warriorTransform.position, _wizardTransform.position,
				0.5f);
			targetPosition.z = _settings.CameraHeight;
			_cameraTransform.position = targetPosition;
		}

		private void UpdateTransmission()
		{
			_transmission.SetPositions(new[]
			{
				_warriorTransform.position,
				_wizardTransform.position,
			});
		}

		private void UpdateSecondCharacterPosition()
		{
			var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			_wizardTransform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - _wizardTransform.position);
		
			if (Input.GetMouseButton(0))
				_wizardTransform.position += _wizardTransform.up * _wizard.Speed;
		}
		
		private void UpdateFirstCharacterPosition()
		{
			var targetTransform = new Vector3();
			var up = Input.GetKey(KeyCode.W);
			var down = Input.GetKey(KeyCode.S);
			var left = Input.GetKey(KeyCode.A);
			var right = Input.GetKey(KeyCode.D);

			if (up)
				targetTransform += Vector3.up;

			if (down)
				targetTransform += Vector3.down;

			if (left)
				targetTransform += Vector3.left;

			if (right)
				targetTransform += Vector3.right;

			var speed = up && left || up && right || down && left || down && right
				? Mathf.Sqrt(_warrior.Speed * _warrior.Speed / 2f)
				: _warrior.Speed;

			targetTransform = _warriorTransform.position + targetTransform * speed;
		
			_warriorTransform.position = targetTransform;
		}
	}
}