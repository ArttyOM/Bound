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
        obj.GetComponent<ProjectileFlight>().direction = owner.LastDir;
        obj.GetComponent<ProjectileFlight>().StartWork();
    }
}
