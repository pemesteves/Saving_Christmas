using System.Collections;
using UnityEngine;

public class Vulture : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;

    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;


    [SerializeField]
    private float minDistanceToChasePlayer = 20f, minDistanceToShoot = 5f;
    [SerializeField]
    private float speed = .075f;
    [SerializeField]
    private float flightTime = 3f;

    private bool isDying = false;
    private bool chasingPlayer = false;
    private IEnumerator damagePlayerCoroutine = null;

    private void Start()
    {
        StartCoroutine(Fly());
    }

    // Update is called once per frame
    void Update()
    {
        if (isDying) return;
        
        if(player == null)
        {
            if(chasingPlayer)
            {
                chasingPlayer = false;
                StartCoroutine(Fly());
            }
            return;
        }

        float dist = CalculateDistance();
        if (dist < minDistanceToChasePlayer)
        {
            if(!chasingPlayer)
            {
                StopAllCoroutines();
            }

            chasingPlayer = true;
            if (player.transform.position.x < transform.position.x)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }

            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
        } 
        else if (chasingPlayer)
        {
            chasingPlayer = false;
            StartCoroutine(Fly());
        }

        /*float horDir;
        if (player.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            horDir = -1;
        } else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            horDir = 1;
        }

        if(dist < minDistanceToWalk && dist > minDistanceToShoot)
        {
            transform.Translate(Vector2.right * horDir * speed, Space.World);

            //fire.SetActive(false);
        } else {
            if(dist < minDistanceToShoot)
            {
                //fire.SetActive(true);
            }
        }*/
    }

    private IEnumerator Fly()
    {
        while(true)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            for (float t = 0; t <= flightTime; t += Time.deltaTime)
            {
                transform.Translate(Vector2.right * speed, Space.World);

                yield return null;
            }

            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            for (float t = 0; t <= flightTime; t += Time.deltaTime)
            {
                transform.Translate(Vector2.left * speed, Space.World);

                yield return null;
            }
        }
    }

    private float CalculateDistance()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    public void KillVulture()
    {
        if(damagePlayerCoroutine != null)
        {
            StopCoroutine(damagePlayerCoroutine);
            damagePlayerCoroutine = null;
        }

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        animator.SetTrigger("die");
        //fire.SetActive(false);
        isDying = true;
        StopAllCoroutines();

        StartCoroutine(DestroyVulture());
    }

    private IEnumerator DestroyVulture()
    {
        float destroyingTime = 3f;

        for (float t = 0; t <= destroyingTime; t += Time.deltaTime)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(1, 0, t / destroyingTime));
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDying)
        {
            damagePlayerCoroutine = DamagePlayer(collision.gameObject);
            StartCoroutine(damagePlayerCoroutine);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && damagePlayerCoroutine != null)
        {
            StopCoroutine(damagePlayerCoroutine);
            damagePlayerCoroutine = null;
        }
    }

    private IEnumerator DamagePlayer(GameObject player)
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        while (true)
        {
            playerHealth.DamageByVulture();

            yield return new WaitForSeconds(.5f);
        }
    }
}
