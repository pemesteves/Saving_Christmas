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

    // OnTriggerEnter2D (collision with player)
    // Functions to instantiate reindeers -> track the number of reindeers
}
