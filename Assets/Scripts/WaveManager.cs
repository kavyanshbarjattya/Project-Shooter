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
    public Enemy_Pool enemyPool;

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
        for (int i = 0; i < wave.enemies.Count; i++)
        {
            EnemySpawnInfo info = wave.enemies[i];
            WaitForSeconds spawnDelay = info.spawnDelay > 0f ? new WaitForSeconds(info.spawnDelay) : shortSpawnDelay;

            for (int j = 0; j < info.count; j++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                GameObject enemy = enemyPool.GetEnemy(info.enemyType);
                if (enemy != null)
                {
                    enemy.transform.position = spawnPoint.position;
                    enemy.SetActive(true);
                }

                yield return spawnDelay;
            }
        }
    }

    void GenerateNextWave()
    {
        if (waves.Count == 0) return;

        Wave baseWave = waves[0];
        Wave newWave = new Wave();

        int difficulty = currentWaveIndex + 1;

        for (int i = 0; i < baseWave.enemies.Count; i++)
        {
            EnemySpawnInfo baseInfo = baseWave.enemies[i];
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
