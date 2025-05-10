using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pool : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] float _poolSize;

    private List<GameObject> _enemies;
    void Start()
    {
        _enemies = new List<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject _enemy = Instantiate(_enemyPrefab);
            _enemy.SetActive(false);
            _enemies.Add(_enemy);
        }
    }

    public GameObject GetEnemy()
    {
        foreach (GameObject _enemy in _enemies)
        {
            if (!_enemy.activeInHierarchy)
            {
                return _enemy;
            }
        }
        GameObject newEnemy = Instantiate(_enemyPrefab);
        newEnemy.SetActive(false);
        return newEnemy;
    }
}
