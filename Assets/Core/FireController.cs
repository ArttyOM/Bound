using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
	public Light Fire;
	
	public float maxIntencity;
	
	public float minIntencity;

	public bool modeSwitcher;
	
	void Start()
	{
		StartCoroutine(UpdateFire());
	}

	IEnumerator UpdateFire()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.value);
			modeSwitcher = !modeSwitcher;
			Fire.intensity = modeSwitcher ? maxIntencity : minIntencity;
		}
	}
}
