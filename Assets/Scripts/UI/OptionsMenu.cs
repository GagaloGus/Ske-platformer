using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider, sfxSlider;
    public Button volumeButton, sfxButton;
    public Sprite[] buttonSprites;

    private void Update()
    {
        if (AudioManager.instance.musicSource.mute) { volumeButton.image.sprite = buttonSprites[1]; }
        else { volumeButton.image.sprite = buttonSprites[0]; }

        if (AudioManager.instance.sfxSource.mute) { sfxButton.image.sprite = buttonSprites[3]; }
        else { sfxButton.image.sprite = buttonSprites[2]; }
    }
    public void ToggleMusic() { AudioManager.instance.ToggleMusic(); }
    public void ToggleSFX() { AudioManager.instance.ToggleSFX(); }
    public void VolumeMusic() { AudioManager.instance.VolumeMusic(volumeSlider.value); }
    public void VolumeSFX() { AudioManager.instance.VolumeSFX(sfxSlider.value); }
}
