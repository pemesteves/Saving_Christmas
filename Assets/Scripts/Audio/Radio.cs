using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class Radio : MonoBehaviour
{
    private AudioSource audioSource;
    private Image image;

    [SerializeField]
    private GameObject radioCanvas = null;

    [SerializeField]
    private List<Broadcaster> broadcasters = new List<Broadcaster>();
    
    private int currentBroadcaster = 0;
    private float radioTime = 0;

    private static Radio radioInstance = null;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (radioInstance != null)
        {
            Destroy(radioCanvas);
            return;
        }

        radioInstance = this;

        DontDestroyOnLoad(radioCanvas);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefs.GetFloat("SoundVolume", .5f);
        image = GetComponent<Image>();

        StartBroadcaster();
        StartCoroutine(CountTime());
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Menu")
        {
            Destroy(gameObject);
        }
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

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
