using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Warrior : Character
{
	public float attackRadius = 1.4f;
	public float attackdmg = 5f;
	public float attackdmgSpetial = 5f;
	public float spetialAttackDuration = 15f;

	//[SerializeField] private GameObject warrior;
	[SerializeField] private GameObject mage;

	[SerializeField]
	private float _cooldown = 0.5f;

	[SerializeField]
	private GameObject _attackObj;

    private Character mageCharacter;

	private bool _canAttack = true;

	[SerializeField] private float _cooldownSpetial = 15f;
	private bool _canSpetialAttack = true;
	//private float _lastAttack = -10;

	private GameObject _currentAttackObj;

	private float _maxCooldown;
	private float _maxCooldownSpetial;
	void Awake()
	{
		_maxCooldown = _cooldown;//StartCoroutine(CheckAttack());
		_maxCooldownSpetial = _cooldownSpetial;
	}

	void Start(){
		_attackObj.transform.position = transform.position;
		//transform.rotation = new Quaternion(0, 180, 0);
	    mageCharacter = mage.GetComponent<Character>();
	}
	void Update()
	{
	    if (mageCharacter != null && !mageCharacter.IsDead && !IsDead)
	    {
	        _attackObj.transform.position = transform.position;
	        if (_cooldown > 0)
	        {
	            _cooldown -= Time.deltaTime;
	        }
	        else
	        {
	            _canAttack = true;
	        }

	        if ((Input.GetButtonDown("Fire1")) && (_canAttack))
	        {
	            _canAttack = false;
	            _cooldown = _maxCooldown;
	            //Debug.Log ("halo");
	            StartCoroutine(Attack());
	        }

	        if (_cooldownSpetial > 0)
	        {
	            _cooldownSpetial -= Time.deltaTime;
	        }
	        else
	        {
	            _canSpetialAttack = true;
	        }

	        if ((Input.GetButtonDown("Fire2") && (_canSpetialAttack)))
	        {
	            _canSpetialAttack = false;
	            _cooldownSpetial = _maxCooldownSpetial;
	            StartCoroutine(SpetialAttack());
	        }
	    }
	}
	public IEnumerator Attack()
	{
		_attackObj.SetActive (true);

		Collider2D[] hitColliders = Physics2D.OverlapCircleAll (transform.position, attackRadius);
		foreach (Collider2D hitCollider in hitColliders) {
			Vector3 direction = hitCollider.transform.position;
			//if (Vector3.Dot (transform.forward, direction) > .5f) {
				//Debug.Log (hitCollider.name);
				if ((hitCollider.gameObject == mage) || (hitCollider.gameObject == this.gameObject)) {
				} else
				{
					hitCollider.SendMessage ("DealDamage", attackdmg, SendMessageOptions.DontRequireReceiver);
                    AudioSource.PlayClipAtPoint(AttackClip, Camera.main.transform.position);
				}
			//}
		}      
		yield return new WaitForSeconds(0.2f);
		_attackObj.SetActive (false);
	}
		
	IEnumerator SpetialAttack(){
		//дописать вызов отключения движения магом
		float distance = Vector3.Distance (this.transform.position, mage.transform.position);
		Transform oldParent = mage.transform.parent;
		mage.transform.SetParent (this.transform);

		// вращаем spetialAttackDuration секунд
		float duration = 0f;
		do 
		{
			//наносим урон всем в радиусе
			Collider2D[] hitColliders = Physics2D.OverlapCircleAll (transform.position, attackRadius);
			foreach (Collider2D hitCollider in hitColliders) {
				Vector3 direction = hitCollider.transform.position;
				if ((hitCollider.gameObject == mage) || (hitCollider.gameObject == this.gameObject)) {
				} else {
					hitCollider.SendMessage ("DealDamage", attackdmgSpetial, SendMessageOptions.DontRequireReceiver);
					hitCollider.SendMessage ("Kill", SendMessageOptions.DontRequireReceiver);
				}

			}
			yield return new WaitForSeconds(0.2f);
			duration+=2f;
		} while (duration<= spetialAttackDuration);
		mage.transform.SetParent (oldParent);

		//yield return null;
	}

	protected override IEnumerator Die()
	{
		Messenger.Broadcast(GameEvent.YOU_DEAD);
		yield return new WaitForSeconds (3f);
        Messenger.Broadcast(GameEvent.NOT_DEAD);
        var game = ServiceLocator.Instance.ResolveSingleton<Game>();
        game.RestartGame();
    }
    /// <summary>
    /// Проверяет атаки
    /// </summary>
    /// <returns></returns>

}