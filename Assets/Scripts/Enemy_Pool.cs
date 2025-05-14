using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyEntry
{
    public string enemyName;
    public GameObject prefab;
    public int initialPoolSize = 10;
}

public class Enemy_Pool : MonoBehaviour
{
    public List<EnemyEntry> enemyEntries;

    private readonly Dictionary<string, Queue<GameObject>> pool = new();
    private readonly Dictionary<string, GameObject> prefabMap = new();

    void Awake()
    {
        foreach (var entry in enemyEntries)
        {
            string key = entry.enemyName.Trim().ToLower();
            prefabMap[key] = entry.prefab;
            pool[key] = new Queue<GameObject>();

            for (int i = 0; i < entry.initialPoolSize; i++)
            {
                GameObject obj = Instantiate(entry.prefab);
                obj.SetActive(false);
                pool[key].Enqueue(obj);
            }
        }
    }

    public GameObject Get(string enemyName)
    {
        string key = enemyName.Trim().ToLower();

        if (!pool.ContainsKey(key))
        {
            Debug.LogWarning($"Enemy type '{enemyName}' not found in pool.");
            return null;
        }

        if (pool[key].Count > 0)
        {
            GameObject obj = pool[key].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        GameObject newObj = Instantiate(prefabMap[key]);
        return newObj;
    }

    public void Return(string enemyName, GameObject obj)
    {
        string key = enemyName.Trim().ToLower();

        if (!pool.ContainsKey(key))
        {
            Debug.LogWarning($"Trying to return unknown enemy type '{enemyName}' to pool.");
            Destroy(obj);
            return;
        }

        obj.SetActive(false);
        pool[key].Enqueue(obj);
    }

    public GameObject GetPrefab(string enemyName)
    {
        string key = enemyName.Trim().ToLower();
        return prefabMap.ContainsKey(key) ? prefabMap[key] : null;
    }
}
