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

    private int numReindeers = 0;

    // OnTriggerEnter2D (collision with player)

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
    }
}
