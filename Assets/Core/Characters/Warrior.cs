using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{
	public float attackRadius = 1.4f;
	public float attackdmg = 5f;

	//[SerializeField] private GameObject warrior;
	[SerializeField] private GameObject mage;

	[SerializeField]
	private float _cooldown = 0.5f;

	[SerializeField]
	private GameObject _attackObj;

	private bool _canAttack = true;

	//private float _lastAttack = -10;

	private GameObject _currentAttackObj;

	private float _maxCooldown;
	void Awake()
	{
		_maxCooldown = _cooldown;//StartCoroutine(CheckAttack());

	}

	void Start(){
		_attackObj.transform.position = transform.position;
		//transform.rotation = new Quaternion(0, 180, 0);
	}
	void Update()
	{
		_attackObj.transform.position = transform.position;
		_cooldown -= Time.deltaTime;
		if (_cooldown == 0)
			_canAttack = true;
		if (Input.GetButtonDown("Fire1")) {
			_canAttack = false;
			//Debug.Log ("halo");
			StartCoroutine(Attack());
		}
	}
	IEnumerator Attack()
	{
		_attackObj.SetActive (true);

		Collider2D[] hitColliders = Physics2D.OverlapCircleAll (transform.position, attackRadius);
		foreach (Collider2D hitCollider in hitColliders) {
			Vector3 direction = hitCollider.transform.position;
			if (Vector3.Dot (transform.forward, direction) > .5f) {
				//Debug.Log (hitCollider.name);
				if ((hitCollider.gameObject == mage) || (hitCollider.gameObject == this.gameObject)) {
				} else {
					hitCollider.SendMessage ("DealDamage", attackdmg, SendMessageOptions.DontRequireReceiver);
				}
			}
		}      
		yield return new WaitForSeconds(0.2f);
		_attackObj.SetActive (false);
	}
	/// <summary>
	/// Проверяет атаки
	/// </summary>
	/// <returns></returns>

}