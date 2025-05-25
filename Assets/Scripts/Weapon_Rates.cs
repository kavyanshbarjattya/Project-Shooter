using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon_Rates : MonoBehaviour
{
    public int rate;
    /*public bool _canPurchased;*/

    public bool _isUnlocked;

    [SerializeField] TextMeshProUGUI _rateTxt;


    private void OnEnable()
    {
        _rateTxt.text = "Cost: " + rate;
    }
}
