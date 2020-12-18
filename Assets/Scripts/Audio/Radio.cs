﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Radio : MonoBehaviour
{
    private AudioSource audioSource;
    private Image image;

    [SerializeField]
    private List<Broadcaster> broadcasters = new List<Broadcaster>();
    
    private int currentBroadcaster = 0;
    private float radioTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        image = GetComponent<Image>();

        StartBroadcaster();
        StartCoroutine(CountTime());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("NextBroadcaster"))
        {
            currentBroadcaster = (currentBroadcaster + 1) % broadcasters.Count;

            StartBroadcaster();
        } else if(Input.GetButtonDown("PreviousBroadcaster"))
        {
            currentBroadcaster = currentBroadcaster == 0 ? broadcasters.Count - 1 : currentBroadcaster - 1;

            StartBroadcaster();
        }
    }

    private void StartBroadcaster()
    {
        broadcasters[currentBroadcaster].StartBroadcaster(radioTime);
        image.sprite = broadcasters[currentBroadcaster].GetImage();
    }

    public void UpdateMusic(AudioClip clip, float time)
    {
        audioSource.clip = clip;
        audioSource.time = time;
        audioSource.Play();
    }

    private IEnumerator CountTime()
    {
        while(true)
        {
            radioTime += Time.deltaTime;
            yield return null;
        }
    }
}
