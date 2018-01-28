using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour {

    public static Vector2 PlayersPosition1 { get; set; }
    public static Vector2 PlayersPosition2 { get; set; }

    private Wizard wizard;
    private Warrior warrior;

    public const float DIST = 2f;

    public static CheckPointScript lastVisited = null;

    // Use this for initialization
    void Start ()
    {
        wizard = GameObject.Find("Wizard").GetComponent<Wizard>();
        warrior = GameObject.Find("Warrior").GetComponent<Warrior>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    float minDistance = Mathf.Min(Vector3.Distance(PlayersPosition1, transform.position),
	        Vector3.Distance(PlayersPosition2, transform.position));
	    if (minDistance < DIST)
	    {
	        lastVisited = this;
	        wizard.Health = 100;
	        warrior.Health = 100;
	    }
	}
}
