using UnityEngine;

public class Coins_Manager : MonoBehaviour
{
    public int Coins { get; private set; }

    private void Start()
    {
        // Load saved coins at the start
        Coins = PlayerPrefs.GetInt("Coins", 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coins"))
        {
            AddCoins(1); // Add 1 coin per pickup; customize if needed
            Destroy(collision.gameObject);
        }
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
}
