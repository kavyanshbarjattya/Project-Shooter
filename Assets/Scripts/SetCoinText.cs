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
        _coinTxt.text = PlayerPrefs.GetInt("Coins").ToString();

    }
    private void Update()
    {
        _coinTxt.text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
