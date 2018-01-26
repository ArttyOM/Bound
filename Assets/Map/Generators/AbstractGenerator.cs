using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractGenerator  {

    abstract protected void GenerateLevel(Level level);

    public void Apply(Level level)
    {
        GenerateLevel(level);
    }

}
