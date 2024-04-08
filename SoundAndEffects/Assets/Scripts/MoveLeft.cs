using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 15f;
    private PlayerController playerControllerScript;
    private float leftBoundary = -10f;
    
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameOver == false)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if (transform.position.x < leftBoundary && gameObject.CompareTag(TagManager.Obstacle))
        {
            Destroy(gameObject);
        }
    }
}
