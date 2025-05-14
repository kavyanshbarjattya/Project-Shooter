using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Events")]
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDeath;

    [SerializeField] GameObject _playerBody , _playerUI;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        OnTakeDamage?.Invoke(); // <- Call the OnTakeDamage event

        if (currentHealth <= 0)
        {
            Die();
        }
        print(currentHealth);
    }

    private void Die()
    {
        isDead = true;
        OnDeath?.Invoke(); // <- Call the OnDeath event
        // You can also add more logic here like disabling player control
        _playerUI.SetActive(false);
        _playerBody.SetActive(false);
        Time.timeScale = 0;
        print("Player Died");
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public int GetCurrentHealth() => currentHealth;
}
