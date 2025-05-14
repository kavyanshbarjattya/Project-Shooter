using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Switching : MonoBehaviour
{
    [SerializeField] SpriteRenderer _currentGun;
    [SerializeField] Sprite[] _guns;
    [SerializeField] int _currentGunIndex = 0;
    void Start()
    {
        _currentGun.sprite = _guns[_currentGunIndex];
    }

    public void GunSwitch()
    {
        if (_currentGunIndex < _guns.Length - 1)
        {
            _currentGunIndex++;
            _currentGun.sprite = _guns[_currentGunIndex];
        }
        else if(_currentGunIndex == _guns.Length - 1)
        {
            _currentGunIndex = 0;
            _currentGun.sprite = _guns[_currentGunIndex];

        }
    }
}
