using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject player;
    public float moveSpeed = 2f;
    private float boundaryY = 10;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get reference to Rigidbody component
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        DeleteEnemyIfOutOfBound();
    }

    void Move()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;

            rb.AddForce(direction * moveSpeed, ForceMode.Force);
        }
    }

    void DeleteEnemyIfOutOfBound()
    {
        if (transform.position.y < -boundaryY)
        {
            Destroy(gameObject);
        }
    }
}
