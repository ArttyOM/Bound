using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DummyGenerator : AbstractGenerator
{

    override protected void GenerateLevel(Level level, LevelType typ, out Vector2Int start, out Vector2Int finish)
    {
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        for (int x = 0; x < config.LevelWidth; x++)
            for (int y = 0; y < config.LevelHeight; y++)
                level.CellTypes[x, y] = Random.Range(0, 2) == 0 ? CellType.Wall : CellType.Floor;
        start = new Vector2Int(10, 10);
        finish = new Vector2Int(11, 11);
    }


}
