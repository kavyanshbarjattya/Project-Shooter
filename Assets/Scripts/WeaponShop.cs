using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    public Button[] _weaponsVisual;
    [SerializeField] GameObject[] _weaponsHolder;

    private void Start()
    {
        print(PlayerPrefs.GetInt("Coins"));

    }
    private void Update()
    {

        for (int i = 0; i < _weaponsHolder.Length; i++)
        {
            if (PlayerPrefs.GetInt("Coins")>= _weaponsHolder[i].GetComponent<Weapon_Rates>().rate)
            {
                _weaponsVisual[i].interactable = true;
                Game_Manager.Instance.UnlockWeaponFromShop(i);
            }
            else
            {
                _weaponsVisual[i].interactable = false;
            }
        }
    }

}
