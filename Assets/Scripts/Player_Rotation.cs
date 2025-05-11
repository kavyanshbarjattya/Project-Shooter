using UnityEngine;

public class Player_Rotation : MonoBehaviour
{
    [SerializeField] private FixedJoystick rightJoystick;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet_Pool bulletPool;
    [SerializeField] private SpriteRenderer[] _playerBody;
    [Range(0f, 1f)]
    [SerializeField] float _deadZone;

    private float fireCooldown = 0f;
    private readonly Vector2 zeroVector = Vector2.zero; // Avoid repeated new Vector2
    private Quaternion rotationCache = Quaternion.identity;

    void Update()
    {
        float h = rightJoystick.Horizontal;
        float v = rightJoystick.Vertical;
        float angle = Mathf.Atan2(v, h) * Mathf.Rad2Deg;

        // Cache Quaternion to avoid allocation
        rotationCache.eulerAngles = new Vector3(0f, 0f, angle);
        transform.rotation = rotationCache;
        PlayerFlip();
        // Use squared magnitude threshold to avoid heavy Mathf.Sqrt
        if ((h * h + v * v) > _deadZone)
        {
            if (fireCooldown <= 0f)
            {
                Fire();
                
                fireCooldown = fireRate;
            }
        }

        

        // Always update cooldown outside conditional
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    void Fire()
    {
        GameObject bullet = bulletPool.GetBullet();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);

        CameraShake.Instance.Shake();
    }

    private void PlayerFlip()
    {
       
        foreach (SpriteRenderer p in _playerBody)
        {
            if (rightJoystick.Horizontal < 0)
            {
                p.flipX = true;
                
            }
            else if(rightJoystick.Horizontal > 0)
            {
                p.flipX = false;
            }
        }
    }
}
