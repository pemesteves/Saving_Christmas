using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private Slider slider = null;
    [SerializeField]
    private AudioSourceManager audioSourceManager = null;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("SoundVolume", .5f);
    }

    public void ChangedValue(float value)
    {
        slider.value = value;
        PlayerPrefs.SetFloat("SoundVolume", value);
        audioSourceManager.UpdateAudioSourceVolume();
    }
}
