using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TowerStatus
{
    Waiting, Active, Breaking, Broken,
}

public class Tower : MonoBehaviour {

    GameObject transmission;
    const float active_distance = 100.0f;
    const float break_distance = 3.0f;
    const float break_time = 3.0f;

    public TowerStatus status = TowerStatus.Active;

    public ParticleSystem EffActive;
    public ParticleSystem EffDestroyed;
    public GameObject MeshActive;

    public Vector2 towerpos;
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
        float distance = Vector2.Distance(towerpos, game.PlayerPos());
        status = TowerStatus.Broken;
        switch (status)
        {
            case TowerStatus.Waiting:
                if (distance < active_distance)
                    SetStatus(TowerStatus.Active);
                break;
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

        EffActive.gameObject.SetActive(spawn.online);
        EffDestroyed.gameObject.SetActive(spawn.dead);

        if (spawn.dead)
            MeshActive.GetComponent<MeshRenderer>().materials[0].color = new Color(98/255.0f, 86 / 255.0f, 86 / 255.0f, 255 / 255.0f);

    }
}
