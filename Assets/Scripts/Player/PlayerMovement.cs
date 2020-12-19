using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private Rigidbody2D rb = null;

    [SerializeField]
    private PlayerArm playerArm = null;

    [SerializeField]
    private float jumpForce = 5f, speed = .2f;

    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            float horDir = Input.GetAxisRaw("Horizontal");

            if (horDir < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            else
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }


            transform.Translate(Vector2.right * horDir * speed, Space.World);

            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }

        if (Input.GetButtonDown("Jump") && !isJumping)
        {       
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            SetJumping(true);
        }
    }

    private void SetJumping(bool jump)
    {
        isJumping = jump;
        animator.SetBool("jumping", jump);
    }

    private void SetArmWalkingParameter()
    {
        playerArm.SetWalking(true);
    }

    private void SetArmIdleParameter()
    {
        playerArm.SetWalking(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            SetJumping(false);
        }

        Debug.Log(collision.gameObject.tag);
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.tag); // Check collisions with particles
    }
}
