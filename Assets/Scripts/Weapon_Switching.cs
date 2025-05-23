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
        for (int i = 0; i < weaponInfos.Length; i++)
        {
            // Sync from GameData
            weaponInfos[i]._isUnlocked = Game_Manager.Instance.IsWeaponUnlocked(i);

            weaponInfos[i]._ammoLeft = weaponInfos[i]._totalAmmo;
            weaponInfos[i]._totalAmmoTxt.text = "/ " + weaponInfos[i]._totalAmmo.ToString();
            weaponInfos[i]._ammoLeftText.text = weaponInfos[i]._ammoLeft.ToString();
            weaponInfos[i]._weaponUI.SetActive(false);
        }

        if (weaponInfos[_currentGunIndex]._isUnlocked)
        {
            SetWeapon(_currentGunIndex);
        }
    }


    public void GunSwitch()
    {
        for(int i = 0;i < weaponInfos.Length; i++)
        {
            weaponInfos[i]._weaponUI.SetActive(false);
        }
        int startIndex = _currentGunIndex;
        do
        {
            _currentGunIndex = (_currentGunIndex + 1) % weaponInfos.Length;

            if (weaponInfos[_currentGunIndex]._isUnlocked)
            {
                SetWeapon(_currentGunIndex);
                return;
            }
        }
        while (_currentGunIndex != startIndex); // Prevent infinite loop if only one gun is unlocked
    }
    private void SetWeapon(int index)
    {
        _currentGun.sprite = weaponInfos[index]._weapon;
        weaponInfos[index]._weaponUI.SetActive(true);

        weaponInfos[index]._ammoLeftText.text = weaponInfos[index]._ammoLeft.ToString();
        weaponInfos[index]._totalAmmoTxt.text = "/ " + weaponInfos[index]._totalAmmo.ToString();
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
    public bool _isUnlocked;
}
