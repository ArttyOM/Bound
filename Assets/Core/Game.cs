using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    Level level;
    AI.GridScript grid;
    GameSettings config;



    public void NewGame()
    {
        NextLevel();
    }

    public void NextLevel()
    {
        level.GenerateNew();
        grid.transform.position = new Vector2(config.GenerationCell * config.LevelWidth / 2, config.GenerationCell * config.LevelHeight / 2);
        grid.gridSize = new Vector2(1.0f * config.GenerationCell * config.LevelWidth / 2, 1.0f * config.GenerationCell * config.LevelHeight / 2);
        grid.InitGrid();
    }


    private void Awake()
    {
        level = GameObject.Find("Level").GetComponent<Level>();
        grid = GameObject.Find("Grid").GetComponent<AI.GridScript>();
        config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
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
