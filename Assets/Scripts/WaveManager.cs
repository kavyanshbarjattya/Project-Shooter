using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo
{
    public string enemyType;
    public int count;
    public float spawnDelay;
}

[System.Serializable]
public class Wave
{
    public List<EnemySpawnInfo> enemies = new List<EnemySpawnInfo>();
}

public class WaveManager : MonoBehaviour
{
    [Header("Waves and Spawning")]
    public List<Wave> waves = new List<Wave>();    // Defined in Inspector
    public Transform[] spawnPoints;                // Assign in Inspector
    public float waveDelay = 5f;

    [Header("References")]
    public Enemy_Pool enemyPool;                   // Assign in Inspector

    private int currentWaveIndex = 0;
    private WaitForSeconds waitBetweenWaves;
    private WaitForSeconds shortSpawnDelay = new WaitForSeconds(0.05f); // fallback if no delay

    void Start()
    {
        waitBetweenWaves = new WaitForSeconds(waveDelay);
        StartCoroutine(HandleWaves());
    }

    IEnumerator HandleWaves()
    {
        while (true)
        {
            if (currentWaveIndex >= waves.Count)
            {
                GenerateNextWave();
            }

            yield return StartCoroutine(SpawnWave(waves[currentWaveIndex]));
            currentWaveIndex++;

            yield return waitBetweenWaves;
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        // Flatten enemy list into a spawn queue
        List<EnemySpawnInfo> spawnQueue = new List<EnemySpawnInfo>();
        foreach (var info in wave.enemies)
        {
            for (int i = 0; i < info.count; i++)
            {
                spawnQueue.Add(new EnemySpawnInfo
                {
                    enemyType = info.enemyType,
                    spawnDelay = info.spawnDelay
                });
            }
        }

        // Shuffle enemy spawn order
        for (int i = 0; i < spawnQueue.Count; i++)
        {
            int randIndex = Random.Range(i, spawnQueue.Count);
            var temp = spawnQueue[i];
            spawnQueue[i] = spawnQueue[randIndex];
            spawnQueue[randIndex] = temp;
        }

        // Spawn all enemies
        foreach (var info in spawnQueue)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = enemyPool.Get(info.enemyType);
            if (enemy != null)
            {
                enemy.transform.position = spawnPoint.position;
                enemy.SetActive(true);
            }

            yield return info.spawnDelay > 0f ? new WaitForSeconds(info.spawnDelay) : shortSpawnDelay;
        }
    }

    void GenerateNextWave()
    {
        if (waves.Count == 0) return;

        Wave baseWave = waves[0];
        Wave newWave = new Wave();

        int difficulty = currentWaveIndex + 1;

        foreach (var baseInfo in baseWave.enemies)
        {
            EnemySpawnInfo newInfo = new EnemySpawnInfo
            {
                enemyType = baseInfo.enemyType,
                count = baseInfo.count + difficulty,
                spawnDelay = Mathf.Max(0.1f, baseInfo.spawnDelay - 0.01f * difficulty)
            };

            newWave.enemies.Add(newInfo);
        }

        waves.Add(newWave);
    }
}
