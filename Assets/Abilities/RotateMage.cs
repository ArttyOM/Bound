using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMage : AbstractAbility 
{

    override protected void Execute()
    {
        StartCoroutine((owner as Warrior).SpecialAttack());

    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
