using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetCoinText : MonoBehaviour
{
    [HideInInspector] public TextMeshProUGUI _coinTxt;

    private void Awake()
    {
        _coinTxt = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        _coinTxt.text = "Coins: " + PlayerPrefs.GetInt("Coins");

    }
    private void Update()
    {
        _coinTxt.text = "Coins: " + PlayerPrefs.GetInt("Coins");
    }
}
