using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
	public string Name;

	public int Damage;

	public float Speed;

	public float Health;

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
		
		Messenger.Broadcast(GameEvent.MAGE_HEALTH_CHANGED);
		Messenger.Broadcast(GameEvent.WARRIOT_HEALTH_CHANGED);
	}
}