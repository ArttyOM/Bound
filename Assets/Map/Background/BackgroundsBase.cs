using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WeightedItem
{
    [SerializeField]
    public GameObject obj;
    [SerializeField]
    public int weight;
}

[CreateAssetMenu(fileName = "BackgroundsBase", menuName = "Game/Map/BackgroundsBase", order = 1)]
public class BackgroundsBase : ScriptableObject {


    [SerializeField]
    List<WeightedItem> Walls;

    [SerializeField]
    List<WeightedItem> Floors;

    [SerializeField]
    List<WeightedItem> Exits;

    [SerializeField]
    List<WeightedItem> Towers;

    Dictionary<CellType, List<WeightedItem>> data;

    public void Prepare()
    {
        if (data == null)
        {
            data = new Dictionary<CellType, List<WeightedItem>> {
                { CellType.Wall, Walls},
                { CellType.Floor, Floors},
                { CellType.Exit, Exits},
                { CellType.Tower, Towers},
            };
        }

    }

    public GameObject GetItem(Level level, CellType typ, int ax, int ay)
    {
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        var alist = data[typ];
        var sum = 0;
        for (int i = 0; i < alist.Count; i++)
            sum += alist[i].weight;
        var choice = UnityEngine.Random.Range(0, sum);
        GameObject selected = alist[0].obj;
        for (int i = 0; i < alist.Count; i++)
        {
            choice -= alist[i].weight;
            if(choice < 0)
            {
                selected = alist[i].obj;
                break;
            }
        }
        var result = Instantiate(selected, level.transform);
        result.transform.position = (new Vector2(ax, ay)) * config.GenerationCell;
        result.transform.localScale = (new Vector3(1, 1, 1)) * config.GenerationCell;
        return result;
    }
}
