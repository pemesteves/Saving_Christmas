using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArm : MonoBehaviour
{
    [SerializeField]
    private GameObject gift = null;
    [SerializeField]
    private GameObject hand = null;
    [SerializeField]
    private GameObject body = null;

    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private float force = 1000f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("shooting", true);
        }
    }

    private void ShootGift()
    {
        GameObject giftInstance = Instantiate(gift, hand.transform.position, Quaternion.identity);

        Vector2 vec = Vector2.right;
        if(body.transform.rotation.eulerAngles.y == 180)
        {
            vec = Vector2.left;
        }

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
