using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuUI : MonoBehaviour
{
    public AudioMixer mixer;

    [Header("Volume Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private bool isInitialized = false;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        float master = PlayerPrefs.GetFloat("MasterVol", 0.75f);
        float music = PlayerPrefs.GetFloat("MusicVol", 0.75f);
        float sfx = PlayerPrefs.GetFloat("SFXVol", 0.75f);

        if (masterSlider != null) masterSlider.value = master;
        if (musicSlider != null) musicSlider.value = music;
        if (sfxSlider != null) sfxSlider.value = sfx;

        SetVolume("MasterVol", master);
        SetVolume("MusicVol", music);
        SetVolume("SFXVol", sfx);

        isInitialized = true;
    }

    public void SaveMaster(float value)
    {
        if (!isInitialized) return;
        SetVolume("MasterVol", value);
        PlayerPrefs.SetFloat("MasterVol", value);
    }

    public void SaveMusic(float value)
    {
        if (!isInitialized) return;
        SetVolume("MusicVol", value);
        PlayerPrefs.SetFloat("MusicVol", value);
    }

    public void SaveSFX(float value)
    {
        if (!isInitialized) return;
        SetVolume("SFXVol", value);
        PlayerPrefs.SetFloat("SFXVol", value);
    }

    void SetVolume(string parameterName, float sliderValue)
    {
        if (mixer == null) return;
        float dB = Mathf.Log10(Mathf.Clamp(sliderValue, 0.0001f, 1f)) * 20;
        mixer.SetFloat(parameterName, dB);
    }
}