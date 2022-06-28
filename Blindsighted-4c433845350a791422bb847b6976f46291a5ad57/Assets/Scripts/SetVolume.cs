using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Settings settings;
    Slider[] sliders = new Slider[4];

    private void Start()
    {
        for(int i = 0; i < sliders.Length; i++)
        {
            sliders[i] = transform.GetChild(i).GetComponent<Slider>();
        }

        sliders[0].value = settings.masterVolume;
        sliders[1].value = settings.sfxVolume;
        sliders[2].value = settings.musicVolume;
        sliders[3].value = settings.ambienceVolume;
    }

    public void SetMaster(float sliderValue)
    {
        float volume = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("MasterVolume", volume);
        settings.masterVolume = volume;
    }

    public void SetMusic(float sliderValue)
    {
        float volume = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("MusicVolume", volume);
        settings.musicVolume = volume;
    }
    public void SetSFX(float sliderValue)
    {
        float volume = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("SFXVolume", volume);
        settings.sfxVolume = volume;
    }
    public void SetAmbience(float sliderValue)
    {
        float volume = Mathf.Log10(sliderValue) * 20;
        mixer.SetFloat("AmbienceVolume", volume);
        settings.ambienceVolume = volume;
    }
}
