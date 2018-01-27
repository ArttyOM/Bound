using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DummyGenerator : AbstractGenerator
{

    override protected void GenerateLevel(Level level, LevelType typ, out Vector2 start, out Vector2 finish)
    {
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        for (int x = 0; x < config.LevelWidth; x++)
            for (int y = 0; y < config.LevelHeight; y++)
                level.CellTypes[x, y] = Random.Range(0, 2) == 0 ? WallType.Wall : WallType.Floor;
        start = new Vector2(10.5f, 10.5f);
        finish = new Vector2(10.5f, 10.5f);
    }


}
