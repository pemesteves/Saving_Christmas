using UnityEngine;

public class AudioSourceManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        UpdateAudioSourceVolume();
    }

    public void UpdateAudioSourceVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("SoundVolume", .5f);
    }
}
