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
        slainText.text = "You were slain by " + entityKiller;
        d_timeText.text = "Time spent: " + timeRemaining;
        d_scoreText.text = "Score: " + scoreRemaining;
    }


    public void GoToMenu() { GameManager.instance.ChangeScene("Menu"); }
    public void Retry() 
    { 
        GameManager.instance.ChangeScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        AudioManager.instance.sfxSource.mute = false;
    }
}
