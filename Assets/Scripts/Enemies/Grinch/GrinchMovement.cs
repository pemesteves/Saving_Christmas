using System.Collections;
using UnityEngine;

public class GrinchMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private Rigidbody2D rb = null;

    [SerializeField]
    private GrinchArm grinchArm = null;

    [SerializeField]
    private GameObject player = null;

    [SerializeField]
    private float jumpForce = 8.5f, speed = .1f;

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            animator.SetBool("walking", false);
            yield return new WaitForSeconds(Random.Range(.5f, 1));

            float max_t = Random.Range(1, 5);

            animator.SetBool("walking", true);

            for (float t = 0; t <= max_t; t += Time.deltaTime)
            {
                if (Vector3.Distance(player.transform.position, transform.position) <= 10f) break;

                float horDir = player.transform.position.x < transform.position.x ? -1 : 1;
                if (Mathf.Abs(player.transform.position.x - transform.position.x) > 1f)
                {
                    if (horDir < 0)
                    {
                        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(Vector3.zero);
                    }
                }

                transform.Translate(Vector2.right * horDir * speed, Space.World);

                yield return null;
            }
        }
    }

    private void SetArmWalkingParameter()
    {
        grinchArm.SetWalking(true);
    }

    private void SetArmIdleParameter()
    {
        grinchArm.SetWalking(false);
    }

    public void SetIsDead()
    {
        Destroy(gameObject);
    }
}
