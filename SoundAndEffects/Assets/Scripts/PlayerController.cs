using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnimation;
    public AudioSource playerAudioSource;
    private const float RaycastMaxDistance = 0.1f;
    private float jumpForce = 400f;
    public bool gameOver;
    
    [Header("Particles")]
    public ParticleSystem explosionParticles;
    public ParticleSystem dirtParticle;
    
    [Header("Sounds")]
    public AudioClip jumpClipSound;
    public AudioClip crashClipSound;
    private float clipVolume = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimation = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        if (IsGrounded() && !gameOver)
        {
            // Check if the dirtParticle is not already playing
            if (!dirtParticle.isPlaying)
            {
                dirtParticle.Play(); // Start playing the dirt particles
            }
        }
    }

    void Jump()
    {
        // Perform raycast to check if the player is grounded
        if (!IsGrounded()) return;
        if (gameOver) return;

        // If the player is grounded, apply the jump force
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playerAnimation.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        playerAudioSource.PlayOneShot(jumpClipSound, clipVolume);
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
            playerAnimation.SetBool("Death_b", true);
            playerAnimation.SetInteger("DeathType_int", 1);
            explosionParticles.Play();
            dirtParticle.Stop();
            playerAudioSource.PlayOneShot(crashClipSound, clipVolume + 1.5f);
        }
    }
}