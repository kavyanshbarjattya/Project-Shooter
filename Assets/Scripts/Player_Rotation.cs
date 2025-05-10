using UnityEngine;

public class Player_Rotation : MonoBehaviour
{
    [SerializeField] private FixedJoystick rightJoystick;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet_Pool bulletPool;

    private float fireCooldown = 0f;
    private readonly Vector2 zeroVector = Vector2.zero; // Avoid repeated new Vector2
    private Quaternion rotationCache = Quaternion.identity;

    void Update()
    {
        float h = rightJoystick.Horizontal;
        float v = rightJoystick.Vertical;

        // Use squared magnitude threshold to avoid heavy Mathf.Sqrt
        if ((h * h + v * v) > 0.01f)
        {
            float angle = Mathf.Atan2(v, h) * Mathf.Rad2Deg;

            // Cache Quaternion to avoid allocation
            rotationCache.eulerAngles = new Vector3(0f, 0f, angle);
            transform.rotation = rotationCache;

            // Cooldown & shoot
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

    private void Fire()
    {
        GameObject bullet = bulletPool.GetBullet();
        if (bullet == null) return;

        bullet.transform.SetPositionAndRotation(firePoint.position, transform.rotation);
        bullet.SetActive(true);
    }
}
