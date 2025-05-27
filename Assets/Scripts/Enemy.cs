using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public string enemyType; // Set by spawner when enemy is activated
    private Enemy_Pool pool;
    [SerializeField] GameObject _coin;
    [SerializeField] int _health;

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
            if(_health > 0)
            {
                TakeDamage(other.gameObject.GetComponent<BulletMove>()._damage);
            }
            else
            {
                ReturnToPool();
            }
            Instantiate(_coin, transform.position , Quaternion.identity);
            other.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if(_health > 0)
        {
            _health -= damage;
        }
    }

}
