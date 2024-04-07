using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    public float spawnInterval = 1f; // Interval between each spawn

    private PlayerController playerController; // Reference to the PlayerController script

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        InvokeRepeating(nameof(SpawnAnimal), 0f, spawnInterval);
    }

    void SpawnAnimal()
    {
        // Pick a random animal from the array
        GameObject animalPrefab = animalPrefabs[Random.Range(0, animalPrefabs.Length)];

        float minX = playerController.leftMargin; // Get left margin from PlayerController
        float maxX = playerController.rightMargin; // Get right margin from PlayerController

        Instantiate(animalPrefab, RandomSpawnPosition(minX, maxX), animalPrefab.transform.rotation);
    }

    Vector3 RandomSpawnPosition(float minX, float maxX)
    {
        float x = Random.Range(minX, maxX);
        float z = 20f; // Adjust this based on your scene
        return new Vector3(x, 0f, z);
    }
}
