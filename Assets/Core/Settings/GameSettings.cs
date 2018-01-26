using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Game/Settings", order = 1)]
public class GameSettings: ScriptableObject
{
	public float TransmissionMaxLength;

	public float TransmissionMinLength;

    public int LevelWidth;
    public int LevelHeight;
    public int GenerationCell;

}