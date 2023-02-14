using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    string levelSelected;
    public void ChangeMenuScene(string name)
    {
        //asigna el personaje a la variable
        GameObject playerMenu = FindObjectOfType<MenuPlayer>().gameObject;
        
        //cambia las variables de su script y animator para que se reproduzca la cinematica
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

    public void PlaySoundEffect(string clip_name)
    {
        AudioManager.instance.PlaySFX(clip_name);
    }
}
