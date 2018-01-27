using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFlight : MonoBehaviour {

    public Vector2 direction;

    [SerializeField]
    float speed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var v = direction / direction.magnitude * speed;
        transform.position = transform.position + new Vector3(v.x, v.y, 0.0f);
	}
}
