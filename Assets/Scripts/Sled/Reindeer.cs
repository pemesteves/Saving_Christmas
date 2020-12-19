using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Reindeer : MonoBehaviour
{
    [SerializeField]
    private Sled sled = null;

    private bool collidingWithPlayer = false;
    private bool pickedReindeer = false;

    [SerializeField]
    private GameObject canvasBalloon = null;

    [SerializeField]
    private TMP_Text helpText = null, thankYouText = null;

    private void Update()
    {
        if (!collidingWithPlayer || pickedReindeer) return;

        if(Input.GetButtonDown("PickItem"))
        {
            pickedReindeer = true;
            sled.PickedReindeer();

            helpText?.gameObject.SetActive(false);
            thankYouText?.gameObject.SetActive(true);


            StartCoroutine(FadeReindeer());
        }
    }

    private IEnumerator FadeReindeer()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();

        for(float t = 0; t <= 1f; t += Time.deltaTime)
        {
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, Mathf.Lerp(1, 0, t));

            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvasBalloon?.SetActive(true);
        CheckCollisionWithPlayer(collision, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canvasBalloon?.SetActive(false);
        CheckCollisionWithPlayer(collision, false);
    }

    private void CheckCollisionWithPlayer(Collider2D collision, bool colliding)
    {
        if (collision.CompareTag("Player")) collidingWithPlayer = colliding;
    }
}
