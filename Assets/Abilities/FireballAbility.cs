using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballAbility : AbstractAbility
{

    /// <summary>
    /// Префаб для фаербола
    /// </summary>
    [SerializeField]
    GameObject Projectile;

    protected override void Execute()
    {
        var obj = Instantiate(Projectile);
        obj.transform.position = transform.position;
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
