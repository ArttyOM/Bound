using UnityEngine;

public class CharacterKeyboardController : CharacterControllerBase
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
	
	public void Update()
	{
		if (Input.GetKey(KeyCode.W))
			_characterCachedTransform.position += Vector3.up * _character.Speed;
		
		if (Input.GetKey(KeyCode.S))
			_characterCachedTransform.position += Vector3.down * _character.Speed;
		
		if (Input.GetKey(KeyCode.A)) 
			_characterCachedTransform.position += Vector3.left * _character.Speed;
		
		if (Input.GetKey(KeyCode.D)) 
			_characterCachedTransform.position += Vector3.right * _character.Speed;
	}
}