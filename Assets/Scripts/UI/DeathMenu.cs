using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public static string entityKiller;
    public TMPro.TMP_Text slainText, d_timeText, d_scoreText;
    public float timeRemaining, scoreRemaining;
    // Start is called before the first frame update

    private void Update()
    {
        //cambia los textos de muerte
        slainText.text = "You were slain by " + entityKiller;
        d_timeText.text = "Time spent: " + timeRemaining;
        d_scoreText.text = "Score: " + scoreRemaining;
    }

    public void PlayDeathMusic()
    {
        //para los sfxs y reproduce el tema de muerte
        AudioManager.instance.sfxSource.mute = true;
        AudioManager.instance.PlayMusic("Death Theme");
    }
    public void GoToMenu() { GameManager.instance.ChangeScene("Menu"); }
    public void Retry() 
    { 
        GameManager.instance.ChangeScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        AudioManager.instance.sfxSource.mute = false;
    }
}
