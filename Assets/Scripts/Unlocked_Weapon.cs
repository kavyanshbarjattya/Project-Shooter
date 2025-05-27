using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unlocked_Weapon : MonoBehaviour
{
    [SerializeField] GameObject[] _guns;
    // Update is called once per frame
    void OnEnable()
    {
        ShowGun();
    }

    void ShowGun()
    {
        for (int i = 0; i < _guns.Length; i++)
        {
            _guns[i].SetActive(false);
            _guns[i].SetActive(Game_Manager.Instance.IsWeaponUnlocked(i));
        }
    }
}
