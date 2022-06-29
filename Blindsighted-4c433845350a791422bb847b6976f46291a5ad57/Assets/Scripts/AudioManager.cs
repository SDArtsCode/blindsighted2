using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioMixer am;
    [SerializeField] Settings settings;

    [SerializeField] Sound[] sounds;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = false;
            s.source.outputAudioMixerGroup = s.audioMixer;
        }

        am.SetFloat("MasterVolume", Mathf.Log10(settings.masterVolume) * 20);
        am.SetFloat("AmbienceVolume", Mathf.Log10(settings.ambienceVolume) * 20);
        am.SetFloat("SFXVolume", Mathf.Log10(settings.sfxVolume) * 20);
        am.SetFloat("MusicVolume", Mathf.Log10(settings.musicVolume) * 20);
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name +  " not found");
            return;
        }
        s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }
}


