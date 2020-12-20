using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct ReindeerValues
{
    public Vector2 reindeerPosition;
    public float ropeLength;
    public int orderInLayer;
}

public class Sled : MonoBehaviour
{
    [SerializeField]
    private GameObject reindeer = null;

    [SerializeField]
    private List<ReindeerValues> reindeerValues = new List<ReindeerValues>();

    [SerializeField]
    private GameObject rope = null;

    [SerializeField]
    private ReindeerNumber reindeerNumber = null;

    [SerializeField]
    private GameObject canvasBalloon = null;
    [SerializeField]
    private GameObject helpText = null, nextLevelText = null;

    [SerializeField]
    private GameController gameController = null;
    [SerializeField]
    private string nextLevel = "Level1";

    private bool collidingWithPlayer = false;

    private int numReindeers = 0;

    private void Start()
    {
        reindeerNumber.SetNumReindeers(reindeerValues.Count);
    }

    private void Update()
    {
        if (!collidingWithPlayer) return;
        if (numReindeers < reindeerValues.Count) return;

        if (Input.GetButtonDown("PickItem"))
        {
            gameController?.LoadScene(nextLevel);
        }
    }

    public void PickedReindeer()
    {
        if (numReindeers >= reindeerValues.Count) return;

        ReindeerValues values = reindeerValues[numReindeers];

        GameObject deer = Instantiate(reindeer, transform);
        deer.transform.localPosition = values.reindeerPosition;
        SpriteRenderer sp = deer.GetComponent<SpriteRenderer>();
        sp.sortingOrder = values.orderInLayer;

        rope.transform.localScale = new Vector3(values.ropeLength, rope.transform.localScale.y, rope.transform.localScale.z);

        numReindeers++;

        if(numReindeers == reindeerValues.Count)
        {
            if (helpText != null)
            {
                helpText.SetActive(false);
                nextLevelText.SetActive(true);
            }
        }

        reindeerNumber.SetNumReindeers(reindeerValues.Count - numReindeers);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(canvasBalloon != null) 
            canvasBalloon.SetActive(true);
        
        CheckCollisionWithPlayer(collision, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (canvasBalloon != null)
            canvasBalloon.SetActive(false);

        CheckCollisionWithPlayer(collision, false);
    }

    private void CheckCollisionWithPlayer(Collider2D collision, bool colliding)
    {
        if (collision.CompareTag("Player")) collidingWithPlayer = colliding;
    }
}
