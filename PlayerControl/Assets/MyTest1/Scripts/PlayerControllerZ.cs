using UnityEngine;

public class PlayerControllerZ : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 5.0f;
    public float jumpHeightMultiplier = 1.0f; // Variable to adjust jump height

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0.0f, forwardInput) * speed * Time.deltaTime;
        rb.MovePosition(rb.position + transform.TransformDirection(movement));

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce * jumpHeightMultiplier, ForceMode.Impulse); // Apply jump height multiplier
        }
    }
}
