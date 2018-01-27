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

    private Vector2 Direction
    {
        get
        {
            return new Vector2(-Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad),
                Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad));
        }
    }
}
