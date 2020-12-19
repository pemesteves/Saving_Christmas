using System.Collections;
using UnityEngine;

public class Penguin : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;

    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    [SerializeField]
    private GameObject fire = null;

    [SerializeField]
    private float minDistanceToWalk = 20f, minDistanceToShoot = 5f;
    [SerializeField]
    private float speed = .075f;

    private bool isDying = false;

    private void Start()
    {
        fire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDying) return;

        float horDir;
        if (player.transform.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            horDir = -1;
        } else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            horDir = 1;
        }

        float dist = CalculateDistance();

        if(dist < minDistanceToWalk && dist > minDistanceToShoot)
        {
            transform.Translate(Vector2.right * horDir * speed, Space.World);
            SetWalking(true);

            fire.SetActive(false);
        } else {
            SetWalking(false);
            
            if(dist < minDistanceToShoot)
            {
                fire.SetActive(true);
            }
        }
    }

    private float CalculateDistance()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    private void SetWalking(bool walking)
    {
        animator.SetBool("walking", walking);
    }

    public void KillPenguin()
    {
        animator.SetTrigger("die");
        fire.SetActive(false);
        isDying = true;

        StartCoroutine(DestroyPenguin());
    }

    private IEnumerator DestroyPenguin()
    {
        Vector2 rotation = new Vector2(transform.localEulerAngles.x, transform.localEulerAngles.y);
        for(float t = 0; t <= 1f; t += Time.deltaTime)
        {
            transform.rotation = Quaternion.Euler(new Vector3(rotation.x, rotation.y, Mathf.Lerp(0, 90, t)));

            yield return null;
        }

        yield return new WaitForSeconds(1);

        for (float t = 0; t <= 1f; t += Time.deltaTime)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(1, 0, t));
            yield return null;
        }

        Destroy(gameObject);
    }
}
