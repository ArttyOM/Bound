using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAbility : AbstractAbility
{

    [SerializeField]
    GameObject Projectile;

    override protected void Execute()
    {
        var item = Instantiate(Projectile);
        var delta = owner.LastDir;
        item.transform.position = owner.transform.position + new Vector3(delta.x, delta.y)*0.5f;
        item.transform.Rotate(new Vector3(0, 0, 1), Mathf.PI / 2);
        item.GetComponent<ProjectileFlight>().direction = delta;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
