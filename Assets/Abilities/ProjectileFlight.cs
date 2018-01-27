using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFlight : MonoBehaviour {

    public Vector2 direction = Vector2.zero;

    [SerializeField]
    float speed;

    // Use this for initialization
    void Start () {
		
	}

    public void StartWork()
    {
        direction = direction.normalized;
        Vector3 rot = new Vector3();
        if (direction.y > 0)
            rot.z = Mathf.Acos(direction.x) * Mathf.Rad2Deg;
        else
            rot.z = -Mathf.Acos(direction.x) * Mathf.Rad2Deg;
        rot.z += 270;
        transform.eulerAngles = rot;
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + new Vector3(direction.x, direction.y, 0.0f) * speed * Time.deltaTime;
        //transform.Translate(new Vector3(direction.x, direction.y, 0.0f) * speed * Time.deltaTime);
	}
}
