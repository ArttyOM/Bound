using System.Collections;
using UnityEngine;

/// <summary>
/// Класс стрелка
/// </summary>
public class RangeEnemy : EnemyScript {

    /// <summary>
    /// Переопределённая атака для стрелка
    /// </summary>
    /// <inheritdoc cref="EnemyScript.Attack"/>
    /// <returns></returns>
    protected override IEnumerator Attack()
    {
        _currentAttackObj = Instantiate(_attackObj);
        _currentAttackObj.transform.position = transform.position;
        _currentAttackObj.GetComponent<EnemyArrowScript>().Direction = Direction;
        _currentAttackObj.GetComponent<EnemyArrowScript>().Damage = Damage;
        _lastAttack = Time.time;
        yield break;
    }
}
