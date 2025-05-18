using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Switching : MonoBehaviour
{
    [SerializeField] SpriteRenderer _currentGun;
    [SerializeField] Sprite[] _guns;
    [SerializeField] GameObject[] _gunsUi;
    int _currentGunIndex = 0;
    void Start()
    {
        _currentGun.sprite = _guns[_currentGunIndex];
        _gunsUi[_currentGunIndex].SetActive(true);
    }

    public void GunSwitch()
    {
        if (_currentGunIndex < _guns.Length - 1)
        {
            _currentGunIndex++;
            _currentGun.sprite = _guns[_currentGunIndex];
            for (int i = 0; i < _gunsUi.Length; i++)
            {
                _gunsUi[_gunsUi.Length - 2].SetActive(false);
                _gunsUi[i].SetActive(true);
            }
        }
        else if(_currentGunIndex == _guns.Length - 1)
        {
            _currentGunIndex = 0;
            _currentGun.sprite = _guns[_currentGunIndex];
            for (int i = 0; i < _gunsUi.Length; i++)
            {
                _gunsUi[_gunsUi.Length - 1].SetActive(false);
            }
            _gunsUi[0].SetActive(true);

        }
    }
}
