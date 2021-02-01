using UnityEngine;
using System.Collections;

public class GrinchArm : MonoBehaviour
{
    [SerializeField]
    private GameObject gift = null;
    [SerializeField]
    private GameObject hand = null;
    [SerializeField]
    private GameObject body = null;

    [SerializeField]
    private GameObject player = null;

    [SerializeField]
    private GrinchHealth grinchHealth = null;

    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private float force = 300f;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(10);

        while (true)
        {
            if (grinchHealth.IsDead())
                break;


            animator.SetBool("shooting", true);

            yield return new WaitForSeconds(Random.Range(2.5f, 10));
        }
    }

    private void ShootGift()
    {
        GameObject giftInstance = Instantiate(gift, hand.transform.position, Quaternion.identity);

        Vector2 vec = player.transform.position - transform.position;

        giftInstance.GetComponent<Rigidbody2D>().AddForce(vec * force);
    }

    private void FinishShooting()
    {
        animator.SetBool("shooting", false);
    }

    public void SetWalking(bool walking)
    {
        animator.SetBool("walking", walking);
    }
}
