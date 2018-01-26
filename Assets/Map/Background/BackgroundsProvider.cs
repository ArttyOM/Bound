using System;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundsProvider
{
    private string base_path = "Backgrounds/";
    private Dictionary<LevelType, string> names = new Dictionary<LevelType, string>{
        { LevelType.Forest, "Forest"},
        { LevelType.Dungeon, "Dungeon"},
    };
    private Dictionary<LevelType, BackgroundsBase> _values = new Dictionary<LevelType, BackgroundsBase>();

    public BackgroundsBase GetValue(LevelType typ)
    {
        if (_values.ContainsKey(typ))
            return _values[typ];
        else
        {
            var result = Resources.Load<BackgroundsBase>(base_path + names[typ]);
            _values.Add(typ, result);
            return result;
        }
    }
}