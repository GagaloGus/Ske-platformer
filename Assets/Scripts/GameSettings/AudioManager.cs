using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;
    private List<GameObject> activeAudioGameObjects;

    private void Awake()
    {
        if (!instance) //comprueba que instance no tenga informacion
        {
            instance = this;
            activeAudioGameObjects = new List<GameObject>();
            DontDestroyOnLoad(gameObject);
        }
        else //si tiene info, ya existe un GM
        {
            Destroy(gameObject);
        }
    }

    public AudioSource PlayAudio(AudioClip clip, float volume = 1)
    {
        GameObject sourceObj = new GameObject(clip.name);
        activeAudioGameObjects.Add(sourceObj);
        //cambiamos el transform para que todos los audios suenen en el mismo sitio
        sourceObj.transform.SetParent(this.transform);
        //
        AudioSource source = sourceObj.AddComponent<AudioSource>();
        //el clip es el archivo mp3
        source.clip = clip; source.volume = volume;
        source.Play();

        StartCoroutine(PlayAudio(source));
        return source;
    }

    public AudioSource PlayAudioOnLoop(AudioClip clip, float volume = 1)
    {
        GameObject sourceObj = new GameObject(clip.name);
        activeAudioGameObjects.Add(sourceObj);
        //cambiamos el transform para que todos los audios suenen en el mismo sitio
        sourceObj.transform.SetParent(this.transform);
        //
        AudioSource source = sourceObj.AddComponent<AudioSource>();
        //el clip es el archivo mp3
        source.clip = clip; source.volume = volume; source.loop = true;
        source.Play();

        return source;
    }
    public void ClearAudioList()
    {
        foreach (GameObject go in activeAudioGameObjects)
        {
            Destroy(go);
        }
        activeAudioGameObjects.Clear();
    }

    IEnumerator PlayAudio(AudioSource source)
    {
        while(source && source.isPlaying)
        {
            yield return null;
        }
        if (source)
        {
            activeAudioGameObjects.Remove(source.gameObject);
            Destroy(source.gameObject);
        }
    }
}
