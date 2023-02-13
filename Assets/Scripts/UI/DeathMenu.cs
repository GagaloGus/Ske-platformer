using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    public static string entityKiller;
    public TMPro.TMP_Text slainText;
    // Start is called before the first frame update

    private void Update()
    {
        slainText.text = "You were slain by " + entityKiller;
    }


    public void GoToMenu() { GameManager.instance.ChangeScene("Menu"); }
    public void Retry() 
    { 
        GameManager.instance.ChangeScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        AudioManager.instance.sfxSource.mute = false;
    }
}
