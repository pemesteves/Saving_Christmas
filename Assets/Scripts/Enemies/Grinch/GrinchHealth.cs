using UnityEngine;

public class GrinchHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100;
    private float currentHealth;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    [SerializeField]
    private GrinchMovement grinchMovement = null;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    private void TakeDamage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        SetHealth();

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            grinchMovement.SetIsDead();

            //winningScreen.SetActive(true);
            //gameOverScreen.SetActive(true);
        }
    }

    public bool IsDead()
    {
        return currentHealth == 0;
    }

    private void SetHealth()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(1, 0, (maxHealth - currentHealth) / maxHealth));
    }
}
