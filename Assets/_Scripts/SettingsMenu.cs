using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        masterSlider.value = SoundManager.instance.masterVolume;
        musicSlider.value = SoundManager.instance.musicVolume;
        sfxSlider.value = SoundManager.instance.sfxVolume;
    }

    public void SetMaster(float value)
    {
        SoundManager.instance.SetMasterVolume(value);
    }

    public void SetMusic(float value)
    {
        SoundManager.instance.SetMusicVolume(value);
    }

    public void SetSFX(float value)
    {
        SoundManager.instance.SetSFXVolume(value);
    }
}