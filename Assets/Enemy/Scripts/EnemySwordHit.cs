using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordHit : MonoBehaviour {

	[SerializeField] private GameObject mage;
	[SerializeField] private GameObject warrior;
	[SerializeField] private GameObject thisEnemy;
	public float rotSpeed = .5f;
	public float delayBeforeAttack = 1f;
	public float maxDistanceToAttack = 2f;

	private float _distanceToMage;
	private float _distanceToWarrior;
	// Use this for initialization
	void Start () {
		//Vector3 tempVect = (mage.transform.position - thisEnemy.transform.position);
		//_distanceToMage = tempVect.magnitude;
		//tempVect = (warrior.transform.position - thisEnemy.transform.position);
		//_distanceToWarrior = tempVect.magnitude;
		StartCoroutine(Attack());
	}


	//void Attack (GameObject target)
	//{
	//	Qutarnion direction = Quaternion.LookRotation (target.transform.position);
	//	thisEnemy.transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed*Time.DeltaTime);
	//}
	IEnumerator Attack ()
	{

		do {
			Vector3 tempVect = (mage.transform.position - thisEnemy.transform.position);
			_distanceToMage = tempVect.magnitude;
			tempVect = (warrior.transform.position - thisEnemy.transform.position);
			_distanceToWarrior = tempVect.magnitude;
			yield return null;
		//if (_distanceToMage < _distanceToWarrior)
		} while ((_distanceToMage > maxDistanceToAttack) && (_distanceToWarrior > maxDistanceToAttack));

		GameObject target;
		if (_distanceToMage < _distanceToWarrior)
			target = mage;
		else
			target = warrior;

		Debug.Log (target.name);
		Quaternion direction;
		do {
			direction = Quaternion.LookRotation (target.transform.position);

			//transform T = this.transform.LookAt(target.transform);
			thisEnemy.transform.LookAt (target.transform);
			//thisEnemy.transform.rotation = Quaternion.Lerp (transform.rotation, direction, rotSpeed * Time.deltaTime);
			yield return null;
		} while (false); // если чо-то не по плану, это тут
			
		yield return new WaitForSeconds (delayBeforeAttack);// 1f по умолчанию

		this.transform.LookAt (target.transform);

	}
}
