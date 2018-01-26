using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DummyGenerator : AbstractGenerator
{

    override protected void GenerateLevel(Level level, LevelType typ)
    {
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        var backs = ServiceLocator.Instance.ResolveService<BackgroundsProvider>().GetValue(typ);
        for (int x = 0; x < config.LevelWidth; x++)
            for (int y = 0; y < config.LevelHeight; y++)
                level.Data[x, y] = backs.GetItem(Random.RandomRange(0, 2) == 0 ? WallType.Wall : WallType.Floor, x, y);
        level.start = new Vector2(0.5f, 0.5f);
        level.finish = new Vector2(10.5f, 10.5f);
        level.Data[0, 0] = backs.GetItem(WallType.Floor, 0, 0);
        level.Data[10, 10] = backs.GetItem(WallType.Floor, 10, 10);
    }


}
