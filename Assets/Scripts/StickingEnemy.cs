using UnityEngine;
using System.Collections;
public class StickingEnemy : EnemyBase
{
    public float speed = 1.5f;
    public float jumpRange = 3f;
    public float explodeDelay = 2f;
    public int damage = 25;

    private bool isSticking = false;

    public override EnemyType GetEnemyType() => EnemyType.Sticking;
    public override void Initialize()
    {
        isSticking = false;
        transform.SetParent(null);
    }

    void Update()
    {
        if (!player || isSticking) return;
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist > jumpRange)
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        else
            StartCoroutine(StickAndExplode());
    }

    IEnumerator StickAndExplode()
    {
        isSticking = true;
        transform.SetParent(player);
        transform.localPosition = Vector3.zero;

        yield return new WaitForSeconds(explodeDelay);

        player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        gameObject.SetActive(false); // Or return to pool
    }
}