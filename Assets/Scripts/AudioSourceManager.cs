using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSourceManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource = null;

    private static AudioSourceManager audioSourceManagerInstance = null;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (audioSourceManagerInstance != null)
        {
            Destroy(gameObject);
            return;
        }

        audioSourceManagerInstance = this;
        DontDestroyOnLoad(gameObject);

        UpdateAudioSourceVolume();
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Menu")
        {
            Destroy(gameObject);
        }
    }

    public void UpdateAudioSourceVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("SoundVolume", .5f);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
