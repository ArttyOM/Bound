﻿using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Game/Settings", order = 1)]
public class GameSettings: ScriptableObject
{
	public float TransmissionMaxLength;

	public float TransmissionMinLength;

	public float TransmissionDangerZone;

	public float TransmissionMinThreshold = 0.5f;

	public float TransmissionMaxThreshold = 1f;

	public float TransmissionBreakDamage = 5f;

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

    /// <summary>
    /// Непроходимые объекты
    /// </summary>
    public LayerMask NotWalkableMask;

    /// <summary>
    /// Промежуток, через который можно обновлять путь
    /// </summary>
    public float PathfindingUpdateDelta;
}