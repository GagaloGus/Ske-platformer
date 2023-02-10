using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void ToggleMusic() { AudioManager.instance.musicSource.mute = !AudioManager.instance.musicSource.mute; }
    public void ToggleSFX() { AudioManager.instance.sfxSource.mute = !AudioManager.instance.sfxSource.mute; }

}
