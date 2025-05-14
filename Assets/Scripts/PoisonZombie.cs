using UnityEngine;

public class PoisonZombie : EnemyBase
{
    public float speed = 1.2f;
    public float spitCooldown = 3f;
    public float spitRange = 6f;
    public GameObject poisonPrefab;
    public Transform firePoint;

    private float lastSpitTime;

    public override EnemyType GetEnemyType() => EnemyType.PoisonZombie;
    public override void Initialize() => lastSpitTime = 0f;

    void Update()
    {
        if (!player) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist > spitRange)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        else if (Time.time >= lastSpitTime + spitCooldown)
        {
            GameObject poison = Instantiate(poisonPrefab, firePoint.position, Quaternion.identity);
            poison.GetComponent<PoisonProjectile>().Initialize(player.position);
            lastSpitTime = Time.time;
        }
    }
}
