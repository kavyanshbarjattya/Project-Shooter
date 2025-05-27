using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    public Image[] _weaponsVisual , _weaponLocked;                      // Show visual indication
    [SerializeField] GameObject[] _weaponsHolder , _buyBtn;       // Hold data & prices
    [SerializeField] SetCoinText _setCoinText;          // UI coin text

    private void Start()
    {
        // Load previously purchased weapons and show visuals
        for (int i = 0; i < _weaponsHolder.Length; i++)
        {
            if (IsWeaponPurchased(i))
            {
                _weaponLocked[i].enabled = false;
                _weaponsVisual[i].enabled = true;
                _buyBtn[i].SetActive(false);
                Game_Manager.Instance.UnlockWeaponFromShop(i); // Tell GameManager
            }
            else
            {
                _weaponLocked[i].enabled = true;
                _buyBtn[i].SetActive(true);
                
            }
        }
    }

    public void Purchased(GameObject g)
    {
        Weapon_Rates weapon = g.GetComponent<Weapon_Rates>();
        int index = System.Array.IndexOf(_weaponsHolder, g);
        int price = weapon.rate;

        if (index < 0 || IsWeaponPurchased(index)) return;

        if (Coins_Manager.instance.GetCoins() >= price)
        {
            // Deduct coins
            Coins_Manager.instance.SpendCoins(price);
            // Save purchase
            SetBool("WeaponPurchased_" + index, true);
            // Update visuals
            _weaponsVisual[index].enabled = true;
            _weaponLocked[index].enabled = false;
            _buyBtn[index].SetActive(false);
            Game_Manager.Instance.UnlockWeaponFromShop(index);
            // Update coin text
            _setCoinText._coinTxt.text = "Coins: " + Coins_Manager.instance.GetCoins();
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }

    private bool IsWeaponPurchased(int index) => GetBool("WeaponPurchased_" + index);

    private void SetInt(string key, int value) => PlayerPrefs.SetInt(key, value);

    private int GetInt(string key, int defVal = 0) => PlayerPrefs.GetInt(key, defVal);

    private void SetBool(string key, bool value) => SetInt(key, value ? 1 : 0);

    private bool GetBool(string key, bool defVal = false) => GetInt(key, defVal ? 1 : 0) == 1;
}
