using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammos_Restore : MonoBehaviour
{
    [SerializeField] Weapon_Switching _weapon_Switching;
    // Start is called before the first frame update
    void Awake()
    {
        _weapon_Switching = FindObjectOfType<Weapon_Switching>();   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ammo"))
        {
            RestoreAmmo();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Coins"))
        {
            Coins_Manager.instance.AddCoins(1); // Add 1 coin per pickup; customize if needed
            Destroy(collision.gameObject);
        }
    }

    void RestoreAmmo()
    {
        _weapon_Switching.weaponInfos[_weapon_Switching._currentGunIndex]._ammoLeft = _weapon_Switching.weaponInfos[_weapon_Switching._currentGunIndex]._totalAmmo;
        _weapon_Switching.weaponInfos[_weapon_Switching._currentGunIndex]._ammoLeftText.text = _weapon_Switching.weaponInfos[_weapon_Switching._currentGunIndex]._ammoLeft.ToString();
        print("Restored Ammos");
    }
}
