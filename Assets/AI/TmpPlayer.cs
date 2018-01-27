using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 delta = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * 6 * Time.deltaTime;
        transform.position += delta;
    }
}
