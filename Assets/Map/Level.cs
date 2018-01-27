using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public WallType[,] CellTypes;
    public GameObject[,] Data;

    public Vector2 start;
    public Vector2 finish;


    public void GenerateNew()
    {
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        var gen = ServiceLocator.Instance.ResolveService<DummyGenerator>();

        var typ = Random.Range(0, 2) == 0 ? LevelType.Forest : LevelType.Dungeon;
        CellTypes = new WallType[config.LevelWidth, config.LevelHeight];
        Data = new GameObject[config.LevelWidth, config.LevelHeight];
        gen.Apply(this, typ);
    }

    public Vector2 RandomPlace()
    {
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        int x, y;
        do
        {
            x = Random.Range(0, config.LevelWidth);
            y = Random.Range(0, config.LevelHeight);
        } while (CellTypes[x, y] != WallType.Floor);
        return new Vector2(x * config.GenerationCell + Random.Range(0.0f, 1.0f), y * config.GenerationCell + Random.Range(0.0f, 1.0f));
    }


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

}
