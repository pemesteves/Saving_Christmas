using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar = null;

    [SerializeField]
    private float maxHealth = 100;
    private float currentHealth;

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
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("EnemyFire"))
        {
            TakeDamage(enemyFireDamage);
        }
    }
}
