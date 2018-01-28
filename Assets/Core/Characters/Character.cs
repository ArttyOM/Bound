using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
public class Character : MonoBehaviour
{
	public string Name;

	public int Damage;

	public float Speed;

	public float Health;

    public Vector2 Direction = new Vector2(0, 1);

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
	bool isDead = false;
	void LateUpdate()
	{
		if ((Health == 0)&&(!isDead)) {
			isDead = true;
			StartCoroutine (Die());
		}
	}


	private IEnumerator Die() {
		this.transform.Rotate (-75, 0, 0);

		yield return new WaitForSeconds (1.5f);
		Destroy (this.gameObject); //закомментил чтобы было проще тестить (не умирая)
	}

    public Vector2 RotationDirection
    {
        get
        {
            return new Vector2(-Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad),
                Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad));
        }
    }

}