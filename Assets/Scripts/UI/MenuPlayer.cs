using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    string levelSelected;
    bool enteringLevel;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enteringLevel = false;
    }

    private void OnMouseDown()
    {
        //si no estoy entrando al nivel
        if (!enteringLevel)
        {
            animator.SetBool("clicked", true);
            AudioManager.instance.PlaySFX("Honk fnaf");
        }
    }

    private void OnMouseUp() //quito el raton del personaje
    {
        animator.SetBool("clicked", false);
    }

    private void OnMouseExit() //dejo de pulsar al personaje
    {
         animator.SetBool("clicked", false);
    }
    public void PlayerLeaveMenuAnimation()//cambia de escena
    {
        GameManager.instance.ChangeScene(levelSelected);
    }
    public string changeLevelSelected
    {
        get { return levelSelected; }
        set { levelSelected = value; }
    }
    public bool gs_enteringLevel
    {
        get { return enteringLevel; }
        set { enteringLevel = value; }
    }
}
