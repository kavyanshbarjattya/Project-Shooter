using TMPro;
using UnityEngine;

public class Coins_Manager : MonoBehaviour
{
    public int Coins { get; private set; }
    public static Coins_Manager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Load saved coins at the start
        Coins = PlayerPrefs.GetInt("Coins", 0);
    }


    public void AddCoins(int amount)
    {
        Coins += amount;
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.Save(); // Optional: ensures it writes immediately

        Debug.Log("Coins Collected: " + Coins);
    }

    public void SpendCoins(int amount)
    {
        Coins -= amount;
        Coins = Mathf.Max(Coins, 0); // Prevent going negative
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.Save();
    }


    public int GetCoins() => Coins;

    private void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", Coins);
        PlayerPrefs.Save();
    }
}
