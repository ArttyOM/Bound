using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
	public string Name;

	public int Damage;

	public float Speed;

	public float Health;

    public Vector2 LastDir = new Vector2(0, 0);

    public List<AbstractAbility> Abilities;

	public void DealDamage(float amount)
	{
		if (Health - amount > 0)
		{
			Health -= amount;
		}
		else
		{
			Health = 0;
		}
        print(amount + " AAAY");
		
		Messenger.Broadcast(GameEvent.MAGE_HEALTH_CHANGED);
		Messenger.Broadcast(GameEvent.WARRIOT_HEALTH_CHANGED);
	}
}