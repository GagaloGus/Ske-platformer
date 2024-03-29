using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndConfig : MonoBehaviour
{
    GameObject player, lightEnd;
    // Start is called before the first frame update
    void Start()
    {
        //encuentra al jugador y al totem del final
        player = FindObjectOfType<Player_Controller>().gameObject;
        lightEnd = transform.Find("totem").gameObject;
        lightEnd.GetComponent<Animator>().SetBool("active", false);
    }

    public void skeDance()
    {
        //que el jugador baile
        AudioManager.instance.PlayMusic("Level Completed");
        player.GetComponent<Animator>().SetBool("endDance", true);
    }

    public void lightUp()
    {
        //se ilumine el totem
        AudioManager.instance.PlaySFX("Totem Activated");
        lightEnd.GetComponent<Animator>().SetBool("active", true);
    }
}
