using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broadcaster : MonoBehaviour
{
    [SerializeField]
    private string pathToMusics = null;
    [SerializeField]
    private Sprite broadcasterImage = null;

    List<AudioClip> musics = new List<AudioClip>();

    private float totalBroadcasterTime = 0f;

    private int currentMusic = 0;

    [SerializeField]
    private Radio radio = null;

    private void Start()
    {
        musics.AddRange(Resources.LoadAll<AudioClip>(pathToMusics));

        totalBroadcasterTime = 0f;
        foreach (AudioClip clip in musics)
        {
            totalBroadcasterTime += clip.length;
        }
    }

    public void StartBroadcaster(float time)
    {
        float currentTime = time % totalBroadcasterTime;

        float tmp = 0;
        for(int i = 0; i < musics.Count; i++)
        {
            if (tmp + musics[i].length > currentTime)
            {
                currentMusic = i;
                radio.UpdateMusic(musics[i], currentTime - tmp);
                StartCoroutine(NextMusic(musics[i].length - currentTime));
                break;
            }
            
            tmp += musics[i].length;
        }
    }

    public void StopBroadcaster()
    {
        StopAllCoroutines();
    }

    private IEnumerator NextMusic(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);

            currentMusic = currentMusic + 1 % musics.Count;
            radio.UpdateMusic(musics[currentMusic], 0);
        }
    }
}
