using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundsBase", menuName = "Game/Map/BackgroundsBase", order = 1)]
public class BackgroundsBase : ScriptableObject {


    [SerializeField]
    GameObject[] data;

    public GameObject GetItem(WallType typ, int ax, int ay)
    {
        var config = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings();
        var result = Instantiate(data[(int)typ]);
        result.transform.position = (new Vector2(ax, ay)) * config.GenerationCell;
        result.transform.localScale = (new Vector2(1, 1)) * config.GenerationCell;
        return result;
    }
}
