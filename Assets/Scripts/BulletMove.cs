using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float lifetime = 10f;
    public int _damage;

    public SpriteRenderer _bulletSprite;
    Weapon_Switching _weaponSwitch;
    private float timer;


    private void Awake()
    {
        _weaponSwitch = FindObjectOfType<Weapon_Switching>();
        _bulletSprite = GetComponent<SpriteRenderer>();

    }

    void OnEnable()
    {
        timer = 0f;
        _bulletSprite.sprite = _weaponSwitch.weaponInfos[_weaponSwitch._currentGunIndex]._ammo;
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            gameObject.SetActive(false);
        }
    }
}
