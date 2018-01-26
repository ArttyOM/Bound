using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGenerator  {

    abstract protected void GenerateLevel(Level level, LevelType typ);

    public void Apply(Level level, LevelType typ)
    {
        GenerateLevel(level, typ);
    }

}
