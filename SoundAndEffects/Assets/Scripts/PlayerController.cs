using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private const float RaycastMaxDistance = 0.1f;
    public float jumpForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
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

        // If the player is grounded, apply the jump force
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
}
