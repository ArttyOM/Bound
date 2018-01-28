using UnityEngine;

public class EnemyArrowScript : MonoBehaviour
{
    public Vector2 Direction { get; set; }

    public float Damage { get; set; }

    [SerializeField] private float _speed;

    private const float LIFE_TIME = 3.0f;

    private float _bornTime;

    // Use this for initialization
	void Start ()
	{
	    _bornTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position += (Vector3) Direction * _speed * Time.deltaTime;
        if (_bornTime + LIFE_TIME < Time.time)
            Destroy(this.gameObject);
	}

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.3f, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<Character>().DealDamage(Damage);
            }
        }
    }
}
