using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    Level level;
    AI.GridScript grid;
    GameSettings config;
    Wizard wizard;
    Warrior warrior;



    public void NewGame()
    {
        NextLevel();
    }


    void TeleportTo(Vector2 position)
    {
        wizard.transform.position = position - new Vector2(-1,0);
        warrior.transform.position = position + new Vector2(1, 0);
    }

    public void NextLevel()
    {
        //generate level
        level.GenerateNew();
        //update grid
        grid.transform.position = new Vector2(config.GenerationCell * config.LevelWidth / 2, config.GenerationCell * config.LevelHeight / 2);
        grid.gridSize = new Vector2(1.0f * config.GenerationCell * config.LevelWidth / 2, 1.0f * config.GenerationCell * config.LevelHeight / 2);
        grid.InitGrid();
        //place players
        TeleportTo(level.start);

    }


    private void Awake()
    {
        level = GameObject.Find("Level").GetComponent<Level>();
        grid = GameObject.Find("Grid").GetComponent<AI.GridScript>();
        config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        warrior = GameObject.Find("Warrior").GetComponent<Warrior>();
        wizard = GameObject.Find("Wizard").GetComponent<Wizard>();
        ServiceLocator.Instance.RegisterSingleton(this);
    }

    // Use this for initialization
    void Start () {
        NewGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
