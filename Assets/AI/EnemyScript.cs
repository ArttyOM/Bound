using System.Collections;
using UnityEngine;
using AI;

/// <summary>
/// Обычный вражеский класс(Ближник)
/// </summary>
[RequireComponent(typeof(Agent))]
public class EnemyScript : Character{
    private Agent agent;

    private Transform player;
    private GameObject[] _players;

    [SerializeField]
    protected float _cooldown;

    [SerializeField]
    protected float _attackDistance;

    [SerializeField]
    protected GameObject _attackObj;

    protected bool _canAttack = false;

    protected float _lastAttack = -10;

    protected GameObject _currentAttackObj;


	// Use this for initialization
	void Start () {
        agent = GetComponent<Agent>();
        agent.Speed = ServiceLocator.Instance.ResolveService<GameSettingsProvider>().GetSettings().EnemyStandardSpeed; ;
	    _players = GameObject.FindGameObjectsWithTag("Player");
	    player = _players[Random.Range(0, _players.Length)].transform;
	    StartCoroutine(CheckAttack());
	}
	
	// Update is called once per frame
	void Update () {
	    if ((transform.position - player.position).magnitude < 15.0f)
	    {
	        agent.Player = player.gameObject;
	        agent.Destination = player.position;
	        _canAttack = (transform.position - player.position).magnitude < _attackDistance;
	    }
	    else
        {
            agent.Stop();
            player = _players[Random.Range(0, _players.Length)].transform;
        }
	}

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerAttack")
        {
            Destroy(gameObject); // TODO: минус хп
        }
    }

    /// <summary>
    /// Атакует
    /// </summary>
    protected virtual IEnumerator Attack()
    {
        _currentAttackObj = Instantiate(_attackObj, transform);
        _currentAttackObj.transform.localPosition = (Vector3)Direction * 0.2f;
        _lastAttack = Time.time;
        player.GetComponent<Character>().DealDamage(Damage);       
        yield return new WaitForSeconds(0.3f);
        Destroy(_currentAttackObj);
    }

    /// <summary>
    /// Проверяет атаки
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckAttack()
    {
        while (isActiveAndEnabled)
        {
            if (_lastAttack + _cooldown < Time.time && _canAttack)
            {
                StartCoroutine(Attack());
            }

            yield return null;
        }
    }

}
