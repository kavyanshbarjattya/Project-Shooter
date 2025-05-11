using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public string enemyType; // Set by spawner when enemy is activated
    private Enemy_Pool pool;

    public void Initialize(string typeName, Enemy_Pool enemyPool)
    {
        enemyType = typeName;
        pool = enemyPool;
    }

    public void ReturnToPool()
    {
        if (pool != null)
        {
            pool.ReturnEnemy(enemyType, gameObject);
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
            other.gameObject.SetActive(false);
        }
    }

}
