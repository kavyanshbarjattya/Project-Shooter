using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] int maxHealth = 100;
    private int currentHealth;

    [Header("Events")]
    public UnityEvent OnTakeDamage;
    public UnityEvent OnDeath;

    [SerializeField] GameObject _playerBody , _playerUI;
    [SerializeField] Slider _healthSlider;
    [SerializeField] GameObject _gameOverCanvas; 

    


    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        _healthSlider.maxValue = currentHealth;
        _healthSlider.value = currentHealth;
        _gameOverCanvas.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        _healthSlider.value = currentHealth;
        OnTakeDamage?.Invoke(); // <- Call the OnTakeDamage event

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        OnDeath?.Invoke(); // <- Call the OnDeath event
        // You can also add more logic here like disabling player control
        _playerUI.SetActive(false);
        _playerBody.SetActive(false);
        Time.timeScale = 0;
        _gameOverCanvas.SetActive(true);
        print("Player Died");
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void GameRetry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetCurrentHealth() => currentHealth;
}
