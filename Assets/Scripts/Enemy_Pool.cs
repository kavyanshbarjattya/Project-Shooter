using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyType
{
    public string name;
    public GameObject prefab;
    public int initialPoolSize = 10;
}

public class Enemy_Pool : MonoBehaviour
{
    public List<EnemyType> enemyTypes;

    private Dictionary<string, Queue<GameObject>> poolDict;
    private Dictionary<string, GameObject> prefabLookup;

    void Awake()
    {
        poolDict = new Dictionary<string, Queue<GameObject>>(enemyTypes.Count);
        prefabLookup = new Dictionary<string, GameObject>(enemyTypes.Count);

        foreach (var type in enemyTypes)
        {
            prefabLookup[type.name] = type.prefab;

            Queue<GameObject> enemyQueue = new Queue<GameObject>(type.initialPoolSize);

            for (int i = 0; i < type.initialPoolSize; i++)
            {
                GameObject obj = Instantiate(type.prefab, transform); // Set pool as parent
                obj.SetActive(false);
                enemyQueue.Enqueue(obj);
            }

            poolDict[type.name] = enemyQueue;
        }
    }

    public GameObject GetEnemy(string typeName)
    {
        if (!poolDict.TryGetValue(typeName, out var enemyQueue)) return null;

        GameObject enemy;

        if (enemyQueue.Count > 0)
        {
            enemy = enemyQueue.Dequeue();
        }
        else
        {
            if (!prefabLookup.TryGetValue(typeName, out var prefab)) return null;

            enemy = Instantiate(prefab, transform); // Parent for cleaner hierarchy
        }

        if (!enemy.activeInHierarchy)
            enemy.SetActive(true);
        // Assign type & pool
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.Initialize(typeName, this);
        }
        return enemy;
    }

    public void ReturnEnemy(string typeName, GameObject enemy)
    {
        if (enemy == null || !poolDict.ContainsKey(typeName)) return;

        enemy.SetActive(false);
        poolDict[typeName].Enqueue(enemy);
    }
}
