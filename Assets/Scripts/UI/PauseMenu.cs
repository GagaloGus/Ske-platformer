using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    GameObject pauseMenu, optionsMenu, player;
    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        //no esten activos al iniciar el juego
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player");
        pauseMenu = transform.Find("PauseMenu").gameObject;
        optionsMenu = transform.Find("OptionsMenu").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //si le doy al escape y el personaje no esta muerto
        if (Input.GetKeyDown(KeyCode.Escape) && !player.GetComponent<Player_Controller>().has_died)
        {
            if (isPaused) { Resume(); }
            else { Pause(); }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        //para el tiempo del juego
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        //pone el tiempo del juego a 1
        Time.timeScale = 1;
        isPaused = false;
    }

    public void GoToMenu()
    {
        //vuelve al menu
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
