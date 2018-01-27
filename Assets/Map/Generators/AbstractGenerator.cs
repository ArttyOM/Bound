using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGenerator  {

    abstract protected void GenerateLevel(Level level, LevelType typ, out Vector2Int start, out Vector2Int finish);

    public void Apply(Level level, LevelType typ)
    {
        Vector2Int start;
        Vector2Int finish;
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        var backs = ServiceLocator.Instance.ResolveService<BackgroundsProvider>().GetValue(typ);

        GenerateLevel(level, typ, out start, out finish);
        for (int x = 0; x < config.LevelWidth; x++)
            for (int y = 0; y < config.LevelHeight; y++)
                level.Data[x, y] = backs.GetItem(level.CellTypes[x,y], x, y);
        level.start = start * config.GenerationCell + new Vector2(0.5f, 0.5f);
        level.finish = finish * config.GenerationCell + new Vector2(0.5f, 0.5f);
    }

}
