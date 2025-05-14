using UnityEngine;

public class PoisonProjectile : MonoBehaviour
{
    public float speed = 4f;
    public int damage = 5;
    private Vector2 direction;

    public void Initialize(Vector2 targetPos)
    {
        direction = (targetPos - (Vector2)transform.position).normalized;
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
