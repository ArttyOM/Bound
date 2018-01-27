using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundsBase", menuName = "Game/Map/BackgroundsBase", order = 1)]
public class BackgroundsBase : ScriptableObject {


    [SerializeField]
    List<GameObject> Walls;

    [SerializeField]
    List<GameObject> Floors;

    [SerializeField]
    List<GameObject> Exits;

    Dictionary<CellType, List<GameObject>> data;

    public void Prepare()
    {
        if (data == null)
        {
            data = new Dictionary<CellType, List<GameObject>> {
                { CellType.Wall, Walls},
                { CellType.Floor, Floors},
                { CellType.Exit, Exits},
            };
        }

    }

    public GameObject GetItem(CellType typ, int ax, int ay)
    {
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        var alist = data[typ];
        var result = Instantiate(alist[Random.Range(0, alist.Count)]);
        result.transform.position = (new Vector2(ax, ay)) * config.GenerationCell;
        result.transform.localScale = (new Vector3(1, 1, 1)) * config.GenerationCell;
        return result;
    }
}
