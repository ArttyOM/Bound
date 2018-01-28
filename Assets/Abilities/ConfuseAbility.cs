using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core.Characters;

public class ConfuseAbility : AbstractAbility 
{

    public float ConfuseTime;

    override protected void Execute()
    {
        Debug.Log("Confuse");
        var contr = ServiceLocator.Instance.ResolveSingleton<CharactersController>();
        contr.ConfusedTo = Time.time + ConfuseTime;
        contr.ConfusedDelta = 90.0f * Random.Range(1, 3);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
