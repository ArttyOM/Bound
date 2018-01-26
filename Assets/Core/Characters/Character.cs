using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
	public string Name;

	public int Damage;

	public float Speed;

	public CharacterControllerBase Controller;

	public void Awake()
	{
		Controller.SetControlledCharacter(this);
	}
}