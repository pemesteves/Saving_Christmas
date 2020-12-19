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

    private void Start()
    {
        fire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
            
            if(dist < minDistanceToShoot) // Shoot
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
}
