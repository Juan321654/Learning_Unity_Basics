using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private float maximumY = 19f; // Maximum height the player can reach
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            FloatUpwards();
        }
    }

    void FloatUpwards()
    {
        // Apply upward force only if below the height limit
        if (transform.position.y < maximumY)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }
        else
        {
            CheckPlayerBoundary();
        }
    }

    void CheckPlayerBoundary()
    {
        // If the player exceeds the height limit, adjust their position and velocity
        if (transform.position.y > maximumY)
        {
            // Stop the player's upward movement by setting the y component of the velocity to 0
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
            // Force the player's position back down to the maximum allowed height
            transform.position = new Vector3(transform.position.x, maximumY, transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // If player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }
        // If player collides with money, trigger fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up * (floatForce - 5f), ForceMode.Impulse);
        }
    }
}
