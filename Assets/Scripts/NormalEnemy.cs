using UnityEngine;

public class NormalEnemy : EnemyBase
{
    public float speed = 2f;
    public float attackRange = 1.5f;
    public int damage = 10;
    public float attackCooldown = 1f;

    private float lastAttackTime;

    public override EnemyType GetEnemyType() => EnemyType.Normal;
    public override void Initialize() => lastAttackTime = 0f;

    void Update()
    {
        if (!player) return;
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist > attackRange)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        else if (Time.time >= lastAttackTime + attackCooldown)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            lastAttackTime = Time.time;
        }
    }
}