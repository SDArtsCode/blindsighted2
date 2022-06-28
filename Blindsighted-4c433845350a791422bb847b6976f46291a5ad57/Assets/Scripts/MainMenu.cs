using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject options;
    [SerializeField] GameObject mainMenu;
    [SerializeField] AudioMixer am;
    [SerializeField] Settings settings;

    private void Start()
    {
        am.SetFloat("MasterVolume", Mathf.Log10(settings.masterVolume) * 20);
        am.SetFloat("AmbienceVolume", Mathf.Log10(settings.ambienceVolume) * 20);
        am.SetFloat("SFXVolume", Mathf.Log10(settings.sfxVolume) * 20);
        am.SetFloat("MusicVolume", Mathf.Log10(settings.musicVolume) * 20);
    }

    public void StartGame()
    {
        LevelLoader.instance.LoadLevel(0);
    }

    public void Menu()
    {
        options.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Options()
    {
        options.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
