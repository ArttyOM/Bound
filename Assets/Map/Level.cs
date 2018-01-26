using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public GameObject[,] Data;

    public Vector2 start;
    public Vector2 finish;


    public void GenerateNew()
    {
        var config = ServiceLocator.Instance.Resolve<GameSettingsProvider>().GetSettings();
        var gen = ServiceLocator.Instance.Resolve<DummyGenerator>();

        var typ = Random.RandomRange(0, 2) == 0 ? LevelType.Forest : LevelType.Dungeon;

        Data = new GameObject[config.LevelWidth, config.LevelHeight];
        gen.Apply(this, typ);
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
