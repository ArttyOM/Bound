using UnityEngine;

public class CharacterMouseController : CharacterControllerBase
{
	private Character _character;
	private Transform _characterCachedTransform;

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

	public override void SetMinMaxDistances(float min, float max)
	{
		
	}

	public void Update()
	{
		var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		_characterCachedTransform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - _characterCachedTransform.position);
		
		if (Input.GetMouseButton(0))
			_characterCachedTransform.position += _characterCachedTransform.up * _character.Speed;
	}
}