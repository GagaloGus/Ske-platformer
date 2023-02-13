using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sounds[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    private void Awake()
    {
        if (!instance) //comprueba que instance no tenga informacion
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else //si tiene info, ya existe un GM
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sounds s = Array.Find(musicSounds, x => x.Name == name);
        if(s == null) { Debug.LogWarning("sound not found"); }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sounds s = Array.Find(sfxSounds, x => x.Name == name);
        if (s == null) { Debug.LogWarning("sound not found"); }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic() { musicSource.mute = !musicSource.mute; }
    public void ToggleSFX() { sfxSource.mute = !sfxSource.mute; }
    public void VolumeMusic(float volume) { musicSource.volume = volume; }
    public void VolumeSFX(float volume) { sfxSource.volume = volume; }
}
