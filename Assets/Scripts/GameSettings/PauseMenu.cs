using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu, optionsMenu;
    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) { Resume(); }
            else { Pause(); }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void GoToMenu()
    {
        GameManager.instance.ChangeScene("Menu");
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void ToggleMusic() { AudioManager.instance.musicSource.mute = !AudioManager.instance.musicSource.mute; }
    public void ToggleSFX() { AudioManager.instance.sfxSource.mute = !AudioManager.instance.sfxSource.mute; }
    public void VolumeMusic(float volume) { AudioManager.instance.musicSource.volume = volume; }
    public void VolumeSFX(float volume) { AudioManager.instance.sfxSource.volume = volume; }
}
