using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    public void ChangeScene(string name)
    {
        GameManager.instance.ChangeScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        AudioManager.instance.PlayAudio(clip);
    }
}
