using UnityEngine;
using UnityEngine.Audio;

public class AudioLoader : MonoBehaviour
{
    public AudioMixer mixer;

    void Start()
    {
        ApplyVolume("MasterVol", PlayerPrefs.GetFloat("MasterVol", 0.75f));
        ApplyVolume("MusicVol", PlayerPrefs.GetFloat("MusicVol", 0.75f));
        ApplyVolume("SFXVol", PlayerPrefs.GetFloat("SFXVol", 0.75f));
    }

    void ApplyVolume(string name, float value)
    {
        if (mixer == null) return;
        float dB = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20;
        mixer.SetFloat(name, dB);
    }
}