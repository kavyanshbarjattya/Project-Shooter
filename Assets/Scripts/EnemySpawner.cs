using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy_Pool _enemyPool;

    public void SpawnEnemy()
    {
        GameObject _enemy  = _enemyPool.GetEnemy();
        if(_enemy == null)
        {
            return;
        }

        _enemy.transform.position = transform.position;
        _enemy.SetActive(true);
    }
}
