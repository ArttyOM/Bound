using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public CellType[,] CellTypes;
    public GameObject[,] Data;

    public Vector2 start;
    public Vector2 finish;

    public bool CellPassable(CellType typ)
    {
        return typ != CellType.Wall;
    }

    public bool CellPassable(VectorMyInt pos)
    {
        return CellPassable(CellTypes[pos.x, pos.y]);
    }

    public void GenerateNew()
    {
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        var gen = ServiceLocator.Instance.ResolveService<AntnestGenerator>();

        var typ = LevelType.Forest;
        CellTypes = new CellType[config.LevelWidth, config.LevelHeight];
        Data = new GameObject[config.LevelWidth, config.LevelHeight];
        gen.Apply(this, typ);
    }

    public Vector2 RandomPlace()
    {
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        var pos = RandomIntPlace();
        return new Vector2(pos.x * config.GenerationCell + Random.Range(0.0f, config.GenerationCell), pos.y * config.GenerationCell + Random.Range(0.0f, config.GenerationCell));
    }

    public VectorMyInt RandomIntPlace()
    {
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        int x, y;
        do
        {
            x = Random.Range(0, config.LevelWidth);
            y = Random.Range(0, config.LevelHeight);
        } while (!CellPassable(CellTypes[x, y]));
        return new VectorMyInt(x, y);
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
