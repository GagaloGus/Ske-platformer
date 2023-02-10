using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    string levelSelected;
    public void ChangeMenuScene(string name)
    {
        GameObject playerMenu = FindObjectOfType<MenuPlayer>().gameObject;
        playerMenu.GetComponent<Animator>().Play("entity leave");
        playerMenu.GetComponent<MenuPlayer>().changeLevelSelected = name;
        playerMenu.GetComponent<MenuPlayer>().gs_enteringLevel = true;
    }

    public void ChangeScene(string name)
    {
        GameManager.instance.ChangeScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlaySoundEffect(string clip)
    {
        AudioManager.instance.PlaySFX(clip);
    }

    
}
