using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAbility : MonoBehaviour {

    abstract protected void Execute();

    public Character owner;

    [SerializeField]
    float Cooldown;

    [SerializeField]
    private KeyCode attacKeyCode;

    float last_use = -1000;

    public bool Available()
    {
        return last_use + Cooldown < Time.time;
    }

    public float RemainingCooldown()
    {
        return (Time.time - last_use) / Cooldown;
    }

    public void Perform()
    {
        if (!Available()) return;
        last_use = Time.time;
        Execute();
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Available() && Input.GetKeyDown(attacKeyCode))
	    {
	        last_use = Time.time;
	        Execute();
            print("lol");
	    }
	}
}
