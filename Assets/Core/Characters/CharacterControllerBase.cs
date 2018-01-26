using UnityEngine;

public abstract class CharacterControllerBase : MonoBehaviour
{
	public abstract void SetControlledCharacter(Character character);

	public abstract void EnableControl();

	public abstract void DisableControl();
}