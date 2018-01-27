using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

/// <summary>
/// Тестовый класс
/// </summary>
[RequireComponent(typeof(Agent))]
public class EnemyScript : MonoBehaviour {
    private Agent agent;

    Transform player;

	// Use this for initialization
	void Start () {
        agent = GetComponent<Agent>();
        agent.Speed = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings().EnemyStandardSpeed; ;
	    var playersArray = GameObject.FindGameObjectsWithTag("Player");
	    player = playersArray[Random.Range(0, playersArray.Length)].transform;
	}
	
	// Update is called once per frame
	void Update () {
        if ((transform.position - player.position).magnitude < 20.0f)
            agent.Destination = player.position;
        else
        {
            agent.Stop();
        }
	}

}
