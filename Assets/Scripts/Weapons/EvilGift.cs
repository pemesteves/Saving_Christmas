using System.Collections;
using UnityEngine;

public class EvilGift : MonoBehaviour
{
    private bool collided = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collided) return;

        switch(collision.collider.tag)
        {
            case "Player":
                collided = true;
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
                playerHealth.DamageByGrinch();
                StartCoroutine(DestroyGift());
                break;
            default:
                break;
        }
    }

    private IEnumerator DestroyGift()
    {
        yield return new WaitForSeconds(1);
        SpriteRenderer sp = GetComponent<SpriteRenderer>();

        Color c = sp.color;
        for(float t = 0; t <= 1; t += Time.deltaTime)
        {
            sp.color = new Color(c.r, c.g, c.b, Mathf.Lerp(1, 0, t));
            yield return null;
        }

        Destroy(gameObject);
    }
}
