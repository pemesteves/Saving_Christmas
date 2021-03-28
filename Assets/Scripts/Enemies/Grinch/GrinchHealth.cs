using UnityEngine;

public class GrinchHealth : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 100;
    private float currentHealth;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    private GrinchMovement grinchMovement = null;

    [SerializeField]
    private GameObject winningScreen = null;

    // Start is called before the first frame update
    void Start()
    {
        grinchMovement = GetComponent<GrinchMovement>();
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

            winningScreen.SetActive(true);
        }
    }

    public void DamageByGift()
    {
        TakeDamage(10);
    }

    public bool IsDead()
    {
        return currentHealth == 0;
    }

    private void SetHealth()
    {
        float alpha = Mathf.Lerp(1, 0, (maxHealth - currentHealth) / maxHealth);
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
        grinchMovement.GetGrinchArm().SetColorAlpha(alpha);
    }
}
