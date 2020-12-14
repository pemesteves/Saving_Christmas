using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        image = GetComponent<Image>();

        broadcasters[0].StartBroadcaster(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMusic(AudioClip clip, float time)
    {
        audioSource.clip = clip;
        audioSource.time = time;
        audioSource.Play();
    }
}
