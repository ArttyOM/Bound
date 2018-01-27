using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrowScript : MonoBehaviour
{
    public Vector2 Direction { get; set; }

    public float Damage { get; set; }

    [SerializeField] private float _speed;

    // Use this for initialization
	void Start ()
	{
	    StartCoroutine(Die());
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position += (Vector3) Direction * _speed * Time.deltaTime;
	}

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.3f, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<Character>().DealDamage(Damage);
                //print("EEEE its attack");
            }
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this);
    }
}
