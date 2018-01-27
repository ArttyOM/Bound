using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGenerator  {

    abstract protected void GenerateLevel(Level level, LevelType typ, out Vector2 start, out Vector2 finish);

    public void Apply(Level level, LevelType typ)
    {
        Vector2 start;
        Vector2 finish;
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        var backs = ServiceLocator.Instance.ResolveService<BackgroundsProvider>().GetValue(typ);

        GenerateLevel(level, typ, out start, out finish);
        for (int x = 0; x < config.LevelWidth; x++)
            for (int y = 0; y < config.LevelHeight; y++)
                level.Data[x, y] = backs.GetItem(level.CellTypes[x,y], x, y);
        level.start = start * config.GenerationCell;
        level.finish = finish * config.GenerationCell;
    }

}
