using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Game/Settings", order = 1)]
public class GameSettings: ScriptableObject
{
	public float TransmissionMaxLength;

	public float TransmissionMinLength;

	public float CameraHeight;

    /// <summary>
    /// Размер клетки сетки
    /// </summary>
    public float PathfindingCellSize;
    public int LevelWidth;
    public int LevelHeight;
    public int GenerationCell;

    /// <summary>
    /// Скорость врагов по умолчанию
    /// </summary>
    public float EnemyStandardSpeed;
    /// <summary>
    /// Размер врага
    /// </summary>
    public float StandardEnemySize;

    public LayerMask NotWalkableMask;
}