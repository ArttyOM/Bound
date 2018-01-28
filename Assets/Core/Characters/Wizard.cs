using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wizard : Character
{
	protected override IEnumerator Die()
	{
		Messenger.Broadcast(GameEvent.YOU_DEAD);
		yield return new WaitForSeconds (3f);
		SceneManager.LoadScene ("FirstLevel");
	}
}