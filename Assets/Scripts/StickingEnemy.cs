using UnityEngine;
using System.Collections;
public class StickingEnemy : EnemyBase
{
    public float speed = 1.5f;
    public float jumpRange = 3f;
    public float explodeDelay = 2f;
    public int damage = 25;

    [SerializeField] float _blastRadius , _scale_multiplier;
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] Transform _blastVisual;


    private bool isSticking = false;
    private float _currentSpeed;

    public override EnemyType GetEnemyType() => EnemyType.Sticking;
    public override void Initialize()
    {
        isSticking = false;
        _currentSpeed = speed;
        _blastVisual.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!player || isSticking) return;
        float dist = Vector2.Distance(transform.position, player.position);

        if (dist > jumpRange)
            transform.position = Vector2.MoveTowards(transform.position, player.position, _currentSpeed * Time.deltaTime);
        else
            StartCoroutine(StickAndExplode());
    }

    IEnumerator StickAndExplode()
    {
        isSticking = true;
        transform.position = player.position;
        // animation for blast 
        _blastVisual.localScale = new Vector2(_blastRadius * _scale_multiplier, _blastRadius * _scale_multiplier);
        _blastVisual.gameObject.SetActive(true);
        _currentSpeed = 0;
        yield return new WaitForSeconds(explodeDelay);

        if (BlastArea(transform.position, _blastRadius, _playerLayer))
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            print("Player Damaged");
            _blastVisual.gameObject.SetActive(false);
            if (Application.isMobilePlatform)
            {
                Handheld.Vibrate();
                print("Vibrating");
            }
            CameraShake.Instance?.Shake();
        }
        gameObject.SetActive(false); // Or return to pool
        _blastVisual.gameObject.SetActive(false);
    }


    bool BlastArea(Vector2 pos, float radius , LayerMask layerMask)
    {
        RaycastHit2D[] hit = Physics2D.CircleCastAll(pos,radius,Vector2.zero,layerMask);

        foreach (RaycastHit2D h in hit)
        {
            if (h.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;        
    }
    private void OnValidate()
    {
        _blastVisual.localScale = new Vector2(_blastRadius * _scale_multiplier , _blastRadius * _scale_multiplier);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _blastRadius );
    }
}