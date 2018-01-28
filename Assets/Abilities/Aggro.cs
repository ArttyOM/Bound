using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aggro : AbstractAbility
{

    public float Radius;

    override protected void Execute()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, Radius);
        foreach (Collider2D hitCollider in hitColliders)
        {
            Vector3 direction = hitCollider.transform.position;
            if (hitCollider.GetComponent<EnemyScript>() != null)
            {
                hitCollider.GetComponent<EnemyScript>().SetPlayer(owner.transform);
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
