using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

/// <summary>
/// Тестовый класс
/// </summary>
public class EnemyScript : MonoBehaviour {
    private Agent agent;

    Transform player;

	// Use this for initialization
	void Start () {
        agent = GetComponent<Agent>();
        agent.Speed = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings().EnemyStandardSpeed; ;
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        agent.Destination = player.position;
	}

}
