using UnityEngine;

public class Player_Rotation : MonoBehaviour
{
    [SerializeField] private FixedJoystick rightJoystick;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet_Pool bulletPool;
    [SerializeField] private SpriteRenderer _gunSprite;
    [Range(0f, 1f)]
    [SerializeField] private float _deadZone = 0.1f;

    private float fireCooldown = 0f;
    private Quaternion rotationCache = Quaternion.identity;

    void Update()
    {
        float h = rightJoystick.Horizontal;
        float v = rightJoystick.Vertical;
        // Only update rotation and fire if joystick input is above the deadzone
        if ((h * h + v * v) > _deadZone)
        {
            float angle = Mathf.Atan2(v, h) * Mathf.Rad2Deg;
            rotationCache.eulerAngles = new Vector3(0f, 0f, angle);
            transform.rotation = rotationCache;

            if(h < 0)
            {
                _gunSprite.flipY = true;
            }
            else
            {
                _gunSprite.flipY = false;
            }

            if (fireCooldown <= 0f)
            {
                Fire();
                fireCooldown = fireRate;
            }
        }

        // Always update cooldown
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }

    void Fire()
    {
        GameObject bullet = bulletPool.GetBullet();
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);

        // Optional: camera shake on fire
        CameraShake.Instance?.Shake();
    }


   
}
