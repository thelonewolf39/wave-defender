using UnityEngine;
using System.Collections;

public class EnemyWaveSpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject enemyPrefab;
    public Transform player;

    [Header("Wave Settings")]
    public int enemiesPerWave = 5;
    public int incrementPerWave = 3;
    public float safeRadius = 2f;

    private Camera cam;
    private int currentWave = 1;
    private int aliveEnemies = 0;

    void Start()
    {
        cam = Camera.main;
        StartCoroutine(WaveLoop());
    }

    IEnumerator WaveLoop()
    {
        while (true)
        {
            SpawnWave();

            // Wait until all enemies are dead
            while (aliveEnemies > 0)
            {
                yield return null;
            }

            // Prepare next wave
            currentWave++;
            enemiesPerWave += incrementPerWave;

            // Optional buffer time after wave finishes
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnWave()
    {
        aliveEnemies = enemiesPerWave;

        for (int i = 0; i < enemiesPerWave; i++)
        {
            Vector2 spawnPos = GetValidSpawnPosition();
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

            // Let the enemy notify the spawner on death
            enemy.AddComponent<EnemyDeathNotifier>().spawner = this;
        }
    }

    // Called by enemy when it dies
    public void OnEnemyKilled()
    {
        aliveEnemies--;
    }

    Vector2 GetValidSpawnPosition()
    {
        Vector2 pos;

        do
        {
            pos = GetRandomOnScreenPosition();
        }
        while (Vector2.Distance(pos, player.position) < safeRadius);

        return pos;
    }

    Vector2 GetRandomOnScreenPosition()
    {
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        float x = Random.Range(cam.transform.position.x - camWidth / 2f,
                               cam.transform.position.x + camWidth / 2f);

        float y = Random.Range(cam.transform.position.y - camHeight / 2f,
                               cam.transform.position.y + camHeight / 2f);

        return new Vector2(x, y);
    }
}
