using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Game/Settings", order = 1)]
public class GameSettings: ScriptableObject
{
	public float TransmissionMaxLength;

	public float TransmissionMinLength;

    /// <summary>
    /// Размер клетки сетки
    /// </summary>
    public float PathfindingCeilSize;
}