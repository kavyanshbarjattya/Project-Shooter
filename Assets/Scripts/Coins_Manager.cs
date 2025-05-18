using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins_Manager : MonoBehaviour
{
    public int _coins;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coins"))
        {
            _coins++;
            PlayerPrefs.SetInt("Coins", _coins);
            Destroy(collision.gameObject);
        }
    }
}
