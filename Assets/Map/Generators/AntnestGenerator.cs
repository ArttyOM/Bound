using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AntnestGenerator : AbstractGenerator
{
    int nx;
    int ny;
    Side start_side;
    Level level;

    enum Side { North, South, East, West};

    const int CFG_START_PATHS = 0;
    const int CFG_START_PATH_LENGTH = 100;
    const int CFG_MID_PATHS = 5;
    const int CFG_MID_PATH_LENGTH = 100;
    const int PREVENT_OPENING = 700;
    const int TENSION = 50;

    const float AVG_FREE_MONSTERS = 0.1f;


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

    Side RandomSide(VectorMyInt frompos)
    {
        int[] nwalls = new int[4]{ 0, 0, 0, 0 };
        int sum = 0;
        for(int i=0; i<4; i++)
        {
            nwalls[i] = 100;
            Side side = (Side)i;
            VectorMyInt apos = frompos;
            one_step(ref apos, side);
            if (at_border(apos).HasValue)
                nwalls[i] += 1*PREVENT_OPENING;
            else
            {
                for (int j = 0; j < 4; j++)
                {
                    Side side2 = (Side)j;
                    VectorMyInt apos2 = apos;
                    one_step(ref apos2, side);
                    if (!level.CellPassable(apos2))
                        nwalls[i]+= PREVENT_OPENING;
                }
            }
            sum += nwalls[i];
        }
        int choice = Random.Range(0, sum);
        for (int i = 0; i < 4; i++)
        {
            choice -= nwalls[i];
            if (choice < 0)
                return (Side)i;
        }
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

    void apply(VectorMyInt pos)
    {
        level.CellTypes[pos.x, pos.y] = CellType.Floor;
    }


    VectorMyInt add_path(VectorMyInt start, int length, bool is_main)
    {
        Side? detect = at_border(start);
        VectorMyInt apos = start;
        bool enough = true;
        int i = 0;
        do
        {
            i++;
            Side step = detect.HasValue ? opposite(detect.Value) : RandomSide(apos);
            //            if (step == start_side && Random.Range(0, 2) == 0)
            //                continue;
            if (is_main && step == start_side && Random.Range(0, 100) < TENSION)
                continue;

            one_step(ref apos, step);
            apply(apos);
            detect = at_border(apos);
            if (is_main)
                enough = detect.HasValue && (detect != start_side);
            else
                enough = i > length;
        }while(!enough);
        return apos;
    }


    override protected void GenerateLevel(Level alevel, LevelType typ, out VectorMyInt start, out VectorMyInt finish)
    {
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        var enemyContainer = GameObject.FindWithTag("EnemyContainer");
        var spawner = GameObject.Find("SpawnPoint").GetComponent<EnemySpawnPoint>();

        level = alevel;
        nx = config.LevelWidth;
        ny = config.LevelHeight;
        //fill with walls
        for (int x = 0; x < nx; x++)
            for (int y = 0; y < ny; y++)
                level.CellTypes[x, y] = CellType.Wall;
        //select starting 
        start_side = (Side)Random.Range(0, 4); ;
        //draw one random path
        start = StartAtSide(start_side);
        apply(start);
        finish = add_path(start, 1, true);

        //additional start paths
        for (int i = 0; i < CFG_START_PATHS; i++)
            add_path(start, (config.LevelHeight + config.LevelWidth) * CFG_START_PATH_LENGTH / 100, false);

        for (int i = 0; i < CFG_MID_PATHS; i++)
        { 
            add_path(level.RandomIntPlace(), (config.LevelHeight + config.LevelWidth) * CFG_MID_PATH_LENGTH / 100, false);
        }

        for (int i = 0; i < AVG_FREE_MONSTERS * config.LevelHeight * config.LevelWidth; i++)
        {

            GameObject.Instantiate(spawner.RandomEnemy(), level.RandomPlace(), Quaternion.identity, enemyContainer.transform);
        }


        level.CellTypes[finish.x, finish.y] = CellType.Exit;
        spawner.gameObject.SetActive(false);
    }

}
