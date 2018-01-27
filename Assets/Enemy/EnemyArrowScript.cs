using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrowScript : MonoBehaviour
{
    public Vector2 Direction { get; set; }

    [SerializeField] private float _speed;

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position += (Vector3) Direction * _speed * Time.deltaTime;
	}
}
