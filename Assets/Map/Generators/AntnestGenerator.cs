using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AntnestGenerator : AbstractGenerator
{
    int nx;
    int ny;

    enum Side { North, South, East, West};

    const int CFG_START_PATHS = 5;
    const int CFG_START_PATH_LENGTH = 10;
    const int CFG_MID_PATHS = 5;
    const int CFG_MID_PATH_LENGTH = 10;

    Side opposite(Side side)
    {
        switch(side)
        {
            case Side.North: return Side.South;
            case Side.South: return Side.North;
            case Side.East: return Side.West;
            case Side.West: return Side.East;
        }
        return Side.South;
    }

    Side RandomSide()
    {
        return (Side)Random.Range(0, 4);
    }

    int RandomX()
    {
        return Random.Range(1, nx - 1);
    }

    int RandomY()
    {
        return Random.Range(1, ny - 1);
    }

    VectorMyInt RandomAtSide(Side side)
    {
        switch(side)
        {
            case Side.North:
                return new VectorMyInt(RandomX(), ny - 2);
            case Side.South:
                return new VectorMyInt(RandomX(), 1);
            case Side.West:
                return new VectorMyInt(1, RandomY());
            default:
            //case Side.East:
                return new VectorMyInt(nx-2, RandomY());
        }
    }

    VectorMyInt StartAtSide(Side side)
    {
        switch (side)
        {
            case Side.North:
                return new VectorMyInt(nx/2, ny - 2);
            case Side.South:
                return new VectorMyInt(nx / 2, 1);
            case Side.West:
                return new VectorMyInt(1, ny/2);
            default:
                //case Side.East:
                return new VectorMyInt(nx - 2, ny / 2);
        }
    }

    Side? at_border(VectorMyInt v)
    {
        if (v.x == 1)
            return Side.West;
        if (v.x == nx - 2)
            return Side.East;
        if (v.y == 1)
            return Side.South;
        if (v.y == ny-2)
            return Side.North;
        return null;
    }

    void one_step(ref VectorMyInt pos, Side side)
    {
        switch (side)
        {
            case Side.North:
                pos.y += 1;
                break;
            case Side.South:
                pos.y -= 1;
                break;
            case Side.West:
                pos.x -= 1;
                break;
            case Side.East:
                pos.x += 1;
                break;
        }
    }

    void apply(Level level, VectorMyInt pos)
    {
        level.CellTypes[pos.x, pos.y] = CellType.Floor;
    }


    void add_path(Level level, VectorMyInt start, int length)
    {
        Side? detect = at_border(start);
        VectorMyInt apos = start;
        for(int i=0; i<length; i++)
        {
            Side step = detect.HasValue ? opposite(detect.Value) : RandomSide();
//            if (step == start_side && Random.Range(0, 2) == 0)
//                continue;
            one_step(ref apos, step);
            apply(level, apos);
            detect = at_border(apos);
            //            Debug.Log(apos);
        };
    }


    override protected void GenerateLevel(Level level, LevelType typ, out VectorMyInt start, out VectorMyInt finish)
    {
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        nx = config.LevelWidth;
        ny = config.LevelHeight;
        //fill with walls
        for (int x = 0; x < nx; x++)
            for (int y = 0; y < ny; y++)
                level.CellTypes[x, y] = CellType.Wall;
        //select starting side
        var start_side = RandomSide();
        //draw one random path
        var apos = StartAtSide(start_side);
        apply(level, apos);
        start = apos;
        Side step = opposite(start_side);
        one_step(ref apos, step);
        apply(level, apos);
        Side prev = step;
        Side? detect = null;
        do
        {
            if (detect == start_side)
                step = opposite(start_side);
            else
                step = RandomSide();
//            if (step == opposite(prev))
//                continue;
            if (step == start_side && Random.Range(0, 2) == 0)
                continue;
            one_step(ref apos, step);
            apply(level, apos);
            prev = step;
            detect = at_border(apos);
//            Debug.Log(apos);
        } while (detect == null || detect == start_side);
        finish = apos;


        //additional start paths
        for (int i = 0; i < CFG_START_PATHS; i++)
            add_path(level, start, CFG_START_PATH_LENGTH);





        level.CellTypes[apos.x, apos.y] = CellType.Exit;
    }

}
