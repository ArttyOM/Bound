using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
	public Light Fire;

	void Start()
	{
		StartCoroutine(UpdateFire());
	}

	IEnumerator UpdateFire()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.value);
			Fire.enabled = !Fire.enabled;
		}
	}
}
