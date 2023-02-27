using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider musicSlider, sfxSlider;
    public Button musicButton, sfxButton;
    public Sprite[] buttonSprites;

    private void Start()
    {
        ChangeButtonSprite();
        ChangeVolumeValue();
    }
    public void ToggleMusic() { 
        AudioManager.instance.ToggleMusic();
        ChangeButtonSprite();
    }
    public void ToggleSFX() { 
        AudioManager.instance.ToggleSFX();
        ChangeButtonSprite();
    }
    public void VolumeMusic() { AudioManager.instance.VolumeMusic(musicSlider.value); }
    public void VolumeSFX() { AudioManager.instance.VolumeSFX(sfxSlider.value); }

    void ChangeButtonSprite()
    {
        //cambia el sprite del volumen si esta muteado o no
        if (AudioManager.instance.musicSource.mute) { musicButton.image.sprite = buttonSprites[1]; }
        else { musicButton.image.sprite = buttonSprites[0]; }

        //cambia el sprite del efecto de sonido si esta muteado o no
        if (AudioManager.instance.sfxSource.mute) { sfxButton.image.sprite = buttonSprites[3]; }
        else { sfxButton.image.sprite = buttonSprites[2]; }
    }

    void ChangeVolumeValue()
    {
        //el valor de los sliders sea igual al del volumen al iniciar el nivel
        musicSlider.value = AudioManager.instance.musicSource.volume;
        sfxSlider.value = AudioManager.instance.sfxSource.volume;
    }
}
