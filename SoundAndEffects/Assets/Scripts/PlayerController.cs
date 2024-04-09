using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimation;
    private const float RaycastMaxDistance = 0.1f;
    private float jumpForce = 400f;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    void Jump()
    {
        // Perform raycast to check if the player is grounded
        if (!IsGrounded()) return;
        if (gameOver) return;

        // If the player is grounded, apply the jump force
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playerAnimation.SetTrigger("Jump_trig");
    }

    // Alternative to using OnCollisionEnter, Raycast provides better precision for ground detection.
    // Raycast allows for more precise control over ground detection compared to collision events, but it may involve more calculations.

    bool IsGrounded()
    {
        // Cast a ray downwards to check for ground collision
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, RaycastMaxDistance))
        {
            // Debug information about the object hit by the raycast
            //Debug.Log("Hit object: " + hit.collider.gameObject.name);
            //Debug.Log("Surface normal: " + hit.normal);

            // Adjust the distance as needed to match your platform heights
            return true; // Grounded
        }
        return false; // Not grounded
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Obstacle"))
        if (collision.gameObject.CompareTag(TagManager.Obstacle)) // from the custom class to get autocomplete for tags
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnimation.SetBool("Death_b", true);
            playerAnimation.SetInteger("DeathType_int", 1);
        }
    }
}