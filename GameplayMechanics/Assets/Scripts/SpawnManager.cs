using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject powerupPrefab;
    private float spawnRange = 9.0f;
    public int enemyCount;
    public int waveNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemiesInRandomPosition(waveNumber);
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount == 0)
        {
            waveNumber += 1;
            SpawnEnemiesInRandomPosition(waveNumber);
            SpawnPowerup();
        }
    }

    private void SpawnEnemiesInRandomPosition(int enemiesCount)
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            Instantiate(enemy, RandomPosition(), enemy.transform.rotation);
        }
    }

    private void SpawnPowerup()
    {
        Instantiate(powerupPrefab, RandomPosition(), powerupPrefab.transform.rotation);
    }

    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
    }
}
