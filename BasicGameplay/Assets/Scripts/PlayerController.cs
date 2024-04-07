using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 25f;
    public float leftMargin = -15f;
    public float rightMargin = 15f;

    public GameObject projectilePrefab;

    void Update()
    {
        HandleMovement();
        ClampToBounds();
        HandleProjectile();
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 movement = moveDirection * speed * Time.deltaTime;

        transform.position += movement;
    }

    void ClampToBounds()
    {
        float clampedX = Mathf.Clamp(transform.position.x, leftMargin, rightMargin);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    void HandleProjectile()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
}



//The expression new Vector3(horizontalInput, 0f, verticalInput) creates a new Vector3 object using the horizontalInput for the X component, 0f(zero)
//for the Y component, and verticalInput for the Z component.

//horizontalInput typically comes from the player's input along the horizontal axis, such as pressing left or right keys or moving a joystick horizontally.
//0f is used here because in many cases, you might not want the player to move vertically.If you're making a 2D game or a game where vertical movement isn't needed, 
//setting the Y component to zero is common.In a 3D game, you might use a different value depending on your game mechanics.
//verticalInput is similar to horizontalInput but represents input along the vertical axis, such as pressing up or down keys or moving a joystick vertically.
//The normalized property of a Vector3 normalizes the vector, which means it scales its components so that the magnitude(length) of the vector becomes 1 while
//preserving its direction. This is useful for ensuring consistent movement speed regardless of the direction of input.For movement, it ensures that diagonal 
//movement doesn't become faster than horizontal or vertical movement, which can happen if you simply use raw input values without normalization.