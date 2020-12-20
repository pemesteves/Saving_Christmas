using System.Collections;
using UnityEngine;

public class Gift : MonoBehaviour
{
    private bool collided = false;

    /*[SerializeField]
    private GameObject explosion = null;*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collided) return;

        switch(collision.collider.tag)
        {
            case "Ground":
                collided = true;
                StartCoroutine(DestroyGift());
                break;
            case "Enemy":
                collided = true;
                GameObject enemy = collision.gameObject;
                
                // Check if this is a penguin
                Penguin penguin;
                if ((penguin = enemy.GetComponent<Penguin>()) != null)
                {
                    penguin.KillPenguin();
                }
                Vulture vulture;
                if((vulture = enemy.GetComponent<Vulture>()) != null)
                {
                    vulture.KillVulture();
                }

                StartCoroutine(DestroyGift());
                break;
            default:
                break;
        }
    }

    private IEnumerator DestroyGift()
    {
        // Shake gift until the explosion
        yield return new WaitForSeconds(1); // Explode 1 second after collided with the floor

        // Particle system for explosion
        //GameObject exp = Instantiate(explosion);
        //exp.transform.position = transform.position;

        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        Color c = sp.color;
        for(float t = 0; t <= 1; t += Time.deltaTime)
        {
            sp.color = new Color(c.r, c.g, c.b, Mathf.Lerp(1, 0, t));
            yield return null;
        }

        //Destroy(exp);
        Destroy(gameObject);
    }
}
