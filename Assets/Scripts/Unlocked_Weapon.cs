using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unlocked_Weapon : MonoBehaviour
{
    [SerializeField] Image[] _guns;
    // Update is called once per frame
    void OnEnable()
    {
        ShowGun();
    }

    void ShowGun()
    {
        for (int i = 0; i < _guns.Length; i++)
        {
            _guns[i].enabled = false;
            _guns[i].enabled = Game_Manager.Instance.IsWeaponUnlocked(i);
        }
    }
}
