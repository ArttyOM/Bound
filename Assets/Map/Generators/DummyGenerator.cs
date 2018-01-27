using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DummyGenerator : AbstractGenerator
{

    override protected void GenerateLevel(Level alevel, LevelType typ, out VectorMyInt start, out VectorMyInt finish)
    {
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        for (int x = 0; x < config.LevelWidth; x++)
            for (int y = 0; y < config.LevelHeight; y++)
                alevel.CellTypes[x, y] = Random.Range(0, 2) == 0 ? CellType.Wall : CellType.Floor;
        start = new VectorMyInt(10, 10);
        finish = new VectorMyInt(11, 11);
    }


}
