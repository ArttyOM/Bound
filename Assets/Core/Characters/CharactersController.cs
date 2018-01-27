using System.Collections;
using UnityEngine;

namespace Assets.Core.Characters
{
	public class CharactersController: MonoBehaviour
	{		
		public Character _wizard;
		public Character _warrior;
		[SerializeField] private LineRenderer _transmission;
		[SerializeField] private Transform _cameraTransform;

		private Transform _warriorTransform;
		private Transform _wizardTransform;

		private Rigidbody2D _warriorRigidbody2D;
		private Rigidbody2D _wizardRigidbody2D;
		
		private GameSettings _settings;
		
		private WaitForSeconds _damagerUpdateRate = new WaitForSeconds(0.3f);

		private Quaternion _wizardTargetRotation = new Quaternion(0,0,0,1);
		
		public void Awake()
		{
			ServiceLocator.Instance.RegisterSingleton(this);
			_settings = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
			_warriorTransform = _warrior.transform;
			_wizardTransform = _wizard.transform;
			_warriorRigidbody2D = _warrior.GetComponent<Rigidbody2D>();
			_wizardRigidbody2D = _wizard.GetComponent<Rigidbody2D>();

            InitAbilities();

			StartCoroutine(UpdateDamager());
		}

		private void Update()
		{
			UpdateFirstCharacterPosition();
			UpdateSecondCharacterPosition();
			UpdateTransmission();
			UpdateCamera();
            ApplyAbilities();

        }

		private IEnumerator UpdateDamager()
		{
			while (true)
			{
				yield return _damagerUpdateRate;
				if (_settings.TransmissionMaxLength - (_wizardTransform.position - _warriorTransform.position).magnitude < _settings.TransmissionMaxThreshold)
				{
					_wizard.DealDamage(_settings.TransmissionBreakDamage);
					_warrior.DealDamage(_settings.TransmissionBreakDamage);
				}
				
				if ((_wizardTransform.position - _warriorTransform.position).magnitude - _settings.TransmissionMinLength < _settings.TransmissionMinThreshold)
				{
					_wizard.DealDamage(_settings.TransmissionBreakDamage);
					_warrior.DealDamage(_settings.TransmissionBreakDamage);
				}
			}
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
			_warriorTransform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - _warriorTransform.position);

			var targetPosition = _warriorTransform.position + _warriorTransform.up * _warrior.Speed;


            _warrior.LastDir = (Vector2)mousePos - (Vector2)_warriorTransform.position;


            if (((Vector2)mousePos - (Vector2)_warriorTransform.position).sqrMagnitude > 0.2f * 0.2f &&
                WarriorNewPositionLessThenMaxLength(targetPosition))
                _warriorRigidbody2D.MovePosition(targetPosition);
            else
                _warriorRigidbody2D.MovePosition(_warriorTransform.position);
		}

		private bool WizardNewPositionLessThenMaxLength(Vector3 newPosition)
		{
			return (newPosition - _warriorTransform.position).sqrMagnitude <
			       _settings.TransmissionMaxLength * _settings.TransmissionMaxLength;
		}
		
		private bool WarriorNewPositionLessThenMaxLength(Vector3 newPosition)
		{
			return (newPosition - _wizardTransform.position).sqrMagnitude <
			       _settings.TransmissionMaxLength * _settings.TransmissionMaxLength;
		}
		
		private bool WizardNewPositionMoreThenMinLength(Vector3 newPosition)
		{
			return (newPosition - _warriorTransform.position).sqrMagnitude >
			       _settings.TransmissionMinLength * _settings.TransmissionMinLength;
		}
		
		private bool WarriorNewPositionMoreThenMinLength(Vector3 newPosition)
		{
			return (newPosition - _wizardTransform.position).sqrMagnitude >
			       _settings.TransmissionMinLength * _settings.TransmissionMinLength;
		}
		
		private void UpdateFirstCharacterPosition()
		{
			var targetTransform = new Vector3();
			var up = Input.GetKey(KeyCode.W);
			var down = Input.GetKey(KeyCode.S);
			var left = Input.GetKey(KeyCode.A);
			var right = Input.GetKey(KeyCode.D);
			var attack = Input.GetKey(KeyCode.Space);

			if (up)
				targetTransform += Vector3.up;

			if (down)
				targetTransform += Vector3.down;

			if (left)
				targetTransform += Vector3.left;

			if (right)
				targetTransform += Vector3.right;

            if (up || down || left || right)
                _wizard.LastDir = targetTransform;


            var speed = up && left || up && right || down && left || down && right
				? Mathf.Sqrt(_wizard.Speed * _wizard.Speed / 2f)
				: _wizard.Speed;



                targetTransform = _wizardTransform.position + targetTransform * speed;

			if (up || down || left || right)
			{
                _wizardTargetRotation = Quaternion.LookRotation(Vector3.forward, targetTransform - _wizardTransform.position);
			}

            _wizardTransform.rotation =
				Quaternion.Lerp(_wizardTransform.rotation, _wizardTargetRotation, 5f * Time.deltaTime);

			if (WizardNewPositionLessThenMaxLength(targetTransform))
			{
				_wizardRigidbody2D.MovePosition(targetTransform);
			}
		}

        void ApplyAbilities()
        {
            if (Input.GetMouseButton(0))
                _warrior.Abilities[0].Perform();
            if (Input.GetMouseButton(1))
                _warrior.Abilities[1].Perform();
            for(int i=1; i<12; i++)
            {
                if (Input.GetKey((KeyCode)((int)KeyCode.F1+i-1)))
                    _wizard.Abilities[i-1].Perform();
            }

        }

        void InitAbilities()
        {
            for (int i = 0; i < _warrior.Abilities.Count; i++)
                _warrior.Abilities[i].owner = _warrior;
            for (int i = 0; i < _wizard.Abilities.Count; i++)
                _wizard.Abilities[i].owner = _wizard;
        }

    }
}