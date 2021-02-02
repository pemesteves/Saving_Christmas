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
    private float jumpForce = 8.5f, speed = .2f;

    private bool isJumping = false;
    private bool isDead = false;

    private SpriteRenderer spRend;

    private void Start()
    {
        spRend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        
        float horDir = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            SetJumping(true);
        }

        // Jump controlled by the player
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0) 
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }


        if (horDir != 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, horDir < 0 ? 180 : 0, 0));

            transform.Translate(Vector2.right * horDir * speed, Space.World);
        }

        animator.SetBool("walking", horDir != 0);
    }

    private void SetJumping(bool jump)
    {
        isJumping = jump;
        //animator.SetBool("jumping", jump);
    }

    private void SetArmWalkingParameter()
    {
        playerArm.SetWalking(true);
    }

    private void SetArmIdleParameter()
    {
        playerArm.SetWalking(false);
    }

    public void SetIsDead()
    {
        isDead = true;
        
        Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemy"), true);
        StartCoroutine(DestroyPlayer());
    }

    private IEnumerator DestroyPlayer()
    {
        Vector2 rotation = new Vector2(transform.localEulerAngles.x, transform.localEulerAngles.y);
        for (float t = 0; t <= 1f; t += Time.deltaTime)
        {
            transform.rotation = Quaternion.Euler(new Vector3(rotation.x, rotation.y, Mathf.Lerp(0, -90, t)));

            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            SetJumping(false);
        }
    }
}
