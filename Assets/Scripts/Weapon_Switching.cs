using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Weapon_Switching : MonoBehaviour
{
    [SerializeField] SpriteRenderer _currentGun;
    public WeaponInfo[] weaponInfos;
    [SerializeField] Bullet_Pool _bulletPool;
    [HideInInspector] public int _currentGunIndex = 0;


    void Start()
    {
        _currentGun.sprite = weaponInfos[_currentGunIndex]._weapon;
        weaponInfos[_currentGunIndex]._weaponUI.SetActive(true);

        for (int i = 0; i < weaponInfos.Length; i++)
        {
            weaponInfos[i]._ammoLeft = weaponInfos[i]._totalAmmo;
            weaponInfos[i]._totalAmmoTxt.text = "/ " + weaponInfos[i]._totalAmmo.ToString();
            weaponInfos[i]._ammoLeftText.text = weaponInfos[i]._ammoLeft.ToString();
        }
    }

    public void GunSwitch()
    {
        if (_currentGunIndex < weaponInfos.Length - 1)
        {
            _currentGunIndex++;
           
        }
        else if (_currentGunIndex == weaponInfos.Length - 1)
        {
            _currentGunIndex = 0;
        }

        _currentGun.sprite = weaponInfos[_currentGunIndex]._weapon;

        for (int i = 0; i < weaponInfos.Length; i++)
        {
            weaponInfos[i]._weaponUI.SetActive(false);
        }
        weaponInfos[_currentGunIndex]._weaponUI.SetActive(true);
    }
}

[System.Serializable]

public class WeaponInfo
{
    public string _gunName;
    public Sprite _weapon;
    public GameObject _weaponUI;
    public Sprite _ammo;
    public TextMeshProUGUI _ammoLeftText, _totalAmmoTxt;
    public int _totalAmmo;
    [HideInInspector] public int _ammoLeft;
    public Image _gunImage;
}
