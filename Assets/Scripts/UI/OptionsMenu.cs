using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider, sfxSlider;

    public void ToggleMusic() { AudioManager.instance.ToggleMusic(); }
    public void ToggleSFX() { AudioManager.instance.ToggleSFX(); }
    public void VolumeMusic() { AudioManager.instance.VolumeMusic(volumeSlider.value); }
    public void VolumeSFX() { AudioManager.instance.VolumeSFX(sfxSlider.value); }
}
