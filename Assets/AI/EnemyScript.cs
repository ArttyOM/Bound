using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

/// <summary>
/// Тестовый класс
/// </summary>
public class EnemyScript : MonoBehaviour {

    public Vector3 target;

    private Agent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<Agent>();
        agent.Speed = 5;
	}
	
	// Update is called once per frame
	void Update () {
        UpTarget();
        agent.Destination = target;
	}

    void UpTarget()
    {
        Vector3 delta = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * 6 * Time.deltaTime;
        target += delta;
    }
}
