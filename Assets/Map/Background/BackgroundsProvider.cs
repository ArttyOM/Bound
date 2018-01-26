using UnityEngine;

public class BackgroundsProvider
{
    private string path = "BackgroundsBase";
    private BackgroundsBase _value;

    public BackgroundsBase GetValue()
    {
        return _value ?? (_value = Resources.Load<BackgroundsBase>(path));
    }
}