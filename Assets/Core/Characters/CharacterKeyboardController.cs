using UnityEngine;

public class CharacterKeyboardController : CharacterControllerBase
{
	private Character _character;
	private Transform _characterCachedTransform;
	private float _maxDistance;
	private float _minDistance;

	public override void SetControlledCharacter(Character character)
	{
		_character = character;
		_characterCachedTransform = character.transform;
	}

	public override void EnableControl()
	{
	}

	public override void DisableControl()
	{
	}

	public void Update()
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
			? Mathf.Sqrt(_character.Speed * _character.Speed / 2f)
			: _character.Speed;

//		if ((targetTransform - _characterCachedTransform.position).sqrMagnitude < _maxDistance * _maxDistance)
//		{
//			
//		}
		
		targetTransform = _characterCachedTransform.position + targetTransform * speed;

		_characterCachedTransform.position = targetTransform;
	}

	public CharacterKeyboardController(float min, float max) : base(min, max)
	{
		_maxDistance = max;
		_minDistance = min;
	}
}