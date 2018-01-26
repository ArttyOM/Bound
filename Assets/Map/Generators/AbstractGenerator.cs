using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGenerator  {

    abstract protected void GenerateLevel(Level level, LevelType typ, out Vector2 start, out Vector2 finish, ref WallType[,] data);

    public void Apply(Level level, LevelType typ)
    {
        Vector2 start;
        Vector2 finish;
        var config = ServiceLocator.Instance.Resolve<GameSettingsProvider>().GetSettings();
        var backs = ServiceLocator.Instance.Resolve<BackgroundsProvider>().GetValue(typ);

        var data = new WallType[config.LevelWidth, config.LevelHeight];
        GenerateLevel(level, typ, out start, out finish, ref data);
        for (int x = 0; x < config.LevelWidth; x++)
            for (int y = 0; y < config.LevelHeight; y++)
                level.Data[x, y] = backs.GetItem(data[x,y], x, y);
        level.start = new Vector2(0.5f, 0.5f);
        level.finish = new Vector2(10.5f, 10.5f);
        level.Data[0, 0] = backs.GetItem(WallType.Floor, 0, 0);
        level.Data[10, 10] = backs.GetItem(WallType.Floor, 10, 10);
    }

}
