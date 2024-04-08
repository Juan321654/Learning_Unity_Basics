using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private Vector3 spawnPosition = new Vector3(20, 0, 0);
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private float spawnInterval = 2f;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player")?.GetComponent<PlayerController>();
        if (playerControllerScript == null)
        {
            Debug.LogError("PlayerController script not found on 'Player' GameObject.");
            return;
        }
        InvokeRepeating(nameof(SpawnObstacle), spawnDelay, spawnInterval);
    }

    // Spawns an obstacle if the game is not over
    void SpawnObstacle()
    {
        if (playerControllerScript != null && !playerControllerScript.gameOver)
        {
            Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);
        }
    }
}
