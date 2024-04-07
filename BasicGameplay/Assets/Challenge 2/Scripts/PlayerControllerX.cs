using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float spawnCooldown = 1.0f; // Cooldown time in seconds

    private float lastSpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = -spawnCooldown; // To allow spawning immediately at start
    }

    // Update is called once per frame
    void Update()
    {
        // Check if enough time has passed since the last spawn
        if (Time.time - lastSpawnTime >= spawnCooldown)
        {
            // On spacebar press, send dog
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                lastSpawnTime = Time.time; // Update last spawn time
            }
        }
    }
}
