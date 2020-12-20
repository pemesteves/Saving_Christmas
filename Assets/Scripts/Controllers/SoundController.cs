using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private Slider slider = null;
    private List<AudioSourceManager> audioSourceManagers = new List<AudioSourceManager>();

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("SoundVolume", .5f);

        GameObject[] objs = GameObject.FindGameObjectsWithTag("AudioSourceManager");
        foreach(GameObject obj in objs)
        {
            audioSourceManagers.Add(obj.GetComponent<AudioSourceManager>());
        }
    }

    public void ChangedValue(float value)
    {
        slider.value = value;
        PlayerPrefs.SetFloat("SoundVolume", value);
        foreach (AudioSourceManager audioSourceManager in audioSourceManagers)
        {
            audioSourceManager.UpdateAudioSourceVolume();
        }
    }
}
