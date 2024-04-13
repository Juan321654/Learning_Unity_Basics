using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public GameObject focalPoint;
    public GameObject powerupIndicator;
    public float speed = 5f;
    public bool hasPowerUp = false;
    private float powerUpStrength = 15;
    private const float powerUpDuration = 7f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // FixedUpdate is called once per physics step
    void FixedUpdate()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * verticalInput, ForceMode.Force);
        PowerupFollowPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            if (powerupIndicator != null)
            {
                powerupIndicator.gameObject.SetActive(true);
            }
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    void PowerupFollowPlayer()
    {
        if (hasPowerUp && powerupIndicator != null)
        {
            powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerUp = false;
        if (powerupIndicator != null)
        {
            powerupIndicator.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
                enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            }
        }
    }
}
