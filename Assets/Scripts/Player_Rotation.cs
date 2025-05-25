using UnityEngine;

public class Player_Rotation : MonoBehaviour
{
    [SerializeField] private FixedJoystick rightJoystick;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet_Pool bulletPool;
    [SerializeField] private Transform _gunHolder;
    [Range(0f, 1f)]
    [SerializeField] private float _deadZone = 0.1f;

    [SerializeField] Weapon_Switching _weapon_Switching;

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
                _gunHolder.localScale = new Vector3(_gunHolder.localScale.x,-0.3f, _gunHolder.localScale.z);
            }
            else
            {
                _gunHolder.localScale = new Vector3(_gunHolder.localScale.x, 0.3f, _gunHolder.localScale.z);
            }

            if (fireCooldown <= 0f)
            {
                if (_weapon_Switching.weaponInfos[_weapon_Switching._currentGunIndex]._ammoLeft > 0)
                {
                    Fire();
                }
                else
                {
                    print("No ammo left");
                }
                fireCooldown = _weapon_Switching.weaponInfos[_weapon_Switching._currentGunIndex].fireRate;
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

        _weapon_Switching.weaponInfos[_weapon_Switching._currentGunIndex]._ammoLeft--;
        _weapon_Switching.weaponInfos[_weapon_Switching._currentGunIndex]._ammoLeftText.text = _weapon_Switching.weaponInfos[_weapon_Switching._currentGunIndex]._ammoLeft.ToString();

        // Optional: camera shake on fire
        CameraShake.Instance?.Shake();
    }


   
}
