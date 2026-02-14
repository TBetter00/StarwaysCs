using UnityEngine;
using System.Collections;

public class EnemyManagerC : MonoBehaviour
{
    private Gamemanager gamemanager;
    public GameObject enemyPrefab;
    [SerializeField] private int maxEnemySpawn = 5;
    [SerializeField] private float spawnDelay = 2.0f;
    [Header("Spawn Settings")]
    [SerializeField] private float spawnHeight = 1.0f;
    [SerializeField] private Vector3 spawnCenter = Vector3.zero;
    [SerializeField] private float spawnWidth = 100f;
    [SerializeField] private float spawnDepth = 100f;

    private int currentEnemyCount = 0;

    void Start()
    {
        // gamemanager = FindAnyObjectByType<Gamemanager>();
        // Start the infinite spawning loop
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true) // Keep running forever
        {
            if (currentEnemyCount < maxEnemySpawn)
            {
                SpawnSingleEnemy();
            }

            // Wait for a few seconds before trying to spawn again
            yield return new WaitForSeconds(spawnDelay / (1 + gamemanager.timeRunner * 0.01f));
        }
    }

    void SpawnSingleEnemy()
    {
        float randomX = Random.Range(spawnCenter.x - spawnWidth / 2, spawnCenter.x + spawnWidth / 2);
        float randomZ = Random.Range(spawnCenter.z - spawnDepth / 2, spawnCenter.z + spawnDepth / 2);
        Vector3 spawnPosition = new Vector3(randomX, spawnHeight, randomZ);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemyCount++;

        // Tell the enemy to notify this manager when it is destroyed
        enemyC status = newEnemy.AddComponent<enemyC>();
        status.manager = this;
    }

    public void EnemyDied()
    {
        // gamemanager.point += 5;
        currentEnemyCount--;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(spawnCenter.x, spawnHeight, spawnCenter.z), new Vector3(spawnWidth, 0.1f, spawnDepth));
    }
}
