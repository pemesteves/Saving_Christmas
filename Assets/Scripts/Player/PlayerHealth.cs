using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar = null;
    [SerializeField]
    private GameObject gameOverScreen = null;

    [SerializeField]
    private float maxHealth = 100;
    private float currentHealth;

    [SerializeField]
    private PlayerMovement playerMovement = null;

    [SerializeField]
    private float enemyFireDamage = .5f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            playerMovement.SetIsDead();

            gameOverScreen.SetActive(true);
        }
    }

    public bool IsDead()
    {
        return currentHealth == 0;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("EnemyFire"))
        {
            TakeDamage(enemyFireDamage);
        }
    }
}
