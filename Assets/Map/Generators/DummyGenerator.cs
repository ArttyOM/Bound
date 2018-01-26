using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DummyGenerator : AbstractGenerator
{

    override protected void GenerateLevel(Level level)
    {
        var config = ServiceLocator.Instance.Resolve<GameSettingsProvider>().GetSettings();
        var backs = ServiceLocator.Instance.Resolve<BackgroundsProvider>().GetValue();
        for (int x = 0; x < config.LevelWidth; x++)
            for (int y = 0; y < config.LevelHeight; y++)
                level.Data[x, y] = backs.GetItem(Random.RandomRange(0, 2) == 0 ? WallType.Wall : WallType.Floor, x, y);
    }


}
