﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum TowerStatus
{
    Waiting, Active, Breaking, Broken,
}

public class Tower : MonoBehaviour {

    GameObject transmission;
    const float active_distance = 100.0f;
    const float break_distance = 3.0f;
    const float break_time = 3.0f;

    TowerStatus status = TowerStatus.Active;
    float breaking_started = 0.0f;
    EnemySpawnPoint spawn;
    Game game;


    void Awake () {
        transmission = GameObject.FindWithTag("Transmission");
        spawn = gameObject.GetComponent<EnemySpawnPoint>();
        game = ServiceLocator.Instance.ResolveSingleton<Game>();
    }


    void SetStatus(TowerStatus astatus)
    {
        status = astatus;
        //TODO: visualization
    }
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(this.transform.position, transmission.transform.position);

        switch (status)
        {
            case TowerStatus.Active:
                if (distance > active_distance)
                    SetStatus(TowerStatus.Waiting);
                else if(distance < break_distance)
                {
                    breaking_started = Time.time;
                    SetStatus(TowerStatus.Breaking);
                }
                break;
            case TowerStatus.Breaking:
                if (distance > break_distance)
                    SetStatus(TowerStatus.Active);
                else if (Time.time - breaking_started > break_time)
                {
                    SetStatus(TowerStatus.Broken);
                    game.TowerBroken();
                }
                break;
            default:
                break;
        }
        spawn.dead = status == TowerStatus.Broken;
        spawn.online = status == TowerStatus.Active || status == TowerStatus.Breaking;
    }
}
