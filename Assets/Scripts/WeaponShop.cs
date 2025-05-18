using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] Button[] _weaponsVisual;

    private void Start()
    {
        for (int i = 0; i < _weaponsVisual.Length; i++)
        {
            _weaponsVisual[i].interactable = false;
            _weaponsVisual[0].interactable = true;
        }
    }
}
