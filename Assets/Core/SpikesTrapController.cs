using UnityEngine;

namespace Assets.Core
{
	public class SpikesTrapController: MonoBehaviour
	{
		public Animator animator;

		public float damageAmount;
		
		private void OnTriggerEnter2D(Collider2D collision)
		{
			var character = collision.transform.GetComponent<Character>();
			if (character != null)
			{
				animator.Play("Open");
				character.DealDamage(damageAmount);
			}
		}
	}
}