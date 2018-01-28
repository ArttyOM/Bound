using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFlight : MonoBehaviour {

    public Vector2 direction = Vector2.zero;

    /// <summary>
    /// Дамаг фаербола
    /// </summary>
    [SerializeField] private float _damage;

    /// <summary>
    /// Скорость
    /// </summary>
    [SerializeField] private float speed;

    /// <summary>
    /// Начало работы
    /// </summary>
    public void StartWork()
    {
        direction = direction.normalized;
        if (direction == Vector2.zero)
            direction = Vector2.up;

        Vector3 rot = new Vector3();
        if (direction.y > 0)
            rot.z = Mathf.Acos(direction.x) * Mathf.Rad2Deg;
        else
            rot.z = -Mathf.Acos(direction.x) * Mathf.Rad2Deg;
        rot.z += 270;
        transform.eulerAngles = rot;
        StartCoroutine(Die());
    }

    /// <summary>
    /// Самоуничтожение через какое-то время
    /// </summary>
    /// <returns></returns>
    IEnumerator Die()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
	
	// Update is called once per frame
    void Update()
    {
        transform.position = transform.position
                             + new Vector3(direction.x, direction.y, 0.0f) * speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.5f, Vector2.zero);
        if (hit.collider != null && hit.collider.tag == "Enemy")
        {
            hit.collider.GetComponent<Character>().DealDamage(_damage);
            Destroy(this.gameObject);
            Debug.LogAssertion("AAAAA");
        }
    }
}
