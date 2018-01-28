using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wizard : Character
{
	[SerializeField] private GameObject warrior;
	public float spetialAttackSpeed = 360f;
	public float attackRadius = 1.4f;
	public float attackdmgSpetial = 5f;
	public float spetialAttackDuration = 3f;
	public GameObject pivotForSpetialAtack;
	void FixedUpdate(){
		pivotForSpetialAtack.transform.position = this.transform.position;
		pivotForSpetialAtack.transform.Rotate(Vector3.forward*spetialAttackSpeed*Time.fixedDeltaTime);

	}

	public IEnumerator SpecialAttack(){
		//дописать вызов отключения движения магом
		float distance = Vector3.Distance (this.transform.position, warrior.transform.position);
		Transform oldParent = warrior.transform.parent;

		//pivotForSpetialAtack.transform.rotation = this.transform.rotation;
		warrior.transform.SetParent (pivotForSpetialAtack.transform);

		//StartCoroutine (RotateMage());
		// вращаем spetialAttackDuration секунд
		float duration = 0f;
		do 
		{	
			warrior.GetComponent<Collider2D>().isTrigger =true;
			//наносим урон всем в радиусе
			Collider2D[] hitColliders = Physics2D.OverlapCircleAll (transform.position, attackRadius);
			foreach (Collider2D hitCollider in hitColliders) {
				Vector3 direction = hitCollider.transform.position;
				if ((hitCollider.gameObject == warrior) || (hitCollider.gameObject == this.gameObject)) {
				} else {
					hitCollider.SendMessage ("DealDamage", attackdmgSpetial, SendMessageOptions.DontRequireReceiver);
					hitCollider.SendMessage ("Kill", SendMessageOptions.DontRequireReceiver);
				}

			}
			yield return new WaitForSeconds(0.2f);
			duration+=0.2f;
		} while (duration<= spetialAttackDuration);
		warrior.GetComponent<Collider2D>().isTrigger = false;
		warrior.transform.SetParent (oldParent);

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
}