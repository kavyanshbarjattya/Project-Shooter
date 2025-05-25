using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public string enemyType; // Set by spawner when enemy is activated
    private Enemy_Pool pool;
    [SerializeField] GameObject _coin;

    public void Initialize(string typeName, Enemy_Pool enemyPool)
    {
        enemyType = typeName;
        pool = enemyPool;
    }

    public void ReturnToPool()
    {
        if (pool != null)
        {
            pool.Return(enemyType, gameObject);
        }
        else
        {
            Debug.LogWarning("No pool reference found on enemy.");
            gameObject.SetActive(false); // Fallback
        }
    }

    // Example: auto-return if off-screen
    private void OnBecameInvisible()
    {
        ReturnToPool();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            ReturnToPool();
            Instantiate(_coin, transform.position , Quaternion.identity);
            other.gameObject.SetActive(false);
        }
    }

}
