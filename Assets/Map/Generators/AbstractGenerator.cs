using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct VectorMyInt {
    public int x;
    public int y;

    public VectorMyInt(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2 ToF()
    {
        return new Vector2(x, y);
    }

};


public abstract class AbstractGenerator  {

    abstract protected void GenerateLevel(Level alevel, LevelType typ, out VectorMyInt start, out VectorMyInt finish);


    public void Apply(Level level, LevelType typ)
    {
        VectorMyInt start;
        VectorMyInt finish;
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        var backs = ServiceLocator.Instance.ResolveService<BackgroundsProvider>().GetValue(typ);
        var game = ServiceLocator.Instance.ResolveSingleton<Game>();

        GenerateLevel(level, typ, out start, out finish);
        for (int x = 0; x < config.LevelWidth; x++)
            for (int y = 0; y < config.LevelHeight; y++)
            {
                level.Data[x, y] = backs.GetItem(level, level.CellTypes[x, y], x, y);
                if (level.CellTypes[x, y] == CellType.Tower)
                    game.Towers.Add(level.Data[x, y].GetComponent<Tower>());
            }
        level.start = start.ToF() * config.GenerationCell + new Vector2(0.5f, 0.5f);
        level.finish = finish.ToF() * config.GenerationCell + new Vector2(0.5f, 0.5f);
    }

}
